using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NuGet.Revit.Ribbon.Attribute;
using Autodesk.Revit.UI.Selection;
namespace BimGo.Revit.Ribbon.Parameters
{
    [Display("Update", 10, Constants.PanelName.Parameters)]
    [Transaction(TransactionMode.Manual)]
    public class UpdateSharedParameters : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;

            //selecting an element to measure
            Selection sel = app.ActiveUIDocument.Selection;
            Reference pickedref = sel.PickObject(ObjectType.Element, "pick an object");
            Element elem = doc.GetElement(pickedref);

            //reading height
            Parameter h = elem.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
            string hAsString = h.AsValueString();

            //adding a comment equal to the height of the chosen wall
            Parameter comment = elem.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
            Transaction addingComment = new Transaction(doc, "adding a comment");
            addingComment.Start();
            comment.Set(hAsString);
            addingComment.Commit();

            //displaying a window with a text message to check if the correct value was stored (used for debugging purposes)
            TaskDialog.Show("height", $"height of the wall is equal to {hAsString}");
            return Result.Succeeded;
        }
    }
}