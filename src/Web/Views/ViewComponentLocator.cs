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
                    "/Views/Shared/GDS_Table/{0}.cshtml",
                    "/Views/Shared/GDS_Button/{0}.cshtml",
                    "/Views/Shared/GDS_Command/{0}.cshtml",
                    "/Views/Shared/GDS_InsetText/{0}.cshtml",
                    "/Views/Shared/GDS_SummaryList/{0}.cshtml",
                    "/Views/Shared/GDS_Details/{0}.cshtml",
                    "/Views/Shared/MOJ_SideNavigation/{0}.cshtml",
                    //"/Views/Shared/GDS_Footer/{0}.cshtml",
                    //"/Views/Shared/GDS_Header/{0}.cshtml",
                    "/Views/Shared/GDS_Breadcrumbs/{0}.cshtml"
                ];

            return customLocations.Concat(viewLocations);
        }

        public void PopulateValues(ViewLocationExpanderContext context){
        }
    }
}
