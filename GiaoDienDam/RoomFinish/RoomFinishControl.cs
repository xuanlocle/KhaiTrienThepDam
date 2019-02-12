using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;

namespace GiaoDienDam.RoomFinish
{


    public partial class RoomsFinishesControl : System.Windows.Forms.Form
    {
        // Fields
        private Autodesk.Revit.DB.Document _doc;
        private UIDocument _UIDoc;
        private WallType _selectedWallType;
        private WallType _duplicatedWallType;
        private double _boardHeight;
        private IEnumerable<Room> _selectedRooms;
        private IEnumerable<WallType> _wallTypes;
        //internal RadioButton all_rooms_radio;
        //internal RadioButton selected_rooms_radio;
        //internal Label board_height_label;
        //internal System.Windows.Forms.TextBox Height_TextBox;
        //internal Label select_wall_label;
        //internal ListBox WallTypeListBox;
        //internal Button Ok_Button;
        //internal Button Cancel_Button;
        private bool _contentLoaded;
        private bool _joinWall;
        public bool JoinWall
        {
            get { return _joinWall; }
        }


        // Properties
        public WallType SelectedWallType =>
            this._selectedWallType;

        public WallType DuplicatedWallType =>
            this._duplicatedWallType;

        public double BoardHeight =>
            this._boardHeight;

        public IEnumerable<Room> SelectedRooms =>
            this._selectedRooms;

        List<string> lstWallName = new List<string>();
        List<double> lstWallHeight = new List<double>();
        List<WallType> lstWallTypes = new List<WallType>();

        // Methods
        public RoomsFinishesControl(UIDocument UIDoc)
        {
            Autodesk.Revit.DB.Document _doc = UIDoc.Document;
            this.InitializeComponent();
            this._doc = _doc;
            this._UIDoc = UIDoc;
            IEnumerable<Room> ModelRooms = null;


            // Find all rooms in current view
            ModelRooms = from elem in new FilteredElementCollector(_doc, _doc.ActiveView.Id).OfClass(typeof(SpatialElement))
                         let room = elem as Room
                         select room;
            txtSoPhong.Text = ModelRooms.Count().ToString();

            int countNotInitial = 0;
            List<string> lstRoomsNotInitial = new List<string>();
            foreach (var k in ModelRooms)
            {
                string name = k.get_Parameter("namewall").AsString();
                double height = k.get_Parameter("heightwall").AsDouble() * 0.00328084;
                lstWallName.Add(name);
                lstWallHeight.Add(height);
                //System.Windows.MessageBox.Show(k.get_Parameter("heightwall").AsDouble().ToString());
                if (height == 0 || name == "")
                {
                    countNotInitial += 1;
                    lstRoomsNotInitial.Add(k.Id + "");
                }
            }
            btnXemID.Enabled = countNotInitial > 0 ? true : false;
            txtSoPhongChuaKhaiBao.Text = countNotInitial.ToString();



            this._wallTypes = from elem in new FilteredElementCollector(_doc).OfClass(typeof(WallType))
                              let type = elem as WallType
                              where type.Kind == 0
                              select type;

            foreach (var k in _wallTypes)
                lstWallTypes.Add(k);



        }

        private IEnumerable<Room> SelectRooms()
        {
            ICollection<ElementId> elementIds = this._UIDoc.Selection.GetElementIds();
            //IEnumerable<Room> source = null;
            IList<Room> list = new List<Room>();
            //if (this.all_rooms_radio.IsChecked.Value)

            return (from elem in new FilteredElementCollector(_doc, _doc.ActiveView.Id).OfClass(typeof(SpatialElement))
                    let room = elem as Room
                    select room);

            //if (elementIds.Count != 0)
            //{
            //    source = from elem in new FilteredElementCollector(this._doc, elementIds).OfClass(typeof(SpatialElement))
            //             let room = elem as Room
            //             select room;
            //    list = source.ToList<Room>();
            //}
            //if (list.Count != 0)
            //{
            //    return source;
            //}
            //ISelectionFilter filter = new RoomSelectionFilter();
            //foreach (Reference reference in this._UIDoc.get_Selection().PickObjects(1, filter, Tools.LangResMan.GetString("roomFinishes_SelectRooms", Tools.Cult)))
            //{
            //    list.Add(this._doc.GetElement(reference) as Room);
            //}
            //return list;
        }


        private void button2_Click(object sender, EventArgs e)
        {//btn ok
         //if (Tools.GetValueFromString(this.Height_TextBox.Text).HasValue)

            //lam thu 1 room
            {
                try
                {
                    _joinWall = (bool)cbJoin.Checked;
                    this._boardHeight = lstWallHeight.First() /*GetValueFromString(this.Height_TextBox.Text).Value*/;
                    //if (this.WallTypeListBox.SelectedItem != null)
                    //gia su tat ca room da duoc khai bao. Lay height + name tu trong parameter. Sau do tim walltype Roi duplicate wall len
                    {
                        this._selectedWallType = /*this.WallTypeListBox.SelectedItem as WallType*/  lstWallTypes.Find(x => x.Name == lstWallName.First());
                        this._duplicatedWallType = this.CreateNewWallType(this._selectedWallType);
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                        this._selectedRooms = this.SelectRooms();

                    }
                }catch(Exception exx)
                {
                    System.Windows.Forms.MessageBox.Show(exx.ToString());   
                }
            }
            //else
            //{
            //    //TaskDialog.Show(Tools.LangResMan.GetString("roomFinishes_TaskDialogName", Tools.Cult), Tools.LangResMan.GetString("roomFinishes_heightValueError", Tools.Cult), 0x20, 8);
            //    //base.Activate();
            //}
        }
        private WallType CreateNewWallType(WallType wallType)
        {
            WallType type;
            if (!(from o in this._wallTypes select o.Name).ToList<string>().Contains("newWallTypeName"))
            {
                type = wallType.Duplicate("newWallTypeName") as WallType;
            }
            else
            {
                type = wallType.Duplicate("newWallTypeName2") as WallType;
            }
            CompoundStructure compoundStructure = type.GetCompoundStructure();
            IList<CompoundStructureLayer> layers = compoundStructure.GetLayers();
            int num = 0;
            foreach (CompoundStructureLayer layer in layers)
            {
                double num2 = layer.Width * 2.0;
                if (compoundStructure.GetRegionsAssociatedToLayer(num).Count == 1)
                {
                    try
                    {
                        compoundStructure.SetLayerWidth(num, num2);
                        goto Label_00E7;
                    }
                    catch
                    {
                        //throw new ErrorMessageException(Tools.LangResMan.GetString("roomFinishes_verticallyCompoundError", Tools.Cult));
                    }
                }
                //throw new ErrorMessageException(Tools.LangResMan.GetString("roomFinishes_verticallyCompoundError", Tools.Cult));
            Label_00E7:
                num++;
            }
            type.SetCompoundStructure(compoundStructure);
            return type;
        }

        public static double? GetValueFromString(string text)
        {
            string str;
            if (text.Contains(" mm"))
            {
                str = text.Replace(" mm", "");
            }
            else if (text.Contains("mm"))
            {
                str = text.Replace("mm", "");
            }
            else
            {
                str = text;
            }
            if (double.TryParse(str, out double num))
            {
                return new double?(num);
            }
            return null;
        }

    }
}

