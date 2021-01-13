using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetImages
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    class CleanUpBackUpFiles : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string userMessage = null;
            List<string> backUpFiles = new List<string>();

            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult dr = folder.ShowDialog();

            if (dr != DialogResult.OK)
            {
                return Autodesk.Revit.UI.Result.Cancelled;
            }

            string[] files = Directory.GetFiles(folder.SelectedPath, "*.rfa");

            foreach (string f in files)
            {

                //if (Path.GetExtension(f) != BIM.Manager_Globals.Revit_FamilyExtension)
                //{
                //    return false;
                //}
                try
                {
                    if (Path.GetFileNameWithoutExtension(f).Contains("."))
                    {
                        string temp = Path.GetFileNameWithoutExtension(f);
                        int index = temp.IndexOf('.');

                        int result = Convert.ToInt16(Regex.Match(temp.Substring(index + 1), @"\d+").Value);

                        if (result >= 1 && result < 10000)
                        {
                            backUpFiles.Add(f);
                            userMessage += Path.GetFileName(f) + Environment.NewLine;
                        }
                    }
                }
                catch
                {
                }
            }

            if (0 == backUpFiles.Count)
            {
                MessageBox.Show("No Backup files found...",
                folder.SelectedPath, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return Autodesk.Revit.UI.Result.Cancelled;
            }

            dr = MessageBox.Show("Do you want to delete " + backUpFiles.Count + "no. files?.." + Environment.NewLine +
              Environment.NewLine + userMessage,
              folder.SelectedPath, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
            {
                return Autodesk.Revit.UI.Result.Cancelled;
            }

            string error = null;

            foreach (string f in backUpFiles)
            {
                try
                {
                    File.Delete(f);
                }
                catch
                {
                    error += Path.GetFileName(f) + Environment.NewLine;
                }
            }

            if (error != null)
            {
                MessageBox.Show("The following files could not be removed." + Environment.NewLine +
                Environment.NewLine + error,
                folder.SelectedPath, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(backUpFiles.Count + "no. files deleted.", folder.SelectedPath, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Autodesk.Revit.UI.Result.Cancelled;
        }
    }
}
