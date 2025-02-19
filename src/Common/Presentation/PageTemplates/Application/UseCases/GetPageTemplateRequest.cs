using DfE.FindSchoolChoices.Core.Application.UseCase;

namespace DfE.Common.Presentation.PageTemplates.Application.UseCases
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GetPageTemplateRequest : PageTemplateRequest, IUseCaseRequest<PageTemplateResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, object> MergeData { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTemplateName"></param>
        /// <param name="mergeData"></param>
        public GetPageTemplateRequest(
            string pageTemplateName, IDictionary<string, object> mergeData) : base(pageTemplateName)
        {
            MergeData = mergeData;
        }
    }
}
