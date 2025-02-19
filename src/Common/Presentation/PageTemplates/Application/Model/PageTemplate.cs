using DfE.Common.Presentation.PageTemplates.CrossCuttingConcerns.Json.Serialisation.Converters;
using System.Text.Json.Serialization;

namespace DfE.Common.Presentation.PageTemplates.Application.Model
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PageTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
         
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(PageTemplateConverter))]
        public IEnumerable<IPageView> Views { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewComponents"></param>
        public void AddViewComponents(IEnumerable<IPageView> viewComponents)
        {
            ArgumentNullException.ThrowIfNull(viewComponents);
            Views = viewComponents;
        }
    }
}
