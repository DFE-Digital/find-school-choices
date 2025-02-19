using DfE.Common.Presentation.PageTemplates.Application.Model;
using DfE.FindSchoolChoices.Core.DependencyInjection;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.DataTranformationRules
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DataTransformationRulesHandler : IDataTransformationRulesHandler
    {
        private readonly IDependencyTypeResolver<IPageViewModelDataRule> _dataTransformationRulesResolver;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTransformationRulesResolver"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DataTransformationRulesHandler(
            IDependencyTypeResolver<IPageViewModelDataRule> dataTransformationRulesResolver)
        {
            _dataTransformationRulesResolver = dataTransformationRulesResolver;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplate"></param>
        /// <returns></returns>
        public async Task<PageTemplate> ApplyDataTransformationRules(PageTemplate pageTemplate)
        {
            AggregateDataTransformationRules(pageTemplate.Views);
            return await Task.FromResult(pageTemplate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplateViews"></param>
        private IEnumerable<IPageView> AggregateDataTransformationRules(IEnumerable<IPageView> pageViews)
        {
            pageViews.ToList().ForEach(pageView =>
            {
                if (ViewHasDataRulesApplied(pageView)){
                    pageView?.ViewModelDataRules.ToList()
                        .ForEach(dataTransformationRule =>
                            ApplyDataTransformationRule(pageView, dataTransformationRule));
                }

                // recursive call to pass through to the next child view.
                if (ViewHasChildViews(pageView)){
                    AggregateDataTransformationRules(pageView.ChildViews);
                }
            });

            return pageViews;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplateView"></param>
        /// <param name="dataTransformationRule"></param>
        private void ApplyDataTransformationRule(
            IPageView pageTemplateView, IPageViewModelDataRule dataTransformationRule) =>
                pageTemplateView.ViewModel =
                    _dataTransformationRulesResolver
                        .ResolveDependency(dataTransformationRule.Type)
                            .TransformDataModel(pageTemplateView);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplateView"></param>
        /// <returns></returns>
        private static bool ViewHasDataRulesApplied(
            IPageView view) => view.ViewModelDataRules.Any();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplateView"></param>
        /// <returns></returns>
        private static bool ViewHasChildViews(
            dynamic pageTemplateView) => pageTemplateView.ChildViews != null;
    }
}
