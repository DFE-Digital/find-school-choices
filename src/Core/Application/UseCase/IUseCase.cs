﻿namespace DfE.FindSchoolChoices.Core.Application.UseCase
{
    /// <summary>
    /// Implement when handling use-case requests when no request object is required.
    /// </summary>
    /// <typeparam name="TUseCaseResponse">The runtime type definition of the use-case response object.</typeparam>
    public interface IUseCase<TUseCaseResponse>
    {
        /// <summary>
        /// Async method to define how a use-case handles a given request
        /// from an exterior source and the appropriate result returned.
        /// </summary>
        /// <returns>The runtime type definition of the use-case response.</returns>
        Task<TUseCaseResponse> HandleRequest();
    }

    /// <summary>
    /// Implement when handling use-case requests when a request object (with parameters) is required.
    /// </summary>
    /// <typeparam name="TUseCaseRequest">The type of request (input) passed to a given use case.</typeparam>
    public interface IUseCase<in TUseCaseRequest, TUseCaseResponse> where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
    {
        /// <summary>
        /// The invocation of the input-port handle that must be implemented by
        /// a use-case that prescribes to the contract. The handle ensures that
        /// the use-case-request object prescribed at run-time is correctly handled.
        /// </summary>
        /// <param name="request">The type of request passed to a given use case.</param>
        /// <returns>The type of request object to send to the input-port, defined at run-time.</returns>
        Task<TUseCaseResponse> HandleRequest(TUseCaseRequest request);
    }
}
