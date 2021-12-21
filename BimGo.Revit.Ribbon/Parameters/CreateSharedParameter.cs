using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;
using System.Text;
using NuGet.Revit.Ribbon.Attribute;

namespace BimGo.Revit.Ribbon.Parameters
{
    [Display("Create", 10, Constants.PanelName.Parameters)]
    [Transaction(TransactionMode.Manual)]
    public class CreateSharedParameters : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Application app = uiapp.Application;

            string path = @"C:\Users\01160780\Desktop\shared-parameters.txt";
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
            app.SharedParametersFilename = path;
            DefinitionFile defFile = app.OpenSharedParameterFile();
            defFile.Groups.Create("testGroup");                         //seems as if it has to check for the existance of a parameter with such name cuz this line crashes revit (but not when defFile is empty)
            DefinitionGroups defGroups = defFile.Groups;
            DefinitionGroup defGroup = defGroups.Create("test parameter");

            ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions("bimgo wall parameter", ParameterType.Text);
            option.UserModifiable = false;
            option.Description = "sample text as an added parameter";
            Definition def = defGroup.Definitions.Create(option);


            CategorySet categories = uiapp.Application.Create.NewCategorySet();
            Category category = Category.GetCategory(uiapp.ActiveUIDocument.Document, BuiltInCategory.OST_Walls);

            categories.Insert(category);


            InstanceBinding instanceBinding = uiapp.Application.Create.NewInstanceBinding(categories);

            BindingMap bindingMap = uiapp.ActiveUIDocument.Document.ParameterBindings;

            Transaction t = new Transaction(doc, "adding a parameter");

            t.Start();
            bindingMap.Insert(def, instanceBinding, BuiltInParameterGroup.PG_TEXT);
            t.Commit();

            return Result.Succeeded;            //the parameter it adds is empty, will have to fix it

        }
    }

}