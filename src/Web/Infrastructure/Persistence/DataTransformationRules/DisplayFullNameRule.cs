using DfE.Common.Presentation.PageTemplates.Application.Model;
using DfE.FindSchoolChoices.Web.Views.Shared.GDS_Table;

namespace DfE.FindSchoolChoices.Web.Infrastructure.Persistence.DataTransformationRules
{
    public class DisplayFullNameRule : IPageViewModelDataRule
    {
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public IPageViewModel TransformDataModel(IPageView dataModel)
        {
            const string TitleColumnKey = "Title";
            const string ForenameColumnKey = "Forename1";
            const string SurnameColumnKey = "Surname";

            if (dataModel.ViewModel is GDS_TableModel model && model != null)
            {
                foreach (IRow row in model.Rows)
                {
                    KeyValuePair<string, object> field =
                        new(
                            key: "FullName",
                            value:
                                $"{GetNamedRowValue(row, TitleColumnKey)} " +
                                $"{GetNamedRowValue(row, ForenameColumnKey)} " +
                                $"{GetNamedRowValue(row, SurnameColumnKey)}");

                    RemoveNamedRowValue(row, TitleColumnKey);
                    RemoveNamedRowValue(row, ForenameColumnKey);
                    RemoveNamedRowValue(row, SurnameColumnKey);

                    row.AddField(field);
                }
            }

            return dataModel.ViewModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="nameKey"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private object? GetNamedRowValue(IRow row, string nameKey)
        {
            ArgumentNullException.ThrowIfNull(nameof(row));

            if (string.IsNullOrWhiteSpace(nameKey)){
                throw new ArgumentException(
                    "Unable to derive row value when key provided is null or empty.", nameof(nameKey));
            }

            return row.Fields.Find(row => row.Key == nameKey).Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="nameKey"></param>
        /// <exception cref="ArgumentException"></exception>
        private void RemoveNamedRowValue(IRow row, string nameKey)
        {
            ArgumentNullException.ThrowIfNull(nameof(row));

            if (string.IsNullOrWhiteSpace(nameKey)){
                throw new ArgumentException(
                    "Unable to remove field when key provided is null or empty.", nameof(nameKey));
            }

            row.Fields.RemoveAll(row => row.Key == nameKey);
        }
    }
}
