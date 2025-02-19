using Microsoft.AspNetCore.Mvc.Razor;

namespace Dfe.Presentation.PageTemplates.Web.Prototype.Views
{
    public class ViewComponentLocator : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            List<string> customLocations =
                [
                    "/Views/Shared/GDS_Table/{0}.cshtml"
                ];

            return customLocations.Concat(viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context){
        }
    }
}
