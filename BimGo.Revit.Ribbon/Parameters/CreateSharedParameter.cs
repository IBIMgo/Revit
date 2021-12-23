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
            DefinitionFile defFile = app.OpenSharedParameterFile();         //so far working only on the wall parameter cuz easier to manage
            //tries to create a shared parameter
            try
            {
                DefinitionGroups defGroups = defFile.Groups;
                DefinitionGroup defGroup = defGroups.Create("bimgo parameters");

                ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions("BimGoComment", ParameterType.Text);
                option.UserModifiable = true;
                option.Description = "type of an element";
                option.Name = "BimGoComment";
                Definition def = defGroup.Definitions.Create(option);

                CategorySet categories = uiapp.Application.Create.NewCategorySet();
                Category catwall = Category.GetCategory(uiapp.ActiveUIDocument.Document, BuiltInCategory.OST_Walls);
                categories.Insert(catwall);

                InstanceBinding instanceBinding = uiapp.Application.Create.NewInstanceBinding(categories);
                BindingMap bindingMap = uiapp.ActiveUIDocument.Document.ParameterBindings;

                Transaction t = new Transaction(doc, "adding a parameter");

                t.Start();
                bindingMap.Insert(def, instanceBinding, BuiltInParameterGroup.PG_TEXT);
                t.Commit();
            }
            //exception below occurs when trying to create DefinitionGroup that already exists, this catch then loads the existing parameter
            catch (Autodesk.Revit.Exceptions.InvalidOperationException)
            {
                
                foreach (DefinitionGroup dg in defFile.Groups)
                {
                    foreach (Definition def in dg.Definitions)
                    {
                        if (def.Name == "BimGoComment")
                        {
                            CategorySet categories = uiapp.Application.Create.NewCategorySet();
                            Category catwall = Category.GetCategory(uiapp.ActiveUIDocument.Document, BuiltInCategory.OST_Walls);
                            categories.Insert(catwall);

                            InstanceBinding instanceBinding = uiapp.Application.Create.NewInstanceBinding(categories);
                            BindingMap bindingMap = uiapp.ActiveUIDocument.Document.ParameterBindings;

                            Transaction t1 = new Transaction(doc, "loading an existing parameter");

                            t1.Start();
                            bindingMap.Insert(def, instanceBinding, BuiltInParameterGroup.PG_TEXT);
                            t1.Commit();
                            
                        }
                    }
                }

                
            }
            return Result.Succeeded;

        }
    }

}