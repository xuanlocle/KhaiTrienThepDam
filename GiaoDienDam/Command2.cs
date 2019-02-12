#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace GiaoDienDam
{
    [Transaction(TransactionMode.Manual)]
    public class Command2 : IExternalCommand
    {
        public ParameterSet parameterSet;
        public Dictionary<string, object> dictNameInst;
        public Dictionary<string, Parameter> dictLuuParam;
        public ICollection<ElementId> selectedIds;
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Selection sel = uidoc.Selection;

            ////codemoi
            //12/7/2018
            //ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();

            if (selectedIds == null)
                selectedIds = uidoc.Selection.GetElementIds();


            Dictionary<Element, Dictionary<string, Parameter>> selectElePara = new Dictionary<Element, Dictionary<string, Parameter>>();
            Dictionary<string, Parameter> ac;
            foreach (var x in selectedIds)
            {
                //selectedEle.Add(doc.GetElement(x));
                Element e = doc.GetElement(x);
                ac = new Dictionary<string, Parameter>();
                foreach (Parameter ni in e.Parameters)
                {
                    try
                    {
                        if (!ac.ContainsKey(ni.Definition.Name))
                            ac.Add(ni.Definition.Name, ni);
                        else
                            ac[ni.Definition.Name] = ni;
                        if (!selectElePara.ContainsKey(e))
                            selectElePara.Add(e, ac);
                        else
                            selectElePara[e] = ac;
                        // CUltilities.Logger("ParamGet: " + e.Name + " = " + ac.Values);

                    }
                    catch (Exception exx)
                    {
                        CUltilities.Logger("Lỗi đa tác vụ: " + exx.ToString());
                    }
                }
            }
            //Dictionary<Element, List<Parameter>> selectElePara = new Dictionary<Element, List<Parameter>>();
            //List<Parameter> ac;
            //foreach (var x in selectedIds)
            //{
            //    //selectedEle.Add(doc.GetElement(x));
            //    Element e = doc.GetElement(x);
            //    ac = new List<Parameter>();
            //    foreach (Parameter ni in e.Parameters)
            //    {
            //        try
            //        {
            //            ac.Add(ni);
            //            selectElePara.Add(e, ac);
            //        }
            //        catch (Exception exx)
            //        {
            //            CUltilities.Logger("Lỗi đa tác vụ: " + exx.ToString());
            //        }
            //    }
            //}

            {
                using (Transaction tr = new Transaction(doc, "set value to parameter"))
                {
                    tr.Start();
                    foreach (var z in selectElePara)
                    {
                        foreach (var keyValuePair in dictNameInst)
                        {
                            //TaskDialog.Show("rv", "count = " + i++);
                            try
                            {
                                //SetParameterValue(dictLuuParam[keyValuePair.Key], keyValuePair.Value);
                                SetParameterValue(z.Value[keyValuePair.Key], keyValuePair.Value);
                                //CUltilities.Logger("Ghi gia tri: " + keyValuePair.Key + " = " + keyValuePair.Value);
                                //SetParameterValue(z.Value[key], keyValuePair.Value);
                            }
                            catch (Exception exx)
                            {
                                TaskDialog.Show("Err", "Can't set value of : " + keyValuePair.Key);
                                //CUltilities.Logger("Error, Can't set value of: " + keyValuePair.Key + " = " + keyValuePair.Value + "\n" + exx);
                            }
                        }
                    }
                    tr.Commit();
                }
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// Set parameter by parameter's Name
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="param"></param>
        /// <param name="paramName"></param>
        private void SetParam(Document doc, Parameter param, string paramName, int value)
        {
            if (param.Definition.Name.Equals(paramName))
            {
                using (Transaction t = new Transaction(doc, "Set Parameter"))
                {
                    t.Start();
                    param.Set(value);
                    t.Commit();
                }
            }
        }
        /// <summary>
        /// Set parameter by parameter's Name
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="param"></param>
        /// <param name="paramName"></param>
        private void SetParamD(Document doc, Parameter param, string paramName, double value)
        {
            if (param.Definition.Name.Equals(paramName))
            {
                using (Transaction t = new Transaction(doc, "Set Parameter"))
                {
                    t.Start();
                    param.Set(value);
                    t.Commit();
                }
            }
        }

        public static void SetParameterValue(Parameter p, object value)
        {
            try
            {
                if (value.GetType().Equals(typeof(string)))
                {
                    if (p.SetValueString(value as string))
                        return;
                }

                switch (p.StorageType)
                {
                    case StorageType.None:
                        break;
                    case StorageType.Double:
                        if (value.GetType().Equals(typeof(string)))
                        {
                            p.Set(double.Parse(value as string) / 304.8);
                        }
                        else
                        {
                            p.Set(Convert.ToDouble(value) / 304.8);
                        }
                        break;
                    case StorageType.Integer:
                        if (value.GetType().Equals(typeof(string)))
                        {
                            p.Set(int.Parse(value as string));
                        }
                        else
                        {
                            p.Set(Convert.ToInt32(value));
                        }
                        break;
                    case StorageType.ElementId:
                        if (value.GetType().Equals(typeof(ElementId)))
                        {
                            p.Set(value as ElementId);
                        }
                        else if (value.GetType().Equals(typeof(string)))
                        {
                            p.Set(new ElementId(int.Parse(value as string)));
                        }
                        else
                        {
                            p.Set(new ElementId(Convert.ToInt32(value)));
                        }
                        break;
                    case StorageType.String:
                        p.Set(value.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                TaskDialog.Show("ERR", ex.ToString());
                throw new Exception("Invalid Value Input!");
            }
        }

    }
}
