namespace DfE.Common.Presentation.PageTemplates.Application.Model
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPageView
    {
        /// <summary>
        /// 
        /// </summary>
        string ViewId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IPageViewContent? ViewContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IPageViewModel? ViewModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IPageViewModelDataRule>? ViewModelDataRules { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IPageView> ChildViews { get; set; }
    }
}
