using DfE.Common.Presentation.PageTemplates.Application.Model;
using DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData.MergeDataHandlers;
using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Patterns.ChainOfResponsibility;
using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Patterns.ChainOfResponsibility.Extensions;
using System.Reflection;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MergePageTemplateDataService : IMergePageTemplateData
    {
        private readonly List<IChainEvaluationHandler<MergeDataHandlerRequest, object>> _mergeDataHandlers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeDataHandlers"></param>
        /// <exception cref="ArgumentException"></exception>
        public MergePageTemplateDataService(IEnumerable<IChainEvaluationHandler<MergeDataHandlerRequest, object>> mergeDataHandlers)
        {
            ArgumentNullException.ThrowIfNull(nameof(mergeDataHandlers));

            if (!mergeDataHandlers.Any())
            {
                throw new ArgumentException(
                    "A configured list of data merge handlers must be provisioned.");
            }

            _mergeDataHandlers = mergeDataHandlers.ToList();
            _mergeDataHandlers.ChainEvaluationHandlers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplateName"></param>
        /// <param name="mergeDataDictionary"></param>
        /// <returns></returns>
        /// <exception cref="PageTemplateNotFoundException"></exception>
        public async Task<PageTemplate> Merge(
            PageTemplate pageTemplate,
            IDictionary<string, object> mergeDataDictionary) =>
                Merge<PageTemplate>(pageTemplate, mergeDataDictionary.ToDictionary());

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="sourceObject"></param>
        /// <param name="mergeData"></param>
        /// <returns></returns>
        private TObject Merge<TObject>(TObject sourceObject, Dictionary<string, object> mergeData) where TObject : class
        {
            Type sourceObjectType = sourceObject.GetType();

            foreach (PropertyInfo sourceObjectMergeProperty in sourceObjectType.GetProperties())
            {
                var sourceObjectMergePropertyValue =
                    sourceObjectMergeProperty.GetValue(sourceObject);

                if (sourceObjectMergePropertyValue != null && sourceObjectMergeProperty.CanWrite)
                {
                    _mergeDataHandlers[0].Evaluate(
                        new MergeDataHandlerRequest(
                            sourceObject, mergeData, sourceObjectMergeProperty, sourceObjectMergePropertyValue,
                            (sourceObject, mergeData) =>
                                Merge<object>(sourceObject, mergeData)));
                }
            }

            return sourceObject;
        }
    }
}