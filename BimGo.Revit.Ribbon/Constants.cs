using System;
using System.Reflection;

namespace BimGo.Revit.Ribbon
{
    public static class Constants
    {
        public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        
        public static class Assets
        {
            public static readonly string DefaultFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Autodesk\Revit\Addins\2022\BimGo.Revit\Resources\";
            public static readonly string DefaultImage = $@"{DefaultFolder}default.png";
        }
        
        public static class PanelName
        {
            public const string Parameters = "Parameters";
        }
    }
}