using System;
using Autodesk.Revit.UI;

namespace BimGo.Revit.Ribbon
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                Panels.Panels.CreatePanels(application);
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                TaskDialog.Show("BIMgoAddIn", e.Message);
                return Result.Failed;
            }
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}