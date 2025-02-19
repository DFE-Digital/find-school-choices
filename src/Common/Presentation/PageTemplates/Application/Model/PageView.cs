namespace DfE.Common.Presentation.PageTemplates.Application.Model
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PageView : IPageView
    {
        /// <summary>
        /// 
        /// </summary>
        public string ViewId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IPageViewContent? ViewContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IPageViewModel? ViewModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IPageViewModelDataRule>? ViewModelDataRules { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IPageView> ChildViews { get; set; } = [];
    }
}
