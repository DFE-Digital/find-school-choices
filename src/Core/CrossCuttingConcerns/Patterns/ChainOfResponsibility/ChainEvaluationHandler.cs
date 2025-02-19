namespace DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Patterns.ChainOfResponsibility
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEvaluationRequest"></typeparam>
    /// <typeparam name="TEvaluationResponse"></typeparam>
    public sealed class ChainEvaluationHandler<TEvaluationRequest, TEvaluationResponse> : IChainEvaluationHandler<TEvaluationRequest, TEvaluationResponse>
    {
        private IChainEvaluationHandler<TEvaluationRequest, TEvaluationResponse> _nextEvaluationHandler;
        private readonly IEvaluator<TEvaluationRequest, TEvaluationResponse> _evaluator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evaluator"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ChainEvaluationHandler(IEvaluator<TEvaluationRequest, TEvaluationResponse> evaluator)
        {
            _evaluator = evaluator;
            _nextEvaluationHandler = RootEvaluationHandler.CreateRoot();    // Set root handler as default.
        }

        public TEvaluationResponse Evaluate(TEvaluationRequest evaluationRequest)
        {
            ArgumentNullException.ThrowIfNull(nameof(evaluationRequest));

            return _evaluator.CanEvaluate(evaluationRequest) ?
                _evaluator.Evaluate(evaluationRequest) :
                _nextEvaluationHandler.Evaluate(evaluationRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nextEvaluator"></param>
        public void ChainNextHandler(IChainEvaluationHandler<TEvaluationRequest, TEvaluationResponse> nextEvaluator) =>
            _nextEvaluationHandler = nextEvaluator;

        /// <summary>
        /// 
        /// </summary>
        internal class RootEvaluationHandler : IChainEvaluationHandler<TEvaluationRequest, TEvaluationResponse>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="evaluationRequest"></param>
            /// <returns></returns>
            public TEvaluationResponse Evaluate(TEvaluationRequest evaluationRequest) => default!;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="nextEvaluator"></param>
            /// <exception cref="InvalidOperationException"></exception>
            public void ChainNextHandler(IChainEvaluationHandler<TEvaluationRequest, TEvaluationResponse> nextEvaluator) =>
                throw new InvalidOperationException("Root evaluation handler must be invoked last.");

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static RootEvaluationHandler CreateRoot() => new();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evaluator"></param>
        /// <returns></returns>
        public static ChainEvaluationHandler<TEvaluationRequest, TEvaluationResponse>
            Create(IEvaluator<TEvaluationRequest, TEvaluationResponse> evaluator) => new(evaluator);
    }
}
