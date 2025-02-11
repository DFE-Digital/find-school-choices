using DfE.FindSchoolChoices.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DfE.FindSchoolChoices.Web.Controllers;

[Route("/Error")]
public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Index()
    {
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
