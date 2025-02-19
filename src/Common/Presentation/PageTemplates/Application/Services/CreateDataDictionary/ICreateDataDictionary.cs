namespace DfE.Common.Presentation.PageTemplates.Application.Services.CreateDataDictionary
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICreateDataDictionary
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeData"></param>
        /// <param name="dataPrefix"></param>
        /// <returns></returns>
        Dictionary<string, object> Create(object mergeData, string dataPrefix = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeData"></param>
        /// <returns></returns>
        Dictionary<string, object> MergeAll(IDictionary<string, object> mergeData);
    }
}
