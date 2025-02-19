using DfE.Common.Presentation.PageTemplates.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace DfE.Common.Presentation.PageTemplates.Presentation.Views.Shared.Components
{
    [ViewComponent(Name = "DynamicPage")]
    public class DynamicPage : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(
            PageTemplate dynamicDisplayModel) =>
                View(await Task.FromResult(dynamicDisplayModel));
    }
}
