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
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements_)
        {
            var app = commandData.Application;
            var uidoc = app.ActiveUIDocument;
            var doc = uidoc.Document;
            Reference r = commandData.Application.ActiveUIDocument.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "please select wall");
            Element selectedWall = doc.GetElement(r.ElementId);

            if (selectedWall is Wall)
            {
                Wall usingWall = selectedWall as Wall;
                LocationCurve Kurwy = usingWall.Location as LocationCurve;
                using (var form_ = new Okno_Revit.Form1);
                
                using (Transaction t = new Transaction(doc, "Param"))
                {
                    t.Start();
                    try
                    {
                        Curve newlocation = Kurwy.Curve.CreateTransformed(Transform.CreateTranslation(usingWall.Orientation * 500));
                        Wall wall1 = Wall.Create(doc, newlocation, usingWall.LevelId, false);
                        
                    }
                    catch { }
                    t.Commit();

                }
            }
            return Result.Succeeded;
        }
    }
}