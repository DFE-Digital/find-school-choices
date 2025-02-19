namespace DfE.Common.Presentation.PageTemplates.Application.Services.CreateDataDictionary
{
    /// <summary>
    /// 
    /// </summary>
    public static class CreateDataDictionaryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataDictionary"></param>
        /// <returns></returns>
        public static bool HasData(
            this IDictionary<string, object> dataDictionary) =>
                dataDictionary?.Count > 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeDictionaryTasks"></param>
        /// <returns></returns>
        public static Task<Dictionary<string, object>> MergeDictionaries(
            this IEnumerable<Task<Dictionary<string, object>>> mergeDictionaryTasks) =>
                Task.WhenAll(mergeDictionaryTasks)
                    .ContinueWith(_ =>
                        mergeDictionaryTasks.SelectMany(
                            dictionaryTask => dictionaryTask.Result)
                                .GroupBy(kvp => kvp.Key)
                                    .ToDictionary(
                                        grouping => grouping.Key,
                                        grouping => grouping.First().Value));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionaries"></param>
        /// <returns></returns>
        public static Dictionary<string, object> MergeDictionaries(
            this IEnumerable<Dictionary<string, object>> dictionaries) =>
                dictionaries.SelectMany(d => d)
                    .GroupBy(kvp => kvp.Key)
                        .ToDictionary(
                            grouping => grouping.Key,
                            grouping => grouping.First().Value);
    }
}
