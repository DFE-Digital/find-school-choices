using Dfe.Common.Presentation.PageTemplates;
using Dfe.Presentation.PageTemplates.Web.Prototype.Views;
using DfE.Common.Presentation.PageTemplates.Application.Model;
using DfE.Common.Presentation.PageTemplates.Application.Repositories;
using DfE.Common.Presentation.PageTemplates.Presentation.Views.Shared.Components;
using DfE.Data.ComponentLibrary.Infrastructure.Persistence.CosmosDb;
using DfE.FindSchoolChoices.Core.DependencyInjection;
using DfE.FindSchoolChoices.Web.Infrastructure.Persistence.Data;
using DfE.FindSchoolChoices.Web.Infrastructure.Persistence.DataTransformationRules;
using DfE.FindSchoolChoices.Web.Infrastructure.Persistence.PageTemplates;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddGovUkFrontend();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
{
    options.FileProviders.Add(new EmbeddedFileProvider(
        typeof(DynamicPage).Assembly));
});

builder.Services.Configure<RazorViewEngineOptions>(options =>
    options.ViewLocationExpanders.Add(new ViewComponentLocator()));

builder.Services.AddScoped(typeof(IDependencyTypeResolver<>), typeof(DependencyTypeResolver<>));
builder.Services.AddScoped<IPageTemplateReadOnlyRepository, PageTemplateReadOnlyRepository>();
builder.Services.AddScoped<IDataAggregator, DataAggregationService>();
builder.Services.AddScoped<IPageViewModelDataRule, DisplayFullNameRule>();
builder.Services.AddPageTemplateDependencies();
builder.Services.AddCosmosDbDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }
