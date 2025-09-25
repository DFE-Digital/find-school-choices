using DfE.Common.Presentation.PageTemplates.Application.Models;

namespace DfE.FindSchoolChoices.Web.Views.Shared.GDS_Details;

public sealed class GDS_DetailsContent : IPageViewContent
{
    public string Type { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
}