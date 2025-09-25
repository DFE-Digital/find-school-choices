using Dfe.Common.Presentation.PageTemplates.Application.Repositories;
using Dfe.Core.CrossCuttingConcerns.Json.Serialisation;
using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Handlers.Query;
using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Options;
using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Options.Extensions;
using DfE.Common.Presentation.PageTemplates.Application.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace DfE.FindSchoolChoices.Web.Infrastructure.Persistence.PageTemplates
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PageTemplateReadOnlyRepository : IPageTemplateReadOnlyRepository
    {
        private readonly ContainerOptions _containerOptions;
        private readonly ICosmosDbQueryHandler _cosmosDbQueryHandler; // Handles Cosmos DB queries
        private readonly IJsonObjectSerialiser _jsonObjectSerialiser;

        /// <summary>
        /// 
        /// </summary>
        private const string Container = "page-templates";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryOptions"></param>
        /// <param name="readOnlyRepository"></param>
        public PageTemplateReadOnlyRepository(
            IOptions<RepositoryOptions> repositoryOptions,
            ICosmosDbQueryHandler cosmosDbQueryHandler,
            IJsonObjectSerialiser jsonObjectSerialser)
        {
            ArgumentNullException.ThrowIfNull(nameof(repositoryOptions.Value));
            _containerOptions = repositoryOptions.Value.GetContainerOptions(Container);
            _cosmosDbQueryHandler = cosmosDbQueryHandler;
            _jsonObjectSerialiser = jsonObjectSerialser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplateName"></param>
        /// <returns></returns>
        public async Task<PageTemplate> Get(string pageTemplateName)
        {
            ArgumentException.ThrowIfNullOrEmpty(_containerOptions.ContainerName);

            JToken? pageTemplateStructure =
                await _cosmosDbQueryHandler.ReadItemByIdAsync<JToken>(
                    pageTemplateName, _containerOptions.ContainerName, pageTemplateName)
                            .ConfigureAwait(false) ??
                                throw new InvalidOperationException($"Page template '{pageTemplateName}' not found.");

            PageTemplate result =
                _jsonObjectSerialiser.DeserializeString<PageTemplate>(pageTemplateStructure.ToString()) ??
                throw new InvalidOperationException($"Failed to deserialize page template '{pageTemplateName}'.");

            return result;
        }
    }
}
