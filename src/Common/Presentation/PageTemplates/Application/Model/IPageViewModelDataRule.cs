namespace DfE.Common.Presentation.PageTemplates.Application.Model
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPageViewModelDataRule : IPageViewType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        IPageViewModel TransformDataModel(IPageView dataModel);
    }
}
