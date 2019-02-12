using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDienDam
{
    public partial class FormSelectTenCauKien : System.Windows.Forms.Form

    {
        private ExternalCommandData m_commandData;
        private string m_message;
        private ElementSet m_elements;
        private Autodesk.Revit.DB.Document doc;
        private Dictionary<string, Dictionary<ElementId, Element>> m_dictLuuParam;
        private FilteredElementCollector m_filt;

        public FormSelectTenCauKien()
        {
            InitializeComponent();
        }

        public void ShowMeUpdate(Autodesk.Revit.DB.Document doc,
            ExternalCommandData p_commandData,
            ref string p_message,
            ElementSet p_elements,
            Dictionary<string,
            Dictionary<ElementId, Element>> p_dictLuuParam,
            FilteredElementCollector p_filt)
        {
            this.doc = doc;
            m_commandData = p_commandData;
            m_elements = p_elements;
            m_message = p_message;
            m_dictLuuParam = p_dictLuuParam;
            m_filt = p_filt;
            this.ShowDialog();
        }

        private void FormSelectTenCauKien_Load(object sender, EventArgs e)
        {
            columnHeader1.Width = clbListParam.Width - 4;
            foreach (var k in m_dictLuuParam)
            {
                clbListParam.Items.Add(k.Key);
            }
        }



        private void clbListParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (var k in clbListElementHaveParam.Items)
            //    clbListElementHaveParam.Items.Remove(k);
            clbListElementHaveParam.Items.Clear();

            ListView lv = sender as ListView;
            int index = -1;
            foreach (ListViewItem item in lv.SelectedItems)
            {
                index = item.Index;
            }
            if (index > -1)
                foreach (var kChild in m_dictLuuParam.ElementAt(index).Value)
                {
                    clbListElementHaveParam.Items.Add(kChild.Key, true);
                }
            else
                foreach (var k in clbListElementHaveParam.Items)
                    clbListElementHaveParam.Items.Remove(k);

            try
            {
                string name = doc.GetElement(clbListElementHaveParam.Items[0] as ElementId).Name;
                string loai = name.Substring(name.IndexOf("Dam") + 3, 2);
                int kk;
                lblLoai.Text = Int32.TryParse(loai[1].ToString(), out kk) ? loai : "Other";
            }
            catch
            {

            }

        }

        private void btnOpenCommand_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(clbListElementHaveParam.CheckedItems[0]+"");
            Element element = doc.GetElement(clbListElementHaveParam.CheckedItems[0] as ElementId);

            List<ElementId> eleids = new List<ElementId>();
            foreach (var k in clbListElementHaveParam.CheckedItems)
                eleids.Add(k as ElementId);
            ICollection<ElementId> elementIds = eleids;
            int kkkk;
            //MessageBox.Show("clblistelementhaveparma: "+clbListElementHaveParam.CheckedItems.Count+"\nelementIds count : " + elementIds.Count + "\neleids count : " + eleids.Count);
            MessageBox.Show("Khai báo thép cho loại: " + (int.TryParse(lblLoai.Text[1].ToString(), out kkkk) ? lblLoai.Text : "Không xác định"), "Khai báo thép", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            var command = new cmdTrungGianGetElement
            { parameterSet = parameterSet, dictLuuParam = dictLuuParam, selectedIds = elementIds };
            command.Execute(m_commandData, ref m_message, m_elements);
            this.Close();

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var command = new CommandChonTheoTenCauKien
            { TENCAUKIEN = txtParameterToFilter.Text };
            this.Hide();
            command.Execute(m_commandData, ref m_message, m_elements);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnS.Enabled = !(String.IsNullOrEmpty(txtS.Text));           

        }

        private void btnS_Click(object sender, EventArgs e)
        {
            clbListParam.Items.Clear();
            bool checkReset = btnS.Text == "Reset" ? true : false;
            if (!checkReset)
            {
                foreach (var k in m_dictLuuParam)
                    if (k.Key.Contains(txtS.Text))
                        clbListParam.Items.Add(k.Key);
                btnS.Text = "Reset";
            }
            else
            {
                foreach (var k in m_dictLuuParam)
                    clbListParam.Items.Add(k.Key);
                btnS.Text = "Search";
                txtS.Text = "";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(clbListElementHaveParam.CheckedItems[0]+"");
            Element element = doc.GetElement(clbListElementHaveParam.CheckedItems[0] as ElementId);

            List<ElementId> eleids = new List<ElementId>();
            foreach (var k in clbListElementHaveParam.CheckedItems)
                eleids.Add(k as ElementId);
            ICollection<ElementId> elementIds = eleids;
            int kkkk;
            //MessageBox.Show("clblistelementhaveparma: "+clbListElementHaveParam.CheckedItems.Count+"\nelementIds count : " + elementIds.Count + "\neleids count : " + eleids.Count);
            MessageBox.Show("Khai báo thép cho loại: " + (int.TryParse(lblLoai.Text[1].ToString(), out kkkk) ? lblLoai.Text : "Không xác định"), "Khai báo thép", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            var command = new cmdTrungGianGetElement
            { parameterSet = parameterSet, dictLuuParam = dictLuuParam, selectedIds = elementIds, forMetro = true };
            command.Execute(m_commandData, ref m_message, m_elements);
            this.Close();

        }
    }
}
