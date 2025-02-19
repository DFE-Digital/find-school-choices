namespace DfE.Common.Presentation.PageTemplates.Application.Services.CreateDataDictionary
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CreateDataDictionaryException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mergeData"></param>
        public CreateDataDictionaryException(string mergeData)
            : base($"Data dictionary could not be initialised from object: {mergeData}."){
        }
    }
}
