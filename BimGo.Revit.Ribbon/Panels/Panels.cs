using System.Linq;
using System.Reflection;
using Autodesk.Revit.UI;
using NuGet.Revit.Ribbon.PanelsHelper;

namespace BimGo.Revit.Ribbon.Panels
{
    public static class Panels
    {
        private const string RibbonTab = "BimGoAddin";
        private static UIControlledApplication _bimGoTab;
        private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        public static void CreatePanels(UIControlledApplication bimGoTab)
        {
            _bimGoTab = bimGoTab;
            _bimGoTab.CreateRibbonTab(RibbonTab);

            var parameters = _bimGoTab.CreateRibbonPanel(RibbonTab, Constants.PanelName.Parameters);
            
            foreach (var instance in Assembly.GetTypesByPanelName(Constants.PanelName.Parameters)) 
            {
                parameters.AddButton(instance);
            }

            // var help = typeof(Help).CreateButtonData($"{Constants.Assets.DefaultFolder}help16.png");
            // var aboutUs = typeof(AboutUs).CreateButtonData($"{Constants.Assets.DefaultFolder}help16.png");
            // var feedback = typeof(Feedback).CreateButtonData($"{Constants.Assets.DefaultFolder}help16.png");
            // helpPanel.AddStackedItems(help, aboutUs, feedback);
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