using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Options;
using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Options.Extensions;
using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Repositories;
using Microsoft.Extensions.Options;

namespace DfE.FindSchoolChoices.Web.Infrastructure.Persistence.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataAggregationService : IDataAggregator
    {
        private readonly RepositoryOptions _repositoryOptions;
        private readonly IReadOnlyRepository _readOnlyRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryOptions"></param>
        /// <param name="readOnlyRepository"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DataAggregationService(IOptions<RepositoryOptions> repositoryOptions, IReadOnlyRepository readOnlyRepository)
        {
            ArgumentNullException.ThrowIfNull(nameof(repositoryOptions));

            _repositoryOptions = repositoryOptions.Value;
            _readOnlyRepository = readOnlyRepository ?? throw new ArgumentNullException(nameof(readOnlyRepository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRequestTask"></param>
        /// <returns></returns>
        public async Task<KeyValuePair<string, object>> GetSingleDataPoint(KeyValuePair<string, Task<object>> dataRequestTask)
        {
            object value = await Task.Run(() =>
                dataRequestTask.Value).ConfigureAwait(continueOnCapturedContext: false);

            return new KeyValuePair<string, object>(dataRequestTask.Key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRequestTasks"></param>
        /// <returns></returns>
        /// <exception cref="DataAggregationException"></exception>
        public async Task<Dictionary<string, object>> GetAllDataPointsInParallel(IDictionary<string, Task<object>> dataRequestTasks)
        {
            IDictionary<string, Task<object>> dataTasks = dataRequestTasks;

            TaskCompletionSource<Dictionary<string, object>> taskCompletionResult = new();
                await Task.WhenAll(dataTasks.Values).ContinueWith((task) =>
                {
                    if (task.Status == TaskStatus.Faulted){
                        throw new DataAggregationException(task.Exception);
                    }

                    taskCompletionResult.SetResult(dataTasks.ToDictionary(
                        (dataRequestResult) => dataRequestResult.Key,
                        (dataRequestResult) => dataRequestResult.Value.Result));
                },
                default, TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

            return await taskCompletionResult.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="container"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public KeyValuePair<string, Task<object>> GetDataRequestByIdTask(string id, string container, string dataKey = "")
        {
            return new KeyValuePair<string, Task<object>>(string.IsNullOrWhiteSpace(dataKey) ?
                container : dataKey, _readOnlyRepository.GetItemByIdAsync<object>(id, _repositoryOptions.GetContainerOptions(container).ContainerName));
        }
    }

}
