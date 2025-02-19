using Newtonsoft.Json.Linq;

namespace DfE.FindSchoolChoices.Web.Infrastructure.Persistence.Extensions.PageTemplates
{
    /// <summary>
    /// 
    /// </summary>
    public static class PageTemplateExtensions
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="pageTemplateStructure"></param>
        /// <returns></returns>
        public static dynamic SanitiseDocumentMetadata(this object pageTemplateStructure)
        {
            var metadataFieldTags =
                new List<string> { "_rid", "_self", "_etag", "_attachments", "_ts" };

            metadataFieldTags
                .ForEach(metadataFieldTag =>
                    ((JObject)pageTemplateStructure).Remove(metadataFieldTag));

            return pageTemplateStructure;
        }
    }
}
