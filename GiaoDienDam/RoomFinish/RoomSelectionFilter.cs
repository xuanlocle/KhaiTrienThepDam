using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienDam.RoomFinish
{
    public class RoomSelectionFilter : ISelectionFilter
    {
        // Methods
        public bool AllowElement(Element element) =>
            (element.Category.Id.IntegerValue == -2_000_160);

        public bool AllowReference(Reference refer, XYZ point) =>
            false;
    }

}
