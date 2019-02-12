using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GiaoDienDam.FloorFinish
{
    [Transaction(TransactionMode.Manual)]
    class FloorFinishes : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument UIdoc = commandData.Application.ActiveUIDocument;
            Document doc = UIdoc.Document;

            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    // Add Your Code Here
                    FloorFinish(UIdoc, tx);
                    // Return Success
                    return Result.Succeeded;
                }

                catch (Autodesk.Revit.Exceptions.OperationCanceledException exceptionCanceled)
                {
                    message = exceptionCanceled.Message;
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Cancelled;
                }
                //catch (ErrorMessageException errorEx)
                //{
                //    // checked exception need to show in error messagebox
                //    message = errorEx.Message;
                //    if (tx.HasStarted())
                //    {
                //        tx.RollBack();
                //    }
                //    return Autodesk.Revit.UI.Result.Failed;
                //}
                catch (Exception ex)
                {
                    // unchecked exception cause command failed
                    message =/* Tools.LangResMan.GetString("floorFinishes_unexpectedError", Tools.Cult) +*/ ex.Message;
                    //Trace.WriteLine(ex.ToString());
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Failed;
                }
            }
        }

        void FloorFinish(UIDocument UIDoc, Transaction tx)
        {
            Document _doc = UIDoc.Document;

            tx.Start(/*Tools.LangResMan.GetString("floorFinishes_transactionName", Tools.Cult)*/ "floorfinishes_transactionName");

            //Load the selection form

            FloorFinishControl control = new FloorFinishControl(UIDoc);
            //userControl.InitializeComponent();

            if (control.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FloorType selectedFloorType = control.SelectedFloorType;
                foreach (Room room in control.SelectedRooms)
                {
                    if ((room != null) && (room.UnboundedHeight != 0.0))
                    {
                        double num;
                        if (control.RoomParameter == null)
                        {
                            num = control.FloorHeight * 0.00328084;
                        }
                        else
                        {
                            num = room.get_Parameter(control.RoomParameter.Definition).AsDouble();
                        }
                        //room.Name;
                        SpatialElementBoundaryOptions options = new SpatialElementBoundaryOptions();
                        options.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish;
                        IList<IList<Autodesk.Revit.DB.BoundarySegment>> boundarySegments = room.GetBoundarySegments(options);
                        CurveArray array = new CurveArray();
                        if (boundarySegments.Count != 0)
                        {
                            TaskDialog.Show("cnt", boundarySegments.Count + "");
                            foreach (Autodesk.Revit.DB.BoundarySegment segment in boundarySegments.First<IList<Autodesk.Revit.DB.BoundarySegment>>())
                            {
                                array.Append(segment.Curve);
                            }

                            //Create list curve for create opening
                            //var lstFamilyInRoom = GetFurniture(room);
                            //TaskDialog.Show("rv", lstFamilyInRoom.Count+"");
                            int cnt = 0;
                            List<CurveArray> lstCrvOpen = new List<CurveArray>();
                            CurveArray tcrv;

                            foreach (var lstboundSeg in boundarySegments)
                            {
                                if (cnt == 0) { cnt = 1; continue; }
                                tcrv = new CurveArray();
                                foreach (var boundSeg in lstboundSeg)
                                {
                                    tcrv.Append(boundSeg.Curve);
                                }
                                lstCrvOpen.Add(tcrv);
                            }
                            //end create opening

                            Level element = _doc.GetElement(room.LevelId) as Level;
                            Parameter param = room.get_Parameter(BuiltInParameter.ROOM_HEIGHT);
                            if (array.Size != 0)

                                Thread.Sleep(0x3e8);
                            //document.Create.NewFloor(array, selectedFloorType, element, false).get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM).Set(num);

                            Floor floor = _doc.Create.NewFloor(array, selectedFloorType, element, false);

                            //Create opening
                            _doc.Regenerate();
                            foreach (var k in lstCrvOpen)
                                _doc.Create.NewOpening(floor, k, false);
                            //eng Create opening
                            param = floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
                            param.Set(num);


                        }
                    }
                }
                tx.Commit();
            }
            else
            {
                tx.RollBack();
            }
            if(control.ShowDialog()== System.Windows.Forms.DialogResult.No)
            {
                control.Close();
            }
        }
    }
}
