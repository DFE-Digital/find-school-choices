using DfE.Common.Presentation.PageTemplates.Application.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DfE.Common.Presentation.PageTemplates.CrossCuttingConcerns.Json.Serialisation.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class PageViewComponentConverter : IPageViewComponentConverter
    {
        private readonly Dictionary<string,
            Func<JObject, JsonSerializer, IPageView, IPageView>> _converterActions;

        /// <summary>
        /// 
        /// </summary>
        public PageViewComponentConverter()
        {
            _converterActions = SetConverterActions();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="serialiser"></param>
        /// <param name="jsonViewsArray"></param>
        /// <param name="pageViewComponents"></param>
        /// <returns></returns>
        public List<IPageView> Convert(
            JsonReader reader,
            JsonSerializer serialiser,
            JArray jsonViewsArray,
            List<IPageView> pageViewComponents)
        {
            jsonViewsArray.ToList()
                .ForEach(jsonView =>
                {
                    IPageView templateViewComponent = new PageView();
                    JObject? jsonViewComponent = jsonView as JObject;
                    ArgumentNullException.ThrowIfNull(jsonViewComponent);
                    templateViewComponent = InvokeConverter(jsonViewComponent, serialiser, templateViewComponent);
                    pageViewComponents.Add(templateViewComponent);
                });

            return pageViewComponents;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="viewContent"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        private TObject? Convert<TObject>(JObject viewContent, JsonSerializer serializer)
        {
            ArgumentNullException.ThrowIfNull(viewContent);
            ArgumentNullException.ThrowIfNull(serializer);
            JProperty? type = viewContent?.Property(nameof(IPageViewType.Type));
            ArgumentNullException.ThrowIfNull(type);
            Type? serialisationType = Type.GetType(type.Value.ToString());
            return (TObject?)serializer.Deserialize(viewContent?.CreateReader()!, serialisationType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonViewComponent"></param>
        /// <param name="serializer"></param>
        /// <param name="pageViewComponent"></param>
        /// <returns></returns>
        private IPageView InvokeConverter(
            JObject jsonViewComponent,
            JsonSerializer serializer,
            IPageView pageViewComponent)
        {
            _converterActions.ToList().ForEach(converterAction =>
            {
                if (jsonViewComponent.ContainsKey(converterAction.Key))
                {
                    converterAction.Value(jsonViewComponent, serializer, pageViewComponent);
                }
            });

            pageViewComponent.ViewId = jsonViewComponent["ViewId"].ToString();
            return pageViewComponent;
        }

        private Dictionary<string,
            Func<JObject, JsonSerializer, IPageView, IPageView>> SetConverterActions() => new()
        {
            { nameof(IPageView.ViewModel),     // convert the necessary JSON into the required view model.
                (json, serializer, viewComponent) => {
                    JObject? viewContent = json[nameof(IPageView.ViewModel)] as JObject;
                    viewComponent.ViewModel = Convert<IPageViewModel>(viewContent!, serializer);
                    return viewComponent;
                }
            },
            { nameof(IPageView.ViewContent),   // convert the necessary JSON into the required view content.
                (json, serializer, viewComponent) => {
                    JObject? viewContent = json[nameof(IPageView.ViewContent)] as JObject;
                    viewComponent.ViewContent = Convert<IPageViewContent>(viewContent!, serializer);
                    return viewComponent;
                }
            },
            { nameof(IPageView.ViewModelDataRules),
                (json, serializer, viewComponent) => {
                    JArray? viewModelDataRules = json[nameof(IPageView.ViewModelDataRules)] as JArray;
                    viewComponent.ViewModelDataRules = ConvertXXX(viewModelDataRules!.CreateReader(), serializer, viewModelDataRules, []);
                    return viewComponent;
                }
            },
            { nameof(IPageView.ChildViews),    // deal with child views by recursing back through the serialisation process. 
                (json, serializer, viewComponent) => {
                    JArray? childViews = json[nameof(IPageView.ChildViews)] as JArray;
                    viewComponent.ChildViews = Convert(childViews!.CreateReader(), serializer, childViews, []);
                    return viewComponent;
                }
            }
        };




        public List<IPageViewModelDataRule> ConvertXXX(
            JsonReader reader,
            JsonSerializer serializer,
            JArray jsonViewsArray,
            List<IPageViewType> pageViewTypes)
        {
            List<IPageViewModelDataRule> result = [];

            jsonViewsArray.ToList()
                .ForEach(jsonView =>
                {
                    IPageViewModelDataRule rule = null;
                    //IPageView templateViewComponent = new PageView();
                    JObject? jsonDataTransformationRule = jsonView as JObject;
                    ArgumentNullException.ThrowIfNull(jsonDataTransformationRule);
                    rule = Convert<IPageViewModelDataRule>(jsonDataTransformationRule, serializer);
                    result.Add(rule);
                });

            return result;
        }


    }
}
