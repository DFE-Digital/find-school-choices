using DfE.Common.Presentation.PageTemplates.Application.Model;
using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Patterns.ChainOfResponsibility;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData.MergeDataHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MergePageViewHandler : IEvaluator<MergeDataHandlerRequest, object?>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evaluationRequest"></param>
        /// <returns></returns>
        public bool CanEvaluate(MergeDataHandlerRequest evaluationRequest) =>
            evaluationRequest.SourceObjectMergeProperty.PropertyType.IsAssignableTo(typeof(IPageViewType));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeDataRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public object? Evaluate(MergeDataHandlerRequest mergeDataRequest)
        {
            if (mergeDataRequest == null){
                throw new ArgumentException("Unable to recurse object property when passed null or invalid request.");
            }

            return mergeDataRequest
                .RecurseObjectGraph(
                    mergeDataRequest.SourceObjectMergeProperty.GetValue(
                        mergeDataRequest.SourceObject), mergeDataRequest.MergeData);
        }
    }
}
