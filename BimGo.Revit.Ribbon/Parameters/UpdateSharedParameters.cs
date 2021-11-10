using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NuGet.Revit.Ribbon.Attribute;

namespace BimGo.Revit.Ribbon.Parameters
{
    [Display("Update", 10, Constants.PanelName.Parameters)]
    [Transaction(TransactionMode.Manual)]
    public class UpdateSharedParameters : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            TaskDialog.Show("Update", "Update Shared Parameters");
            return Result.Succeeded;
        }
    }
}