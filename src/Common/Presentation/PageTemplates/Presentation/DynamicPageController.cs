using DfE.Common.Presentation.PageTemplates.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace DfE.Common.Presentation.PageTemplates.Presentation
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicPageController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        const string DynamicPage = "DynamicPageView";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="DynamicPageControllerException"></exception>
        public IActionResult DynamicPageView(PageTemplate? model)
        {
            if (model == null)
            {
                throw new DynamicPageControllerException();
            }

            return base.View(DynamicPage, model);
        }
    }
}
