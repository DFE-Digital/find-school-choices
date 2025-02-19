using DfE.Common.Presentation.PageTemplates.Application.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DfE.Common.Presentation.PageTemplates.CrossCuttingConcerns.Json.Serialisation.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPageViewComponentConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="serialiser"></param>
        /// <param name="jsonViewsArray"></param>
        /// <param name="pageViewComponents"></param>
        /// <returns></returns>
        List<IPageView> Convert(
            JsonReader reader,
            JsonSerializer serialiser,
            JArray jsonViewsArray,
            List<IPageView> pageViewComponents);
    }
}
