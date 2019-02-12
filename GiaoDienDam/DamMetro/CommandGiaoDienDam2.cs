#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace GiaoDienDam
{
    [Transaction(TransactionMode.Manual)]
    public class CommandGiaoDienDam2 : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Selection sel = uidoc.Selection;
            Element element = null;
            ElementSetIterator elementSetItr = sel.Elements.ForwardIterator();
            elementSetItr.MoveNext();
            element = elementSetItr.Current as Element;

            //List<string> listCanLay = new List<string>()
            //{
            //    "TEN DAM LIEN TUC",
            //    "Thép liên tục",
            //    "L_Giới hạn 2 đầu cột",
            //    "GỐI 1",
            //    "GỐI 2",
            //    "B_Cột đầu 1",
            //    "B_Cột đầu 2",
            //    "D_Thép đai gối 2",
            //    "a_Đai phụ"
            //};

            Dictionary<Parameter, string> dictLayLen
                = new Dictionary<Parameter, string>();

            ParameterSet parameterSet = element.Parameters;
            //SetParameterFromString(doc, parameterSet, dictLayLen);
            Dictionary<string, Parameter> dictLuuParam = new Dictionary<string, Parameter>();
            foreach (Parameter param in parameterSet)
            {
                //SetParam(doc, param, "GỐI 1", 1);
                try
                {
                    if (!dictLuuParam.ContainsKey(param.Definition.Name))
                        dictLuuParam.Add(param.Definition.Name, param);
                    else
                        dictLuuParam[param.Definition.Name] = param;
                }
                catch (Exception ex)
                {
                    CUltilities.Logger(ex.ToString() + "Name = " + param.Definition.Name + ",Value = " + param.AsValueString());
                }
            }



            //Gọi form
            frm1 v_frm = new frm1();
            v_frm.ShowMeUpdate(doc, commandData, ref message, elements, parameterSet, dictLuuParam, true);

            //GiaoDienDam2 frm = new GiaoDienDam2(uidoc);
            //frm.ShowMeUpdate(doc, commandData, ref message, elements, parameterSet, dictLuuParam, true);
            //frm.Content = "hi";


            return Result.Succeeded;
        }

        private Dictionary<string, Parameter> GetParameterFromParameterSet(ParameterSet parameterSet, List<string> lstKey)
        {
            Dictionary<string, Parameter> kq = new Dictionary<string, Parameter>();


            foreach (Parameter p in parameterSet)
                foreach (string entry in lstKey)
                    if (p.Definition.Name.Equals(entry) && !kq.ContainsKey(entry))
                    {
                        kq.Add(entry, p);
                        //TaskDialog.Show("add dict", kq[entry].Definition.Name + " = " + kq[entry].AsValueString());
                    }
            return kq;
        }

        private void SetParameterFromString(Document doc, ParameterSet parameters, Dictionary<string, Parameter> lstCanSet)
        {
            foreach (Parameter param in parameters)
            {
                foreach (string str in lstCanSet.Keys)
                    //if (param.Definition.Name.Equals("TEN DAM LIEN TUC") /*&& param.AsString() != ""*/)
                    if (param.Definition.Name.Equals(str) /*&& param.AsString() != ""*/)
                    {
                        using (Transaction t = new Transaction(doc, "Set Parameter"))
                        {
                            t.Start();
                            param.Set(0);
                            t.Commit();
                        }
                    }
            }

            TaskDialog.Show("Report", "Done!");

            throw new NotImplementedException();

        }

    }
}
