using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace GiaoDienDam.FloorFinish
{
    public partial class FloorFinishControl : System.Windows.Forms.Form
    {
        private Autodesk.Revit.DB.Document _doc;
        private UIDocument _UIDoc;

        private FloorType _selectedFloorType;
        public FloorType SelectedFloorType
        {
            get { return _selectedFloorType; }
        }

        private double _floorHeight;
        public double FloorHeight
        {
            get { return _floorHeight; }
        }

        private IEnumerable<Room> _selectedRooms;
        public IEnumerable<Room> SelectedRooms
        {
            get { return _selectedRooms; }
        }

        private Parameter _roomParameter;
        public Parameter RoomParameter
        {
            get { return _roomParameter; }
        }

        public FloorFinishControl(UIDocument UIDoc)
        {
            InitializeComponent();
            _doc = UIDoc.Document;
            _UIDoc = UIDoc;
            
            //Select the floor type in the document
            IEnumerable<FloorType> floorTypes = from elem in new FilteredElementCollector(_doc).OfClass(typeof(FloorType))
                                                let type = elem as FloorType
                                                where type.IsFoundationSlab == false
                                                select type;

            floorTypes = floorTypes.OrderBy(floorType => floorType.Name);

            // Bind ArrayList with the ListBox
            BindingSource bindingft = new BindingSource();
            bindingft.DataSource = floorTypes;
            listBoxFloorType.DataSource = bindingft;
            listBoxFloorType.DisplayMember = "Name";
            listBoxFloorType.SelectedItem = listBoxFloorType.Items[0];

            //Find a room
            IList<Element> roomList = new FilteredElementCollector(_doc).OfCategory(BuiltInCategory.OST_Rooms).ToList();

            if (roomList.Count != 0)
            {
                //Get all double parameters
                Room room = roomList.First() as Room;

                List<Parameter> doubleParam = (from Parameter p in room.Parameters
                                               where p.Definition.ParameterType == ParameterType.Length
                                               select p).ToList();

                BindingSource binding = new BindingSource();
                binding.DataSource = doubleParam;
                paramSelector.DataSource = binding;
                paramSelector.DisplayMember = "Definition.Name";
                paramSelector.SelectedIndex = 0;
            }
            else
            {
                paramSelector.Enabled = false;
                height_param_radio.Enabled = false;
            }
            
        }
        protected void Height_TextBox_LostFocus(object sender, EventArgs e)
        {
            if (GetValueFromString(Height_TextBox.Text, _doc.GetUnits()) != null)
            {
                _floorHeight = (double)GetValueFromString(Height_TextBox.Text, _doc.GetUnits());

                Height_TextBox.Text = UnitFormatUtils.Format(_doc.GetUnits(), UnitType.UT_Length, _floorHeight, true, true);
            }
            else
            {
                //TaskDialog.Show(Tools.LangResMan.GetString("roomFinishes_TaskDialogName", Tools.Cult),
                //    Tools.LangResMan.GetString("roomFinishes_heightValueError", Tools.Cult), TaskDialogCommonButtons.Close, TaskDialogResult.Close);
                this.Activate();
            }
        }

        private void FloorFinishControl_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (floor_height_radio.Checked == true)
            {
                _roomParameter = null;
                if (GetValueFromString(Height_TextBox.Text, _doc.GetUnits()) != null)
                {
                    _floorHeight = (double)GetValueFromString(Height_TextBox.Text, _doc.GetUnits());

                    if (listBoxFloorType.SelectedItem != null)
                    {
                        //Select wall type for skirting board
                        _selectedFloorType = listBoxFloorType.SelectedItem as FloorType;

                        //this.DialogResult = DialogResult.No;
                        //this.Close();
                        if (this.DialogResult == DialogResult.No) this.Close();


                        //Select the rooms
                        _selectedRooms = SelectRooms();
                    }
                }
                else
                {
                    TaskDialog.Show(/*Tools.LangResMan.GetString(*/"floorFinishes_TaskDialogName"/*, Tools.Cult)*/,
                        /*Tools.LangResMan.GetString(*/"floorFinishes_heightValueError"/*, Tools.Cult)*/, TaskDialogCommonButtons.Close, TaskDialogResult.Close);
                    this.Activate();
                }
            }
            else
            {
                _roomParameter = paramSelector.SelectedItem as Parameter;

                if (listBoxFloorType.SelectedItem != null)
                {
                    //Select floor type
                    _selectedFloorType = listBoxFloorType.SelectedItem as FloorType;

                    //this.DialogResult = DialogResult.No;
                    this.Close();

                    //Select the rooms
                    _selectedRooms = SelectRooms();
                }
            }
        }
        private IEnumerable<Room> SelectRooms()
        {
            //Create a set of selected elements ids
            ICollection<ElementId> selectedObjectsIds = _UIDoc.Selection.GetElementIds();

            //Create a set of rooms
            IEnumerable<Room> ModelRooms = null;
            IList<Room> tempList = new List<Room>();
            
            if (all_rooms_radio.Checked)
            {
                // Find all rooms in current view
                ModelRooms = from elem in new FilteredElementCollector(_doc, _doc.ActiveView.Id).OfClass(typeof(SpatialElement))
                             let room = elem as Room
                             select room;
            }
            else
            {
                if (selectedObjectsIds.Count != 0)
                {
                    // Find all rooms in selection
                    ModelRooms = from elem in new FilteredElementCollector(_doc, selectedObjectsIds).OfClass(typeof(SpatialElement))
                                 let room = elem as Room
                                 select room;
                    tempList = ModelRooms.ToList();
                }

                if (tempList.Count == 0)
                {
                    //Create a selection filter on rooms
                    ISelectionFilter filter = new RoomSelectionFilter();

                    IList<Reference> rs = _UIDoc.Selection.PickObjects(ObjectType.Element, filter,
                        /*Tools.LangResMan.GetString("roomFinishes_SelectRooms", Tools.Cult)*/ "roomFinishes_SelectRooms");

                    foreach (Reference r in rs)
                    {
                        tempList.Add(_doc.GetElement(r) as Room);
                    }

                    ModelRooms = tempList;
                }
            }

            return ModelRooms;
        }
        public class RoomSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                if (element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Rooms)
                {
                    return true;
                }
                return false;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public static double? GetValueFromString(string text, Units units)
        {
            //Check the string value
            string heightValueString = text;
            double lenght;

            if (Autodesk.Revit.DB.UnitFormatUtils.TryParse(units, UnitType.UT_Length, heightValueString, out lenght))
            {
                return lenght;
            }
            else
            {
                return null;
            }
        }

        private void height_param_radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (!rd.Checked)
                paramSelector.Enabled = false;
            else
                paramSelector.Enabled = true;
        }
    }
}
