using DfE.Common.Presentation.PageTemplates.Application.Models;

namespace DfE.FindSchoolChoices.Web.Views.Shared.GDS_Command;

public class GDS_CommandModel : IPageViewModel
{
    /// <summary>
    /// Fully qualified type name for dynamic instantiation
    /// </summary>
    public string Type { get; set; } = typeof(GDS_CommandModel).AssemblyQualifiedName!;

    /// <summary>
    /// Unique identifier for this command instance
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// MVC controller to post to
    /// </summary>
    public string Controller { get; set; } = "Home";

    /// <summary>
    /// MVC action to invoke
    /// </summary>
    public string Action { get; set; } = "TriggerCommand";

    /// <summary>
    /// Parameters to pass to the command handlers
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = [];

    /// <summary>
    /// Ordered list of command handlers to execute
    /// </summary>
    public List<CommandHandler> CommandHandlers { get; set; } = [];
}

public class CommandHandler
{
    /// <summary>
    /// Key used to resolve the handler from the registry
    /// </summary>
    public string HandlerKey { get; set; } = default!;
}