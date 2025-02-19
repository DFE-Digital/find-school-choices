using System.Reflection;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData.MergeDataHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MergeDataHandlerRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public object SourceObject { get; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> MergeData { get; }

        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo SourceObjectMergeProperty { get; }

        /// <summary>
        /// 
        /// </summary>
        public object SourceObjectMergePropertyValue { get; }

        /// <summary>
        /// 
        /// </summary>
        public Func<object, Dictionary<string, object>, object> RecurseObjectGraph { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <param name="mergeData"></param>
        /// <param name="sourceObjectMergeProperty"></param>
        /// <param name="sourceObjectMergePropertyValue"></param>
        /// <param name="recurseObjectGraph"></param>
        public MergeDataHandlerRequest(
            object sourceObject,
            Dictionary<string, object> mergeData,
            PropertyInfo sourceObjectMergeProperty,
            object sourceObjectMergePropertyValue,
            Func<object, Dictionary<string, object>, object> recurseObjectGraph)
        {
            SourceObject = sourceObject;
            MergeData = mergeData;
            SourceObjectMergeProperty = sourceObjectMergeProperty;
            SourceObjectMergePropertyValue = sourceObjectMergePropertyValue;
            RecurseObjectGraph = recurseObjectGraph;
        }
    }
}
