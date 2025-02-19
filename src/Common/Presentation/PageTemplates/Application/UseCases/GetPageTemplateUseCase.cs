using DfE.Common.Presentation.PageTemplates.Application.Model;
using DfE.Common.Presentation.PageTemplates.Application.Repositories;
using DfE.Common.Presentation.PageTemplates.Application.Services.CreateDataDictionary;
using DfE.Common.Presentation.PageTemplates.Application.Services.DataTranformationRules;
using DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData;
using DfE.FindSchoolChoices.Core.Application.UseCase;

namespace DfE.Common.Presentation.PageTemplates.Application.UseCases
{
    /// <summary>
    ///
    /// </summary>
    public sealed class GetPageTemplateUseCase :
        IUseCase<GetPageTemplateRequest, PageTemplateResponse>
    {
        private readonly ICreateDataDictionary _createDataDictionaryService;
        private readonly IMergePageTemplateData _mergePageTemplateDataService;
        private readonly IPageTemplateReadOnlyRepository _pageTemplateReadOnlyRepository;
        private readonly IDataTransformationRulesHandler _dataTransformationRulesHandler;

        
        public GetPageTemplateUseCase(
            ICreateDataDictionary createDataDictionaryService,
            IMergePageTemplateData mergePageTemplateDataService,
            IPageTemplateReadOnlyRepository pageTemplateReadOnlyRepository,
            IDataTransformationRulesHandler dataTransformationRulesHandler)
        {
            _createDataDictionaryService = createDataDictionaryService;
            _mergePageTemplateDataService = mergePageTemplateDataService;
            _pageTemplateReadOnlyRepository = pageTemplateReadOnlyRepository;
            _dataTransformationRulesHandler = dataTransformationRulesHandler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="PageTemplateException"></exception>
        public async Task<PageTemplateResponse> HandleRequest(GetPageTemplateRequest request)
        {
            Dictionary<string, object> mergedDataDictionaries =
                _createDataDictionaryService
                    .MergeAll(request.MergeData);

            PageTemplate pageTemplate =
                await _pageTemplateReadOnlyRepository
                    .Get(request.PageTemplateName)
                    .ContinueWith(async pageTemplate =>
                        await _mergePageTemplateDataService.Merge(
                            pageTemplate.Result, mergedDataDictionaries),
                        TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap()
                        .ContinueWith(async pageTemplate =>
                            await _dataTransformationRulesHandler
                                .ApplyDataTransformationRules(pageTemplate.Result),
                            TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap();

            return pageTemplate is null ?
                throw new PageTemplateException(page: request.PageTemplateName) :
                new PageTemplateResponse(pageTemplate);
        }
    }
}