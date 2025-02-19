using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Patterns.ChainOfResponsibility;
using System.Collections;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData.MergeDataHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MergeEnumerableObjectsHandler : IEvaluator<MergeDataHandlerRequest, object?>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evaluationRequest"></param>
        /// <returns></returns>
        public bool CanEvaluate(MergeDataHandlerRequest evaluationRequest) => evaluationRequest.SourceObjectMergePropertyValue is IEnumerable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evaluationRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="JsonSerialisationException"></exception>
        public object? Evaluate(MergeDataHandlerRequest mergeDataRequest)
        {
            if (mergeDataRequest == null || mergeDataRequest.SourceObjectMergePropertyValue is not IList)
            {
                throw new ArgumentException("Unable to parse list of objects when passed null or invalid request.");
            }

            object obj = null!;
            IEnumerable? replacementDataValues = mergeDataRequest.SourceObjectMergePropertyValue as IEnumerable;

            ArgumentNullException.ThrowIfNull(replacementDataValues);

            foreach (var item in replacementDataValues)
            {
                obj = mergeDataRequest.RecurseObjectGraph(item, mergeDataRequest.MergeData);
            }

            return obj;
        }
    }
}
