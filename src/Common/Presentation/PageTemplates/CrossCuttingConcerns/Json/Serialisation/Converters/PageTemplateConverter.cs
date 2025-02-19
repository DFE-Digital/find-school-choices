using DfE.Common.Presentation.PageTemplates.Application.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DfE.Common.Presentation.PageTemplates.CrossCuttingConcerns.Json.Serialisation.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class PageTemplateConverter : JsonConverter
    {
        private readonly IPageViewComponentConverter _converter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageViewComponentConverter"></param>
        public PageTemplateConverter(IPageViewComponentConverter pageViewComponentConverter)
        {
            _converter = pageViewComponentConverter;
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// 
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType) => objectType.Equals(typeof(PageTemplate));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            throw new InvalidOperationException("Use default serialization.");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(
            JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JToken? jsonPageTemplate = JToken.Load(reader);
            List<IPageView> pageViewComponents = [];

            jsonPageTemplate.Children<JProperty>().ToList()
                .ForEach(jsonTemplateProperty =>
                {
                    if (jsonTemplateProperty.Name.Equals(nameof(PageTemplate.Views)))
                    {
                        JArray? jsonViewsArray = (JArray)jsonTemplateProperty.Value;
                        pageViewComponents =
                            _converter.Convert(
                                jsonTemplateProperty.CreateReader(),
                                serializer, jsonViewsArray, pageViewComponents);
                    }
                });

            PageTemplate pageTemplate = new();
            pageTemplate.AddViewComponents(pageViewComponents);

            return pageTemplate;
        }
    }
}
