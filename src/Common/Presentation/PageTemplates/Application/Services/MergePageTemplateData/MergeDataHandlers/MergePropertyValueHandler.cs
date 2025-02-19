using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Patterns.ChainOfResponsibility;
using System.Text.RegularExpressions;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData.MergeDataHandlers
{
    /// <summary>
    /// TODO: rename to merge property value
    /// </summary>
    public sealed class MergePropertyValueHandler : IEvaluator<MergeDataHandlerRequest, object?>
    {
        const string regexPattern = @"@\w*@|@\w*\.\w\S*@";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeDataRequest"></param>
        /// <returns></returns>
        public bool CanEvaluate(MergeDataHandlerRequest mergeDataRequest) =>
            Convert.GetTypeCode(mergeDataRequest.SourceObjectMergePropertyValue) != TypeCode.Object;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeDataRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="JsonSerialisationException"></exception>
        public object? Evaluate(MergeDataHandlerRequest mergeDataRequest)
        {
            if (mergeDataRequest == null){
                throw new ArgumentException("Unable to parse string when passed null or invalid request.");
            }

            object fieldTokenValue =
                GetFieldTokenValue(
                    mergeDataRequest.SourceObjectMergePropertyValue, mergeDataRequest.MergeData);

            mergeDataRequest.SourceObjectMergeProperty
                .SetValue(mergeDataRequest.SourceObject, fieldTokenValue);

            return mergeDataRequest.SourceObject;
        }

        private object GetFieldTokenValue(object fieldContents, Dictionary<string, object> lookupObj)
        {
            var matchedTokens = fieldContents != null ?
                Regex.Matches(fieldContents.ToString(), regexPattern, RegexOptions.IgnoreCase) : null;

            if (matchedTokens != null && matchedTokens.OfType<Match>().Any())
            {
                matchedTokens.OfType<Match>().ToList().ForEach(t =>
                {
                    var fieldPath = t.Value.Replace("@", string.Empty);
                    var content = GetPropertyValue(lookupObj, fieldPath)?.ToString();

                    if (content == t.Value){
                        throw new ArgumentException($"{t.Value} is self referencing.");
                    }

                    fieldContents = content != null ?
                        fieldContents.ToString().Replace(t.Value, content) :
                        fieldContents.ToString().Replace(t.Value, "");
                });
            }

            return fieldContents;
        }

        private object GetPropertyValue(
            Dictionary<string, object> mergeData, string propertyName) =>
                mergeData.ContainsKey(propertyName) ? mergeData[propertyName] : default!;
    }
}
