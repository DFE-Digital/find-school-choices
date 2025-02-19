using DfE.Common.Presentation.PageTemplates.Application.Services.CreateDataDictionary;
using DfE.Common.Presentation.PageTemplates.Application.Services.DataTranformationRules;
using DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData;
using DfE.Common.Presentation.PageTemplates.Application.Services.MergePageTemplateData.MergeDataHandlers;
using DfE.Common.Presentation.PageTemplates.Application.UseCases;
using DfE.Common.Presentation.PageTemplates.CrossCuttingConcerns.Json.Serialisation;
using DfE.Common.Presentation.PageTemplates.CrossCuttingConcerns.Json.Serialisation.Converters;
using DfE.Common.Presentation.PageTemplates.Presentation.Views.Locators;
using DfE.FindSchoolChoices.Core.Application.UseCase;
using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Json.Serialisation;
using DfE.FindSchoolChoices.Core.CrossCuttingConcerns.Patterns.ChainOfResponsibility;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.Common.Presentation.PageTemplates
{
    /// <summary>
    /// 
    /// </summary>
    public static class CompositionRoot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddPageTemplateDependencies(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services),
                    "A service collection is required to configure the dynamic page core dependencies.");
            }

            services.AddScoped<MergePropertyValueHandler>();
            services.AddScoped<MergeEnumerableObjectsHandler>();
            services.AddScoped<MergePageViewHandler>();
            services.AddScoped<MergeViewModelWithMergeMapHandler>();

            services.AddSingleton<IChainEvaluationHandler<MergeDataHandlerRequest, object>>(provider =>
            {
                var scopedEvaluationHandlerProvider = provider.CreateScope();
                return ChainEvaluationHandler<MergeDataHandlerRequest, object>.Create(
                    scopedEvaluationHandlerProvider.ServiceProvider.GetRequiredService<MergePropertyValueHandler>());
            });

            services.AddSingleton<IChainEvaluationHandler<MergeDataHandlerRequest, object>>(provider =>
            {
                var scopedEvaluationHandlerProvider = provider.CreateScope();
                return ChainEvaluationHandler<MergeDataHandlerRequest, object>.Create(
                    scopedEvaluationHandlerProvider.ServiceProvider.GetRequiredService<MergeEnumerableObjectsHandler>());
            });

            services.AddSingleton<IChainEvaluationHandler<MergeDataHandlerRequest, object>>(provider =>
            {
                var scopedEvaluationHandlerProvider = provider.CreateScope();
                return ChainEvaluationHandler<MergeDataHandlerRequest, object>.Create(
                    scopedEvaluationHandlerProvider.ServiceProvider.GetRequiredService<MergePageViewHandler>());
            });

            services.AddSingleton<IChainEvaluationHandler<MergeDataHandlerRequest, object>>(provider =>
            {
                var scopedEvaluationHandlerProvider = provider.CreateScope();
                return ChainEvaluationHandler<MergeDataHandlerRequest, object>.Create(
                    scopedEvaluationHandlerProvider.ServiceProvider.GetRequiredService<MergeViewModelWithMergeMapHandler>());
            });

            services.Configure<RazorViewEngineOptions>(options =>
                options.ViewLocationExpanders.Add(new DynamicPageViewLocator()));

            services.AddScoped<JsonObjectToDictionaryReducer>();
            services.AddScoped<IPageViewComponentConverter, PageViewComponentConverter>();
            services.AddScoped<IJsonObjectSerialiser, PageViewComponentConverterSerialiser>();
            services.AddScoped<IDataTransformationRulesHandler, DataTransformationRulesHandler>();

            services.AddScoped<
                ICreateDataDictionary, CreateDataDictionaryService>();
            services.AddScoped<
                IMergePageTemplateData, MergePageTemplateDataService>();
            services.AddScoped<
                IUseCase<GetPageTemplateRequest, PageTemplateResponse>,
                GetPageTemplateUseCase>();
        }
    }
}