namespace DfE.Common.Presentation.PageTemplates.Application.UseCases
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PageTemplateException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public PageTemplateException(string page)
            : base($"An error has occurred creating the requested page template for page {page}."){
        }
    }
}
