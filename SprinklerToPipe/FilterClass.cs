using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI.Selection;

namespace SprinklerToPipe
{
    //sprinkler selection filter class
    class SprinklerSelectionFilter : ISelectionFilter
    {
        Document m_doc = null;
        //constructor
        public SprinklerSelectionFilter(Document doc)
        {
            m_doc = doc;
        }

        //allow independent tags to be selected
        public bool AllowElement(Element elem)
        {
            if (elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Sprinklers)
            {
                return true;
            }
            return false;
        }

        //allow all references to be selected
        public bool AllowReference(Reference refer, XYZ point)
        {
            return true;
        }
    }

    //pipe selection filter class
    class PipeSelectionFilter : ISelectionFilter
    {
        Document m_doc = null;
        //constructor
        public PipeSelectionFilter(Document doc)
        {
            m_doc = doc;
        }

        //allow independent tags to be selected
        public bool AllowElement(Element elem)
        {
            if (elem is Pipe)
            {
                return true;
            }
            return false;
        }

        //allow all references to be selected
        public bool AllowReference(Reference refer, XYZ point)
        {
            return true;
        }
    }
}