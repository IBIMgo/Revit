using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NuGet.Revit.Ribbon.Attribute;
using Autodesk.Revit.UI.Selection;
using System;

namespace BimGo.Revit.Ribbon.Parameters
{
    [Display("Wall Copier", 20, Constants.PanelName.Copier)]        //expected a new category to appear named "copier" but the new button is still in the "parameters" category
    [Transaction(TransactionMode.Manual)]
    public class WallCopier : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;



            Selection sel = app.ActiveUIDocument.Selection;
            Reference pickedref = sel.PickObject(ObjectType.Element, "pick a wall");
            Element elem = doc.GetElement(pickedref);
            Parameter h = elem.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);
            double hAsDouble = h.AsDouble();
            ElementId elemid = elem.GetTypeId();

            Form1 form1 = new Form1(commandData);
            form1.ShowDialog();
            double wallDisplacement = form1.wallDisplacement;


            double revitUnitConstant = 30.480;       //to convert from imperial to metric
            Wall wall = elem as Wall;
            LocationCurve cv = elem.Location as LocationCurve;
            XYZ vector = wall.Orientation.Divide(revitUnitConstant).Multiply(wallDisplacement);
            
            //double dl = vector.GetLength();

            Transaction trans = new Transaction(doc, "wallcopier");
            trans.Start();

            Curve newlocation = cv.Curve.CreateTransformed(Transform.CreateTranslation(vector));
            Wall.Create(doc, newlocation, elemid, wall.LevelId, hAsDouble, 0, false, false);

            trans.Commit();

            //TaskDialog.Show("test", hAsString);

            return Result.Succeeded;
        }
    }
}