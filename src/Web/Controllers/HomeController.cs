using Dfe.Core.CrossCuttingConcerns.Json.Serialisation;
using DfE.Common.Presentation.PageTemplates.Application.UseCases.Request;
using DfE.Common.Presentation.PageTemplates.Application.UseCases.Response;
using DfE.Common.Presentation.PageTemplates.View.Presentation;
using DfE.Core.CleanArchitecture.Application.UseCase;
using DfE.FindSchoolChoices.Web.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;

namespace DfE.FindSchoolChoices.Web.Controllers;

public class HomeController : DynamicPageController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUseCase<GetPageTemplateRequest, PageTemplateResponse> _getPageTemplateWithDataUseCase;
    private readonly IDataAggregator _dataAggregator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="jsonObjectSerialiser"></param>
    /// <param name="getPageTemplateWithDataUseCase"></param>
    /// <param name="dataAggregator"></param>
    public HomeController(
        ILogger<HomeController> logger,
        IJsonObjectSerialiser jsonObjectSerialiser,
        IUseCase<GetPageTemplateRequest, PageTemplateResponse> getPageTemplateWithDataUseCase,
        IDataAggregator dataAggregator)
    {
        _logger = logger;
        _getPageTemplateWithDataUseCase = getPageTemplateWithDataUseCase;
        _dataAggregator = dataAggregator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        const string urn = "100000";
        const string pagename = "home";

        IDictionary<string, object> dataDictionary =
            await _dataAggregator
                .GetAllDataPointsInParallel(
                    new List<KeyValuePair<string, Task<object>>>()
                    {
                            _dataAggregator.GetDataRequestByIdTask(urn, "establishments"),
                            _dataAggregator.GetDataRequestByIdTask(urn, "governance")
                    }
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value));

        PageTemplateResponse pageTemplateResponse =
            await _getPageTemplateWithDataUseCase
                .HandleRequest(new GetPageTemplateRequest(pagename, dataDictionary));

        return DynamicPageView(pageTemplateResponse.PageTemplate);
    }
}
