using DfE.Common.Presentation.PageTemplates.Application.Models;

namespace DfE.FindSchoolChoices.Web.Views.Shared.GDS_Table
{
    public sealed class GDS_TableContent : IPageViewContent
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Heading> Headings { get; set; } = [];

        public class Heading
        {
            public string Header { get; set; }
        }
    }
}