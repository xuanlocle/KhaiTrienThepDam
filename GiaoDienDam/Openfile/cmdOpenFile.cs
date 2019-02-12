using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienDam.Openfile
{
    [TransactionAttribute(TransactionMode.Manual), RegenerationAttribute(RegenerationOption.Manual)]
    public class cmdOpenFile : IExternalCommand
    {



        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            //Document doc = uiapp.ActiveUIDocument.Document;
            //UIDocument uidoc = new UIDocument(doc);


            //string serPath = @"RSN://10.200.203.12/1701CPV-CC PHUONG VIET/2-MH THIET KE/2.1-KET CAU/PHAN NGAM/BE NGAM/";
            //string desPath = @"V:\1701CPV-CC PHUONG VIET\2-MH THIET KE\2.1-KET CAU\PHAN NGAM\BE NGAM TEST\";
            //string cnt= "";

            OpenFileControl usercontrol = new OpenFileControl();
            usercontrol.ShowMe(commandData, ref message, elements);
            
            //foreach (string s in (serPath, "*.rvt").Select(System.IO.Path.GetFileName))
            //    cnt += s + "\n";


            //CopyFileFromServer(uiapp, serPath, desPath);
            //TaskDialog.Show("filename", cnt);




            //String modelPath = @"C:\Users\loc.lx\Documents\projectlinked.rvt";
          

            //doc = uidoc.Document;
            //doc.Regenerate();
            //OpenOptions openOptions = new OpenOptions();
            
            //uiapp.OpenAndActivateDocument(modelPath);

            //String path = @"C:\Users\loc.lx\Documents\proojecttest.rvt";
            //UnloadRevitLinks(ModelPathUtils.ConvertUserVisiblePathToModelPath(path));



            //try
            //{
            //    FilteredElementCollector collector = new FilteredElementCollector(uidoc.Document);
            //    collector.OfClass(typeof(RevitLinkInstance));

            //    string tmpstr = "";
            //    foreach (Element elem in collector)
            //    {
            //        RevitLinkInstance instance = elem as RevitLinkInstance;
            //        Document linkDoc = instance.GetLinkDocument();
            //        RevitLinkType type = doc.GetElement(instance.GetTypeId()) as RevitLinkType;
            //        type.Unload(null);
            //    }

            //}
            //catch (Exception e)
            //{
            //    TaskDialog.Show("in doc:", "err" + e.Message);
            //    message = e.Message;
            //    return Result.Failed;
            //}
            return Result.Succeeded;
        
        }

        //private void CopyFileFromServer(UIApplication uiapp, string serverFilePath, string des)
        //{
        //    //string serverFilePath = @"RSN://10.200.203.12/1701CPV-CC PHUONG VIET/2-MH THIET KE/2.1-KET CAU/PHAN NGAM/BE NGAM/CPV-MHT-KC-PN-BN_Benuoc.rvt";

        //    if (ModelPathUtils.IsValidUserVisibleFullServerPath(serverFilePath))
        //    {
        //        ModelPath tempMp = ModelPathUtils.ConvertUserVisiblePathToModelPath(serverFilePath);
        //        uiapp.Application.CopyModel(tempMp, /*@"V:\1701CPV-CC PHUONG VIET\2-MH THIET KE\2.1-KET CAU\PHAN NGAM\BE NGAM\testcopybangcode.rvt"*/ des, true);
        //    }
        //}

        void UnloadRevitLinks(ModelPath location)
        {
            TransmissionData transData = TransmissionData.ReadTransmissionData(location);
            TaskDialog.Show("unlink", "in UnloadRevitLinks -----" + transData.ToString());
            if (transData != null)
            {
                // collect all(immediate) external references in the model
                ICollection<ElementId> externalReferences = transData.GetAllExternalFileReferenceIds();

                TaskDialog.Show("unlink", "count ----- " + externalReferences.Count().ToString());

                // find every reference that is a link
                foreach (ElementId refId in externalReferences)
                {
                    ExternalFileReference extRef = transData.GetLastSavedReferenceData(refId);

                    TaskDialog.Show("unlink", "in UnloadRevitLinks -----" + refId.ToString()); // prompt keynotetable
                    TaskDialog.Show("unlink", "in UnloadRevitLinks -----" + extRef.ExternalFileReferenceType.ToString());
                    TaskDialog.Show("unlink", "in UnloadRevitLinks -----" + extRef.GetPath().ToString());
                    TaskDialog.Show("unlink", "in UnloadRevitLinks -----" + extRef.PathType.ToString());

                    transData.SetDesiredReferenceData(refId, extRef.GetPath(), extRef.PathType, false);

                    // ---- can not go inside here
                    if (extRef.ExternalFileReferenceType == ExternalFileReferenceType.RevitLink)
                    {
                        TaskDialog.Show("unlink", "in UnloadRevitLinks -----4");
                        // set the links to be unloaded (shouldLoad == false)
                        transData.SetDesiredReferenceData(refId, extRef.GetPath(), extRef.PathType, false);
                    }
                }

                // make sure the IsTransmitted property is set
                transData.IsTransmitted = true;

                // modified transmission data must be saved back to the model
                TransmissionData.WriteTransmissionData(location, transData);
            }
            else
            {
                TaskDialog.Show("Unload Links", "The document does not have any transmission data");
            }
        }
    }
}
