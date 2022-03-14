using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPISelection
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> SelectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new WallFilter(), "Выберите элементы");
            var wallList = new List<Wall>();

            string info = string.Empty;

            foreach (var selrctedElement in SelectedElementRefList)
            {
                Wall oWall = doc.GetElement(selrctedElement) as Wall;
                wallList.Add(oWall);
                    double width = UnitUtils.ConvertFromInternalUnits(oWall.Width, UnitTypeId.Millimeters);
                info += $"Name: {oWall.Name}, width: {width} {Environment.NewLine}";
            }

            info += $"Количество: {wallList.Count}";

            TaskDialog.Show("Selection", info);

            return Result.Succeeded;

        }
    }
}
