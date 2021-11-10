using System;
using System.Reflection;

namespace BimGo.Revit.Ribbon
{
    public static class Constants
    {
        public static Assembly Assembly => Assembly.GetExecutingAssembly();
        
        public static class Assets
        {
            public static readonly string DefaultFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Autodesk\Revit\Addins\2020\BIMgo\Resources";
        }
        
        public static class PanelName
        {
            public const string Parameters = "Parameters";
        }
    }
}