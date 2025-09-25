using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Handlers.Query;
using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Options;
using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Options.Extensions;
using Microsoft.Extensions.Options;

namespace DfE.FindSchoolChoices.Web.Infrastructure.Persistence.Data;

/// <summary>
/// Aggregates data from Cosmos DB using configured container mappings and query handlers.
/// </summary>
public class DataAggregationService : IDataAggregator
{
    private readonly RepositoryOptions _repositoryOptions; // Holds container configuration mappings
    private readonly ICosmosDbQueryHandler _cosmosDbQueryHandler; // Handles Cosmos DB queries

    /// <summary>
    /// Initializes a new instance of the <see cref="DataAggregationService"/> class.
    /// Validates and stores injected dependencies.
    /// </summary>
    /// <param name="repositoryOptions">Injected container configuration options.</param>
    /// <param name="cosmosDbQueryHandler">Injected query handler for Cosmos DB operations.</param>
    /// <exception cref="ArgumentNullException">Thrown if dependencies are null.</exception>
    public DataAggregationService(
        IOptions<RepositoryOptions> repositoryOptions,
        ICosmosDbQueryHandler cosmosDbQueryHandler)
    {
        ArgumentNullException.ThrowIfNull(nameof(repositoryOptions));
        ArgumentNullException.ThrowIfNull(nameof(cosmosDbQueryHandler));

        _repositoryOptions = repositoryOptions.Value;
        _cosmosDbQueryHandler = cosmosDbQueryHandler;
    }

    /// <summary>
    /// Awaits a single data retrieval task and returns its resolved key-value pair.
    /// </summary>
    /// <param name="dataRequestTask">A key-value pair containing the data key and its retrieval task.</param>
    /// <returns>Resolved key-value pair with the retrieved object.</returns>
    public async Task<KeyValuePair<string, object>> GetSingleDataPoint(
        KeyValuePair<string, Task<object>> dataRequestTask)
    {
        object value = await Task.Run(() => dataRequestTask.Value)
                                 .ConfigureAwait(continueOnCapturedContext: false);

        return new KeyValuePair<string, object>(dataRequestTask.Key, value);
    }

    /// <summary>
    /// Executes multiple data retrieval tasks in parallel and aggregates the results.
    /// </summary>
    /// <param name="dataRequestTasks">Dictionary of data keys mapped to retrieval tasks.</param>
    /// <returns>Dictionary of resolved data points keyed by their identifiers.</returns>
    /// <exception cref="DataAggregationException">Thrown if any task fails during execution.</exception>
    public async Task<Dictionary<string, object>> GetAllDataPointsInParallel(
        IDictionary<string, Task<object>> dataRequestTasks)
    {
        IDictionary<string, Task<object>> dataTasks = dataRequestTasks;

        TaskCompletionSource<Dictionary<string, object>> taskCompletionResult = new();

        await Task.WhenAll(dataTasks.Values).ContinueWith((task) =>
        {
            if (task.Status == TaskStatus.Faulted)
            {
                throw new DataAggregationException(task.Exception);
            }

            // Aggregate results into a dictionary once all tasks complete
            taskCompletionResult.SetResult(dataTasks.ToDictionary(
                dataRequestResult => dataRequestResult.Key,
                dataRequestResult => dataRequestResult.Value.Result));
        },
        default,
        TaskContinuationOptions.DenyChildAttach |
        TaskContinuationOptions.ExecuteSynchronously,
        TaskScheduler.Default);

        return await taskCompletionResult.Task;
    }

    /// <summary>
    /// Constructs a data retrieval task for a specific item ID within a configured container.
    /// </summary>
    /// <param name="id">The unique identifier of the item to retrieve.</param>
    /// <param name="container">The logical container name used to resolve configuration.</param>
    /// <param name="dataKey">Optional override for the data key used in the result.</param>
    /// <returns>Key-value pair containing the data key and its retrieval task.</returns>
    /// <exception cref="ArgumentException">Thrown if container configuration is missing or invalid.</exception>
    public KeyValuePair<string, Task<object>> GetDataRequestByIdTask(
        string id, string container, string dataKey = "")
    {
        ContainerOptions containerOptions =
            _repositoryOptions.GetContainerOptions(container);

        if (containerOptions == null ||
            string.IsNullOrWhiteSpace(containerOptions.ContainerName))
        {
            throw new ArgumentException(
                $"Container options for '{container}' are not configured or ContainerName is null/empty.",
                nameof(container));
        }

        return new KeyValuePair<string, Task<object>>(
            string.IsNullOrWhiteSpace(dataKey) ? container : dataKey,
            _cosmosDbQueryHandler.ReadItemByIdAsync<object>(
                id, containerOptions.ContainerName, id));
    }

}