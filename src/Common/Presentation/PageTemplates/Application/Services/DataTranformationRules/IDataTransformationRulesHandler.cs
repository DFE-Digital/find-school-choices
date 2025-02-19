using DfE.Common.Presentation.PageTemplates.Application.Model;

namespace DfE.Common.Presentation.PageTemplates.Application.Services.DataTranformationRules
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataTransformationRulesHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplate"></param>
        /// <returns></returns>
        Task<PageTemplate> ApplyDataTransformationRules(PageTemplate pageTemplate);
    }
}
