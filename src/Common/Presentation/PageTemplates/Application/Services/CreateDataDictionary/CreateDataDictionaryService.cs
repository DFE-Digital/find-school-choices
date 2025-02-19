using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Json.Serialisation;
using Newtonsoft.Json.Linq;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.CreateDataDictionary
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CreateDataDictionaryService : ICreateDataDictionary
    {
        private readonly IJsonObjectSerialiser _objectToJsonStringSerialiser;
        private readonly JsonObjectToDictionaryReducer _jsonObjectToDictionaryReducer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToJsonStringSerialiser"></param>
        /// <param name="jsonObjectToDictionaryReducer"></param>
        public CreateDataDictionaryService(
            IJsonObjectSerialiser objectToJsonStringSerialiser,
            JsonObjectToDictionaryReducer jsonObjectToDictionaryReducer)
        {
            _objectToJsonStringSerialiser = objectToJsonStringSerialiser;
            _jsonObjectToDictionaryReducer = jsonObjectToDictionaryReducer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeData"></param>
        /// <param name="dataPrefix"></param>
        /// <returns></returns>
        /// <exception cref="JsonSerialisationException"></exception>
        /// <exception cref="CreateDataDictionaryException"></exception>
        public Dictionary<string, object> Create(object mergeData, string dataPrefix = "")
        {
            ArgumentNullException.ThrowIfNull(nameof(mergeData));

            string? jsonMergeData =
                _objectToJsonStringSerialiser.SerialiseObject(mergeData);

            if (string.IsNullOrWhiteSpace(jsonMergeData))
            {
                throw new JsonSerialisationException($"Object to JSON string serialization error for {mergeData}");
            }

            Dictionary<string, object> dataDictionary =
                _jsonObjectToDictionaryReducer.ReduceJsonObject(jsonMergeData, $"{dataPrefix}");

            if (!dataDictionary.HasData())
            {
                throw new CreateDataDictionaryException(jsonMergeData);
            }

            return dataDictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeData"></param>
        /// <returns></returns>
        public Dictionary<string, object> MergeAll(IDictionary<string, object> mergeData) =>
            mergeData.Select(mergeDataRequest =>
            {
                return mergeDataRequest.Value is IEnumerable<JToken> ?
                    Create(mergeDataRequest.Value, mergeDataRequest.Key) : (Dictionary<string, object>)mergeData;
            })
            .MergeDictionaries();
    }
}
