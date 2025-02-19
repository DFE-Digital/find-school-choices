namespace DfE.FindSchoolChoices.Web.Infrastructure.Persistence.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataAggregator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRequestTask"></param>
        /// <returns></returns>
        Task<KeyValuePair<string, object>> GetSingleDataPoint(KeyValuePair<string, Task<object>> dataRequestTask);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRequestTasks"></param>
        /// <returns></returns>
        Task<Dictionary<string, object>> GetAllDataPointsInParallel(IDictionary<string, Task<object>> dataRequestTasks);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="container"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        KeyValuePair<string, Task<object>> GetDataRequestByIdTask(string id, string container, string dataKey = "");
    }
}
