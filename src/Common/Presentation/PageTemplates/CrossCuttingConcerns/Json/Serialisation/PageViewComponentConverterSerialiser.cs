using DfE.Common.Presentation.PageTemplates.CrossCuttingConcerns.Json.Serialisation.Converters;
using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Json.Serialisation;
using MR.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DfE.Common.Presentation.PageTemplates.CrossCuttingConcerns.Json.Serialisation
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PageViewComponentConverterSerialiser : IJsonObjectSerialiser
    {
        private readonly JsonSerializerSettings _settings;

        public PageViewComponentConverterSerialiser(IPageViewComponentConverter pageViewComponentConverter)
        {
            _settings = new()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                FloatParseHandling = FloatParseHandling.Decimal,
                Converters = [
                    new ExpandoObjectConverter(),
                    new GracefulExpandoObjectConverter(),
                    new PageTemplateConverter(pageViewComponentConverter)
                ]
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        /// <exception cref="JsonSerialisationException"></exception>
        public TObject? DeserializeString<TObject>(string jsonString) =>
            string.IsNullOrWhiteSpace(jsonString) ?
                throw new JsonSerialisationException(jsonString) : DeserialiseJsonStringToObject<TObject>(jsonString);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="targetType"></param>
        /// <param name="objectToSerialise"></param>
        /// <returns></returns>
        /// <exception cref="JsonSerialisationException"></exception>
        public TObject DeserialiseToType<TObject>(Type targetType, object objectToSerialise)
        {
            string? serialisedObject = SerialiseObjectToJsonString(objectToSerialise);

            return string.IsNullOrWhiteSpace(serialisedObject)
                ? throw new JsonSerialisationException(serialisedObject)
                : (TObject)JsonConvert.DeserializeObject(serialisedObject, targetType, _settings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="objectToSerialise"></param>
        /// <returns></returns>
        public string? SerialiseObject<TObject>(TObject objectToSerialise) =>
            SerialiseObjectToJsonString(objectToSerialise);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="objectToSerialise"></param>
        /// <returns></returns>
        /// <exception cref="JsonSerialisationException"></exception>
        private string SerialiseObjectToJsonString<TObject>(TObject objectToSerialise) =>
            objectToSerialise == null ?
                throw new JsonSerialisationException(typeof(TObject)) :
                JsonConvert.SerializeObject(objectToSerialise, _settings);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        /// <exception cref="JsonSerialisationException"></exception>
        private TObject? DeserialiseJsonStringToObject<TObject>(string jsonString) =>
            string.IsNullOrWhiteSpace(jsonString) ?
                throw new JsonSerialisationException(jsonString) :
                JsonConvert.DeserializeObject<TObject>(jsonString, _settings);

    }
}
