using DfE.Common.Presentation.PageTemplates.Application.Model;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMergePageTemplateData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplate"></param>
        /// <param name="mergeDataDictionary"></param>
        /// <returns></returns>
        Task<PageTemplate> Merge(PageTemplate pageTemplate, IDictionary<string, object> mergeDataDictionary);
    }
}