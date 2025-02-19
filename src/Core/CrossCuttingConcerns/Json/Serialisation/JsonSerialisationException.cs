namespace DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Json.Serialisation
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JsonSerialisationException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonSerialisationException()
            : base("Json string could not be serialised from Json string. Ensure string is not null or empty.")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        public JsonSerialisationException(Type objectType)
            : base($"Json string could not be serialised from object of type: {objectType}.")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        public JsonSerialisationException(string jsonString)
            : base($"Object could not be deserialised from Json string: {jsonString}.")
        {
        }
    }
}
