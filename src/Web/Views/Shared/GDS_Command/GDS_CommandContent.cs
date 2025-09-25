using DfE.Common.Presentation.PageTemplates.Application.Models;

namespace DfE.FindSchoolChoices.Web.Views.Shared.GDS_Command;

public class GDS_CommandContent : IPageViewContent
{
    public string Type { get; set; }

    public string Heading { get; set; }

    /// <summary>
    /// Label for the submit button
    /// </summary>
    public string ButtonLabel { get; set; } = "Submit";
}
