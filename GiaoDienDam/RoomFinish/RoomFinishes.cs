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

namespace GiaoDienDam.RoomFinish
{
    [Transaction(TransactionMode.Manual)]
    class RoomFinishes : IExternalCommand
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
                    RoomFinish(UIdoc, tx);
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

        public class PlintePreprocessor : IFailuresPreprocessor
        {
            public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
            {
                // Inside event handler, get all warnings
                IList<FailureMessageAccessor> failList = failuresAccessor.GetFailureMessages();
                foreach (FailureMessageAccessor failure in failList)
                {
                    // check FailureDefinitionIds against ones that you want to dismiss,
                    FailureDefinitionId failId = failure.GetFailureDefinitionId();
                    // prevent Revit from showing Unenclosed room warnings
                    if (failId == BuiltInFailures.OverlapFailures.WallsOverlap)
                    {
                        failuresAccessor.DeleteWarning(failure);
                    }
                }

                return FailureProcessingResult.Continue;
            }
        }

        void RoomFinish(UIDocument uiDoc, Transaction tx)
        {
            //Document doc = uiDoc.Document;
            //try
            //{
            //    tx.Start(/*Tools.LangResMan.GetString(*/"roomFinishes_transactionName"/*, Tools.Cult)*/);

            //    //Load the selection form
            //    RoomsFinishesControl userControl = new RoomsFinishesControl(uiDoc);
            //    //userControl.InitializeComponent();

            //    if (userControl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {

            //        //Select wall types
            //        WallType plinte = userControl.SelectedWallType;
            //        WallType newWallType = userControl.DuplicatedWallType;

            //        //Get all finish properties
            //        double height = userControl.BoardHeight;
            //        //double baseOffset = userControl.BoardBaseOffset;
            //        //double topOffset = userControl.BoardTopOffset;
            //        //ElementId levelTop = (ElementId)userControl.cmbTopLevel.SelectedValue;

            //        //Select Rooms in model
            //        IEnumerable<Room> modelRooms = userControl.SelectedRooms;

            //        Dictionary<ElementId, ElementId> skirtingDictionary = new Dictionary<ElementId, ElementId>();
            //        List<KeyValuePair<Wall, Wall>> addedWalls = new List<KeyValuePair<Wall, Wall>>();

            //        //Loop on all rooms to get boundaries
            //        foreach (Room currentRoom in modelRooms)
            //        {
            //            ElementId roomLevelId = currentRoom.LevelId;

            //            SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();
            //            opt.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish;

            //            IList<IList<Autodesk.Revit.DB.BoundarySegment>> boundarySegmentArray = currentRoom.GetBoundarySegments(opt);
            //            if (null == boundarySegmentArray)  //the room may not be bound
            //            {
            //                continue;
            //            }

            //            foreach (IList<Autodesk.Revit.DB.BoundarySegment> boundarySegArr in boundarySegmentArray)
            //            {
            //                if (0 == boundarySegArr.Count)
            //                {
            //                    continue;
            //                }
            //                else
            //                {
            //                    //TaskDialog.Show("count", boundarySegmentArray.Count+"");
            //                    foreach (Autodesk.Revit.DB.BoundarySegment boundarySegment in boundarySegArr)
            //                    {
            //                        //Check if the boundary is a room separation lines
            //                        //Element boundaryElement = doc.GetElement(boundarySegment.Element.Id);

            //                        if (boundaryElement == null) { continue; }

            //                        Categories categories = doc.Settings.Categories;
            //                        Category RoomSeparetionLineCat = categories.get_Item(BuiltInCategory.OST_RoomSeparationLines);

            //                        if (boundaryElement.Category.Id != RoomSeparetionLineCat.Id)
            //                        {
            //                            Wall currentWall = Wall.Create(doc, boundarySegment.Curve, newWallType.Id, roomLevelId, height, 0, false, false);
            //                            Parameter wallJustification = currentWall.get_Parameter(BuiltInParameter.WALL_KEY_REF_PARAM);
            //                            Parameter wallTopcontrain = currentWall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE);

            //                            //wallTopcontrain.Set(levelTop);
            //                            //TaskDialog.Show("lv", levelTop.IntegerValue + " " + levelTop.ToString());

            //                            //Parameter wallBaseOffset = currentWall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
            //                            //Parameter wallTopOffset = currentWall.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET);
            //                            wallJustification.Set(2);
            //                            //wallBaseOffset.Set(baseOffset);
            //                            //wallTopOffset.Set(topOffset);
            //                            skirtingDictionary.Add(currentWall.Id, boundarySegment.Element.Id);


            //                            //TaskDialog.Show("boundaryElement True", boundaryElement.Id + "");
            //                        }
            //                    }
            //                }
            //            }

            //        }

            //        FailureHandlingOptions options = tx.GetFailureHandlingOptions();

            //        options.SetFailuresPreprocessor(new PlintePreprocessor());
            //        // Now, showing of any eventual mini-warnings will be postponed until the following transaction.
            //        tx.Commit(options);

            //        tx.Start(/*Tools.LangResMan.GetString(*/"roomFinishes_transactionName"/*, Tools.Cult)*/);

            //        List<ElementId> addedIds = new List<ElementId>(skirtingDictionary.Keys);
            //        foreach (ElementId addedSkirtingId in addedIds)
            //        {
            //            if (doc.GetElement(addedSkirtingId) == null)
            //            {
            //                skirtingDictionary.Remove(addedSkirtingId);
            //            }
            //        }

            //        Wall.ChangeTypeId(doc, skirtingDictionary.Keys, plinte.Id);

            //        //Join both wall
            //        if (userControl.JoinWall)
            //        {
            //            foreach (ElementId skirtingId in skirtingDictionary.Keys)
            //            {
            //                Wall skirtingWall = doc.GetElement(skirtingId) as Wall;

            //                if (skirtingWall != null)
            //                {
            //                    Parameter wallJustification = skirtingWall.get_Parameter(BuiltInParameter.WALL_KEY_REF_PARAM);
            //                    wallJustification.Set(3);
            //                    Wall baseWall = doc.GetElement(skirtingDictionary[skirtingId]) as Wall;

            //                    if (baseWall != null)
            //                    {
            //                        JoinGeometryUtils.JoinGeometry(doc, skirtingWall, baseWall);
            //                    }
            //                }
            //            }
            //        }

            //        doc.Delete(newWallType.Id);

            //        tx.Commit();
            //    }
            //    else
            //    {
            //        tx.RollBack();
            //    }
            //}catch(Exception exx)
            //{
            //    TaskDialog.Show("err",exx.ToString());
            //}
        }
    }
}
