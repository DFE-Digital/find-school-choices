namespace DfE.Common.Presentation.PageTemplates.Application.Model
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPageViewModelWithMergeMap : IPageViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public TypePropertyMap? TypePropertyMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        void MapModel(object model);
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class TypePropertyMap
    {
        /// <summary>
        /// 
        /// </summary>
        public string? MergeTypeKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string>? PropertiesToMap { get; set; }
    }
}