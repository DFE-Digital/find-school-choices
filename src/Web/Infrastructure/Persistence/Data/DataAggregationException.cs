using System.Text;

namespace DfE.FindSchoolChoices.Web.Infrastructure.Persistence.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DataAggregationException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateException"></param>
        public DataAggregationException(AggregateException? aggregateException)
            : base($"An error has occurred with a data request. {AggregatedExceptionMessage(aggregateException)}")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateException"></param>
        /// <returns></returns>
        private static string AggregatedExceptionMessage(AggregateException? aggregateException)
        {
            StringBuilder aggregatedExceptionMessage = new();

            aggregateException?.Flatten().InnerExceptions.ToList()
                .ForEach((ex) =>
                    aggregatedExceptionMessage.AppendLine(ex.ToString())
            );

            return aggregatedExceptionMessage.ToString();
        }
    }
}
