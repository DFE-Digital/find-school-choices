using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Options;
using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Options.Extensions;
using Dfe.Data.Common.Infrastructure.Persistence.CosmosDb.Repositories;
using DfE.Common.Presentation.PageTemplates.Application.Model;
using DfE.Common.Presentation.PageTemplates.Application.Repositories;
using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Json.Serialisation;
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
        private readonly IReadOnlyRepository _readOnlyRepository;
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
            IReadOnlyRepository readOnlyRepository,
            IJsonObjectSerialiser jsonObjectSerialser)
        {
            ArgumentNullException.ThrowIfNull(nameof(repositoryOptions.Value));
            _containerOptions = repositoryOptions.Value.GetContainerOptions(Container);
            _readOnlyRepository = readOnlyRepository;
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
                await _readOnlyRepository.GetItemByIdAsync<JToken>(
                    pageTemplateName, _containerOptions.ContainerName)
                            .ConfigureAwait(false);

            return _jsonObjectSerialiser.DeserializeString<PageTemplate>(pageTemplateStructure.ToString());
        }
    }
}
