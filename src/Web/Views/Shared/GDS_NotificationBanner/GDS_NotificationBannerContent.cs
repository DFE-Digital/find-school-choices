using DfE.Common.Presentation.PageTemplates.Application.Models;

namespace DfE.FindSchoolChoices.Web.Views.Shared.GDS_NotificationBanner
{
    public sealed class GDS_NotificationBannerContent : IPageViewContent
    {
        public string Type { get; set; }
        public string Heading { get; set; }
    }
}
