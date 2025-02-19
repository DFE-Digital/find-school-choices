using Microsoft.AspNetCore.Mvc.Razor;

namespace DfE.Common.Presentation.PageTemplates.Presentation.Views.Locators
{
    public class DynamicPageViewLocator : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            List<string> customLocations =
                [
                    "/Presentation/Views/Shared/{0}.cshtml"
                ];

            return customLocations.Concat(viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context){
        }
    }
}
