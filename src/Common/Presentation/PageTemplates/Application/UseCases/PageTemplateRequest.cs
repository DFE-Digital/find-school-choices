using DfE.FindSchoolChoices.Core.Application.UseCase;

namespace DfE.Common.Presentation.PageTemplates.Application.UseCases
{
    /// <summary>
    /// 
    /// </summary>
    public class PageTemplateRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string PageTemplateName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplateName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PageTemplateRequest(string pageTemplateName)
        {
            ArgumentNullException.ThrowIfNull(nameof(pageTemplateName));

            PageTemplateName = pageTemplateName;
        }
    }
}
