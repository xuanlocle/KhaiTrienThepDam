using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienDam
{
    [Transaction(TransactionMode.Manual)]
    public class cmdTrungGianGetElement : IExternalCommand
    {
        public ParameterSet parameterSet;
        public Dictionary<string, object> dictNameInst;
        public Dictionary<string, Parameter> dictLuuParam;
        public ICollection<ElementId> selectedIds;
        public bool forMetro;
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //Gọi form
            if (!forMetro)
            {
                frm1 v_frm = new frm1();
                v_frm.ShowMeUpdate(doc, commandData, ref message, elements, parameterSet, dictLuuParam, selectedIds,false);
            }
            else
            {
                frm1 v_frm = new frm1();
                v_frm.ShowMeUpdate(doc, commandData, ref message, elements, parameterSet, dictLuuParam, selectedIds,true);
            }

            return Result.Succeeded;
        }
    }
}
