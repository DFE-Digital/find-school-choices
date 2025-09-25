using DfE.Common.Presentation.PageTemplates.Application.Models;
using Newtonsoft.Json.Linq;

namespace DfE.FindSchoolChoices.Web.Views.Shared.GDS_Table
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GDS_TableModel : IPageViewModelWithMergeMap
    {
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TypePropertyMap? TypePropertyMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IPageViewModelDataRule> ViewModelDataRules { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        public List<IRow> Rows { get; set; } = [];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void MapModel(object model)
        {
            ArgumentNullException.ThrowIfNull(model);

            List<IRow> rows = [];

            foreach (Object? rawRow in ((JArray)model).ToList<object>())
            {
                rows.Add(new Row([.. ((JObject)rawRow)], TypePropertyMap.PropertiesToMap));
            }

            Rows = rows;
        }

        /// <summary>
        /// 
        /// </summary>
        internal class Row : IRow
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="fields"></param>
            /// <param name="propertiesToMap"></param>
            public Row(List<KeyValuePair<string, JToken>> fields, IEnumerable<string> propertiesToMap)
            {
                Fields =
                    fields.Where(field =>
                        propertiesToMap.Contains(field.Key))
                            .Select(field => new KeyValuePair<string, object>(field.Key, field.Value)).ToList();
            }

            /// <summary>
            /// 
            /// </summary>
            public List<KeyValuePair<string, object>> Fields { get; }
        }
    }
}

/// <summary>
/// 
/// </summary>
public interface IRow
{
    /// <summary>
    /// 
    /// </summary>
    List<KeyValuePair<string, object>> Fields { get; }
}