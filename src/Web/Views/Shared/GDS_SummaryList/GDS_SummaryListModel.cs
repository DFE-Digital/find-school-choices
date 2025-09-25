using DfE.Common.Presentation.PageTemplates.Application.Models;

namespace DfE.FindSchoolChoices.Web.Views.Shared.GDS_SummaryList;

public sealed class GDS_SummaryListModel : IPageViewModel
{
    public string Type { get; set; }
    public string Heading { get; set; } // Heading component which would infer its level e.g h1->h6
    public List<SummaryListEntry> Data { get; set; }
}

public sealed class SummaryListEntry
{
    public string Key { get; set; }
    public string Value { get; set; }
}