namespace DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Json.Serialisation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJsonObjectSerialiser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        TObject? DeserializeString<TObject>(string jsonString);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="targetType"></param>
        /// <param name="objectToSerialise"></param>
        /// <returns></returns>
        TType DeserialiseToType<TType>(Type targetType, object objectToSerialise);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="objectToSerialise"></param>
        /// <returns></returns>
        public string? SerialiseObject<TObject>(TObject objectToSerialise);
    }
}
