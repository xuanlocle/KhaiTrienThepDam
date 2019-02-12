using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDienDam.Openfile
{
    public partial class OpenFileControl : System.Windows.Forms.Form
    {
        private ExternalCommandData m_commandData;
        private string m_message;
        private ElementSet m_elements;
        private UIApplication m_uiapp;
        string[] _lstPath, _lstFamily_name;
        public OpenFileControl()
        {
            InitializeComponent();
        }
        public void ShowMe(ExternalCommandData p_commandData, ref string p_message, ElementSet p_element)
        {
            m_commandData = p_commandData;
            m_message = p_message;
            m_elements = p_element;
            m_uiapp = m_commandData.Application;
            this.ShowDialog();

            //string strLibPaths = "";
            //foreach (String path in m_uiapp.Application.GetLibraryPaths().Keys)
            //{
            //    strLibPaths += path + "\n";
            //}
            //MessageBox.Show(strLibPaths);
            //MessageBox.Show(m_uiapp.Application.GetRevitServerNetworkHosts().First()..ToString());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //string serPath = @"RSN://10.200.203.12/1701CPV-CC PHUONG VIET/2-MH THIET KE/2.1-KET CAU/PHAN NGAM/BE NGAM/";
            string serverFilePath, des;
            serverFilePath = @"RSN://" + tbServer.Text;
            serverFilePath += (serverFilePath[serverFilePath.Length - 1].ToString() != "/" ? "/" : "");

            serverFilePath += tbFolderFrom.Text;
            serverFilePath += (serverFilePath[serverFilePath.Length - 1].ToString() != "/" ? "/" : "");


            des = tbFolderTo.Text;
            des += (des[des.Length - 1].ToString() != "/" ? "/" : "");

            String[] lstNameSplited = SplitFileName(rtbFileNames);
            //MessageBox.Show("server: " + serverFilePath + "\ndes: " + des);
            int countErr=0;

            foreach (var k in lstNameSplited)
            {
                //MessageBox.Show(k+"\n");

                try
                {
                    Directory.CreateDirectory(@"V:\"+des);
                    CopyFileFromServer(m_uiapp, serverFilePath + k, des + k);
                    lboxResult.Items.Add("SUCCESS: --" + k + "--");
                }
                catch (Exception ex)
                {
                    lboxResult.Items.Add("ERROR: --" + k + " -- Can't copy");
                    countErr++;
                }
            }

            lboxResult.Items.Add("----------\nFINISH: Success: " + (lstNameSplited.Count() - countErr) + " , Error: " + countErr+".");
            rtbFileNames.Text = "";
        }

        private String[] SplitFileName(RichTextBox rtb)
        {
            var lines = rtb.Text.Split('\n');

            return lines;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbFolderTo.Text = tbFolderFrom.Text;
        }

        private void CopyFileFromServer(UIApplication uiapp, string serverFilePath, string des)
        {
            //string serverFilePath = @"RSN://10.200.203.12/1701CPV-CC PHUONG VIET/2-MH THIET KE/2.1-KET CAU/PHAN NGAM/BE NGAM/CPV-MHT-KC-PN-BN_Benuoc.rvt";
            OpenOptions openOptions = new OpenOptions();
            openOptions.DetachFromCentralOption = DetachFromCentralOption.DetachAndDiscardWorksets;
            
            

            if (ModelPathUtils.IsValidUserVisibleFullServerPath(serverFilePath))
            {
                ModelPath tempMp = ModelPathUtils.ConvertUserVisiblePathToModelPath(serverFilePath);
                uiapp.Application.CopyModel(tempMp, /*@"V:\1701CPV-CC PHUONG VIET\2-MH THIET KE\2.1-KET CAU\PHAN NGAM\BE NGAM\testcopybangcode.rvt"*/ @"V:\"+des, cbOverride.Checked);

            }
        }

      
        ///Demo copy from revit server with detact and subfolder 
        //https://knowledge.autodesk.com/search-result/caas/CloudHelp/cloudhelp/2016/ENU/Revit-API/files/GUID-F99FBBCC-AE58-46F5-85AF-0A7C3C5C0576-htm.html?_ga=2.130503375.1574265860.1546929511-1167342609.1543979434

        ///// <summary>
        ///// Uses the Revit Server REST API to recursively search the folders of the Revit Server for a particular model.
        ///// </summary>
        //private static ModelPath FindWSAPIModelPathOnServer(Application app, string hostId, string folderName, string fileName)
        //{
        //    // Connect to host to find list of available models (the "/contents" flag)
        //    XmlDictionaryReader reader = GetResponse(app, hostId, folderName + "/contents");
        //    bool found = false;

        //    // Look for the target model name in top level folder
        //    List<String> folders = new List<String>();
        //    while (reader.Read())
        //    {
        //        // Save a list of subfolders, if found
        //        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Folders")
        //        {
        //            while (reader.Read())
        //            {
        //                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Folders")
        //                    break;

        //                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Name")
        //                {
        //                    reader.Read();
        //                    folders.Add(reader.Value);
        //                }
        //            }
        //        }
        //        // Check for a matching model at this folder level
        //        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Models")
        //        {
        //            found = FindModelInServerResponseJson(reader, fileName);
        //            if (found)
        //                break;
        //        }
        //    }

        //    reader.Close();

        //    // Build the model path to match the found model on the server
        //    if (found)
        //    {
        //        // Server URLs use "|" for folder separation, Revit API uses "/"
        //        String folderNameFragment = folderName.Replace('|', '/');

        //        // Add trailing "/" if not present
        //        if (!folderNameFragment.EndsWith("/"))
        //            folderNameFragment += "/";

        //        // Build server path
        //        ModelPath modelPath = new ServerPath(hostId, folderNameFragment + fileName);
        //        return modelPath;
        //    }
        //    else
        //    {
        //        // Try subfolders
        //        foreach (String folder in folders)
        //        {
        //            ModelPath modelPath = FindWSAPIModelPathOnServer(app, hostId, folder, fileName);
        //            if (modelPath != null)
        //                return modelPath;
        //        }
        //    }

        //    return null;
        //}
        //// This string is different for each RevitServer version
        //private static string s_revitServerVersion = "/RevitServerAdminRESTService2014/AdminRESTService.svc/";

        ///// <summary>
        ///// Connect to server to get list of available models and return server response
        ///// </summary>
        //private static XmlDictionaryReader GetResponse(Application app, string hostId, string info)
        //{
        //    // Create request	
        //    WebRequest request = WebRequest.Create("http://" + hostId + s_revitServerVersion + info);
        //    request.Method = "GET";

        //    // Add the information the request needs

        //    request.Headers.Add("User-Name", app.Username);
        //    request.Headers.Add("User-Machine-Name", app.Username);
        //    request.Headers.Add("Operation-GUID", Guid.NewGuid().ToString());

        //    // Read the response
        //    XmlDictionaryReaderQuotas quotas =
        //        new XmlDictionaryReaderQuotas();
        //    XmlDictionaryReader jsonReader =
        //        JsonReaderWriterFactory.CreateJsonReader(request.GetResponse().GetResponseStream(), quotas);

        //    return jsonReader;
        //}

        ///// <summary>
        ///// Read through server response to find particular model
        ///// </summary>
        //private static bool FindModelInServerResponseJson(XmlDictionaryReader reader, string fileName)
        //{
        //    // Read through entries in this section
        //    while (reader.Read())
        //    {
        //        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Models")
        //            break;

        //        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Name")
        //        {
        //            reader.Read();
        //            String modelName = reader.Value;
        //            if (modelName.Equals(fileName))
        //            {
        //                // Match found, stop looping and return
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}

    }
}
