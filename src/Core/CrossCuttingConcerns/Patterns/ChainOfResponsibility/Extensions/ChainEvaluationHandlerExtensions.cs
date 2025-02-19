namespace DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Patterns.ChainOfResponsibility.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ChainEvaluationHandlerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="evaluationHandlers"></param>
        public static void ChainEvaluationHandlers<TRequest, TResponse>(
            this List<IChainEvaluationHandler<TRequest, TResponse>> evaluationHandlers)
        {
            ArgumentNullException.ThrowIfNull(nameof(evaluationHandlers));

            for (int jsonParsingHandlerTally = 0;
                jsonParsingHandlerTally < evaluationHandlers.Count;
                jsonParsingHandlerTally++)
            {
                if (jsonParsingHandlerTally + 1 < evaluationHandlers.Count)
                {
                    evaluationHandlers[jsonParsingHandlerTally]
                        .ChainNextHandler(evaluationHandlers[jsonParsingHandlerTally + 1]);
                }
            }
        }
    }
}
