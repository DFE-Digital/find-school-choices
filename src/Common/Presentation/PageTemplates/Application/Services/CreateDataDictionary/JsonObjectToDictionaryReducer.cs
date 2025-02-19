using Newtonsoft.Json.Linq;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.CreateDataDictionary
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JsonObjectToDictionaryReducer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawJson"></param>
        /// <param name="keyPrefix"></param>
        /// <param name="keyPostfix"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, object> ReduceJsonObject(
            string rawJson, string keyPrefix = "", string keyPostfix = "")
        {
            if (string.IsNullOrWhiteSpace(rawJson))
            {
                throw new ArgumentException(
                    "The raw JSON string cannot not be null or empty.", nameof(rawJson));
            }

            return ReduceJsonObject(JToken.Parse(rawJson), keyPrefix, keyPostfix);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <param name="keyPrefix"></param>
        /// <param name="keyPostfix"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, object> ReduceJsonObject(
            JToken jsonObject, string keyPrefix = "", string keyPostfix = "")
        {
            ArgumentNullException.ThrowIfNull(nameof(jsonObject));

            var container = jsonObject as JContainer ??
                throw new ArgumentException($"{nameof(jsonObject)} must be an object or an array");

            return container.DescendantsAndSelf()
                 .Where(d => d.Type != JTokenType.Property)
                 .Aggregate(
                    new Dictionary<string, object>(),
                    (reducedJsonDict, jsonToken) =>
                    {
                        var dot = jsonToken.Path.Length > 0 ? "." : "";
                        reducedJsonDict.Add(
                            $"{keyPrefix}{dot}{jsonToken.Path}{keyPostfix}", jsonToken);

                        return reducedJsonDict;
                    });
        }
    }
}
