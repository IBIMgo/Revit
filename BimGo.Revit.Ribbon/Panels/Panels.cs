using Autodesk.Revit.UI;
using NuGet.Revit.Ribbon.Attribute;
using NuGet.Revit.Ribbon.PanelsHelper;
using System.Linq;
using System.Reflection;

namespace BimGo.Revit.Ribbon.Panels
{
    public static class Panels
    {
        private const string RibbonTab = "BimGoAddin";
        private static UIControlledApplication _bimGoTab;
        private static readonly Assembly Assembly = Constants.Assembly;
        public static void CreatePanels(UIControlledApplication bimGoTab)
        {
            _bimGoTab = bimGoTab;
            _bimGoTab.CreateRibbonTab(RibbonTab);
            DisplayAttribute.DefaultImagePath = Constants.Assets.DefaultImage;

            var parameters = _bimGoTab.CreateRibbonPanel(RibbonTab, Constants.PanelName.Parameters);
            foreach (var instance in Assembly.GetTypesByPanelName(Constants.PanelName.Parameters))
            {
                parameters.AddButton(instance);
            }
        }

        private static void CreatePanel(string panelName)
        {
            var panel = _bimGoTab.CreateRibbonPanel(RibbonTab, panelName);
            var panelInstances = Assembly.GetTypesByPanelName(panelName);
            var panelButtonData = panelInstances
                .Select(x => x.CreateButtonData())
                .Cast<ButtonData>()
                .ToList();
            panel.AddPushButtonDataToPanel(panelButtonData);
        }
    }

}