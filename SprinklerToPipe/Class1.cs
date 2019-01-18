using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

using Security_Check;

namespace SprinklerToPipe
{
    //Transaction assigned as automatic
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Automatic)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]

    //Creating an external command to add TYP to dimensions
    public class SprinklerToPipe : IExternalCommand
    {
        bool security = false;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                //call to the security check method to check for authentication
                security = SecurityLNT.Security_Check();

                if (security == false)
                {
                    return Result.Succeeded;
                }

                UIDocument m_document = commandData.Application.ActiveUIDocument;

                IList<Reference> selectedObjects = m_document.Selection.PickObjects(ObjectType.Element, 
                    new SprinklerSelectionFilter(m_document.Document), "Select sprinklers to connect with pipe.");

                MessageBox.Show(selectedObjects.Count.ToString());

                Reference pipeRefer = m_document.Selection.PickObject(ObjectType.Element, 
                    new PipeSelectionFilter(m_document.Document), "Select the pipe to which sprinklers are to be connected.");
                MessageBox.Show(m_document.Document.GetElement(pipeRefer).Id.ToString());

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;

            }
        }
    }
}
