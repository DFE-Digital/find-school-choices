using DfE.Common.Presentation.PageTemplates.Application.Model;

namespace DfE.Common.Presentation.PageTemplates.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DynamicPageControllerException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        public DynamicPageControllerException()
            : base($"Unable to create a dynamic page when the {typeof(PageTemplate)} is null.")
        {
        }
    }
}
