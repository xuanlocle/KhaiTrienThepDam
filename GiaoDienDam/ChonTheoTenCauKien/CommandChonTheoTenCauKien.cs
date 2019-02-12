#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace GiaoDienDam
{
    [Transaction(TransactionMode.Manual)]
    public class CommandChonTheoTenCauKien : IExternalCommand
    {
        public string TENCAUKIEN;
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Dictionary<string, Dictionary<ElementId, Element>> dictSend;
            if (TENCAUKIEN != null)
                dictSend = LayTenCauKien(uidoc, TENCAUKIEN);
            else
            {
                dictSend = LayTenCauKien(uidoc);
            }

            Selection sel = uidoc.Selection;
            
            string rst = "";
            foreach (Element k in sel.Elements)
            {
                
                rst += "Tên: "+k.LookupParameter("TEN CAU KIEN").Element.Name + "\n"+ k.Id.IntegerValue;

            }
            TaskDialog.Show("rst", rst);


            FilteredElementCollector filt =
               new FilteredElementCollector(uidoc.Document)
               .OfClass(typeof(FamilyInstance))
               .OfCategory(BuiltInCategory.OST_StructuralFraming);

            //Gọi form
            FormSelectTenCauKien v_frm = new FormSelectTenCauKien();
            v_frm.ShowMeUpdate(doc, commandData, ref message, elements, dictSend, filt);




            return Result.Succeeded;
        }

        private Dictionary<string, Dictionary<ElementId, Element>> LayTenCauKien(UIDocument uidoc, string TENCAUKIEN = "TEN CAU KIEN")
        {
            //if (TENCAUKIEN.Length < 2) TENCAUKIEN = "TEN CAU KIEN";
            Dictionary<string, Dictionary<ElementId, Element>> result = new Dictionary<string, Dictionary<ElementId, Element>>();
            //retrieve document from uidoc
            Dictionary<ElementId, Element> dictTemp;
            Document doc = uidoc.Document;


            //test
            BuiltInCategory[] bics = new BuiltInCategory[] { BuiltInCategory.OST_StructuralFraming };
            IList<ElementFilter> a = new List<ElementFilter>(bics.Length);

            foreach (BuiltInCategory bic in bics)
            {
                a.Add(new ElementCategoryFilter(bic));
            }
            LogicalOrFilter categoryFilter = new LogicalOrFilter(a);

            LogicalAndFilter familyInstanceFilter
              = new LogicalAndFilter(categoryFilter,
                new ElementClassFilter(
                  typeof(FamilyInstance)));

            IList<ElementFilter> b = new List<ElementFilter>();
            b.Add(familyInstanceFilter);

            LogicalOrFilter classFilter = new LogicalOrFilter(b);
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector.WherePasses(classFilter);
            ICollection<ElementId> selectedIds = collector.ToElementIds();
            //endtest

            //Get all element which is selecting in active view
            //ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            foreach (var selectedId in selectedIds)
            {
                dictTemp = new Dictionary<ElementId, Element>();
                //Get an element in selected elements 
                Element e = doc.GetElement(selectedId);
                //try if it isn't structural framing
                try
                {
                    //get set of param
                    ParameterSet parameterset = e.Parameters;
                    Parameter tenCauKien = null;
                    foreach (Parameter temp in parameterset)
                    {
                        try
                        {
                            if (tenCauKien.AsString().Length > 0)
                            {
                                //find out
                                //TaskDialog.Show("TEN CAU KIEN", tenCauKien.AsString());
                                if (!(result.ContainsKey(tenCauKien.AsString())))
                                {
                                    dictTemp.Add(e.Id, e);
                                    result.Add(tenCauKien.AsString(), dictTemp);
                                }
                                else
                                {
                                    dictTemp.Add(e.Id, e);
                                    result[tenCauKien.AsString()].Add(e.Id, e);
                                }
                                break;
                            }
                        }
                        catch (Exception)
                        {
                        }

                        //filter tencaukien
                        if (temp.Definition.Name.Equals(TENCAUKIEN))
                        {
                            tenCauKien = temp;
                        }
                        else
                        {
                            continue;
                        }
                    }

                }
                catch (Exception exx)
                {
                    TaskDialog.Show("getparam", exx.ToString());
                }
            }
            return result;
        }

    }
}
