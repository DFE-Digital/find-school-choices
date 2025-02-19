using DfE.Common.Presentation.PageTemplates.Application.Model;

namespace DfE.Common.Presentation.PageTemplates.Application.UseCases
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PageTemplateResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public PageTemplate PageTemplate { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplate"></param>
        public PageTemplateResponse(PageTemplate pageTemplate)
        {
            PageTemplate = pageTemplate;
        }
    }
}
