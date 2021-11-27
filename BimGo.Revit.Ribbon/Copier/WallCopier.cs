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
            Wall offsetWall = Wall.Create(doc, newlocation, wall.LevelId, false);                   //idk why the wall it creates is exactly two times smaller in height
            Parameter h = offsetWall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);        //compared to the original one
            string hAsString = h.AsValueString();                                                   //EDIT: from what i've tested not every wall is copied the same and some
            double hAsDouble = Convert.ToDouble(hAsString);                                         //are getting copied with the right height
            hAsDouble = hAsDouble * 2 / 304.8;                                                      //so the multipication/division at the left is unnecessary some times, weird
            h.Set(hAsDouble);

            trans.Commit();

            //TaskDialog.Show("test", hAsString);

            return Result.Succeeded;
        }
    }
}