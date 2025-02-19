namespace DfE.FindSchoolChoices.Core.DependencyInjection
{
    /// <summary>
    /// Allows collections of dependencies to be resolved by type, type name,
    /// or generic type argument.
    /// </summary>
    /// <typeparam name="TContract">
    /// Defines the contract (interface) to which the typed dependencies belong.
    /// </typeparam>
    /// <remarks>
    /// This utility class is designed to facilitate the task of resolving contracted
    /// instances assigned to a collection of associated instances (i.e. collection
    /// of instances bound to the same interface), usually registered within a DI container.
    /// A given instance within any given collection can be resolved by concrete type.
    /// </remarks>
    public sealed class DependencyTypeResolver<TContract> :
        IDependencyTypeResolver<TContract> where TContract : class
    {
        private readonly IEnumerable<TContract> _registeredDependencies;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredDependencies"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DependencyTypeResolver(
            IEnumerable<TContract> registeredDependencies)
        {
            _registeredDependencies = registeredDependencies ??
                throw new ArgumentNullException(nameof(registeredDependencies));
        }

        /// <summary>
        /// Resolve the dependency of type <see cref="TImplementation"/>
        /// by generic argument.
        /// </summary>
        public TContract ResolveDependency<TImplementation>()
            where TImplementation : class, TContract => ResolveDependency(typeof(TImplementation));

        /// <summary>
        /// Resolve the dependency of type <see cref="Type"/>
        /// by type argument.
        /// </summary>
        public TContract ResolveDependency(Type type) =>
            type == null ?
            throw new ArgumentNullException(nameof(type)) : ResolveDependency(typeName: type.Name);

        /// <summary>
        /// Resolve the dependency by type name.
        /// by name argument.
        /// </summary>
        public TContract ResolveDependency(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException(nameof(typeName));
            }

            if (IsFullyQualifiedType(typeName))
            {
                Type? type = Type.GetType(typeName);

                if (type != null)
                {
                    typeName = type.Name;
                }
            }

            return _registeredDependencies.First(dependency => dependency.GetType().Name == typeName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool IsFullyQualifiedType(string typeName) => Type.GetType(typeName) != null;
    }
}
