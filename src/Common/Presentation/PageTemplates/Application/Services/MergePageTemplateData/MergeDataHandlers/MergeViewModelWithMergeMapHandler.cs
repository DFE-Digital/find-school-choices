using DfE.Common.Presentation.PageTemplates.Application.Model;
using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Json.Serialisation;
using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Patterns.ChainOfResponsibility;
using System.Text.RegularExpressions;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData.MergeDataHandlers
{
    public sealed class MergeViewModelWithMergeMapHandler : IEvaluator<MergeDataHandlerRequest, object?>
    {
        private readonly IJsonObjectSerialiser _jsonObjectSerialiser;

        public MergeViewModelWithMergeMapHandler(IJsonObjectSerialiser jsonObjectSerialiser)
        {
            _jsonObjectSerialiser = jsonObjectSerialiser;
        }

        const string regexPattern = @"@\w*@|@\w*\.\w\S*@";

        public bool CanEvaluate(MergeDataHandlerRequest evaluationRequest) => evaluationRequest.SourceObject is IPageViewModelWithMergeMap;

        public object? Evaluate(MergeDataHandlerRequest mergeDataRequest)
        {
            if (mergeDataRequest == null){
                throw new ArgumentException("Unable to parse string when passed null or invalid request.");
            }

            TypePropertyMap? fieldTokenValue =
                GetFieldTokenValue(mergeDataRequest.SourceObjectMergePropertyValue, mergeDataRequest.MergeData) as TypePropertyMap;

            ArgumentNullException.ThrowIfNull(fieldTokenValue);

            object? result = _jsonObjectSerialiser.DeserializeString<object>(fieldTokenValue.MergeTypeKey);

            ArgumentNullException.ThrowIfNull(result);

            ((IPageViewModelWithMergeMap)mergeDataRequest.SourceObject).MapModel(result);

            return mergeDataRequest.SourceObject;
        }

        private object GetFieldTokenValue(object fieldContents, Dictionary<string, object> lookupObj)
        {
            TypePropertyMap pageViewModelWithMergeMap = fieldContents as TypePropertyMap;

            var matchedTokens = pageViewModelWithMergeMap != null ?
                Regex.Matches(pageViewModelWithMergeMap.MergeTypeKey, regexPattern, RegexOptions.IgnoreCase) : null;

            if (matchedTokens != null && matchedTokens.OfType<Match>().Any())
            {
                matchedTokens.OfType<Match>().ToList().ForEach(t =>
                {
                    var fieldPath = t.Value.Replace("@", string.Empty);
                    var content = GetPropertyValue(lookupObj, fieldPath)?.ToString();

                    if (content == t.Value)
                    {
                        throw new ArgumentException($"{t.Value} is self referencing.");
                    }

                    pageViewModelWithMergeMap.MergeTypeKey = content != null ?
                        pageViewModelWithMergeMap.MergeTypeKey.Replace(t.Value, content) :
                        pageViewModelWithMergeMap.MergeTypeKey.Replace(t.Value, "");
                });
            }

            return fieldContents;
        }

        private object GetPropertyValue(Dictionary<string, object> mergeData, string propertyName)
        {
            return mergeData.ContainsKey(propertyName) ? mergeData[propertyName] : default!;
        }
    }
}
