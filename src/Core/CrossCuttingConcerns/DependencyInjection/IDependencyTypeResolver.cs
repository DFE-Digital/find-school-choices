namespace DfE.FindSchoolChoices.Core.DependencyInjection
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public interface IDependencyTypeResolver<TContract> where TContract : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        TContract ResolveDependency<TImplementation>()
            where TImplementation : class, TContract;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        TContract ResolveDependency(string typeName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        TContract ResolveDependency(Type type);
    }
}
