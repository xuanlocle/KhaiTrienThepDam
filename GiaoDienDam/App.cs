#region Namespaces
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace GiaoDienDam
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            // Method to add Tab and Panel  
            RibbonPanel panel = ribbonPanel(a);
            RibbonPanel panel2 = ribbonPanel2(a);

            RibbonPanel panel3 = ribbonPanel2(a, "By STKa", "GDDNew");
            // Reflection to look for this assembly path 
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            // Add button to panel



            PushButtonData FinishButtonData = new PushButtonData("Button", "Khai triển Thép dầm GEM", thisAssemblyPath, "GiaoDienDam.Command");
            PushButton button = panel.AddItem(FinishButtonData) as PushButton;
            var globePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(thisAssemblyPath), "steal.png");
            Uri uriImage = new Uri(globePath);
            BitmapImage largeImage = new BitmapImage(uriImage);
            button.LargeImage = largeImage;
            button.Image = largeImage;
            //button.ToolTip = "";

            PushButtonData pushButtonData2 = new PushButtonData("Button", "Lọc theo TENCAUKIEN", thisAssemblyPath, "GiaoDienDam.CommandChonTheoTenCauKien");        
            PushButton button2 = panel2.AddItem(pushButtonData2) as PushButton;
            button2.ToolTip = "Chọn theo TENCAUKIEN";
            button2.LargeImage = largeImage;
            button2.Image = largeImage;

            PushButtonData FinishButtonData3 = new PushButtonData("Button", "Khai triển thép dầm METRO", thisAssemblyPath, "GiaoDienDam.CommandGiaoDienDam2");
            PushButton button3 = panel3.AddItem(FinishButtonData3) as PushButton;
            //var globePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(thisAssemblyPath), "steal.png");
            //Uri uriImage = new Uri(globePath);
            //BitmapImage largeImage = new BitmapImage(uriImage);
            button3.LargeImage = largeImage;
            button3.Image = largeImage;
            //button3.ToolTip = "Khai triển thép dầm NEW";


            PushButtonData itemData22 = new PushButtonData("copyRvSever", "Copy Revit Server", thisAssemblyPath, "GiaoDienDam.Openfile.cmdOpenFile");
            PushButton item22 = panel.AddItem(itemData22) as PushButton;
            item22.LargeImage = largeImage;
            item22.Image = largeImage;
            item22.ToolTip = "Copy file from revit server";

            PushButtonData itemData23 = new PushButtonData("FloorFinish", "FloorFinish", thisAssemblyPath, "GiaoDienDam.FloorFinish.FloorFinishes");
            PushButton item23 = panel.AddItem(itemData23) as PushButton;
            item23.LargeImage = largeImage;
            item23.Image = largeImage;
            item23.ToolTip = "FloorFinish";

            a.ApplicationClosing += a_ApplicationClosing;

            //Set Application to Idling
            a.Idling += a_Idling;

            return Result.Succeeded;
        }
        
        //*****************************a_Idling()*****************************
        void a_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {

        }

        //*****************************a_ApplicationClosing()*****************************
        void a_ApplicationClosing(object sender, Autodesk.Revit.UI.Events.ApplicationClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        //*****************************ribbonPanel()*****************************
        public RibbonPanel ribbonPanel(UIControlledApplication a)
        {
            string tab = "By STKa"; // Tab name
            // Empty ribbon panel 
            RibbonPanel ribbonPanel = null;
            //RibbonPanel ribbonPanel2 = null;
            // Try to create ribbon tab. 
            try
            {
                a.CreateRibbonTab(tab);
            }
            catch { }
            // Try to create ribbon panel.
            try
            {
                RibbonPanel panel = a.CreateRibbonPanel(tab, "Khai triển thép dầm");
                //RibbonPanel panel2 = a.CreateRibbonPanel(tab, "Chọn theo TENCAUKIEN");
            }
            catch { }
            // Search existing tab for your panel.
            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels)
            {
                if (p.Name == "Khai triển thép dầm")
                {
                    ribbonPanel = p;
                }
                //if (p.Name == "Chọn theo TENCAUKIEN")
                //    ribbonPanel += p;
            }
            //return panel 
            return ribbonPanel;
        }

        public RibbonPanel ribbonPanel2(UIControlledApplication a)
        {
            string tab = "By STKa"; // Tab name
            // Empty ribbon panel 
            RibbonPanel ribbonPanel = null;
            // Try to create ribbon tab. 
            try
            {
                a.CreateRibbonTab(tab);
            }
            catch { }
            // Try to create ribbon panel.
            try
            {
                RibbonPanel panel = a.CreateRibbonPanel(tab, "Lọc dầm");
                //RibbonPanel panel2 = a.CreateRibbonPanel(tab, "Chọn theo TENCAUKIEN");
            }
            catch { }
            // Search existing tab for your panel.
            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels)
            {
                //if (p.Name == "Khai triển thép dầm")
                //{
                //    ribbonPanel = p;
                //}
                if (p.Name == "Lọc dầm")
                    ribbonPanel = p;
            }
            //return panel 
            return ribbonPanel;
        }
        public RibbonPanel ribbonPanel2(UIControlledApplication a,string tabName,string panelName)
        {
            string tab = tabName; // Tab name
            // Empty ribbon panel 
            RibbonPanel ribbonPanel = null;
            // Try to create ribbon tab. 
            try
            {
                a.CreateRibbonTab(tab);
            }
            catch { }
            // Try to create ribbon panel.
            try
            {
                RibbonPanel panel = a.CreateRibbonPanel(tab, panelName);
                //RibbonPanel panel2 = a.CreateRibbonPanel(tab, "Chọn theo TENCAUKIEN");
            }
            catch { }
            // Search existing tab for your panel.
            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels)
            {
                //if (p.Name == "Khai triển thép dầm")
                //{
                //    ribbonPanel = p;
                //}
                if (p.Name == panelName)
                    ribbonPanel = p;
            }
            //return panel 
            return ribbonPanel;
        }


        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

      

        private static ImageSource RetriveImage(string imagePath)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imagePath);

            switch (imagePath.Substring(imagePath.Length - 3))
            {
                case "jpg":
                    var jpgDecoder = new System.Windows.Media.Imaging.JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    return jpgDecoder.Frames[0];
                case "bmp":
                    var bmpDecoder = new System.Windows.Media.Imaging.BmpBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    return bmpDecoder.Frames[0];
                case "png":
                    var pngDecoder = new System.Windows.Media.Imaging.PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    return pngDecoder.Frames[0];
                case "ico":
                    var icoDecoder = new System.Windows.Media.Imaging.IconBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    return icoDecoder.Frames[0];
                default:
                    return null;
            }
        }
    }

 



}
