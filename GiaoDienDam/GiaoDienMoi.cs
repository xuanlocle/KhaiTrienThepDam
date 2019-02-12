using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiaoDienDam;

namespace GiaoDienDam
{

    [Transaction(TransactionMode.Manual)]
    class GiaoDienMoi : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            //Subscribe to the FailuresProcessing Event
            uiApp.Application.FailuresProcessing += new EventHandler<Autodesk.Revit.DB.Events.FailuresProcessingEventArgs>(FailuresProcessing);


            using (Transaction tx = new Transaction(doc))
            {
                    // Add Your Code Here
                    Main(uiDoc, tx);
                    //Unsubscribe to the FailuresProcessing Event
                    uiApp.Application.FailuresProcessing -= FailuresProcessing;
                    // Return Success
                    return Result.Succeeded;
                
               
            }
        }

        void Main(UIDocument uiDoc, Transaction tx)
        {
            Document doc = uiDoc.Document;

            tx.Start("GDMoi");

            //Load the selection form
            GiaoDienMoiControl userControl = new GiaoDienMoiControl(uiDoc);
            userControl.InitializeComponent();    
            
            if(userControl.ShowDialog() == true)
            {
                tx.Commit(tx.GetFailureHandlingOptions());
            }
            else
            {
                tx.RollBack();
            }
        }

        /// <summary>
        /// Implements the FailuresProcessing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FailuresProcessing(object sender, Autodesk.Revit.DB.Events.FailuresProcessingEventArgs e)
        {
            FailuresAccessor failuresAccessor = e.GetFailuresAccessor();
            //failuresAccessor
            String transactionName = failuresAccessor.GetTransactionName();

            IList<FailureMessageAccessor> failures = failuresAccessor.GetFailureMessages();

            if (failures.Count != 0)
            {
                foreach (FailureMessageAccessor f in failures)
                {
                    FailureDefinitionId id = f.GetFailureDefinitionId();

                    if (id == BuiltInFailures.JoinElementsFailures.CannotJoinElementsError)
                    {
                        // only default option being choosen,  not good enough!
                        //failuresAccessor.DeleteWarning(f);
                        failuresAccessor.ResolveFailure(f);
                        //failuresAccessor.
                        e.SetProcessingResult(FailureProcessingResult.ProceedWithCommit);
                    }
                    return;
                }
            }

        }
    }
}
