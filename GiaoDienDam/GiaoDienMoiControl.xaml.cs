using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GiaoDienDam
{
    /// <summary>
    /// Interaction logic for GiaoDienDam2.xaml
    /// </summary>
    public partial class GiaoDienMoiControl : Window
    {
        private ExternalCommandData m_commandData;
        private string m_message;
        private ElementSet m_elements;
        private Document doc;
        const int numBCotDau = 150;
        Dictionary<string, Parameter> dictLuuParam;
        ParameterSet ParameterSet;
        private ICollection<ElementId> m_elementIds;

        string[] lstTypeA = new string[] { "A1", "A2", "A3", "A4" };

        string[] lstTypeB = new string[] { "B1", "B2", "B3", "B4" };

        string[] lstTypeC = new string[] { "C1", "C2", "C3", "C4" };

        string[] lstTypeD = new string[] { "D1", "D2", "D3" };

        string[] lstTypeE = new string[] { "E1", "E2", "E3", "E4", "E5", "E6" };

        string[] lstTypeF = new string[] { "F1" };

        double lengthDam = 0;

        Dictionary<System.Windows.Controls.TextBox, TabItem> lstCheck0 = new Dictionary<System.Windows.Controls.TextBox, TabItem>();
        bool neoTheped = false;


        bool damLoaiMoi = false;
        public GiaoDienMoiControl(UIDocument uidoc)
        {
            InitializeComponent();
            
            //if (cbbLoaiDam.SelectedIndex == -1) tabControl2.Enabled = false;
            //cbDam2Goi.Checked = true;
            //gbDam2Goi.Click += new System.EventHandler(this.cbDam2Goi_CheckedChanged);

            //cbThepChu1Lop.Visible = false;
            //cbThepChu2Lop.Visible = false;
            //gbThepTren2.Visible = false;
            //gbThepDuoi2.Visible = false;
            //cbThepTren2.Visible = false;
            //cbThepDuoi2.Visible = false;
            //ShowThepGiaCuong(false);
            //ShowThepGia(false);

            ////int MAX_L_DINH_VI_THEP_GIA_CUONG = (int)dictLuuParam["Length"].AsDouble();
            ////toolTip1 = new CustomToolTip();
            //toolTip1.ToolTipTitle = "Mô tả ngắn";
            //toolTip1.SetToolTip(this.gbDam2Goi, "Hai đầu dầm được đỡ bởi 2 cột:\n-Neo đầu 1:");
            //toolTip1.SetToolTip(this.numBCotDau1, "Kích thước đầu cột theo phương dầm");
            //toolTip1.SetToolTip(this.numBCotDau2, "Kích thước đầu cột theo phương dầm");
            //toolTip1.SetToolTip(this.lbCotDau1, "Kích thước đầu cột theo phương dầm");
            //toolTip1.SetToolTip(this.lbCotDau2, "Kích thước đầu cột theo phương dầm");

        }
        public void ShowMeUpdate(
      Document doc,
      ExternalCommandData p_commandData,
      ref string p_message,
      ElementSet p_elements,
      ParameterSet parameterSet,
      Dictionary<string, Parameter> dictLuuParam,
      bool damMoi = false)
        {
            this.doc = doc;
            m_commandData = p_commandData;
            m_elements = p_elements;
            m_message = p_message;
            ParameterSet = parameterSet;
            this.dictLuuParam = dictLuuParam;
            //lengthDam = (double)GetParamDouble("Length");
            damLoaiMoi = damMoi;

            //this.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hi");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //tabPage1.BorderBrush.
        }
    }
}
