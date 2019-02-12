using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace GiaoDienDam
{
    public partial class frm1 : System.Windows.Forms.Form
    {
        private ExternalCommandData m_commandData;
        private string m_message;
        private ElementSet m_elements;
        private Document doc;
        const int numBCotDau = 150;
        Dictionary<string, Parameter> dictLuuParam;
        ParameterSet ParameterSet;
        private ICollection<ElementId> m_elementIds;
        //List<DrawItemState>

        string[] lstTypeA = new string[] { "A1", "A2", "A3", "A4" };

        string[] lstTypeB = new string[] { "B1", "B2", "B3", "B4" };

        string[] lstTypeC = new string[] { "C1", "C2", "C3", "C4" };

        string[] lstTypeD = new string[] { "D1", "D2", "D3" };

        string[] lstTypeE = new string[] { "E1", "E2", "E3", "E4", "E5", "E6" };

        string[] lstTypeF = new string[] { "F1" };

        double lengthDam = 0;

        Dictionary<NumericUpDown, TabPage> lstCheck0 = new Dictionary<NumericUpDown, TabPage>();
        bool neoTheped = false;


        bool damLoaiMoi = false;
        public frm1()
        {
            InitializeComponent();

            //ListView.SelectedIndexCollection indices = listView1.SelectedIndices;
            if (cbbLoaiDam.SelectedIndex == -1) tabControl2.Enabled = false;
            cbDam2Goi.Checked = true;
            gbDam2Goi.Click += new System.EventHandler(this.cbDam2Goi_CheckedChanged);

            cbThepChu1Lop.Visible = false;
            cbThepChu2Lop.Visible = false;
            gbThepTren2.Visible = false;
            gbThepDuoi2.Visible = false;
            cbThepTren2.Visible = false;
            cbThepDuoi2.Visible = false;
            ShowThepGiaCuong(false);
            ShowThepGia(false);

            //int MAX_L_DINH_VI_THEP_GIA_CUONG = (int)dictLuuParam["Length"].AsDouble();
            //toolTip1 = new CustomToolTip();
            toolTip1.ToolTipTitle = "Mô tả ngắn";
            toolTip1.SetToolTip(this.gbDam2Goi, "Hai đầu dầm được đỡ bởi 2 cột:\n-Neo đầu 1:");
            toolTip1.SetToolTip(this.numBCotDau1, "Kích thước đầu cột theo phương dầm");
            toolTip1.SetToolTip(this.numBCotDau2, "Kích thước đầu cột theo phương dầm");
            toolTip1.SetToolTip(this.lbCotDau1, "Kích thước đầu cột theo phương dầm");
            toolTip1.SetToolTip(this.lbCotDau2, "Kích thước đầu cột theo phương dầm");

        }
        ///
        //public void ShowMeUpdate(
        //   Document doc,
        //   ExternalCommandData p_commandData,
        //   ref string p_message,
        //   ElementSet p_elements,
        //   ParameterSet parameterSet,
        //   Dictionary<string, Parameter> dictLuuParam,
        //   ICollection<ElementId> elementIds)
        //{
        //    this.doc = doc;
        //    m_commandData = p_commandData;
        //    m_elements = p_elements;
        //    m_message = p_message;
        //    ParameterSet = parameterSet;
        //    this.dictLuuParam = dictLuuParam;
        //    m_elementIds = elementIds;
        //    lengthDam = (double)GetParamDouble("Length");

        //    try
        //    {
        //        string loaiCauKien = dictLuuParam["LOAI CAU KIEN"].AsString();

        //        cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[0];
        //        cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[0];
        //        numBCotDau1.Value = (decimal)(dictLuuParam[CUltilities.bCotDau1].AsDouble() * 304.8);
        //        numBCotDau2.Value = (decimal)(dictLuuParam[CUltilities.bCotDau2].AsDouble() * 304.8);

        //        if (loaiCauKien.Length > 1)
        //        {
        //            //update 
        //            if (MessageBox.Show("Giữ số liệu hiện có?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //            {
        //                //Update parameter
        //                //TaskDialog.Show("neo", dictLuuParam[CUltilities.goi1].AsInteger() + "");
        //                int goi1 = (int)GetParamInt(CUltilities.goi1);
        //                int goi2 = (int)GetParamInt(CUltilities.goi2);
        //                double b1 = (double)GetParamDouble(CUltilities.bCotDau1);
        //                double b2 = (double)GetParamDouble(CUltilities.bCotDau2);

        //                if (b1 >= 150 && b2 >= 150)
        //                {
        //                    cbDam2Goi.Checked = true;
        //                    if (goi1 == goi2)
        //                    {
        //                        if (goi1 == 1)
        //                        {
        //                            rdNeoDauDam1Va2.Checked = true;
        //                        }
        //                        else
        //                        {
        //                            rdKhongNeo.Checked = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (goi1 == 1)
        //                        {
        //                            rdNeoDauDam1.Checked = true;
        //                        }
        //                        else
        //                        {
        //                            rdNeoDauDam2.Checked = true;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    //console
        //                    cbDamConsole.Checked = true;
        //                    if (b1 == 0)
        //                    {
        //                        if (goi2 == 0)
        //                            //console dau dam 1, neo dau dam 2
        //                            rdConsoleDau1KhongNeo2.Checked = true;
        //                        else
        //                            rdConsoleDau1Neo2.Checked = true;
        //                    }
        //                    else
        //                    {
        //                        if (b2 == 0)
        //                            if (goi1 == 0)
        //                                rdConsoleDau2KhongNeo2.Checked = true;
        //                            else
        //                                rdConsoleDau2Neo1.Checked = true;
        //                    }
        //                }

        //                numBCotDau1.Value = (decimal)b1;
        //                numBCotDau2.Value = (decimal)b2;

        //                string viTriDam = dictLuuParam[CUltilities.viTriDam].AsString();
        //                string viTriGoi1 = dictLuuParam[CUltilities.viTriGoi1].AsString();
        //                string viTriGoi2 = dictLuuParam[CUltilities.viTriGoi2].AsString();

        //                if (viTriDam.Length > 2)
        //                    tbViTriDam.Text = viTriDam;
        //                if (viTriGoi1.Length > 2)
        //                    tbViTriGoi1.Text = viTriGoi1;
        //                if (viTriGoi2.Length > 2)
        //                    tbViTriGoi2.Text = viTriGoi2;



        //                numThepChuTren1DuongKinh.Value = GetParamDouble(CUltilities.thepTren1DuongKinh);
        //                numThepChuTren1SoLuong.Value = GetParamInt(CUltilities.thepTren1SoLuong);
        //                numThepChuDuoi1DuongKinh.Value = GetParamDouble(CUltilities.thepDuoi1DuongKinh);
        //                //TaskDialog.Show("Thep chu duoi", GetParamDouble(CUltilities.thepDuoi1DuongKinh).ToString());
        //                numThepChuDuoi1SoLuong.Value = GetParamInt(CUltilities.thepDuoi1SoLuong);
        //                cbThepTren2.Checked = GetParamCheck(CUltilities.thepTren2Check);
        //                //numThepChuTren2Check.Value = GetParamDouble(CUltilities.thepTren2Check);
        //                numThepChuTren2DuongKinh.Value = GetParamDouble(CUltilities.thepTren2DuongKinh);
        //                numThepChuTren2SoLuong.Value = GetParamInt(CUltilities.thepTren2SoLuong);
        //                cbThepDuoi2.Checked = GetParamCheck(CUltilities.thepDuoi2Check);
        //                //numThepChuDuoi2Check.Value = GetParamDouble(CUltilities.thepDuoi2Check);
        //                numThepChuDuoi2DuongKinh.Value = GetParamDouble(CUltilities.thepDuoi2DuongKinh);
        //                numThepChuDuoi2SoLuong.Value = GetParamInt(CUltilities.thepDuoi2SoLuong);


        //                cbGiaCuongGoi1.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi1Check);
        //                numThepGiaCuongGoi1DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1DuongKinh);
        //                numThepGiaCuongGoi1SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi1SoLuong);
        //                numThepGiaCuongGoi1DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1DinhVi);
        //                cbThepGiaCuongD1L2.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi1L2Check);
        //                numThepGiaCuongGoi1L2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1L2DuongKinh);
        //                numThepGiaCuongGoi1L2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi1L2SoLuong);
        //                cbGiaCuongNhip.Checked = GetParamCheck(CUltilities.thepGiaCuongNhipCheck);
        //                numThepGiaCuongNhipDuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDuongKinh);
        //                //numThepGiaCuongNhipSoLuong.Value = GetParamDouble(CUltilities.thepGiaCuongNhipSoLuong);
        //                //numThepGiaCuongNhipL2Check.Value = GetParamDouble(CUltilities.thepGiaCuongNhipL2Check);
        //                numThepGiaCuongNhipL2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipL2DuongKinh);
        //                numThepGiaCuongNhipL2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongNhipL2SoLuong);
        //                try
        //                {
        //                    cbAutoRangBuocNhip.Checked = !GetParamCheck(CUltilities.thepGiaCuongNhipLCheck);
        //                    numThepGiaCuongNhipDinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDinhVi);
        //                }
        //                catch (Exception exx)
        //                {
        //                    //MessageBox.Show("Không thể load biến " + numThepGiaCuongNhipDinhVi.Name + "\n" + exx.ToString());
        //                }

        //                cbGiaCuongGoi2.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi2Check);
        //                numThepGiaCuongGoi2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2DuongKinh);
        //                numThepGiaCuongGoi2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi2SoLuong);
        //                numThepGiaCuongGoi2DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2DinhVi);
        //                cbThepGiaCuongD2L2.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi2L2Check);
        //                numThepGiaCuongGoi2L2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2L2DuongKinh);
        //                numThepGiaCuongGoi2L2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi2L2SoLuong);
        //                //cbAuToRangBuoc.Checked = (GetParamCheck(CUltilities.thepGiaCuongAuTo)==true)
        //                cbAuToRangBuoc.Checked = ((double)numThepGiaCuongGoi1DinhVi.Value == lengthDam / 4) ? true : false;
        //                //cbAuToRangBuoc2.Checked = !GetParamCheck(CUltilities.thepGiaCuongAuTo);
        //                cbAuToRangBuoc2.Checked = ((double)numThepGiaCuongGoi2DinhVi.Value == lengthDam / 4) ? true : false;

        //                //numThepDaiCheck.Value = GetParamDouble(CUltilities.thepDaiCheck);
        //                numThepDaiGoi1DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGoi1DuongKinh);
        //                numThepDaiGoi1KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGoi1KhoangRai);
        //                numThepDaiNhipDuongKinh.Value = GetParamDouble(CUltilities.thepDaiNhipDuongKinh);
        //                numThepDaiNhipKhoangCach.Value = GetParamDouble(CUltilities.thepDaiNhipKhoangRai);
        //                numThepDaiGoi2DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGoi2DuongKinh);
        //                numThepDaiGoi2KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGoi2KhoangRai);
        //                cbThepGia.Checked = GetParamCheck(CUltilities.thepGiaCheck);
        //                if (cbThepGia.Checked)
        //                {
        //                    numThepGiaDuongKinh.Value = GetParamDouble(CUltilities.thepGiaDuongKinh);
        //                }
        //                cbThepDaiGia.Checked = GetParamCheck(CUltilities.thepGiaDaiGiaCheck);
        //                if (cbThepDaiGia.Checked)
        //                {
        //                    numThepGiaDaiDuongKinh.Value = GetParamDouble(CUltilities.thepGiaDaiGiaDuongKinh);
        //                    numThepGiaDaiKhoangRai.Value = GetParamDouble(CUltilities.thepGiaDaiGiaKhoangCach);
        //                }
        //                cbThepGiaDiToiGoi1.Checked = GetParamCheck(CUltilities.thepGiaToiGoi1);
        //                cbThepGiaDiToiGoi2.Checked = GetParamCheck(CUltilities.thepGiaToiGoi2);


        //                cbThepDaiLong.Checked = GetParamCheck(CUltilities.thepDaiLongDonCheck);
        //                numThepDaiLongSoLuong.Value = GetParamInt(CUltilities.thepDaiLongDonN);
        //                cbDaiLongKep.Checked = GetParamCheck(CUltilities.thepDaiLongKepCheck);
        //                cbDaiLongKepBen1.Checked = GetParamCheck(CUltilities.thepDaiLongKepBen1);
        //                cbDaiLongKepBen2.Checked = GetParamCheck(CUltilities.thepDaiLongKepBen2);
        //                numDaiLongKepN1.Value = GetParamInt(CUltilities.thepDaiLongKepN1);
        //                numDaiLongKepN2.Value = GetParamInt(CUltilities.thepDaiLongKepN2);

        //                cbThepVaiBoDau1.Checked = GetParamCheck(CUltilities.thepVaiBoDau1Check);
        //                //MessageBox.Show(GetParamCheck(CUltilities.thepVaiBoDau1Check) + "");
        //                if (cbThepVaiBoDau1.Checked)
        //                {
        //                    numThepVaiBoDau1B.Value = GetParamDouble(CUltilities.thepVaiBo1B);
        //                    numThepVaiBoDau1H.Value = GetParamDouble(CUltilities.thepVaiBo1H);
        //                    //cbThepVaiBoDau1VB.Checked = GetParamCheck(CUltilities.thepVaiBo1Check);
        //                    cbThepVaiBo1DaiTangCuong.Checked = GetParamCheck(CUltilities.thepVaiBo1DaiCheck);
        //                    numThepVaiBoDau1DuongKinhDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo1DuongKinhDaiTru);
        //                    numThepVaiBoDau1KhoangRaiDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo1KhoangCachDaiTru);
        //                }
        //                cbThepVaiBoDau2.Checked = GetParamCheck(CUltilities.thepVaiBoDau2Check);
        //                if (cbThepVaiBoDau2.Checked)
        //                {
        //                    numThepVaiBoDau2B.Value = GetParamDouble(CUltilities.thepVaiBo2B);
        //                    numThepVaiBoDau2H.Value = GetParamDouble(CUltilities.thepVaiBo2H);
        //                    //cbThepVaiBoDau2VB.Checked = GetParamCheck(CUltilities.thepVaiBo2Check);
        //                    cbThepVaiBo2DaiTangCuong.Checked = GetParamCheck(CUltilities.thepVaiBo2DaiCheck);
        //                    numThepVaiBoDau2DuongKinhDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo2DuongKinhDaiTru);
        //                    numThepVaiBoDau2KhoangRaiDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo2KhoangCachDaiTru);
        //                }
        //                cbThepDaiC.Checked = true;
        //                cbThepDaiCDungDon.Checked = GetParamCheck(CUltilities.thepDaiCDungDonCheck);
        //                //numThepDaiCDungGiua.Value = GetParamDouble(CUltilities.thepDaiCDungGiua);
        //                cbThepDaiCDungKep.Checked = GetParamCheck(CUltilities.thepDaiCDungKepCheck);
        //                cbThepDaiCDungKepBen1.Checked = GetParamCheck(CUltilities.thepDaiCDungKepBen1);
        //                cbThepDaiCDungKepBen2.Checked = GetParamCheck(CUltilities.thepDaiCDungKepBen2);
        //                numThepDaiCDungKepN.Value = GetParamInt(CUltilities.thepDaiCDungKepN);
        //                cbThepDaiCNgangTren.Checked = GetParamCheck(CUltilities.thepDaiCNgangTrenCheck);
        //                numThepDaiCNgangTrenD.Value = GetParamDouble(CUltilities.thepDaiCNgangTrenD);
        //                numThepDaiCNgangTrenR.Value = GetParamDouble(CUltilities.thepDaiCNgangTrenR);
        //                cbThepDaiCNgangDuoi.Checked = GetParamCheck(CUltilities.thepDaiCNgangDuoiCheck);
        //                numThepDaiCNgangDuoiD.Value = GetParamDouble(CUltilities.thepDaiCNgangDuoiD);
        //                numThepDaiCNgangDuoiR.Value = GetParamDouble(CUltilities.thepDaiCNgangDuoiR);
        //                if (cbThepDaiCDungDon.Checked == false &&
        //                    cbThepDaiCDungKep.Checked == false &&
        //                    cbThepDaiCNgangTren.Checked == false &&
        //                    cbThepDaiCNgangDuoi.Checked == false)
        //                    cbThepDaiC.Checked = false;

        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    catch (Exception exx)
        //    {
        //        TaskDialog.Show("Update", exx.ToString());
        //        CUltilities.Logger("Lỗi update:" + exx.ToString());
        //    }
        //    this.ShowDialog();
        //}




        /// <summary>
        /// Update load theo tên cấu kiện
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="p_commandData"></param>
        /// <param name="p_message"></param>
        /// <param name="p_elements"></param>
        /// <param name="parameterSet"></param>
        /// <param name="dictLuuParam"></param>
        /// <param name="elementIds"></param>
        /// <param name="damMoi"></param>
        public void ShowMeUpdate(
        Document doc,
        ExternalCommandData p_commandData,
        ref string p_message,
        ElementSet p_elements,
        ParameterSet parameterSet,
        Dictionary<string, Parameter> dictLuuParam,
        ICollection<ElementId> elementIds,
        bool damMoi)
        {
            this.doc = doc;
            m_commandData = p_commandData;
            m_elements = p_elements;
            m_message = p_message;
            ParameterSet = parameterSet;
            this.dictLuuParam = dictLuuParam;
            m_elementIds = elementIds;
            lengthDam = (double)GetParamDouble("Length");
            damLoaiMoi = damMoi;
            if (damLoaiMoi == true)
            {

                groupBox7.Size = new Size(new System.Drawing.Point(168, 104));
                panel4.Visible = true;
                panel6.Visible = true;
                panel8.Visible = true;

                cbAuToRangBuoc.Checked = false;
                cbAuToRangBuoc2.Checked = cbAuToRangBuoc.Checked;
                cbAutoRangBuocNhip.Checked = cbAuToRangBuoc.Checked;

                cbAuToRangBuoc.Visible = false;
                cbAuToRangBuoc2.Visible = cbAuToRangBuoc.Visible;
                cbAutoRangBuocNhip.Visible = cbAuToRangBuoc.Visible;


                pnGiaCuongDau1L.Location = new System.Drawing.Point(pnGiaCuongDau1L.Location.X, 77);
                pnGiaCuongDau2L.Location = new System.Drawing.Point(pnGiaCuongDau2L.Location.X, 77);
                pnGiaCuongGiuaL.Location = new System.Drawing.Point(pnGiaCuongGiuaL.Location.X, 77);

                pnGiaCuongDauDam1Lop1.Location = new System.Drawing.Point(pnGiaCuongDauDam1Lop1.Location.X, 89);
                pnGiaCuongDauDam2Lop1.Location = new System.Drawing.Point(pnGiaCuongDauDam2Lop1.Location.X, 89);
                pnGiaCuongGiuaDamLop1.Location = new System.Drawing.Point(pnGiaCuongGiuaDamLop1.Location.X, 89);


                pnDaiCNgangTren.Location = new System.Drawing.Point(12, 43);
                pnDaiCNgangDuoi.Location = new System.Drawing.Point(16, 127);

                pnDaiCDungKep.Size = new Size(new System.Drawing.Point(242, 187));
                cbThepDaiCDungKep.Text = "Thép đai C đứng thứ 1";

                MessageBox.Show("Change thanh cong");
            }
            try
            {
                string loaiCauKien = dictLuuParam["LOAI CAU KIEN"].AsString();



                cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[0];
                cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[0];
                try
                {
                    numBCotDau1.Value = (decimal)(dictLuuParam[CUltilities.bCotDau1].AsDouble() * 304.8);
                }
                catch (Exception exx)
                {
                    TaskDialog.Show("Err from " + CUltilities.bCotDau1, "Giá trị hiện có của " + CUltilities.bCotDau1 + " không phù hợp. Add-in sẽ tự gán bằng mặc định.\n" + exx.ToString());
                    numBCotDau1.Value = numBCotDau1.Minimum;
                }
                try
                {
                    numBCotDau2.Value = (decimal)(dictLuuParam[CUltilities.bCotDau2].AsDouble() * 304.8);
                }
                catch (Exception exx)
                {
                    TaskDialog.Show("Err from " + CUltilities.bCotDau2, "Giá trị hiện có của " + CUltilities.bCotDau2 + " không phù hợp. Add-in sẽ tự gán bằng mặc định.\n" + exx.ToString());
                    numBCotDau2.Value = numBCotDau2.Minimum;

                }

                if (loaiCauKien.Length > 1)
                {
                    //update 
                    //if (MessageBox.Show("Giữ số liệu hiện có?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {



                        //Update parameter
                        //TaskDialog.Show("neo", dictLuuParam[CUltilities.goi1].AsInteger() + "");
                        int goi1 = (int)GetParamInt(CUltilities.goi1);
                        int goi2 = (int)GetParamInt(CUltilities.goi2);
                        double b1 = (double)GetParamDouble(CUltilities.bCotDau1);
                        double b2 = (double)GetParamDouble(CUltilities.bCotDau2);

                        if (b1 >= 150 && b2 >= 150)
                        {
                            cbDam2Goi.Checked = true;
                            if (goi1 == goi2)
                            {
                                if (goi1 == 1)
                                {
                                    rdNeoDauDam1Va2.Checked = true;
                                }
                                else
                                {
                                    rdKhongNeo.Checked = true;
                                }
                            }
                            else
                            {
                                if (goi1 == 1)
                                {
                                    rdNeoDauDam1.Checked = true;
                                }
                                else
                                {
                                    rdNeoDauDam2.Checked = true;
                                }
                            }
                        }
                        else
                        {
                            //console
                            cbDamConsole.Checked = true;
                            if (b1 == 0)
                            {
                                if (goi2 == 0)
                                    //console dau dam 1, neo dau dam 2
                                    rdConsoleDau1KhongNeo2.Checked = true;
                                else
                                    rdConsoleDau1Neo2.Checked = true;
                            }
                            else
                            {
                                if (b2 == 0)
                                    if (goi1 == 0)
                                        rdConsoleDau2KhongNeo2.Checked = true;
                                    else
                                        rdConsoleDau2Neo1.Checked = true;
                            }
                        }

                        numBCotDau1.Value = (decimal)b1;
                        numBCotDau2.Value = (decimal)b2;

                        string viTriDam = dictLuuParam[CUltilities.viTriDam].AsString();
                        string viTriGoi1 = dictLuuParam[CUltilities.viTriGoi1].AsString();
                        string viTriGoi2 = dictLuuParam[CUltilities.viTriGoi2].AsString();

                        if (viTriDam.Length > 2)
                            tbViTriDam.Text = viTriDam;
                        if (viTriGoi1.Length > 2)
                            tbViTriGoi1.Text = viTriGoi1;
                        if (viTriGoi2.Length > 2)
                            tbViTriGoi2.Text = viTriGoi2;



                        numThepChuTren1DuongKinh.Value = GetParamDouble(CUltilities.thepTren1DuongKinh);
                        numThepChuTren1SoLuong.Value = GetParamInt(CUltilities.thepTren1SoLuong);
                        numThepChuDuoi1DuongKinh.Value = GetParamDouble(CUltilities.thepDuoi1DuongKinh);
                        //TaskDialog.Show("Thep chu duoi", GetParamDouble(CUltilities.thepDuoi1DuongKinh).ToString());
                        numThepChuDuoi1SoLuong.Value = GetParamInt(CUltilities.thepDuoi1SoLuong);
                        cbThepTren2.Checked = GetParamCheck(CUltilities.thepTren2Check);
                        //numThepChuTren2Check.Value = GetParamDouble(CUltilities.thepTren2Check);
                        numThepChuTren2DuongKinh.Value = GetParamDouble(CUltilities.thepTren2DuongKinh);
                        numThepChuTren2SoLuong.Value = GetParamInt(CUltilities.thepTren2SoLuong);
                        cbThepDuoi2.Checked = GetParamCheck(CUltilities.thepDuoi2Check);
                        //numThepChuDuoi2Check.Value = GetParamDouble(CUltilities.thepDuoi2Check);
                        numThepChuDuoi2DuongKinh.Value = GetParamDouble(CUltilities.thepDuoi2DuongKinh);
                        numThepChuDuoi2SoLuong.Value = GetParamInt(CUltilities.thepDuoi2SoLuong);


                        cbGiaCuongGoi1.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi1Check) || GetParamCheck(CUltilities.thepGiaCuongGoi1L2Check);
                        cbThepGiaCuongD1L1.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi1Check);
                        numThepGiaCuongGoi1DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1DuongKinh);
                        numThepGiaCuongGoi1SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi1SoLuong);


                        cbThepGiaCuongD1L2.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi1L2Check);
                        numThepGiaCuongGoi1L2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1L2DuongKinh);
                        numThepGiaCuongGoi1L2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi1L2SoLuong);


                        if (!damLoaiMoi)
                        {
                            numThepGiaCuongGoi1DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1DinhVi);
                            cbGiaCuongNhip.Checked = GetParamCheck(CUltilities.thepGiaCuongNhipCheck);
                            numThepGiaCuongNhipDuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDuongKinh);
                            numThepGiaCuongNhipL2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipL2DuongKinh);
                            numThepGiaCuongNhipL2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongNhipL2SoLuong);
                            numThepGiaCuongGoi2DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2DinhVi);

                            try
                            {
                                cbAutoRangBuocNhip.Checked = !GetParamCheck(CUltilities.thepGiaCuongNhipLCheck);
                                numThepGiaCuongNhipDinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDinhVi);
                            }
                            catch (Exception exx)
                            {
                                //MessageBox.Show("Không thể load biến " + numThepGiaCuongNhipDinhVi.Name + "\n" + exx.ToString());
                            }
                        }
                        else
                        {
                            numThepGiaCuongGoi1DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1DinhViD2);

                            numThepGiaCuongNhipDuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDuongKinhD2);
                            numThepGiaCuongNhipL2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipL2DuongKinhD2);
                            numThepGiaCuongNhipL2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongNhipL2SoLuongD2);
                            numThepGiaCuongGoi2DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2DinhViD2);
                            numThepDaiGoi1DinhVi.Value = GetParamDouble(CUltilities.thepDaiGoi1DinhViD2);
                            numThepGiaCuongNhipDinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDinhViD2);
                            numThepDaiGoi2DinhVi.Value = numThepDaiGoi1DinhVi.Value;

                            cbGiaCuongNhip.Checked = GetParamCheck(CUltilities.thepGiaCuongNhipCheckD2) || GetParamCheck(CUltilities.thepGiaCuongNhipL2CheckD2);
                            cbThepGiaCuongGL1.Checked = GetParamCheck(CUltilities.thepGiaCuongNhipCheckD2);

                        }
                        //numThepGiaCuongNhipSoLuong.Value = GetParamDouble(CUltilities.thepGiaCuongNhipSoLuong);
                        //numThepGiaCuongNhipL2Check.Value = GetParamDouble(CUltilities.thepGiaCuongNhipL2Check);


                        cbGiaCuongGoi2.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi2Check);
                        numThepGiaCuongGoi2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2DuongKinh);
                        numThepGiaCuongGoi2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi2SoLuong);

                        cbThepGiaCuongD2L2.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi2L2Check);
                        numThepGiaCuongGoi2L2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2L2DuongKinh);
                        numThepGiaCuongGoi2L2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi2L2SoLuong);
                        //cbAuToRangBuoc.Checked = (GetParamCheck(CUltilities.thepGiaCuongAuTo)==true)
                        cbAuToRangBuoc.Checked = ((double)numThepGiaCuongGoi1DinhVi.Value == lengthDam / 4) ? true : false;
                        //cbAuToRangBuoc2.Checked = !GetParamCheck(CUltilities.thepGiaCuongAuTo);
                        cbAuToRangBuoc2.Checked = ((double)numThepGiaCuongGoi2DinhVi.Value == lengthDam / 4) ? true : false;

                        //numThepDaiCheck.Value = GetParamDouble(CUltilities.thepDaiCheck);
                        numThepDaiGoi1DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGoi1DuongKinh);
                        numThepDaiGoi1KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGoi1KhoangRai);
                        numThepDaiNhipDuongKinh.Value = GetParamDouble(CUltilities.thepDaiNhipDuongKinh);
                        numThepDaiNhipKhoangCach.Value = GetParamDouble(CUltilities.thepDaiNhipKhoangRai);
                        numThepDaiGoi2DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGoi2DuongKinh);
                        numThepDaiGoi2KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGoi2KhoangRai);
                        // numThepDaiGiaCuongNhanhTrenCheck.Value = GetParamDouble(CUltilities.thepDaiGiaCuongNhanhTrenCheck);
                        //numThepDaiGiaCuongDauDam1DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGiaCuongDauDam1DuongKinh);
                        //numThepDaiGiaCuongDauDam1KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGiaCuongDauDam1KhoangCach);
                        //numThepDaiGiaCuongNhanhDuoiCheck.Value = GetParamDouble(CUltilities.thepDaiGiaCuongNhanhDuoiCheck);
                        //numThepDaiGiaCuongDauDam2DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGiaCuongDauDam2DuongKinh);
                        //numThepDaiGiaCuongDauDam2KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGiaCuongDauDam2KhoangCach);
                        cbThepGia.Checked = GetParamCheck(CUltilities.thepGiaCheck);
                        if (cbThepGia.Checked)
                        {
                            numThepGiaDuongKinh.Value = GetParamDouble(CUltilities.thepGiaDuongKinh);
                        }
                        cbThepDaiGia.Checked = GetParamCheck(CUltilities.thepGiaDaiGiaCheck);
                        if (cbThepDaiGia.Checked)
                        {
                            numThepGiaDaiDuongKinh.Value = GetParamDouble(CUltilities.thepGiaDaiGiaDuongKinh);
                            numThepGiaDaiKhoangRai.Value = GetParamDouble(CUltilities.thepGiaDaiGiaKhoangCach);
                        }
                        cbThepGiaDiToiGoi1.Checked = GetParamCheck(CUltilities.thepGiaToiGoi1);
                        cbThepGiaDiToiGoi2.Checked = GetParamCheck(CUltilities.thepGiaToiGoi2);


                        cbThepDaiLong.Checked = GetParamCheck(CUltilities.thepDaiLongDonCheck);
                        numThepDaiLongSoLuong.Value = GetParamInt(CUltilities.thepDaiLongDonN);
                        cbDaiLongKep.Checked = GetParamCheck(CUltilities.thepDaiLongKepCheck);
                        cbDaiLongKepBen1.Checked = GetParamCheck(CUltilities.thepDaiLongKepBen1);
                        cbDaiLongKepBen2.Checked = GetParamCheck(CUltilities.thepDaiLongKepBen2);
                        numDaiLongKepN1.Value = GetParamInt(CUltilities.thepDaiLongKepN1);
                        numDaiLongKepN2.Value = GetParamInt(CUltilities.thepDaiLongKepN2);

                        cbThepVaiBoDau1.Checked = GetParamCheck(CUltilities.thepVaiBoDau1Check);
                        //MessageBox.Show(GetParamCheck(CUltilities.thepVaiBoDau1Check) + "");
                        if (cbThepVaiBoDau1.Checked)
                        {
                            numThepVaiBoDau1B.Value = GetParamDouble(CUltilities.thepVaiBo1B);
                            numThepVaiBoDau1H.Value = GetParamDouble(CUltilities.thepVaiBo1H);
                            //cbThepVaiBoDau1VB.Checked = GetParamCheck(CUltilities.thepVaiBo1Check);
                            cbThepVaiBo1DaiTangCuong.Checked = GetParamCheck(CUltilities.thepVaiBo1DaiCheck);
                            numThepVaiBoDau1DuongKinhDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo1DuongKinhDaiTru);
                            numThepVaiBoDau1KhoangRaiDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo1KhoangCachDaiTru);
                        }
                        cbThepVaiBoDau2.Checked = GetParamCheck(CUltilities.thepVaiBoDau2Check);
                        if (cbThepVaiBoDau2.Checked)
                        {
                            numThepVaiBoDau2B.Value = GetParamDouble(CUltilities.thepVaiBo2B);
                            numThepVaiBoDau2H.Value = GetParamDouble(CUltilities.thepVaiBo2H);
                            //cbThepVaiBoDau2VB.Checked = GetParamCheck(CUltilities.thepVaiBo2Check);
                            cbThepVaiBo2DaiTangCuong.Checked = GetParamCheck(CUltilities.thepVaiBo2DaiCheck);
                            numThepVaiBoDau2DuongKinhDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo2DuongKinhDaiTru);
                            numThepVaiBoDau2KhoangRaiDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo2KhoangCachDaiTru);
                        }
                        cbThepDaiC.Checked = true;
                        cbThepDaiCDungDon.Checked = GetParamCheck(CUltilities.thepDaiCDungDonCheck);
                        //numThepDaiCDungGiua.Value = GetParamDouble(CUltilities.thepDaiCDungGiua);
                        cbThepDaiCDungKep.Checked = GetParamCheck(CUltilities.thepDaiCDungKepCheck);
                        cbThepDaiCDungKepBen1.Checked = GetParamCheck(CUltilities.thepDaiCDungKepBen1);
                        cbThepDaiCDungKepBen2.Checked = GetParamCheck(CUltilities.thepDaiCDungKepBen2);
                        numThepDaiCDungKepN.Value = GetParamInt(CUltilities.thepDaiCDungKepN);
                        cbThepDaiCNgangTren.Checked = GetParamCheck(CUltilities.thepDaiCNgangTrenCheck);
                        numThepDaiCNgangTrenD.Value = GetParamDouble(CUltilities.thepDaiCNgangTrenD);
                        numThepDaiCNgangTrenR.Value = GetParamDouble(CUltilities.thepDaiCNgangTrenR);
                        cbThepDaiCNgangDuoi.Checked = GetParamCheck(CUltilities.thepDaiCNgangDuoiCheck);
                        numThepDaiCNgangDuoiD.Value = GetParamDouble(CUltilities.thepDaiCNgangDuoiD);
                        numThepDaiCNgangDuoiR.Value = GetParamDouble(CUltilities.thepDaiCNgangDuoiR);
                        if (cbThepDaiCDungDon.Checked == false &&
                            cbThepDaiCDungKep.Checked == false &&
                            cbThepDaiCNgangTren.Checked == false &&
                            cbThepDaiCNgangDuoi.Checked == false)
                            cbThepDaiC.Checked = false;


                        if (damLoaiMoi)
                        {

                            //numThepGiaCuongNhipDinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDinhViD2);
                        }

                        //}
                        //else
                        //{

                    }
                }
            }
            catch (Exception exx)
            {
                TaskDialog.Show("Update", exx.ToString());
                CUltilities.Logger("Lỗi update:" + exx.ToString());
            }
            this.ShowDialog();
        }


        /// <summary>
        /// Update chính
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="p_commandData"></param>
        /// <param name="p_message"></param>
        /// <param name="p_elements"></param>
        /// <param name="parameterSet"></param>
        /// <param name="dictLuuParam"></param>
        /// <param name="damMoi"></param>
        public void ShowMeUpdate(
        Document doc,
        ExternalCommandData p_commandData,
        ref string p_message,
        ElementSet p_elements,
        ParameterSet parameterSet,
        Dictionary<string, Parameter> dictLuuParam,
        bool damMoi)
        {
            this.doc = doc;
            m_commandData = p_commandData;
            m_elements = p_elements;
            m_message = p_message;
            ParameterSet = parameterSet;
            this.dictLuuParam = dictLuuParam;
            lengthDam = (double)GetParamDouble("Length");
            damLoaiMoi = damMoi;
            if (damLoaiMoi)
            {

                groupBox7.Size = new Size(new System.Drawing.Point(168, 104));
                panel4.Visible = true;
                panel6.Visible = true;
                panel8.Visible = true;

                cbAuToRangBuoc.Checked = false;
                cbAuToRangBuoc2.Checked = cbAuToRangBuoc.Checked;
                cbAutoRangBuocNhip.Checked = cbAuToRangBuoc.Checked;

                cbAuToRangBuoc.Visible = false;
                cbAuToRangBuoc2.Visible = cbAuToRangBuoc.Visible;
                cbAutoRangBuocNhip.Visible = cbAuToRangBuoc.Visible;


                pnGiaCuongDau1L.Location = new System.Drawing.Point(pnGiaCuongDau1L.Location.X, 77);
                pnGiaCuongDau2L.Location = new System.Drawing.Point(pnGiaCuongDau2L.Location.X, 77);
                pnGiaCuongGiuaL.Location = new System.Drawing.Point(pnGiaCuongGiuaL.Location.X, 77);

                pnGiaCuongDauDam1Lop1.Location = new System.Drawing.Point(pnGiaCuongDauDam1Lop1.Location.X, 89);
                pnGiaCuongDauDam2Lop1.Location = new System.Drawing.Point(pnGiaCuongDauDam2Lop1.Location.X, 89);
                pnGiaCuongGiuaDamLop1.Location = new System.Drawing.Point(pnGiaCuongGiuaDamLop1.Location.X, 89);


                pnDaiCNgangTren.Location = new System.Drawing.Point(12, 43);
                pnDaiCNgangDuoi.Location = new System.Drawing.Point(16, 127);

                pnDaiCDungKep.Size = new Size(new System.Drawing.Point(242, 187));
                cbThepDaiCDungKep.Text = "Thép đai C đứng thứ 1";

            }
            try
            {
                string loaiCauKien = dictLuuParam["LOAI CAU KIEN"].AsString();



                cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[0];
                cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[0];
                numBCotDau1.Value = (decimal)(dictLuuParam[CUltilities.bCotDau1].AsDouble() * 304.8);
                numBCotDau2.Value = (decimal)(dictLuuParam[CUltilities.bCotDau2].AsDouble() * 304.8);


                if (loaiCauKien.Length > 1)
                {
                    //update 
                    //if (MessageBox.Show("Giữ số liệu hiện có?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {



                        //Update parameter
                        //TaskDialog.Show("neo", dictLuuParam[CUltilities.goi1].AsInteger() + "");
                        int goi1 = (int)GetParamInt(CUltilities.goi1);
                        int goi2 = (int)GetParamInt(CUltilities.goi2);
                        double b1 = (double)GetParamDouble(CUltilities.bCotDau1);
                        double b2 = (double)GetParamDouble(CUltilities.bCotDau2);

                        if (b1 >= 150 && b2 >= 150)
                        {
                            cbDam2Goi.Checked = true;
                            if (goi1 == goi2)
                            {
                                if (goi1 == 1)
                                {
                                    rdNeoDauDam1Va2.Checked = true;
                                }
                                else
                                {
                                    rdKhongNeo.Checked = true;
                                }
                            }
                            else
                            {
                                if (goi1 == 1)
                                {
                                    rdNeoDauDam1.Checked = true;
                                }
                                else
                                {
                                    rdNeoDauDam2.Checked = true;
                                }
                            }
                        }
                        else
                        {
                            //console
                            cbDamConsole.Checked = true;
                            if (b1 == 0)
                            {
                                if (goi2 == 0)
                                    //console dau dam 1, neo dau dam 2
                                    rdConsoleDau1KhongNeo2.Checked = true;
                                else
                                    rdConsoleDau1Neo2.Checked = true;
                            }
                            else
                            {
                                if (b2 == 0)
                                    if (goi1 == 0)
                                        rdConsoleDau2KhongNeo2.Checked = true;
                                    else
                                        rdConsoleDau2Neo1.Checked = true;
                            }
                        }

                        numBCotDau1.Value = (decimal)b1;
                        numBCotDau2.Value = (decimal)b2;

                        string viTriDam = dictLuuParam[CUltilities.viTriDam].AsString();
                        string viTriGoi1 = dictLuuParam[CUltilities.viTriGoi1].AsString();
                        string viTriGoi2 = dictLuuParam[CUltilities.viTriGoi2].AsString();

                        if (viTriDam.Length > 2)
                            tbViTriDam.Text = viTriDam;
                        if (viTriGoi1.Length > 2)
                            tbViTriGoi1.Text = viTriGoi1;
                        if (viTriGoi2.Length > 2)
                            tbViTriGoi2.Text = viTriGoi2;



                        numThepChuTren1DuongKinh.Value = GetParamDouble(CUltilities.thepTren1DuongKinh);
                        numThepChuTren1SoLuong.Value = GetParamInt(CUltilities.thepTren1SoLuong);
                        numThepChuDuoi1DuongKinh.Value = GetParamDouble(CUltilities.thepDuoi1DuongKinh);
                        //TaskDialog.Show("Thep chu duoi", GetParamDouble(CUltilities.thepDuoi1DuongKinh).ToString());
                        numThepChuDuoi1SoLuong.Value = GetParamInt(CUltilities.thepDuoi1SoLuong);
                        cbThepTren2.Checked = GetParamCheck(CUltilities.thepTren2Check);
                        //if (cbThepTren2.Checked == false)
                        //{
                        numThepChuTren2DuongKinh.Visible = cbThepTren2.Checked;
                        numThepChuTren2SoLuong.Visible = cbThepTren2.Checked;
                        //}
                        //else
                        //{

                        //}
                        //numThepChuTren2Check.Value = GetParamDouble(CUltilities.thepTren2Check);
                        numThepChuTren2DuongKinh.Value = GetParamDouble(CUltilities.thepTren2DuongKinh);
                        numThepChuTren2SoLuong.Value = GetParamInt(CUltilities.thepTren2SoLuong);
                        cbThepDuoi2.Checked = GetParamCheck(CUltilities.thepDuoi2Check);
                        //if (cbThepDuoi2.Checked == false)
                        //{
                            numThepChuDuoi2DuongKinh.Visible = cbThepDuoi2.Checked;
                            numThepChuDuoi2SoLuong.Visible = cbThepDuoi2.Checked;
                        //}

                        //numThepChuDuoi2Check.Value = GetParamDouble(CUltilities.thepDuoi2Check);
                        numThepChuDuoi2DuongKinh.Value = GetParamDouble(CUltilities.thepDuoi2DuongKinh);
                        numThepChuDuoi2SoLuong.Value = GetParamInt(CUltilities.thepDuoi2SoLuong);



                        cbGiaCuongGoi1.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi1Check) || GetParamCheck(CUltilities.thepGiaCuongGoi1L2Check);
                        cbThepGiaCuongD1L1.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi1Check);

                        numThepGiaCuongGoi1DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1DuongKinh);
                        numThepGiaCuongGoi1SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi1SoLuong);

                        cbThepGiaCuongD1L2.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi1L2Check);
                        numThepGiaCuongGoi1L2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1L2DuongKinh);
                        numThepGiaCuongGoi1L2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi1L2SoLuong);


                        if (!damLoaiMoi)
                        {
                            numThepGiaCuongGoi1DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1DinhVi);
                            cbGiaCuongNhip.Checked = GetParamCheck(CUltilities.thepGiaCuongNhipCheck);
                            numThepGiaCuongNhipDuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDuongKinh);
                            numThepGiaCuongNhipL2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipL2DuongKinh);
                            numThepGiaCuongNhipL2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongNhipL2SoLuong);
                            numThepGiaCuongGoi2DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2DinhVi);

                            try
                            {
                                cbAutoRangBuocNhip.Checked = !GetParamCheck(CUltilities.thepGiaCuongNhipLCheck);
                                numThepGiaCuongNhipDinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDinhVi);
                            }
                            catch (Exception exx)
                            {
                                //MessageBox.Show("Không thể load biến " + numThepGiaCuongNhipDinhVi.Name + "\n" + exx.ToString());
                            }
                        }
                        else
                        {
                            numThepGiaCuongGoi1DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi1DinhViD2);

                            numThepGiaCuongNhipDuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDuongKinhD2);
                            numThepGiaCuongNhipL2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongNhipL2DuongKinhD2);
                            numThepGiaCuongNhipL2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongNhipL2SoLuongD2);
                            numThepGiaCuongGoi2DinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2DinhViD2);
                            numThepDaiGoi1DinhVi.Value = GetParamDouble(CUltilities.thepDaiGoi1DinhViD2);
                            numThepGiaCuongNhipDinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDinhViD2);
                            numThepDaiGoi2DinhVi.Value = numThepDaiGoi1DinhVi.Value;

                            cbGiaCuongNhip.Checked = GetParamCheck(CUltilities.thepGiaCuongNhipCheckD2) || GetParamCheck(CUltilities.thepGiaCuongNhipL2CheckD2);
                            cbThepGiaCuongGL1.Checked = GetParamCheck(CUltilities.thepGiaCuongNhipCheckD2);

                        }
                        //numThepGiaCuongNhipSoLuong.Value = GetParamDouble(CUltilities.thepGiaCuongNhipSoLuong);
                        //numThepGiaCuongNhipL2Check.Value = GetParamDouble(CUltilities.thepGiaCuongNhipL2Check);


                        cbGiaCuongGoi2.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi2Check) || GetParamCheck(CUltilities.thepGiaCuongGoi2L2Check);
                        cbThepGiaCuongD2L1.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi2Check);
                        numThepGiaCuongGoi2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2DuongKinh);
                        numThepGiaCuongGoi2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi2SoLuong);

                        cbThepGiaCuongD2L2.Checked = GetParamCheck(CUltilities.thepGiaCuongGoi2L2Check);
                        numThepGiaCuongGoi2L2DuongKinh.Value = GetParamDouble(CUltilities.thepGiaCuongGoi2L2DuongKinh);
                        numThepGiaCuongGoi2L2SoLuong.Value = GetParamInt(CUltilities.thepGiaCuongGoi2L2SoLuong);
                        //cbAuToRangBuoc.Checked = (GetParamCheck(CUltilities.thepGiaCuongAuTo)==true)
                        cbAuToRangBuoc.Checked = ((double)numThepGiaCuongGoi1DinhVi.Value == lengthDam / 4) ? true : false;
                        //cbAuToRangBuoc2.Checked = !GetParamCheck(CUltilities.thepGiaCuongAuTo);
                        cbAuToRangBuoc2.Checked = ((double)numThepGiaCuongGoi2DinhVi.Value == lengthDam / 4) ? true : false;

                        //numThepDaiCheck.Value = GetParamDouble(CUltilities.thepDaiCheck);
                        numThepDaiGoi1DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGoi1DuongKinh);
                        numThepDaiGoi1KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGoi1KhoangRai);
                        numThepDaiNhipDuongKinh.Value = GetParamDouble(CUltilities.thepDaiNhipDuongKinh);
                        numThepDaiNhipKhoangCach.Value = GetParamDouble(CUltilities.thepDaiNhipKhoangRai);
                        numThepDaiGoi2DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGoi2DuongKinh);
                        numThepDaiGoi2KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGoi2KhoangRai);
                        // numThepDaiGiaCuongNhanhTrenCheck.Value = GetParamDouble(CUltilities.thepDaiGiaCuongNhanhTrenCheck);
                        //numThepDaiGiaCuongDauDam1DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGiaCuongDauDam1DuongKinh);
                        //numThepDaiGiaCuongDauDam1KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGiaCuongDauDam1KhoangCach);
                        //numThepDaiGiaCuongNhanhDuoiCheck.Value = GetParamDouble(CUltilities.thepDaiGiaCuongNhanhDuoiCheck);
                        //numThepDaiGiaCuongDauDam2DuongKinh.Value = GetParamDouble(CUltilities.thepDaiGiaCuongDauDam2DuongKinh);
                        //numThepDaiGiaCuongDauDam2KhoangCach.Value = GetParamDouble(CUltilities.thepDaiGiaCuongDauDam2KhoangCach);
                        cbThepGia.Checked = GetParamCheck(CUltilities.thepGiaCheck);
                        if (cbThepGia.Checked)
                        {
                            numThepGiaDuongKinh.Value = GetParamDouble(CUltilities.thepGiaDuongKinh);
                        }
                        cbThepDaiGia.Checked = GetParamCheck(CUltilities.thepGiaDaiGiaCheck);
                        if (cbThepDaiGia.Checked)
                        {
                            numThepGiaDaiDuongKinh.Value = GetParamDouble(CUltilities.thepGiaDaiGiaDuongKinh);
                            numThepGiaDaiKhoangRai.Value = GetParamDouble(CUltilities.thepGiaDaiGiaKhoangCach);
                        }
                        cbThepGiaDiToiGoi1.Checked = GetParamCheck(CUltilities.thepGiaToiGoi1);
                        cbThepGiaDiToiGoi2.Checked = GetParamCheck(CUltilities.thepGiaToiGoi2);


                        cbThepDaiLong.Checked = GetParamCheck(CUltilities.thepDaiLongDonCheck);
                        numThepDaiLongSoLuong.Value = GetParamInt(CUltilities.thepDaiLongDonN);
                        cbDaiLongKep.Checked = GetParamCheck(CUltilities.thepDaiLongKepCheck);
                        cbDaiLongKepBen1.Checked = GetParamCheck(CUltilities.thepDaiLongKepBen1);
                        cbDaiLongKepBen2.Checked = GetParamCheck(CUltilities.thepDaiLongKepBen2);
                        numDaiLongKepN1.Value = GetParamInt(CUltilities.thepDaiLongKepN1);
                        numDaiLongKepN2.Value = GetParamInt(CUltilities.thepDaiLongKepN2);

                        cbThepVaiBoDau1.Checked = GetParamCheck(CUltilities.thepVaiBoDau1Check);
                        //MessageBox.Show(GetParamCheck(CUltilities.thepVaiBoDau1Check) + "");
                        if (cbThepVaiBoDau1.Checked)
                        {
                            numThepVaiBoDau1B.Value = GetParamDouble(CUltilities.thepVaiBo1B);
                            numThepVaiBoDau1H.Value = GetParamDouble(CUltilities.thepVaiBo1H);
                            //cbThepVaiBoDau1VB.Checked = GetParamCheck(CUltilities.thepVaiBo1Check);
                            cbThepVaiBo1DaiTangCuong.Checked = GetParamCheck(CUltilities.thepVaiBo1DaiCheck);
                            numThepVaiBoDau1DuongKinhDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo1DuongKinhDaiTru);
                            numThepVaiBoDau1KhoangRaiDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo1KhoangCachDaiTru);
                        }
                        cbThepVaiBoDau2.Checked = GetParamCheck(CUltilities.thepVaiBoDau2Check);
                        if (cbThepVaiBoDau2.Checked)
                        {
                            numThepVaiBoDau2B.Value = GetParamDouble(CUltilities.thepVaiBo2B);
                            numThepVaiBoDau2H.Value = GetParamDouble(CUltilities.thepVaiBo2H);
                            //cbThepVaiBoDau2VB.Checked = GetParamCheck(CUltilities.thepVaiBo2Check);
                            cbThepVaiBo2DaiTangCuong.Checked = GetParamCheck(CUltilities.thepVaiBo2DaiCheck);
                            numThepVaiBoDau2DuongKinhDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo2DuongKinhDaiTru);
                            numThepVaiBoDau2KhoangRaiDaiTru.Value = GetParamDouble(CUltilities.thepVaiBo2KhoangCachDaiTru);
                        }
                        cbThepDaiC.Checked = true;
                        cbThepDaiCDungDon.Checked = GetParamCheck(CUltilities.thepDaiCDungDonCheck);
                        //numThepDaiCDungGiua.Value = GetParamDouble(CUltilities.thepDaiCDungGiua);
                        cbThepDaiCDungKep.Checked = GetParamCheck(CUltilities.thepDaiCDungKepCheck);
                        cbThepDaiCDungKepBen1.Checked = GetParamCheck(CUltilities.thepDaiCDungKepBen1);
                        cbThepDaiCDungKepBen2.Checked = GetParamCheck(CUltilities.thepDaiCDungKepBen2);
                        numThepDaiCDungKepN.Value = GetParamInt(CUltilities.thepDaiCDungKepN);
                        
                        ///30/01/2019
                        //cbThepDaiCNgangTren.Checked = GetParamCheck(CUltilities.thepDaiCNgangTrenCheck);
                        //numThepDaiCNgangTrenD.Value = GetParamDouble(CUltilities.thepDaiCNgangTrenD);
                        //numThepDaiCNgangTrenR.Value = GetParamDouble(CUltilities.thepDaiCNgangTrenR);
                        //cbThepDaiCNgangDuoi.Checked = GetParamCheck(CUltilities.thepDaiCNgangDuoiCheck);
                        //numThepDaiCNgangDuoiD.Value = GetParamDouble(CUltilities.thepDaiCNgangDuoiD);
                        //numThepDaiCNgangDuoiR.Value = GetParamDouble(CUltilities.thepDaiCNgangDuoiR);
                        ////30/01/2019

                        if (cbThepDaiCDungDon.Checked == false &&
                            cbThepDaiCDungKep.Checked == false &&
                            cbThepDaiCNgangTren.Checked == false &&
                            cbThepDaiCNgangDuoi.Checked == false)
                            cbThepDaiC.Checked = false;


                        if (damLoaiMoi)
                        {

                            //numThepGiaCuongNhipDinhVi.Value = GetParamDouble(CUltilities.thepGiaCuongNhipDinhViD2);
                        }

                        //}
                        //else
                        //{

                    }
                }
            }
            catch (Exception exx)
            {
                TaskDialog.Show("Update", exx.ToString());
                CUltilities.Logger("Lỗi update:" + exx.ToString());
            }
            this.ShowDialog();
        }

        public decimal GetParamDouble(string str)
        {
            return (decimal)(dictLuuParam[str].AsDouble() * 304.8d);

        }
        public bool GetParamCheck(string str)
        {
            int temp = (dictLuuParam[str].AsInteger());
            if (temp == 0) return false;
            else return true;
        }
        public decimal GetParamInt(string str)
        {
            return (decimal)(dictLuuParam[str].AsInteger());
        }

        private void cbbNhomDoiTuongDam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbNhomDoiTuongDam.SelectedIndex == -1)
            {
                //flowLayoutPanel1.Enabled = false;
                return;
            }
            else
            {
                tabControl2.Enabled = true;
            }
            switch (this.cbbNhomDoiTuongDam.SelectedIndex)
            {
                case 0:
                    if (!cbbLoaiDam.Enabled)
                        cbbLoaiDam.Enabled = true;
                    cbbLoaiDam.DataSource = lstTypeA;
                    break;
                case 1:
                    if (!cbbLoaiDam.Enabled)
                        cbbLoaiDam.Enabled = true;
                    cbbLoaiDam.DataSource = lstTypeB;
                    break;
                case 2:
                    if (!cbbLoaiDam.Enabled)
                        cbbLoaiDam.Enabled = true;
                    cbbLoaiDam.DataSource = lstTypeC;
                    break;
                case 3:
                    if (!cbbLoaiDam.Enabled)
                        cbbLoaiDam.Enabled = true;
                    cbbLoaiDam.DataSource = lstTypeD;
                    break;
                case 4:
                    if (!cbbLoaiDam.Enabled)
                        cbbLoaiDam.Enabled = true;
                    cbbLoaiDam.DataSource = lstTypeE;
                    break;
                case 5:
                    if (!cbbLoaiDam.Enabled)
                        cbbLoaiDam.Enabled = true;
                    cbbLoaiDam.DataSource = lstTypeF;
                    break;


                default:
                    if (cbbLoaiDam.Enabled)
                        cbbLoaiDam.Enabled = false;
                    break;

            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstCheck0 = new Dictionary<NumericUpDown, TabPage>();
            tabControl2.SelectedIndex = 0;
            tpNeoThep.BackColor = Color.White;
            tpThepChu.BackColor = Color.White;
            try
            {
                switch (cbbLoaiDam.SelectedValue)
                {
                    case "A1":
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.A1;
                        tbMoTa.Text = "-Thép chủ 1 lớp.";
                        ShowThepGia(false);
                        grThepDaiLong.Visible = false;
                        gbDaiLongKep.Visible = false; cbDaiLongKep.Visible = false;
                        ShowThepGiaCuong(false);
                        grThepDaiGiaCuong.Visible = false;
                        cbThepChu1Lop.Checked = true;
                        cbThepDaiC.Checked = false;
                        break;
                    case "A2":
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.A2;
                        tbMoTa.Text = "-Thép chủ 1 lớp.\n-Thép gia cường.";
                        ShowThepGia(false);
                        grThepDaiLong.Visible = false; gbDaiLongKep.Visible = false; cbDaiLongKep.Visible = false;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;

                        cbThepChu1Lop.Checked = true;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        cbThepDaiC.Checked = false;
                        break;
                    case "A3":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.A3;
                        tbMoTa.Text = "-Thép chủ 2 lớp.";
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        ShowThepGia(false);
                        grThepDaiLong.Visible = false; gbDaiLongKep.Visible = false; cbDaiLongKep.Visible = false;
                        ShowThepGiaCuong(false);
                        grThepDaiGiaCuong.Visible = false;

                        cbThepChu1Lop.Checked = false;
                        cbThepDaiC.Checked = false;
                        break;
                    case "A4":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.A4;
                        tbMoTa.Text = "-Thép chủ 2 lớp.\n-Thép gia cường.";
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());

                        ShowThepGia(false);
                        grThepDaiLong.Visible = false; gbDaiLongKep.Visible = false; cbDaiLongKep.Visible = false;

                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;

                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        cbThepDaiC.Checked = false;
                        break;
                    case "B1":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.B1;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Thép chủ 1 lớp.\n-Thép giá.";

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = false;
                        gbDaiLongKep.Visible = false; cbDaiLongKep.Visible = false;

                        ShowThepGiaCuong(false);
                        grThepDaiGiaCuong.Visible = false;

                        cbThepChu1Lop.Checked = true;
                        cbThepDaiC.Checked = false;
                        break;
                    case "B2":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.B2;
                        tbMoTa.Text = "-Thép chủ 1 lớp.\n-Thép gia cường.\n-Thép giá.";
                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = false;
                        gbDaiLongKep.Visible = false;
                        cbDaiLongKep.Visible = false;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = true;
                        cbThepDaiC.Checked = false;
                        break;
                    case "B3":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.B3;
                        tbMoTa.Text = "-Thép chủ 2 lớp.\n-Thép giá.";
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = false; gbDaiLongKep.Visible = false; cbDaiLongKep.Visible = false;
                        ShowThepGiaCuong(false);
                        grThepDaiGiaCuong.Visible = false;
                        cbThepChu1Lop.Checked = false;
                        cbThepDaiC.Checked = false;
                        break;
                    case "B4":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.B4;
                        tbMoTa.Text = "-Thép chủ 2 lớp.\n-Thép gia cường.\n-Thép giá.";
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = false; gbDaiLongKep.Visible = false; cbDaiLongKep.Visible = false;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        cbThepDaiC.Checked = false;
                        break;
                    case "C1":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.C1;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Thép chủ 1 lớp.\n-Thép gia cường.\n-Đai lồng.";

                        ShowThepGia(false);
                        grThepDaiLong.Visible = true;
                        gbDaiLongKep.Visible = true;
                        cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "C2":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.C2;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Thép chủ 1 lớp.\n-Thép gia cường.\n-Đai lồng.";

                        ShowThepGia(false);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = true;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "C3":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.C3;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Thép chủ 2 lớp.\n-Đai lồng.";

                        ShowThepGia(false);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;

                        ShowThepGiaCuong(false);
                        grThepDaiGiaCuong.Visible = false;
                        cbThepChu1Lop.Checked = false;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "C4":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.C4;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Thép chủ 2 lớp.\n-Thép gia cường.\n-Đai lồng.";

                        ShowThepGia(false);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;

                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        cbThepChu1Lop.Checked = false;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "D1":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D1;
                        tbMoTa.Text = "-Thép chủ 1 lớp.\n-Thép gia cường.\n-Thép giá.\n-Đai lồng.";
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true;
                        gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = true;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "D2":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D2;
                        tbMoTa.Text = "-Thép chủ 2 lớp.\n-Thép giá.\n-Đai lồng.";
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;

                        ShowThepGiaCuong(false);
                        cbThepChu1Lop.Checked = false;
                        cbThepGia.Checked = true;
                        cbThepDaiGia.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "D3":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D3;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Thép chủ 2 lớp.\n-Thép gia cường.\n-Thép giá.\n-Đai lồng.";

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "E1":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D3;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Dầm giật cấp.";

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "E2":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D3;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Dầm to hơn cột tiếp xúc.";
                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "E3":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D3;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Dầm chữ Z";

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "E4":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D3;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Dầm đỡ cột cấy.";

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "E5":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D3;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Dầm dạng vách.";

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "E6":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D3;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Dầm chuyển.";

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                    case "F1":
                        pictureBox1.Image = GiaoDienDam.Properties.Resources.D3;
                        //MessageBox.Show(comboBox1.SelectedValue.ToString());
                        tbMoTa.Text = "-Gờ bê tông.";

                        ShowThepGia(true, 3);
                        grThepDaiLong.Visible = true; gbDaiLongKep.Visible = true; cbDaiLongKep.Visible = true;
                        ShowThepGiaCuong(true);
                        grThepDaiGiaCuong.Visible = true;
                        cbThepChu1Lop.Checked = false;
                        cbThepGiaCuongDaiGoi1.Checked = true;
                        cbThepGiaCuongDaiNhip.Checked = true;
                        cbThepGiaCuongDaiGoi2.Checked = true;
                        //cbThepDaiLong.Checked = true;
                        cbThepDaiC.Checked = true;
                        break;
                }
                //if (!grThepGia.Visible) tabControl2.TabPages.Remove(tpThepGia);
                //else if (tabControl2.TabPages.Contains(tpThepGia)) { tabControl2.SuspendLayout(); tabControl2.TabPages.Add(tpThepGia); tabControl2.ResumeLayout(); }

                //if (!grThepGia.Visible)
                //{
                //    tabControl2.SuspendLayout();
                //    tabControl2.TabPages[3].Text = "";
                //    tabControl2.TabPages[3].
                //    tabControl2.TabPages[3].Size = new Size(0, 0);
                //    tabControl2.ResumeLayout();
                //}
                //else
                //{
                //    tabControl2.SuspendLayout();
                //    tabControl2.TabPages[3].Text = "THÉP GIÁ";
                //    tabControl2.TabPages[3].Size = tabControl2.TabPages[1].Size;
                //    tabControl2.ResumeLayout();
                //}
            }
            catch (Exception exx)
            {
                //TaskDialog.Show("ERROR", exx.ToString());
                m_message = "Lỗi ở bước khởi tạo.";
            }
            finally
            {
                GRThepDaiC(cbThepDaiC.Checked);
                GRThepDaiKep(grThepDaiLong.Visible);

            }

        }


        private void cbThepChu1Lop_CheckedChanged(object sender, EventArgs e)
        {
            if (cbThepChu1Lop.Checked)
            {
                cbThepChu2Lop.Checked = false;
                gbThepTren2.Visible = false;
                gbThepDuoi2.Visible = false;
                cbThepTren2.Visible = false;
                cbThepDuoi2.Visible = false;
                gbThepChu.Size = new Size(180, 200);
                pnThepChu.Size = new Size(170, 178);
                //pnKhoangCachThepChu.Size = new Size(180, 72);
            }
            else
            {
                cbThepChu2Lop.Checked = true;
                gbThepTren2.Visible = true;
                gbThepDuoi2.Visible = true;
                cbThepTren2.Visible = true;
                cbThepDuoi2.Visible = true;
                gbThepChu.Size = new Size(600, 200);
                pnThepChu.Size = new Size(590, 178);
            }
        }

        private void cbThepChu2Lop_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbThepChu2Lop.Checked)
            //{
            //    cbThepChu1Lop.Checked = false;
            //    //gbThepTren2.Visible = true;
            //    //gbThepDuoi2.Visible = true;
            //    //cbThepTren2.Checked = true;
            //    //cbThepDuoi2.Checked = true;
            //    //cbThepTren2.Visible = true;
            //    //cbThepDuoi2.Visible = true;
            //}
            //else
            //{
            //    cbThepChu1Lop.Checked = true;
            //    //gbThepTren2.Visible = false;
            //    //gbThepDuoi2.Visible = false;
            //    //cbThepTren2.Visible = false;
            //    //cbThepDuoi2.Checked = false;
            //    //cbThepDuoi2.Visible = false;
            //}
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            tbMoTaNgan.Text = "-Gối 1,2: Tích chọn là gối kết thúc, bỏ chọn là gối liên tục.\n-Console: tích chọn cho những đầu dầm là console.";
        }

        private void cbThepChu1Lop_MouseHover(object sender, EventArgs e)
        {
            tbMoTaNgan.Text = "Trường hợp dầm có 1 lớp thép chủ.";

        }

        private void cbThepChu2Lop_MouseHover(object sender, EventArgs e)
        {
            tbMoTaNgan.Text = "Trường hợp dầm có 2 lớp thép chủ.\n-Thép trên: thép chủ lớp trên cùng.\n-Thép trên 2: thép chủ lớp thứ 2.\n-Thép dứoi: thép chủ lớp dưới cùng.\n-Thép dưới 2: thép chủ lớp dưới thứ 2 (tính từ dứoi lên).";
        }

        private void groupBox2_MouseHover(object sender, EventArgs e)
        {
            if (cbThepChu1Lop.Checked) tbMoTaNgan.Text = "Trường hợp dầm có 1 lớp thép chủ.";
            if (cbThepChu2Lop.Checked) tbMoTaNgan.Text = "Trường hợp dầm có 2 lớp thép chủ.\n-Thép trên: thép chủ lớp trên cùng.\n-Thép trên 2: thép chủ lớp thứ 2.\n-Thép dứoi: thép chủ lớp dưới cùng.\n-Thép dưới 2: thép chủ lớp dưới thứ 2 (tính từ dứoi lên).";

        }

        private void tbMoTaNgan_TextChanged(object sender, EventArgs e)
        {

        }

        private void grThepGiaCuong_MouseHover(object sender, EventArgs e)
        {
            tbMoTaNgan.Text = "Trường hợp dầm có 2 lớp thép chủ.\n-Thép trên: thép chủ lớp trên cùng.\n-Thép trên 2: thép chủ lớp thứ 2.\n-Thép dứoi: thép chủ lớp dưới cùng.\n-Thép dưới 2: thép chủ lớp dưới thứ 2 (tính từ dứoi lên).";

        }

        private void cbThepGiaCuongDaiGoi1_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbThepGiaCuongDaiGoi1.Checked)
            {
                gbThepDaiGiaCuongDauDam1.Enabled = false;
                //cbThepGiaCuongDaiGoi1.Text = "Thép đai gia cường gối 1";
                //numThepGiaCuongDaiGoi1DuongKinh.Visible = false;
                //numThepGiaCuongDaiGoi1RaiThep.Visible = false;
                //lblGiaCuongRaiThep1.Visible = false;
            }
            else
            {
                gbThepDaiGiaCuongDauDam1.Enabled = true;
                //cbThepGiaCuongDaiGoi1.Text = "Đai : Đường kính";
                //numThepGiaCuongDaiGoi1DuongKinh.Visible = true;
                //numThepGiaCuongDaiGoi1RaiThep.Visible = true;
                //lblGiaCuongRaiThep1.Visible = true;
            }

        }

        private void cbThepGiaCuongDaiNhip_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbThepGiaCuongDaiNhip.Checked)
            {
                gbThepDaiGiaCuongGiuaDam.Enabled = false;
                //    cbThepGiaCuongDaiNhip.Text = "Thép đai gia cường nhịp";
                //    numThepGiaCuongDaiNhipDuongKinh.Visible = false;
                //    numThepGiaCuongDaiNhipRaiThep.Visible = false;
                //    lblGiaCuongRaiThepNhip.Visible = false;
            }
            else
            {
                gbThepDaiGiaCuongGiuaDam.Enabled = true;
                //    cbThepGiaCuongDaiNhip.Text = "Đai : Đường kính";
                //    numThepGiaCuongDaiNhipDuongKinh.Visible = true;
                //    numThepGiaCuongDaiNhipRaiThep.Visible = true;
                //    lblGiaCuongRaiThepNhip.Visible = true;
            }
        }

        private void cbThepGiaCuongDaiGoi2_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbThepGiaCuongDaiGoi2.Checked)
            {
                gbThepDaiGiaCuongDauDam2.Enabled = false;
                //    cbThepGiaCuongDaiGoi2.Text = "Thép đai gia cường gối 2";
                //    numThepGiaCuongDaiGoi2DuongKinh.Visible = false;
                //    numThepGiaCuongDaiGoi2RaiThep.Visible = false;
                //    lblGiaCuongRaiThep2.Visible = false;
            }
            else
            {
                gbThepDaiGiaCuongDauDam2.Enabled = true;
                //    cbThepGiaCuongDaiGoi2.Text = "Đai : Đường kính";
                //    numThepGiaCuongDaiGoi2DuongKinh.Visible = true;
                //    numThepGiaCuongDaiGoi2RaiThep.Visible = true;
                //    lblGiaCuongRaiThep2.Visible = true;
            }
        }

        private void cbGiaCuongGoi1_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox cb = sender as CheckBox;
            gbThepGiaCuongDauDam1.Enabled = cb.Checked;
            gbThepGiaCuongDauDam1.Visible = cb.Checked;

            //if (!cbGiaCuongGoi1.Checked)
            //{
            //    gbThepGiaCuongDauDam1.Enabled = false;
            //    //cbGiaCuongGoi1.Text = "Thép gia cường gối 1";
            //    //numThepGiaCuongGoi1DuongKinh.Visible = false;
            //    //numThepGiaCuongGoi1SoLuong.Visible = false;
            //    //lblGiaCuongGoi1SoLuongThep.Visible = false;
            //}
            //else
            //{
            //    gbThepGiaCuongDauDam1.Enabled = true;
            //    //cbGiaCuongGoi1.Text = "Gối 1: Đường kính";
            //    //numThepGiaCuongGoi1DuongKinh.Visible = true;
            //    //numThepGiaCuongGoi1SoLuong.Visible = true;
            //    //lblGiaCuongGoi1SoLuongThep.Visible = true;
            //}
        }

        private void cbGiaCuongNhip_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            gbThepGiaCuongGiuaDam.Enabled = cb.Checked;
            gbThepGiaCuongGiuaDam.Visible = cb.Checked;

        }

        private void cbGiaCuongGoi2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            gbThepGiaCuongDauDam2.Enabled = cb.Checked;
            gbThepGiaCuongDauDam2.Visible = cb.Checked;

            //if (!cbGiaCuongGoi2.Checked)
            //{
            //    gbThepGiaCuongDauDam2.Enabled = false;
            //    //    cbGiaCuongGoi2.Text = "Thép gia cường gối 2";
            //    //    numThepGiaCuongGoi2DuongKinh.Visible = false;
            //    //    numThepGiaCuongGoi2SoLuong.Visible = false;
            //    //    lblGiaCuongGoi2SoLuongThep.Visible = false;
            //}
            //else
            //{
            //    gbThepGiaCuongDauDam2.Enabled = true;
            //    //    cbGiaCuongGoi2.Text = "Gối 2: Đường kính";
            //    //    numThepGiaCuongGoi2DuongKinh.Visible = true;
            //    //    numThepGiaCuongGoi2SoLuong.Visible = true;
            //    //    lblGiaCuongGoi2SoLuongThep.Visible = true;
            //}
        }
        private void cbThepDaiLong_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbThepDaiLong.Checked)
            {
                gbThepDaiLong.Enabled = false;
            }
            else
            {
                gbThepDaiLong.Enabled = true;
            }
        }
        private void cbThepDaiC_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbThepDaiC.Checked)
            {
                gbThepDaiC.Enabled = false;
            }
            else
            {
                gbThepDaiC.Enabled = true;
            }
        }

        private void cbThepGia_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbThepGia.Checked)
            {
                numThepGiaDuongKinh.Visible = false;
                lblThepGiaDuongKinh.Visible = false;
            }
            else
            {
                numThepGiaDuongKinh.Visible = true;
                lblThepGiaDuongKinh.Visible = true;
            }
        }

        private void cbThepDaiGia_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbThepDaiGia.Checked)
            {
                numThepGiaDaiDuongKinh.Visible = false;
                numThepGiaDaiKhoangRai.Visible = false;
                lblThepGiaDaiKhoangRai.Visible = false;
                lblThepGiaDaiDuongKinh.Visible = false;
            }
            else
            {
                numThepGiaDaiDuongKinh.Visible = true;
                lblThepGiaDaiDuongKinh.Visible = true;
                numThepGiaDaiKhoangRai.Visible = true;
                lblThepGiaDaiKhoangRai.Visible = true;
            }
        }

        private void cbThepTren2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            gbThepTren2.Enabled = cb.Checked;
            gbThepTren2.Visible = cb.Checked;

        }

        private void cbThepDuoi2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            gbThepDuoi2.Enabled = cb.Checked;
            gbThepDuoi2.Visible = cb.Checked;
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbDam2Goi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDam2Goi.Checked)
            {
                gbDam2Goi.Enabled = true;
                gbDamConsole.Enabled = false;
                cbDamConsole.Checked = false;

            }
            else
            {
                cbDamConsole.Checked = true;
                cbDam2Goi.Checked = false;
                gbDam2Goi.Enabled = false;
                gbDamConsole.Enabled = true;

            }
        }

        private void cbDamConsole_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDamConsole.Checked)
            {
                gbDamConsole.Enabled = true;
                gbDam2Goi.Enabled = false;
                cbDam2Goi.Checked = false;
            }
            else
            {
                cbDam2Goi.Checked = true;
                cbDamConsole.Checked = false;
                gbDamConsole.Enabled = false;
                gbDam2Goi.Enabled = true;
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                gbDam2Goi.Enabled = true;
                gbDamConsole.Enabled = false;
                cbDamConsole.Checked = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (rd.Checked)
            {
                rd.Font = new Font(rd.Font.Name, rd.Font.Size, FontStyle.Bold);
                if (cbDamConsole.Checked)
                {
                    switch (rd.Name)
                    {
                        case "rdConsoleDau1Neo2":
                            numBCotDau1.Minimum = 0;
                            numBCotDau1.Value = 0;
                            numBCotDau2.Minimum = numBCotDau;
                            numBCotDau1.ReadOnly = true;
                            numBCotDau2.ReadOnly = false;
                            neoTheped = true;
                            break;
                        case "rdConsoleDau1KhongNeo2":
                            numBCotDau1.Minimum = 0;
                            numBCotDau1.Value = 0;
                            numBCotDau2.Minimum = numBCotDau;
                            numBCotDau1.ReadOnly = true;
                            numBCotDau2.ReadOnly = false;
                            neoTheped = true;
                            break;
                        case "rdConsoleDau2Neo1":
                            numBCotDau2.Minimum = 0;
                            numBCotDau2.Value = 0;
                            numBCotDau1.Minimum = numBCotDau;
                            numBCotDau1.ReadOnly = false;
                            numBCotDau2.ReadOnly = true;
                            neoTheped = true;
                            break;
                        case "rdConsoleDau2KhongNeo2":// khongneo1
                            numBCotDau2.Minimum = 0;
                            numBCotDau2.Value = 0;
                            numBCotDau1.Minimum = numBCotDau;
                            numBCotDau1.ReadOnly = false;
                            numBCotDau2.ReadOnly = true;
                            neoTheped = true;
                            break;
                    }
                }
                else
                {
                    switch (rd.Name)
                    {
                        case "rdNeoDauDam1":
                        case "rdNeoDauDam2":
                        case "rdNeoDauDam1Va2":
                        case "rdKhongNeo":
                            neoTheped = true;
                            break;
                    }

                    if (numBCotDau1.Minimum < numBCotDau) numBCotDau1.Minimum = numBCotDau;
                    if (numBCotDau2.Minimum < numBCotDau) numBCotDau2.Minimum = numBCotDau;
                    numBCotDau1.ReadOnly = false;
                    numBCotDau2.ReadOnly = false;
                }
            }
            else
            {
                rd.Font = new Font(rd.Font.Name, rd.Font.Size, FontStyle.Regular);
            }
        }
        private void rdGiaCuong2Lop_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox rd = sender as CheckBox;
            switch (rd.Name)
            {
                case "cbThepGiaCuongD1L1":
                    pnGiaCuongDauDam1Lop1.Visible = rd.Checked;
                    pnGiaCuongDau1L.Visible = (cbThepGiaCuongD1L1.Checked || cbThepGiaCuongD1L2.Checked) ? true : false;
                    pnGiaCuongDau1L.Enabled = (cbThepGiaCuongD1L1.Checked || cbThepGiaCuongD1L2.Checked);
                    //pnGiaCuongDau1L.Visible = rd.Checked;
                    //pnGiaCuongDau1L.Enabled = rd.Checked;
                    break;
                case "cbThepGiaCuongD1L2":
                    pnGiaCuongDauDam1Lop2.Visible = rd.Checked;
                    pnGiaCuongDau1L.Visible = (cbThepGiaCuongD1L1.Checked || cbThepGiaCuongD1L2.Checked) ? true : false;
                    pnGiaCuongDau1L.Enabled = (cbThepGiaCuongD1L1.Checked || cbThepGiaCuongD1L2.Checked);
                    //pnGiaCuongDau1L.Visible = rd.Checked;
                    //pnGiaCuongDau1L.Enabled = rd.Checked;
                    //pnGiaCuongDau1L2.Visible = rd.Checked;
                    //pnGiaCuongDau1L2.Enabled = rd.Checked;

                    break;
                case "cbThepGiaCuongGL1":
                    pnGiaCuongGiuaDamLop1.Visible = rd.Checked;
                    pnGiaCuongGiuaL.Visible = (cbThepGiaCuongGL1.Checked || cbThepGiaCuongGL2.Checked) ? true : false;
                    pnGiaCuongGiuaL.Enabled = (cbThepGiaCuongGL1.Checked || cbThepGiaCuongGL2.Checked);
                    break;
                case "cbThepGiaCuongGL2":
                    pnGiaCuongGiuaDamLop2.Visible = rd.Checked;
                    pnGiaCuongGiuaL.Visible = (cbThepGiaCuongGL1.Checked || cbThepGiaCuongGL2.Checked) ? true : false;
                    pnGiaCuongGiuaL.Enabled = (cbThepGiaCuongGL1.Checked || cbThepGiaCuongGL2.Checked);
                    break;
                case "cbThepGiaCuongD2L1":
                    pnGiaCuongDauDam2Lop1.Visible = rd.Checked;
                    pnGiaCuongDau2L.Visible = (cbThepGiaCuongD2L1.Checked || cbThepGiaCuongD2L2.Checked) ? true : false;
                    pnGiaCuongDau2L.Enabled = (cbThepGiaCuongD2L1.Checked || cbThepGiaCuongD2L2.Checked);

                    //pnGiaCuongDau2L.Visible = rd.Checked;
                    //pnGiaCuongDau2L.Enabled = rd.Checked;
                    break;
                case "cbThepGiaCuongD2L2":
                    pnGiaCuongDauDam2Lop2.Visible = rd.Checked;
                    pnGiaCuongDau2L.Visible = (cbThepGiaCuongD2L1.Checked || cbThepGiaCuongD2L2.Checked) ? true : false;
                    pnGiaCuongDau2L.Enabled = (cbThepGiaCuongD2L1.Checked || cbThepGiaCuongD2L2.Checked);

                    //pnGiaCuongDau2L.Visible = rd.Checked;
                    //pnGiaCuongDau2L.Enabled = rd.Checked;

                    //pnGiaCuongDau2L2.Visible = rd.Checked;
                    //pnGiaCuongDau2L2.Enabled = rd.Checked;
                    break;
            }

        }
        private void frm1_Load(object sender, EventArgs e)
        {
            string loaiCauKien = dictLuuParam["LOAI CAU KIEN"].AsString();

            switch (loaiCauKien[0].ToString())
            {
                case "A":
                    cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[0];
                    break;
                case "B":
                    cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[1];
                    break;
                case "C":
                    cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[2];
                    break;
                case "D":
                    cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[3];
                    break;
                case "E":
                    cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[4];
                    break;
                case "F":
                    cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[5];
                    break;
                default: break;
            }

            switch (loaiCauKien[1].ToString())
            {
                case "1":
                    cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[0];
                    break;
                case "2":
                    cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[1];
                    break;
                case "3":
                    cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[2];
                    break;
                case "4":
                    cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[3];
                    break;
                case "5":
                    cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[4];
                    break;
                case "6":
                    cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[5];
                    break;
                default:
                    break;
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit dialog!", "Exit?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                this.Close();
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        public IEnumerable<System.Windows.Forms.Control> GetAll(System.Windows.Forms.Control control, Type type)
        {
            var controls = control.Controls.Cast<System.Windows.Forms.Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> dictCacGiaTriThepCanDoi = new Dictionary<string, object>();
            bool check = CheckListNumDifferent0(lstCheck0) && neoTheped;

            if (!check)
            {
                if (!neoTheped)
                    if (MessageBox.Show("Chưa khai báo neo thép đầu dầm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                        tabControl2.SelectedTab = tpNeoThep;
                return;
            }
            else
                try
                {
                    //Xu Ly Goi 1, Goi 2
                    //if (tabControl2.TabPages.Contains(tpNeoThep))
                    CommandChoGoi1Goi2(dictCacGiaTriThepCanDoi);
                    //Xu ly Thep Chu    
                    //if (tabControl2.TabPages.Contains(tpThepChu))
                    CommandChoThepChu(dictCacGiaTriThepCanDoi);
                    //Xu ly Thep gia cuong
                    CommandChoThepGiaCuong(dictCacGiaTriThepCanDoi);
                    //Xu Ly Thep Dai
                    CommandChoThepDai(dictCacGiaTriThepCanDoi);
                    //Xu Ly Thep Dai Gia Cuong
                    //CommandChoThepDaiGiaCuong(dictCacGiaTriThepCanDoi);
                    //Xu Ly Thep Dai Long
                    CommandChoThepDaiLong(dictCacGiaTriThepCanDoi);
                    //Xu Ly Thep Dai C
                    CommandChoThepDaiC(dictCacGiaTriThepCanDoi);
                    //Xu Ly Thep Gia
                    CommandChoThepGia(dictCacGiaTriThepCanDoi);
                    //Xu Ly Thep Vai Bo
                    CommandChoThepVaiBo(dictCacGiaTriThepCanDoi);

                    dictCacGiaTriThepCanDoi.Add("LOAI CAU KIEN", cbbLoaiDam.Text);
                    var command = new Command2
                    { parameterSet = this.ParameterSet, dictNameInst = dictCacGiaTriThepCanDoi, dictLuuParam = this.dictLuuParam, selectedIds = m_elementIds };
                    command.Execute(m_commandData, ref m_message, m_elements);

                    this.Close();
                }
                catch (Exception exx)
                {
                    TaskDialog.Show("Error", "Lỗi: " + exx.ToString());
                    CUltilities.Logger("Lỗi OK: " + exx.ToString());
                }
        }

        private void CommandChoThepDaiC(Dictionary<string, object> dictCacGiaTriThepCanDoi)
        {
            {
                if (cbThepDaiC.Checked)
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungDonCheck, cbThepDaiCDungDon.Checked);


                    if (cbThepDaiCDungKep.Checked)
                    {
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepCheck, true);
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepBen1, cbThepDaiCDungKepBen1.Checked);
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepBen2, cbThepDaiCDungKepBen2.Checked);
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepN, numThepDaiCDungKepN);
                    }
                    else
                    {
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepCheck, false);
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepBen1, false);
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepBen2, false);
                    }
                    if (damLoaiMoi)
                    {
                        if (cbThepDaiCDungKep2.Checked)
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKep2Check, true);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKep2Ben1, cbThepDaiCDungKep2Ben1.Checked);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKep2Ben2, cbThepDaiCDungKep2Ben2.Checked);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKep2N, numThepDaiCDungKep2N);
                        }
                        else
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKep2Check, false);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKep2Ben1, false);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKep2Ben2, false);
                        }
                    }



                    //30/01/2019

                    //if (cbThepDaiCNgangTren.Checked)
                    //{
                    //    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangTrenCheck, true);
                    //    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangTrenD, numThepDaiCNgangTrenD);
                    //    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangTrenR, numThepDaiCNgangTrenR);
                    //}
                    //else
                    //{
                    //    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangTrenCheck, false);
                    //}
                    //if (cbThepDaiCNgangDuoi.Checked)
                    //{
                    //    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangDuoiCheck, true);
                    //    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangDuoiD, numThepDaiCNgangDuoiD);
                    //    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangDuoiR, numThepDaiCNgangDuoiR);
                    //}
                    //else
                    //{
                    //    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangDuoiCheck, false);
                    //}

                    //30/01/2019
                }
                else
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungDonCheck, false);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepCheck, false);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepBen1, false);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCDungKepBen2, false);
                    //30/01/2019
                    //AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangTrenCheck, false);
                    //AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCNgangDuoiCheck, false);
                    //30/01/2019
                }
            }

        }

        private void CommandChoThepVaiBo(Dictionary<string, object> dictCacGiaTriThepCanDoi)
        {

            if (tabControl2.TabPages.Contains(tpThepVaiBo))
            {
                if (cbThepVaiBoDau1.Checked)
                {
                    if (cbThepVaiBoDau1VB.Checked)
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBoDau1Check, 1);
                    else
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBoDau1Check, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo1B, numThepVaiBoDau1B.Value);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo1H, numThepVaiBoDau1H.Value);
                    if (cbThepVaiBo1DaiTangCuong.Checked)
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo1DaiCheck, 1);
                    else
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo1DaiCheck, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo1DuongKinhDaiTru, numThepVaiBoDau1DuongKinhDaiTru.Value);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo1KhoangCachDaiTru, numThepVaiBoDau1KhoangRaiDaiTru.Value);
                }
                else
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBoDau1Check, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo1DaiCheck, 0);
                }
                if (cbThepVaiBoDau2.Checked)
                {
                    if (cbThepVaiBoDau2VB.Checked)
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBoDau2Check, 1);
                    else
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBoDau2Check, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo2B, numThepVaiBoDau2B.Value);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo2H, numThepVaiBoDau2H.Value);
                    if (cbThepVaiBo2DaiTangCuong.Checked)
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo2DaiCheck, 1);
                    else
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo2DaiCheck, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo2DuongKinhDaiTru, numThepVaiBoDau2DuongKinhDaiTru.Value);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo2KhoangCachDaiTru, numThepVaiBoDau2KhoangRaiDaiTru.Value);

                }
                else
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBoDau2Check, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo2DaiCheck, 0);
                }
            }
            else
            {
                dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBoDau1Check, 0);
                dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBoDau2Check, 0);
                dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo1DaiCheck, 0);
                dictCacGiaTriThepCanDoi.Add(CUltilities.thepVaiBo2DaiCheck, 0);
            }
        }

        private void CommandChoThepDaiLong(Dictionary<string, object> dictCacGiaTriThepCanDoi)
        {

            if (cbbLoaiDam.Text.Contains("C") || cbbLoaiDam.Text.Contains("D"))
            {
                if (cbThepDaiLong.Checked)
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongDonCheck, true);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongDonN, numThepDaiLongSoLuong);
                }
                else
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongDonCheck, false);
                }
                if (cbDaiLongKep.Checked)
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepCheck, true);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepBen1, cbDaiLongKepBen1.Checked);

                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepBen2, cbDaiLongKepBen2.Checked);

                    if (cbDaiLongKepBen1.Checked || cbDaiLongKepBen2.Checked)
                    {
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepN1, numDaiLongKepN1);
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepN2, numDaiLongKepN2);
                    }
                }
                else
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepCheck, false);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepBen1, false);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepBen2, false);
                }

            }
            else
            {
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongDonCheck, false);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepCheck, false);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepBen1, false);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiLongKepBen2, false);
            }
        }

        private void CommandChoThepGia(Dictionary<string, object> dictCacGiaTriThepCanDoi)
        {


            if (tabControl2.TabPages.Contains(tpThepGia))
            {

                if (cbThepGia.Checked)
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCheck, true);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaDuongKinh, numThepGiaDuongKinh);

                }
                else
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCheck, false);
                    //AddDictChecked(dictCacGiaTriThepCanDoi,CUltilities.thepGiaToiGoitrue, false);
                    //AddDictChecked(dictCacGiaTriThepCanDoi,CUltilities.thepGiaToiGoi2, false);
                }
                if (cbThepDaiGia.Checked)
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaDaiGiaCheck, true);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaDaiGiaDuongKinh, numThepGiaDaiDuongKinh);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaDaiGiaKhoangCach, numThepGiaDaiKhoangRai);
                }
                else
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaDaiGiaCheck, false);
                }

                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaToiGoi1, cbThepGiaDiToiGoi1.Checked);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaToiGoi2, cbThepGiaDiToiGoi2.Checked);


            }
            else
            {
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCheck, false);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaDaiGiaCheck, false);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaToiGoi1, false);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaToiGoi2, false);

                //AddDictChecked(dictCacGiaTriThepCanDoi,CUltilities.thepGiaToiGoitrue, false);
                //AddDictChecked(dictCacGiaTriThepCanDoi,CUltilities.thepGiaToiGoi2, false);
            }

        }


        private void CommandChoThepDai(Dictionary<string, object> dictCacGiaTriThepCanDoi)
        {

            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiCheck, cbKhaiBaoThepDai.Checked);


            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiGoi1DuongKinh, numThepDaiGoi1DuongKinh);
            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiGoi1KhoangRai, numThepDaiGoi1KhoangCach);

            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiNhipDuongKinh, numThepDaiNhipDuongKinh);


            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiNhipKhoangRai, numThepDaiNhipKhoangCach);
            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiGoi2DuongKinh, numThepDaiGoi2DuongKinh);
            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiGoi2KhoangRai, numThepDaiGoi2KhoangCach);

            if (damLoaiMoi)
            {
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDaiGoi1DinhViD2, numThepDaiGoi1DinhVi);
                //AddDictChecked(dictCacGiaTriThepCanDoi,CUltilities.thepDaiGoi2DinhViD2, numThepDaiGoi2DinhVi)
            }

        }

        private void CommandChoThepGiaCuong(Dictionary<string, object> dictCacGiaTriThepCanDoi)
        {
            try
            {
                if (tabControl2.TabPages.Contains(tpThepGiaCuong))
                {
                    if (cbGiaCuongGoi1.Checked)
                    {
                        if (cbThepGiaCuongD1L1.Checked)
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1DuongKinh, numThepGiaCuongGoi1DuongKinh);
                            //if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi1DuongKinh))
                            //    dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi1DuongKinh, numThepGiaCuongGoi1DuongKinh.Value);
                            //else
                            //    dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi1DuongKinh] = numThepGiaCuongGoi1DuongKinh.Value;

                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1SoLuong, numThepGiaCuongGoi1SoLuong);
                            //if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi1SoLuong))
                            //    dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi1SoLuong, numThepGiaCuongGoi1SoLuong.Value);
                            //else
                            //    dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi1SoLuong] = numThepGiaCuongGoi1SoLuong.Value;

                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1Check, true);
                            //if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi1Check))
                            //    dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi1Check, 1);
                            //else
                            //    dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi1Check] = 1;
                        }
                        else
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1Check, false);
                        }
                        if (cbThepGiaCuongD1L2.Checked)
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1L2DuongKinh, numThepGiaCuongGoi1L2DuongKinh);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1L2SoLuong, numThepGiaCuongGoi1L2SoLuong);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1L2Check, true);
                        }
                        else
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1L2Check, false);
                        }
                    }
                    else
                    {
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1Check, false);
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1L2Check, false);
                    }
                    if (cbGiaCuongNhip.Checked)
                    {

                        if (cbThepGiaCuongGL1.Checked)
                        {

                            //if (!dictCacGiaTriThepCanDoi.ContainsKey(thepGiaCuongNhipSoLuong))
                            //    dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongNhipSoLuong, numThepGiaCuongNhipSoLuong.Value);
                            //else
                            //    dictCacGiaTriThepCanDoi[thepGiaCuongNhipSoLuong] = numThepGiaCuongNhipSoLuong.Value;
                            //dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi1DinhVi, numThepGiaCuongGoi1DinhVi.Value);

                            if (damLoaiMoi)
                            {
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipCheckD2, true);
                            }
                            else
                            {
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipCheck, true);
                                //if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongNhipCheck))
                                //    dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongNhipCheck, 1);
                                //else
                                //    dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongNhipCheck] = 1;
                            }

                            if (!damLoaiMoi)
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipDuongKinh, numThepGiaCuongNhipDuongKinh);
                            else
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipDuongKinhD2, numThepGiaCuongNhipDuongKinh);


                            if (damLoaiMoi)
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipDinhViD2, numThepGiaCuongNhipDinhVi);
                            else
                            {
                                dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongNhipLCheck, (cbAutoRangBuocNhip.Checked ? 0 : 1));


                                if (cbAutoRangBuocNhip.Checked == false)
                                {

                                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipDinhVi, numThepGiaCuongNhipDinhVi);
                                    //if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongNhipDinhVi))
                                    //    dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongNhipDinhVi, numThepGiaCuongNhipDinhVi.Value);
                                    //else
                                    //    dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongNhipDinhVi] = numThepGiaCuongNhipDinhVi.Value;
                                }
                                else
                                {

                                    if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongNhipDinhVi))
                                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongNhipDinhVi, lengthDam / 4);
                                    else
                                        dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongNhipDinhVi] = lengthDam / 4;

                                }
                            }
                        }
                        else
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipCheck, false);
                        }
                        if (cbThepGiaCuongGL2.Checked)
                        {

                            if (damLoaiMoi)
                            {
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2CheckD2, true);
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipDinhViD2, numThepGiaCuongNhipDinhVi);

                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2DuongKinhD2, numThepGiaCuongNhipL2DuongKinh);
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2SoLuongD2, numThepGiaCuongNhipL2SoLuong);
                            }
                            else
                            {
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2Check, true);
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2DuongKinh, numThepGiaCuongNhipL2DuongKinh);
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2SoLuong, numThepGiaCuongNhipL2SoLuong);
                            }
                        }
                        else
                        {
                            if (damLoaiMoi)
                            {
                                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2CheckD2, false);
                                //AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipDinhViD2, lengthDam / 4.0);
                                if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongNhipDinhViD2))
                                    dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongNhipDinhViD2, lengthDam / 4);
                                else
                                    dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongNhipDinhVi] = lengthDam / 4;

                            }
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2Check, false);
                        }
                    }
                    else
                    {
                        if (damLoaiMoi)
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2CheckD2, false);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipCheckD2, false);
                        }
                        else
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipCheck, false);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2Check, false);
                        }
                    }


                    if (cbGiaCuongGoi2.Checked)
                    {
                        //if (cbAuToRangBuoc.Checked)
                        //{
                        //    if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongAuTo))
                        //        dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongAuTo, 0);
                        //}
                        //else
                        //{
                        //    if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongAuTo))
                        //        dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongAuTo, 1);
                        //    else
                        //        dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongAuTo] = 1;
                        //    //if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi1DinhVi))
                        //    //    dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi1DinhVi, numThepGiaCuongGoi1DinhVi.Value);
                        //    //else
                        //    //    dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi1DinhVi] = numThepGiaCuongGoi1DinhVi.Value;
                        //    if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi2DinhVi))
                        //        dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi2DinhVi, numThepGiaCuongGoi2DinhVi.Value);
                        //    else
                        //        dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi2DinhVi] = numThepGiaCuongGoi2DinhVi.Value;
                        //}

                        if (cbThepGiaCuongD2L1.Checked)
                        {

                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2DuongKinh, numThepGiaCuongGoi2DuongKinh);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2SoLuong, numThepGiaCuongGoi2SoLuong);
                            //dictCacGiaTriThepCanDoi,CUltilities.thepGiaCuongGoi2DinhVi, numThepGiaCuongGoi2DinhVi);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2Check, true);
                        }
                        else
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2Check, false);
                        }
                        if (cbThepGiaCuongD2L2.Checked)
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2L2DuongKinh, numThepGiaCuongGoi2L2DuongKinh);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2L2SoLuong, numThepGiaCuongGoi2L2SoLuong);
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2L2Check, true);
                        }
                        else
                        {
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2L2Check, false);
                        }


                    }
                    else
                    {
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2Check, false);
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2L2Check, false);
                    }

                    if (damLoaiMoi)
                    {
                        if (cbGiaCuongGoi1.Checked)
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi1DinhViD2, numThepGiaCuongGoi1DinhVi);
                        if (cbGiaCuongGoi2.Checked)
                            AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2DinhViD2, numThepGiaCuongGoi2DinhVi);
                    }
                    else
                    {
                        //int boolDinhViL1L2Auto;
                        if (cbAuToRangBuoc.Checked && cbAuToRangBuoc2.Checked)
                            if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongAuTo))
                                dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongAuTo, 0);
                            else
                                dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongAuTo] = 0;

                        else
                            if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongAuTo))
                            dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongAuTo, 1);
                        else
                            dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongAuTo] = 1;


                        if (!cbAuToRangBuoc.Checked)
                        {

                            if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi1DinhVi))
                                dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi1DinhVi, numThepGiaCuongGoi1DinhVi.Value);
                            else
                                dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi1DinhVi] = numThepGiaCuongGoi1DinhVi.Value;
                        }
                        else
                        {
                            if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi1DinhVi))
                                dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi1DinhVi, lengthDam / 4);
                            else
                                dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi1DinhVi] = lengthDam / 4;
                        }
                        if (!cbAuToRangBuoc2.Checked)
                        {

                            if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi2DinhVi))
                                dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi2DinhVi, numThepGiaCuongGoi2DinhVi.Value);
                            else
                                dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi2DinhVi] = numThepGiaCuongGoi2DinhVi.Value;
                        }
                        else
                        {
                            if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi2DinhVi))
                                dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi2DinhVi, lengthDam / 4);
                            else
                                dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi2DinhVi] = lengthDam / 4;

                        }
                    }
                }
                else
                {
                    if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi1Check))
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi1Check, 0);
                    else
                        dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi1Check] = 0;


                    if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongGoi1L2Check))
                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongGoi1L2Check, 0);
                    else
                        dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongGoi1L2Check] = 0;

                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2Check, false);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongGoi2L2Check, false);
                    if (!damLoaiMoi)
                    {
                        if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongAuTo))
                            dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongAuTo, 0);
                        else
                            dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongAuTo] = 0;


                        dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongNhipCheck, 0);

                        if (!dictCacGiaTriThepCanDoi.ContainsKey(CUltilities.thepGiaCuongNhipL2Check))
                            dictCacGiaTriThepCanDoi.Add(CUltilities.thepGiaCuongNhipL2Check, 0);
                        else
                            dictCacGiaTriThepCanDoi[CUltilities.thepGiaCuongNhipL2Check] = 0;
                    }
                    else
                    {
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipCheckD2, false);
                        AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepGiaCuongNhipL2CheckD2, false);

                    }
                }
            }
            catch (Exception exx)
            {
                TaskDialog.Show("Revit", "Lỗi: " + exx + "\n" + dictCacGiaTriThepCanDoi.Keys.Last());
                CUltilities.Logger(exx + "");
            }
        }
        private void CommandChoThepChu(Dictionary<string, object> dictCacGiaTriThepCanDoi)
        {
            if (cbThepChu1Lop.Checked)
            {
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepTren1DuongKinh, numThepChuTren1DuongKinh);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepTren1SoLuong, numThepChuTren1SoLuong);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDuoi1DuongKinh, numThepChuDuoi1DuongKinh);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDuoi1SoLuong, numThepChuDuoi1SoLuong);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepTren2Check, false);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDuoi2Check, false);

            }
            else
            {
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepTren1DuongKinh, numThepChuTren1DuongKinh);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepTren1SoLuong, numThepChuTren1SoLuong);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDuoi1DuongKinh, numThepChuDuoi1DuongKinh);
                AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDuoi1SoLuong, numThepChuDuoi1SoLuong);
                if (cbThepTren2.Checked)
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepTren2Check, true);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepTren2DuongKinh, numThepChuTren2DuongKinh);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepTren2SoLuong, numThepChuTren2SoLuong);
                }
                else AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepTren2Check, false);
                if (cbThepDuoi2.Checked)
                {
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDuoi2Check, true);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDuoi2DuongKinh, numThepChuDuoi2DuongKinh);
                    AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDuoi2SoLuong, numThepChuDuoi2SoLuong);
                }
                else AddDictChecked(dictCacGiaTriThepCanDoi, CUltilities.thepDuoi2Check, false);
            }
        }

        private void CommandChoGoi1Goi2(Dictionary<string, object> dictCacGiaTriThepCanDoi)
        {


            if (cbDam2Goi.Checked)
            {
                if (rdNeoDauDam1.Checked)
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi1, 1);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi2, 0);
                }
                else if (rdNeoDauDam2.Checked)
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi1, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi2, 1);
                }
                else if (rdNeoDauDam1Va2.Checked)
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi1, 1);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi2, 1);
                }
                else if (rdKhongNeo.Checked)
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi1, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi2, 0);
                }
                else
                {
                    TaskDialog.Show("ERROR", "Lỗi ở 2 gối.");
                }
            }
            else
            {
                if (rdConsoleDau1Neo2.Checked)
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi1, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi2, 1);
                    //dictCacGiaTriThepCanDoi.Add(bCotDau1, 0);
                    //dau 2 phai  <>0  &  >=150
                }
                if (rdConsoleDau1KhongNeo2.Checked)
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi1, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi2, 0);
                    //dictCacGiaTriThepCanDoi.Add(bCotDau1, 0);
                    //chua hoi
                }
                if (rdConsoleDau2Neo1.Checked)
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi1, 1);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi2, 0);
                    //dictCacGiaTriThepCanDoi.Add(bCotDau2, 0);
                    //dau 1 phai  <>0  &  >=150
                }
                if (rdConsoleDau2KhongNeo2.Checked)
                {
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi1, 0);
                    dictCacGiaTriThepCanDoi.Add(CUltilities.goi2, 0);
                    //dictCacGiaTriThepCanDoi.Add(bCotDau2, 0);
                    //dau 1 phai  <>0  &  >=150
                }
            }
            dictCacGiaTriThepCanDoi.Add(CUltilities.bCotDau1, numBCotDau1.Value);
            dictCacGiaTriThepCanDoi.Add(CUltilities.bCotDau2, numBCotDau2.Value);
            dictCacGiaTriThepCanDoi.Add(CUltilities.viTriDam, tbViTriDam.Text);
            dictCacGiaTriThepCanDoi.Add(CUltilities.viTriGoi1, tbViTriGoi1.Text);
            dictCacGiaTriThepCanDoi.Add(CUltilities.viTriGoi2, tbViTriGoi2.Text);


        }

        private void cbGiaCuongCheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            switch (cb.Name)
            {
                case "cbGiaCuongGoi1":
                    gbThepGiaCuongDauDam1.Enabled = cb.Checked;
                    pnGiaCuongDau1L.Enabled = pnGiaCuongDau1L.Visible;
                    gbThepGiaCuongDauDam1.Visible = cb.Checked;

                    cbThepGiaCuongD1L1.Checked = cb.Checked;
                    cbThepGiaCuongD1L2.Checked = cb.Checked;
                    cbAuToRangBuoc.Checked = !cb.Checked;
                    //cbThepGiaCuongD1L2.Checked = cb.Checked;
                    break;
                case "cbGiaCuongNhip":
                    gbThepGiaCuongGiuaDam.Enabled = cb.Checked;
                    gbThepGiaCuongGiuaDam.Visible = cb.Checked;
                    cbThepGiaCuongGL1.Checked = cb.Checked;
                    cbThepGiaCuongGL2.Checked = cb.Checked;
                    pnGiaCuongGiuaL.Enabled = pnGiaCuongGiuaL.Visible;
                    //pnGiaCuongGiuaL.Enab= cb.Checked;
                    cbAutoRangBuocNhip.Checked = !cb.Checked;

                    break;
                case "cbGiaCuongGoi2":
                    gbThepGiaCuongDauDam2.Enabled = cb.Checked;
                    gbThepGiaCuongDauDam2.Visible = cb.Checked;

                    pnGiaCuongDau2L.Enabled = pnGiaCuongDau2L.Visible;
                    //if (cb.Checked)
                    //{
                    //    pnGiaCuongDau2L.Visible = true;
                    //    pnGiaCuongDau2L.Enabled = cb.Checked;
                    //}
                    //else
                    //{
                    //    cbAuToRangBuoc2.Checked = true;
                    //}
                    cbThepGiaCuongD2L1.Checked = cb.Checked;
                    cbThepGiaCuongD2L2.Checked = cb.Checked;
                    cbAuToRangBuoc2.Checked = !cb.Checked;


                    //pnGiaCuongDau2L.Visible = (cbThepGiaCuongD2L1.Checked || cbThepGiaCuongD2L2.Checked) ? true : false;
                    //pnGiaCuongDau2L.Enabled = pnGiaCuongDau2L.Visible;
                    break;
            }


        }


        private void tabControl1_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
        }



        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (tabControl2.SelectedTab.Name)
            {

                case "tpNeoThep":
                    //if (cbDam2Goi.Checked || cbDamConsole.Checked)
                    //tpNeoThep.BackColor = Color.Beige;



                    break;
                case "tpThepChu":
                    //if (numThepChuTren1DuongKinh.Value != 0 && numThepChuTren1SoLuong.Value != 0 && numThepChuDuoi1DuongKinh.Value != 0 && numThepChuDuoi1SoLuong.Value != 0)
                    //tpThepChu.BackColor = Color.Beige;
                    AddDictChecked(lstCheck0, numThepChuTren1DuongKinh, tpThepChu);
                    AddDictChecked(lstCheck0, numThepChuDuoi1DuongKinh, tpThepChu);
                    if (cbThepChu2Lop.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepChuTren2DuongKinh, tpThepChu);
                        AddDictChecked(lstCheck0, numThepChuDuoi2DuongKinh, tpThepChu);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepChuTren2DuongKinh);
                        RemoveKeyInDict(lstCheck0, numThepChuDuoi2DuongKinh);
                    }
                    break;
                case "tpThepGiaCuong":
                    if (cbThepGiaCuongD1L1.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepGiaCuongGoi1DuongKinh, tpThepGiaCuong);
                        AddDictChecked(lstCheck0, numThepGiaCuongGoi1SoLuong, tpThepGiaCuong);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongGoi1DuongKinh);
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongGoi1SoLuong);
                    }

                    if (cbThepGiaCuongD1L2.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepGiaCuongGoi1L2DuongKinh, tpThepGiaCuong);
                        AddDictChecked(lstCheck0, numThepGiaCuongGoi1L2SoLuong, tpThepGiaCuong);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongGoi1L2DuongKinh);
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongGoi1L2SoLuong);
                    }
                    if (cbThepGiaCuongD2L1.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepGiaCuongGoi2DuongKinh, tpThepGiaCuong);
                        AddDictChecked(lstCheck0, numThepGiaCuongGoi2SoLuong, tpThepGiaCuong);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongGoi2DuongKinh);
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongGoi2SoLuong);
                    }
                    if (cbThepGiaCuongD2L2.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepGiaCuongGoi2L2DuongKinh, tpThepGiaCuong);
                        AddDictChecked(lstCheck0, numThepGiaCuongGoi2L2SoLuong, tpThepGiaCuong);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongGoi2L2DuongKinh);
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongGoi2L2SoLuong);


                    }
                    if (cbThepGiaCuongGL1.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepGiaCuongNhipDuongKinh, tpThepGiaCuong);
                        //AddDictChecked(lstCheck0,numThepGiaCuongNhipSoLuong, tpThepGiaCuong);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongNhipDuongKinh);
                    }
                    if (!cbAuToRangBuoc.Checked)
                        AddDictChecked(lstCheck0, numThepGiaCuongGoi1DinhVi, tpThepGiaCuong);
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepGiaCuongGoi1DinhVi);
                    }
                    break;
                case "tpThepDai":

                    AddDictChecked(lstCheck0, numThepDaiGoi1DuongKinh, tpThepDai);
                    AddDictChecked(lstCheck0, numThepDaiGoi1KhoangCach, tpThepDai);
                    AddDictChecked(lstCheck0, numThepDaiGoi2DuongKinh, tpThepDai);
                    AddDictChecked(lstCheck0, numThepDaiGoi2KhoangCach, tpThepDai);
                    AddDictChecked(lstCheck0, numThepDaiNhipDuongKinh, tpThepDai);
                    AddDictChecked(lstCheck0, numThepDaiNhipKhoangCach, tpThepDai);

                    if (cbThepDaiLong.Checked)
                        AddDictChecked(lstCheck0, numThepDaiLongSoLuong, tpThepDai);
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepDaiLongSoLuong);
                    }
                    if (cbDaiLongKep.Checked)
                    {
                        AddDictChecked(lstCheck0, numDaiLongKepN1, tpThepDai);
                        AddDictChecked(lstCheck0, numDaiLongKepN2, tpThepDai);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numDaiLongKepN1);
                        RemoveKeyInDict(lstCheck0, numDaiLongKepN2);
                    }
                    if (cbThepDaiCDungKep.Checked)
                        AddDictChecked(lstCheck0, numThepDaiCDungKepN, tpThepDai);
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepDaiCDungKepN);
                    }
                    if (cbThepDaiCNgangTren.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepDaiCNgangTrenD, tpThepDai);
                        AddDictChecked(lstCheck0, numThepDaiCNgangTrenR, tpThepDai);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepDaiCNgangTrenD);
                        RemoveKeyInDict(lstCheck0, numThepDaiCNgangTrenR);


                    }
                    if (cbThepDaiCNgangDuoi.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepDaiCNgangDuoiD, tpThepDai);
                        AddDictChecked(lstCheck0, numThepDaiCNgangDuoiR, tpThepDai);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepDaiCNgangDuoiD);
                        RemoveKeyInDict(lstCheck0, numThepDaiCNgangDuoiR);


                    }

                    break;
                case "tpThepGia":
                    if (cbThepGia.Checked)
                        AddDictChecked(lstCheck0, numThepGiaDuongKinh, tpThepGia);
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepGiaDuongKinh);

                    }
                    if (cbThepDaiGia.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepGiaDaiDuongKinh, tpThepGia);
                        AddDictChecked(lstCheck0, numThepGiaDaiKhoangRai, tpThepGia);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepGiaDaiDuongKinh);
                        RemoveKeyInDict(lstCheck0, numThepGiaDaiKhoangRai);
                    }

                    break;
                case "tpThepVaiBo":
                    if (cbThepVaiBoDau1.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepVaiBoDau1B, tpThepVaiBo);
                        AddDictChecked(lstCheck0, numThepVaiBoDau1H, tpThepVaiBo);
                        AddDictChecked(lstCheck0, numThepVaiBoDau1DuongKinhDaiTru, tpThepVaiBo);
                        AddDictChecked(lstCheck0, numThepVaiBoDau1KhoangRaiDaiTru, tpThepVaiBo);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepVaiBoDau1B);
                        RemoveKeyInDict(lstCheck0, numThepVaiBoDau1H);
                        RemoveKeyInDict(lstCheck0, numThepVaiBoDau1DuongKinhDaiTru);
                        RemoveKeyInDict(lstCheck0, numThepVaiBoDau1KhoangRaiDaiTru);

                    }
                    if (cbThepVaiBoDau2.Checked)
                    {
                        AddDictChecked(lstCheck0, numThepVaiBoDau2B, tpThepVaiBo);
                        AddDictChecked(lstCheck0, numThepVaiBoDau2H, tpThepVaiBo);
                        AddDictChecked(lstCheck0, numThepVaiBoDau2DuongKinhDaiTru, tpThepVaiBo);
                        AddDictChecked(lstCheck0, numThepVaiBoDau2KhoangRaiDaiTru, tpThepVaiBo);
                    }
                    else
                    {
                        RemoveKeyInDict(lstCheck0, numThepVaiBoDau2B);
                        RemoveKeyInDict(lstCheck0, numThepVaiBoDau2H);
                        RemoveKeyInDict(lstCheck0, numThepVaiBoDau2DuongKinhDaiTru);
                        RemoveKeyInDict(lstCheck0, numThepVaiBoDau2KhoangRaiDaiTru);


                    }
                    break;

            }
        }

        private bool CheckListNumDifferent0(Dictionary<NumericUpDown, TabPage> lst)
        {
            foreach (var o in lst)
                if (o.Key.Value == 0)
                {
                    if (MessageBox.Show("Còn giá trị chưa nhập!\n" + o.Key.Name + "\nNhấn \"Cancel\" kiểm tra lại.\n\n*Nếu bạn đã nhập hết, hãy nhấn \"OK\"", "Cảnh báo",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        tabControl2.SelectedTab = o.Value;

                        o.Key.Select();

                        return false;
                    }
                    else
                        return true;
                }
            return true;
        }
        private void AddDictChecked(Dictionary<NumericUpDown, TabPage> dictionaryThis, NumericUpDown key, TabPage value)
        {
            if (!dictionaryThis.ContainsKey(key))
                dictionaryThis.Add(key, value);
            else
                dictionaryThis[key] = value;

            // TaskDialog.Show("Value", key + "=" + dictionaryThis[key] + "");
        }

        private void AddDictChecked(Dictionary<string, object> dictionaryThis, string key, NumericUpDown num)
        {
            if (!dictionaryThis.ContainsKey(key))
                dictionaryThis.Add(key, num.Value);
            else
                dictionaryThis[key] = num.Value;

            // TaskDialog.Show("Value", key + "=" + dictionaryThis[key] + "");
        }
        private void AddDictChecked(Dictionary<string, object> dictionaryThis, string key, bool value)
        {
            if (value)
                if (!dictionaryThis.ContainsKey(key))
                    dictionaryThis.Add(key, 1);
                else
                    dictionaryThis[key] = 1;
            else
                if (!dictionaryThis.ContainsKey(key))
                dictionaryThis.Add(key, 0);
            else
                dictionaryThis[key] = 0;

            // TaskDialog.Show("Value", key + "=" + dictionaryThis[key] + "");
        }
        private void RemoveKeyInDict(Dictionary<NumericUpDown, TabPage> dictionaryThis, NumericUpDown key)
        {
            if (dictionaryThis.ContainsKey(key))
                dictionaryThis.Remove(key);


            // TaskDialog.Show("Value", key + "=" + dictionaryThis[key] + "");
        }



        private void grThepDaiLong_Enter(object sender, EventArgs e)
        {

        }

        private void gbThepDaiLongC_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void grThepDaiGiaCuong_Enter(object sender, EventArgs e)
        {

        }

        private void num_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cbThepVaiBoDau1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            grThepVaiBoDau1.Enabled = cb.Checked;
            grThepVaiBoDau1.Visible = cb.Checked;

        }

        private void cbThepVaiBoDau2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            grThepVaiBoDau2.Enabled = cb.Checked;
            grThepVaiBoDau2.Visible = cb.Checked;
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numBCotDau1_Leave(object sender, EventArgs e)
        {
        }

        private void numBCotDau1_ValueChanged(object sender, EventArgs e)
        {

            NumericUpDown num = sender as NumericUpDown;
            if (num.Value > num.Maximum || num.Value < num.Minimum)
                TaskDialog.Show("Error", "Vui lòng nhập đúng giá trị cho phép!");
        }

        private void cbThepDaiC_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbThepDaiC.Checked)
            {
                gbThepDaiC.Enabled = true;
                //cbThepDaiCDungDon.Checked = true;
                //cbThepDaiCDungKep.Checked = true;
                //cbThepDaiCNgangTren.Checked = true;
                //cbThepDaiCNgangDuoi.Checked = true;
            }
            else
            {
                gbThepDaiC.Enabled = false;
                cbThepDaiCDungDon.Checked = false;
                cbThepDaiCDungKep.Checked = false;
                cbThepDaiCNgangTren.Checked = false;
                cbThepDaiCNgangDuoi.Checked = false;
            }
        }

        private void cbDaiLongKep_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDaiLongKep.Checked)
                gbDaiLongKep.Enabled = true;
            else
                gbDaiLongKep.Enabled = false;
            gbDaiLongKep.Visible = cbDaiLongKep.Checked;
        }

        private void GRThepDaiC(bool vis)
        {
            gbThepDaiC.Visible = vis;
            cbThepDaiC.Visible = vis;
        }
        private void GRThepDaiKep(bool vis)
        {
            gbDaiLongKep.Visible = vis;
            //cbDaiLongKep.Checked = vis;
        }

        private void cbAuToRangBuoc_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                cbAuToRangBuoc.Checked = true;
                //cbAuToRangBuoc2.Checked = true;
            }
            else
            {
                cbAuToRangBuoc.Checked = false;
                //cbAuToRangBuoc2.Checked = false;
                numThepGiaCuongGoi1DinhVi.Value = (decimal)lengthDam / 4;
                //numThepGiaCuongGoi2DinhVi.Value = 0;

            }
            numThepGiaCuongGoi1DinhVi.Enabled = !cb.Checked;
            //numThepGiaCuongGoi2DinhVi.Enabled = !cb.Checked;

        }
        private void cbAuToRangBuoc2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                //cbAuToRangBuoc.Checked = true;
                cbAuToRangBuoc2.Checked = true;
            }
            else
            {
                //cbAuToRangBuoc.Checked = false;
                cbAuToRangBuoc2.Checked = false;
                //numThepGiaCuongGoi1DinhVi.Value = 0;
                //numThepGiaCuongGoi2DinhVi.Value = 0;
                numThepGiaCuongGoi2DinhVi.Value = (decimal)lengthDam / 4;
            }
            //numThepGiaCuongGoi1DinhVi.Enabled = !cb.Checked;
            numThepGiaCuongGoi2DinhVi.Enabled = !cb.Checked;
        }

        private void cbAuToRangBuocNhip_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                //cbAuToRangBuoc.Checked = true;
                cbAutoRangBuocNhip.Checked = true;
            }
            else
            {
                //cbAuToRangBuoc.Checked = false;
                cbAutoRangBuocNhip.Checked = false;
                //numThepGiaCuongGoi1DinhVi.Value = 0;
                //numThepGiaCuongNhipDinhVi.Value = 0;

            }
            //numThepGiaCuongGoi1DinhVi.Enabled = !cb.Checked;
            numThepGiaCuongNhipDinhVi.Enabled = !cb.Checked;
        }


        private void numThepGiaCuongGoi2DinhVi_ValueChanged(object sender, EventArgs e)
        {
        }

        private void cbDaiLongKepBen1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDaiLongKepBen1.Checked || cbDaiLongKepBen2.Checked)
            {
                numDaiLongKepN1.Enabled = true;
                numDaiLongKepN2.Enabled = true;
            }
            else
            {
                numDaiLongKepN1.Enabled = false;
                numDaiLongKepN2.Enabled = false;
                numDaiLongKepN1.Value = 0;
                numDaiLongKepN2.Value = 0;
            }

        }

        private void cbThepDaiCNgangTren_CheckedChanged(object sender, EventArgs e)
        {
            if (cbThepDaiCNgangTren.Checked)
            {
                numThepDaiCNgangTrenD.Enabled = true;
                numThepDaiCNgangTrenR.Enabled = true;
            }
            else
            {
                numThepDaiCNgangTrenD.Enabled = false;
                numThepDaiCNgangTrenR.Enabled = false;
            }
        }

        private void cbThepDaiCNgangDuoi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbThepDaiCNgangDuoi.Checked)
            {
                numThepDaiCNgangDuoiD.Enabled = true;
                numThepDaiCNgangDuoiR.Enabled = true;
            }
            else
            {
                numThepDaiCNgangDuoiD.Enabled = false;
                numThepDaiCNgangDuoiR.Enabled = false;
            }
        }

        private void cbThepDaiCDungKepBen1_CheckedChanged(object sender, EventArgs e)
        {


            if (cbThepDaiCDungKepBen1.Checked || cbThepDaiCDungKepBen2.Checked)
            {
                numThepDaiCDungKepN.Enabled = true;
            }
            else
                numThepDaiCDungKepN.Enabled = false;
        }

        private void cbThepDaiCDungKep_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            switch (cb.Name)
            {
                case "cbThepDaiCDungKep":
                    cbThepDaiCDungKepBen1.Checked = cb.Checked;
                    cbThepDaiCDungKepBen2.Checked = cb.Checked;
                    break;
                case "cbThepDaiCDungKep2":
                    cbThepDaiCDungKep2Ben1.Checked = cb.Checked;
                    cbThepDaiCDungKep2Ben2.Checked = cb.Checked;
                    break;
            }
        }


        private void numThepChuTren1DuongKinh_ValueChanged(object sender, EventArgs e)
        {
            //int defaultValue = 20;

            CustomNumericUpDown nm = sender as CustomNumericUpDown;
            string tapHop;
            int[] defaultValue;

            if (nm.Tag == "!")
            {
                defaultValue = new int[] { 6, 8, 10, 12 };
                tapHop = "{ 6, 8, 10, 12 }";
            }
            else
            if (nm.Tag == "@")
            {
                defaultValue = new int[] { 50, 75, 100, 150, 200 };
                tapHop = "{ 50, 75, 100, 150, 200 }";
            }
            else
            {
                defaultValue = new int[] { 14, 16, 18, 20, 22, 25, 28, 32, 40 };
                tapHop = "{ 14, 16, 18, 20, 22, 25, 28, 32, 40 }";

            }


            bool checkEnable = nm.Enabled;
            bool check = false;
            if (checkEnable)
            {
                foreach (int k in defaultValue)
                    if (k == nm.Value)
                    {
                        check = true;
                        break;
                    }
                if (!check)
                {
                    MessageBox.Show("Đường kính phải thuộc tập hợp: " + tapHop + "\nVui lòng khai báo lại!", "Có lỗi xảy ra", MessageBoxButtons.RetryCancel);


                    nm.Value = defaultValue[0];
                    nm.Select();
                    nm.BackColor = Color.OrangeRed;
                }
                else
                {
                    nm.BackColor = Color.PaleGreen;
                }
            }
            else
            {
                nm.BackColor = Color.White;
            }

        }

        private void numThepGiaCuongGoi1DinhVi_ValueChanged(object sender, EventArgs e)
        {
        }

        private void numThepChuTren1SoLuong_ValueChanged(object sender, EventArgs e)
        {

            int b = (int)(dictLuuParam["b_So sanh"].AsDouble() * 304.8);
            ////int b = 250;
            int aBaoVe = (int)(dictLuuParam["a_Bảo vệ"].AsDouble() * 304.8);
            //int aBaoVe = 25;
            int dThep = 0;
            NumericUpDown nm = sender as NumericUpDown;
            int n = (int)nm.Value;
            int k = 0;
            switch (nm.Name)
            {
                case "numThepChuTren1SoLuong":
                    dThep = (int)numThepChuTren1DuongKinh.Value;
                    break;
                case "numThepChuDuoi1SoLuong":
                    dThep = (int)numThepChuDuoi1DuongKinh.Value;
                    break;
                case "numThepChuTren2SoLuong":
                    dThep = (int)numThepChuTren2DuongKinh.Value;
                    break;
                case "numThepChuDuoi2SoLuong":
                    dThep = (int)numThepChuDuoi2DuongKinh.Value;
                    break;
                case "numThepGiaCuongGoi1SoLuong":
                    dThep = (int)numThepGiaCuongGoi1DuongKinh.Value;
                    k = (int)numThepGiaCuongGoi1SoLuong.Value;
                    break;
                case "numThepGiaCuongGoi1L2SoLuong":
                    dThep = (int)numThepGiaCuongGoi1L2DuongKinh.Value;
                    k = (int)numThepGiaCuongGoi1L2SoLuong.Value;
                    break;
                case "numThepGiaCuongGoi2SoLuong":
                    dThep = (int)numThepGiaCuongGoi1DuongKinh.Value;
                    k = (int)numThepGiaCuongGoi2SoLuong.Value;
                    break;
                case "numThepGiaCuongGoi2L2SoLuong":
                    dThep = (int)numThepGiaCuongGoi2L2DuongKinh.Value;
                    k = (int)numThepGiaCuongGoi2L2SoLuong.Value;
                    break;
                case "numThepGiaCuongNhipSoLuong":
                    dThep = (int)numThepGiaCuongNhipL2DuongKinh.Value;
                    break;
                default: break;
            }
            double congthuc = (b - 2 * (aBaoVe - dThep / 2)) / (n + k - 1);

            if (!(congthuc >= dThep * 1.5 && congthuc >= (int)numDMax.Value && congthuc >= 38))
            {
                if (MessageBox.Show("Khoảng cách giữa các thép quá nhỏ.", "WARNING!", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    //int nRequire = ((b - 2 * (aBaoVe - (dThep / 2))) / 38) + 1;
                    //nm.Value = nRequire;
                    nm.Value = (decimal)1;
                    nm.Select();
                    nm.BackColor = Color.OrangeRed;
                }
                else
                {
                    nm.Select();
                    nm.BackColor = Color.LightYellow;
                }
            }
            else
                nm.BackColor = Color.PaleGreen;

            //MessageBox.Show(congthuc + "\nb=" + b + "\naBaoVe = " + aBaoVe + "\ndThep = " + dThep, "KQ");

        }

        private void grThepGiaCuong_Enter(object sender, EventArgs e)
        {

        }


        private void ShowThepGiaCuong(bool check, int index = 2)
        {
            if (check)
            {
                if (!tabControl2.TabPages.Contains(tpThepGiaCuong))
                    tabControl2.TabPages.Insert(index, tpThepGiaCuong);

                //An di vi gay loi update
                //cbGiaCuongGoi1.Checked = check;
                //cbGiaCuongNhip.Checked = check;
                //cbGiaCuongGoi2.Checked = check;


                //cbGiaCuongGoi1.Checked = check;
                //cbGiaCuongNhip.Checked = check;
                //cbGiaCuongGoi2.Checked = check;
            }
            else
            {
                if (tabControl2.TabPages.Contains(tpThepGiaCuong))
                    tabControl2.TabPages.Remove(tpThepGiaCuong);

                //An di vi gay loi update
                //cbGiaCuongGoi1.Checked = check;
                //cbGiaCuongNhip.Checked = check;
                //cbGiaCuongGoi2.Checked = check;
            }
        }
        private void ShowThepGia(bool check, int index = 3)
        {
            if (!check)
            {
                if (tabControl2.TabPages.Contains(tpThepGia))
                    tabControl2.TabPages.Remove(tpThepGia);
                //cbThepGia.Checked = false;
                //cbThepDaiGia.Checked = false;
            }
            else
            {
                if (!tabControl2.TabPages.Contains(tpThepGia))
                    tabControl2.TabPages.Insert(index, tpThepGia);
                //cbThepGia.Checked = true;
                //cbThepDaiGia.Checked = true;
            }
        }
        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ProcessAllControls(System.Windows.Forms.Control rootControl, Action<System.Windows.Forms.Control> action)
        {
            foreach (System.Windows.Forms.Control childControl in rootControl.Controls)
            {

                ProcessAllControls(childControl, action);
                action(childControl);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            cbGiaCuongGoi1.Checked = false;
            cbGiaCuongGoi2.Checked = false;
            cbGiaCuongNhip.Checked = false;
            numThepGiaCuongGoi1DuongKinh.Value = 0;
            numThepGiaCuongDaiGoi1RaiThep.Value = 0;
            numThepGiaCuongGoi1DinhVi.Value = 0;
            numThepGiaCuongGoi1L2DuongKinh.Value = 0;
            numThepGiaCuongGoi1L2SoLuong.Value = 0;
            numThepGiaCuongGoi1L2DinhVi.Value = 0;


            numThepGiaCuongNhipDinhVi.Value = 0;

            numThepGiaCuongGoi1DuongKinh.Value = 0;
            numThepGiaCuongDaiGoi2RaiThep.Value = 0;
            numThepGiaCuongGoi2DinhVi.Value = 0;
            numThepGiaCuongGoi2L2DuongKinh.Value = 0;
            numThepGiaCuongGoi2L2SoLuong.Value = 0;
            numThepGiaCuongGoi2L2DinhVi.Value = 0;
            cbbNhomDoiTuongDam.SelectedItem = cbbNhomDoiTuongDam.Items[0];
            cbbLoaiDam.SelectedItem = cbbLoaiDam.Items[0];
            //numThepChuTren1DuongKinh.Value = 0;
            //numThepChuTren1SoLuong.Value = 0;
            //numThepChuDuoi1DuongKinh.Value = 0;
            //numThepChuDuoi1SoLuong.Value = 0;

            //numThepChuTren2DuongKinh.Value = 0;
            //numThepChuTren2SoLuong.Value = 0;
            //numThepChuDuoi2DuongKinh.Value = 0;
            //numThepChuDuoi2SoLuong.Value = 0;



        }

        private void numThepChuTren1DuongKinh_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void DoiMauKhac0_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nm = sender as NumericUpDown;

            if (nm.Value != 0)
                nm.BackColor = Color.PaleGreen;
            else
                nm.BackColor = Color.OrangeRed;
        }

        private void tpNeoThep_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            //hoverImage = new HoverImage(GiaoDienCot.Properties.Resources.w1);
            //hoverImage.ShowDialog();
            //pictureBox1.Location = new System.Drawing.Point(9,60);

            //originalSize = pictureBox1.Size;   //**
            //originalLoc = pictureBox1.Location;
            //resize = 1;
            //timer1.Interval = 10;
            //timer1.Start();

            //frm1.ActiveForm.Size = new Size(1024, 850);

            pictureBox1.Size = new Size(731, 436);

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //try
            //{
            //    hoverImage.Close();
            //}catch(Exception exx)
            //{
            //    CUltilities.Logger("Lỗi bitmap: \n" + exx.ToString());
            //}
            //timer1.Stop();
            //pictureBox1.Size = originalSize;
            //pictureBox1.Location = originalLoc;

            pictureBox1.Size = new Size(93, 102);
            //pictureBox1.Location = new System.Drawing.Point(17, 179);
            //frm1.ActiveForm.Size = new Size(653, 502);
        }

        private void cbKhaiBaoThepDai_CheckedChanged(object sender, EventArgs e)
        {

            if (cbKhaiBaoThepDai.Checked)
            {
                cbKhaiBaoThepDaiXoan.Checked = false;
                cbKhaiBaoThepDaiXoan.Text = "                                   ";
            }
            else
            {
                cbKhaiBaoThepDaiXoan.Checked = true;
                cbKhaiBaoThepDaiXoan.Text = "______________________";
            }
        }

        private void cbKhaiBaoThepDaiXoan_CheckedChanged(object sender, EventArgs e)
        {
            if (cbKhaiBaoThepDaiXoan.Checked)
            {
                cbKhaiBaoThepDai.Checked = false;
                cbKhaiBaoThepDai.Text = "                            ";
            }
            else
            {
                cbKhaiBaoThepDai.Checked = true;
                cbKhaiBaoThepDai.Text = "__________________";
            }
        }

        private void tbViTriDam_TextChanged(object sender, EventArgs e)
        {

        }

        private void gDDam2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //grThepDai.Size = new Size(grThepDai.Size.Width, 117);

        }

        private void numThepDaiGoi1DinhVi_ValueChanged(object sender, EventArgs e)
        {
            numThepDaiGoi2DinhVi.Value = (sender as NumericUpDown).Value;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: " + CUltilities.VersionName + "\nBuilt: " + CUltilities.BuildDate, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbThepDaiCDungKep2Ben1_CheckedChanged(object sender, EventArgs e)
        {
            numThepDaiCDungKep2N.Enabled = (cbThepDaiCDungKep2Ben1.Checked || cbThepDaiCDungKep2Ben2.Checked);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

