using DfE.Common.Presentation.PageTemplates.Application.Models;

namespace DfE.FindSchoolChoices.Web.Views.Shared.MOJ_SideNavigation;

public class MOJ_SideNavigationContent : IPageViewContent
{
    public string Type { get; set; }
    public IEnumerable<AnchorLink> Links { get; set; }
}
