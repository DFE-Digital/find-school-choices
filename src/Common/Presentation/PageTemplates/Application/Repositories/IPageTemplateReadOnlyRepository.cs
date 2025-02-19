using DfE.Common.Presentation.PageTemplates.Application.Model;

namespace DfE.Common.Presentation.PageTemplates.Application.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPageTemplateReadOnlyRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplateName"></param>
        /// <returns></returns>
        Task<PageTemplate> Get(string pageTemplateName);
    }
}
