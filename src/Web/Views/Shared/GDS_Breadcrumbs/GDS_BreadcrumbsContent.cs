using DfE.Common.Presentation.PageTemplates.Application.Models;

namespace DfE.FindSchoolChoices.Web.Views.Shared.GDS_Breadcrumbs;

public sealed class GDS_BreadcrumbsContent : IPageViewContent
{
    public string Type { get; set; }
    public IEnumerable<AnchorLink> Links { get; set; }
}
