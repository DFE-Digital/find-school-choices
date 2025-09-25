using DfE.Common.Presentation.PageTemplates.Application.Models;
using DfE.FindSchoolChoices.Web.Views.Shared.GDS_Table;

namespace DfE.FindSchoolChoices.Web.Infrastructure.Persistence.DataTransformationRules;

/// <summary>
/// Data rule that constructs a FullName field from Title, Forename1, and Surname.
/// </summary>
public class DisplayFullNameRule : IPageViewModelDataRule
{
    /// <summary>
    /// Gets or sets the rule type.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Transforms the data model by combining name fields into a FullName.
    /// </summary>
    /// <param name="dataModel">The input page view model.</param>
    /// <returns>The transformed page view model.</returns>
    public IPageViewModel TransformDataModel(IPageView dataModel)
    {
        const string TitleColumnKey = "Title";
        const string ForenameColumnKey = "Forename1";
        const string SurnameColumnKey = "Surname";

        if (dataModel.ViewModel is GDS_TableModel model)
        {
            foreach (IRow row in model.Rows)
            {
                string fullName =
                    $"{GetNamedRowValue(row, TitleColumnKey)} " +
                    $"{GetNamedRowValue(row, ForenameColumnKey)} " +
                    $"{GetNamedRowValue(row, SurnameColumnKey)}";

                RemoveNamedRowValue(row, TitleColumnKey);
                RemoveNamedRowValue(row, ForenameColumnKey);
                RemoveNamedRowValue(row, SurnameColumnKey);

                row.Fields.Insert(index: 0, new KeyValuePair<string, object>("FullName", fullName.Trim()));
            }
        }

        return dataModel.ViewModel;
    }

    /// <summary>
    /// Retrieves the value of a named field from the row.
    /// </summary>
    /// <param name="row">The row to inspect.</param>
    /// <param name="nameKey">The key of the field.</param>
    /// <returns>The value of the field, or null.</returns>
    /// <exception cref="ArgumentException">Thrown if the key is null or empty.</exception>
    private object? GetNamedRowValue(IRow row, string nameKey)
    {
        ArgumentNullException.ThrowIfNull(row);

        if (string.IsNullOrWhiteSpace(nameKey))
        {
            throw new ArgumentException(
                "Unable to derive row value when key provided is null or empty.", nameof(nameKey));
        }

        return row.Fields.Find(f => f.Key == nameKey).Value;
    }

    /// <summary>
    /// Removes a named field from the row.
    /// </summary>
    /// <param name="row">The row to modify.</param>
    /// <param name="nameKey">The key of the field to remove.</param>
    /// <exception cref="ArgumentException">Thrown if the key is null or empty.</exception>
    private void RemoveNamedRowValue(IRow row, string nameKey)
    {
        ArgumentNullException.ThrowIfNull(row);

        if (string.IsNullOrWhiteSpace(nameKey))
        {
            throw new ArgumentException(
                "Unable to remove field when key provided is null or empty.", nameof(nameKey));
        }

        row.Fields.RemoveAll(f => f.Key == nameKey);
    }
}
