//
// (C) Copyright 2003-2017 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GetImages_2
{
    /// <summary>
    ///   Una classe con metodi per eseguire le richieste effettuate dall'utente della finestra di dialogo.
    /// </summary>
    /// 
    public class RequestHandler : IExternalEventHandler  // Un'istanza di una classe che implementa questa interfaccia verrà registrata prima con Revit e ogni volta che viene generato l'evento esterno corrispondente, verrà richiamato il metodo Execute di questa interfaccia.
    {
        #region Private data members
        // Il valore dell'ultima richiesta effettuata dal modulo non modale
        private Request m_request = new Request();

        // Un instanza della finestra di dialogo
        private ModelessForm modelessForm;

        // La stringa da riempire con la Path
        private static string _path = "";

        // Stabilisce il percorso di salvataggio delle imaagini
        private string _desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Bold Software\GetImages\Images";

        // Ricava il nome del file da salvare
        private static string _pathName = Path.GetFileNameWithoutExtension(_path);

        // Percorso temporaneo della cartella dei file salvati
        private static string _dirpath = @"C:\Users\Bold\Documents\Bold Software\GetImages\File modificati";

        // Percorso temporaneo dei file salvati
        private static string _filepath = "";

        // La stringa che memorizza l'ultima view vista
        private string _imageViewed = "";

        // La stringa con il nome dell'ultimo file salvato
        private string _exportedView;

        // Valore contenuto nella View Scale
        private int _scale = 30;

        // Valore booleano che mi dice se cancellare o meno un file
        private bool _clear = false;

        #endregion

        #region Class public property
        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public Request Request
        {
            get { return m_request; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public String PathName
        {
            get { return _path; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public String ViewShowed
        {
            get { return _imageViewed; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public String ExportedView
        {
            get { return _exportedView; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public int Scale
        {
            get { return _scale; }
        }
        #endregion

        #region Class public method
        /// <summary>
        /// Costruttore di default di RequestHandler
        /// </summary>
        public RequestHandler()
        {
            // Costruisce i membri dei dati per le proprietà
            
        }
        #endregion

        /// <summary>
        ///   Un metodo per identificare questo gestore di eventi esterno
        /// </summary>
        public String GetName()
        {
            return "R2014 External Event Sample";
        }

        /// <summary>
        ///   Il metodo principale del gestore di eventi.
        /// </summary>
        /// <remarks>
        ///   Viene chiamato da Revit dopo che è stato generato l'evento esterno corrispondente 
        ///   (dal modulo non modale) e Revit ha raggiunto il momento in cui potrebbe 
        ///   chiamare il gestore dell'evento (cioè questo oggetto)
        /// </remarks>
        /// 
        public void Execute(UIApplication uiapp)
        {
            try
            {
                switch (Request.Take())
                {
                    case RequestId.None:
                        {
                            return;  // no request at this time -> we can leave immediately
                        }
                    case RequestId.File:
                        {
                            // Cancella i file
                            DeleteFileModified();
                            // Chiama la Form
                            modelessForm = App.thisApp.RetriveForm();
                            // Ottiene il Path del file da importare
                            _path = modelessForm.GetPath();
                            // Apre il file selezionato
                            if(_path != "")
                            {
                                OpenFile(uiapp, _path);
                                // Scrive nel TextBox l'ultima operazione effettuata
                                modelessForm.LastFileOpened();
                            }
                            break;
                        }
                    case RequestId.View:
                        {
                            // Apro la vista selezionata
                            OpenView(uiapp);
                            break;
                        }
                    case RequestId.Export:
                        {
                            // Definisce il nome del path
                            _pathName = Path.GetFileNameWithoutExtension(_path);
                            // Chiama la Form
                            modelessForm = App.thisApp.RetriveForm();
                            // Richiama il valore della View Scale
                            int value = modelessForm.ScaleView;
                            if (value != _scale && value != 0)
                            {
                                _scale = value;
                            }
                            // Cambio la scala della vista attiva
                            ChangeScale(uiapp, _path);
                            // Cambio il livello di dettaglio della vista attiva
                            ChangeDetailLevel(uiapp, _path);
                            // Cambio il livello di dettaglio della vista attiva
                            ChangeVisualStyle(uiapp, _path);
                            // Salvo le modifiche effettuate sulla vista
                            SaveChanges(uiapp);
                            // Esporta la View attiva 
                            ExportViewActive(uiapp);
                            // Mostra nel TextBox l'ultima view esportata
                            modelessForm.LastViewExported();
                            break;
                        }
                    case RequestId.Esc:
                        {
                            // Chiama la Form
                            modelessForm = App.thisApp.RetriveForm();
                            // Chiude la Form
                            modelessForm.CloseForm();
                            // Chiude il documento .rfa ancora aperto
                            if(_path.Contains(".rfa"))
                            {
                                CloseDocByCommand(uiapp);
                            }
                            break;
                        }
                    case RequestId.Scale:

                        break;
                    default:
                        {
                            // Una sorta di avviso qui dovrebbe informarci di una richiesta imprevista
                            break;
                        }
                }
            }
            finally
            {
                App.thisApp.WakeFormUp();
                App.thisApp.ShowFormTop();
            }
            return;
        }



        /// <summary>
        ///   Metodo che apre il file selezionato
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="uiapp">L'oggetto Applicazione di Revit</param>m>
        /// 
        private void OpenFile(UIApplication uiapp, string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                TaskDialog.Show("Carica il file .rfa", "Non trovo il file " + fullPath);
            }
            else
            {
                // Apre il nuovo documento
                uiapp.OpenAndActivateDocument(fullPath);
            }
        }

        /// <summary>
        /// Metodo che salva il file modificato in un certo percorso
        /// </summary>
        private void SaveChanges(UIApplication uiapp)
        {
            _filepath= _dirpath + "\\" + _pathName + ".rfa";

            // Salva le modifiche al file
            Document doc = uiapp.ActiveUIDocument.Document;

            SaveAsOptions opt = new SaveAsOptions
            {
                OverwriteExistingFile = true
            };

            doc.SaveAs(_filepath, opt);
        }

        /// <summary>
        /// Metodo che cancella il file in un certo percorso
        /// </summary>
        private void DeleteFile(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                var files = Directory.EnumerateFiles(dirPath, "*.rfa");

                if(files.Count() > 0)
                {
                    List<string> backUpFiles = new List<string>();

                    foreach (string file in files)
                    {
                        backUpFiles.Add(file);
                    }

                    foreach (string file in backUpFiles)
                    {
                        File.Delete(file);
                    }

                }
            }
        }

        /// <summary>
        /// Metodo per la modifica della Scala della View
        /// </summary>
        private void ChangeScale(UIApplication uiapp, string path)
        {
            Autodesk.Revit.DB.View viewActive = uiapp.ActiveUIDocument.Document.ActiveView;
            Document doc = viewActive.Document;

            // Cambia la View Scale della View attiva
            using (Transaction tsx = new Transaction(doc))
            {
                tsx.Start("Change the View Scale");

                doc.ActiveView.get_Parameter(
                      BuiltInParameter.VIEW_SCALE)
                        .Set(_scale);

                tsx.Commit();
            }

            //// metodo che salva il file in un percorso 
            //SaveChanges(uiapp);
        }

        /// <summary>
        /// Metodo per la modifica della Livello di Dettaglio della View
        /// </summary>
        private void ChangeDetailLevel(UIApplication uiapp, string path)
        {
            Autodesk.Revit.DB.View viewActive = uiapp.ActiveUIDocument.Document.ActiveView;
            Document doc = viewActive.Document;

            // Cambia il Livello di Dettaglio della View attiva
            using (Transaction tsx = new Transaction(doc))
            {
                tsx.Start("Change the View Detail Level");

                doc.ActiveView.get_Parameter(
                      BuiltInParameter.VIEW_DETAIL_LEVEL)
                        .Set(modelessForm.DetailLevelIndex);

                tsx.Commit();
            }

            //// metodo che salva il file in un percorso 
            //SaveChanges(uiapp);
        }

        /// <summary>
        /// Metodo per la modifica dello Stile di Visualizzazione della View
        /// </summary>
        private void ChangeVisualStyle(UIApplication uiapp, string path)
        {
            Autodesk.Revit.DB.View viewActive = uiapp.ActiveUIDocument.Document.ActiveView;
            Document doc = viewActive.Document;

            // Cambia lo Stile di Visualizzazione della View attiva
            using (Transaction tsx = new Transaction(doc))
            {
                tsx.Start("Change the Visual Style Level");

                doc.ActiveView.get_Parameter(
                      BuiltInParameter.MODEL_GRAPHICS_STYLE)
                        .Set(modelessForm.VisualStyleIndex);

                tsx.Commit();
            }

            //// metodo che salva il file in un percorso 
            //SaveChanges(uiapp);
        }

        /// <summary>
        /// Metodo per l'apertura della view
        /// </summary>
        private void OpenView(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Autodesk.Revit.DB.View viewActive = doc.ActiveView;

            FilteredElementCollector viewCollector = new FilteredElementCollector(doc);
            viewCollector.OfClass(typeof(Autodesk.Revit.DB.View));

            // Verifico se il file si apre su Exterior e nel caso salto direttamente all'esportazione dell'immagine
            string nameViewActive = viewActive.Name;
            if (nameViewActive == "Exterior" && _imageViewed == "")
            {
                _imageViewed = nameViewActive;
                // Chiama la Form
                modelessForm = App.thisApp.RetriveForm();
                // Mostra nella combobox la vista attiva
                modelessForm.AssignValueComboBox();
                // Salvo ed esco
                uidoc.SaveAndClose();
            }

            foreach (Autodesk.Revit.DB.View viewElement in viewCollector)
            {
                var name = viewElement.Name;                
                //string[] elevations = new string[] { "Exterior", "Interior", "Left", "Right" };

                switch(name)
                {
                    case "Exterior":
                        if(nameViewActive != "Exterior" && nameViewActive != "Interior" && nameViewActive != "Left" && nameViewActive != "Right")
                        {
                            uidoc.RequestViewChange(viewElement);
                            _imageViewed = name;
                            // Chiama la Form
                            modelessForm = App.thisApp.RetriveForm();
                            // Mostra nella combobox la vista attiva
                            modelessForm.AssignValueComboBox();
                            // Salvo ed esco
                            uidoc.SaveAndClose();                            
                        }
                        break;
                    case "Interior":
                        if (nameViewActive != "Interior" && nameViewActive != "Left" && nameViewActive != "Right")
                        {
                            uidoc.RequestViewChange(viewElement);
                            _imageViewed = name;
                            // Chiama la Form
                            modelessForm = App.thisApp.RetriveForm();
                            // Mostra nella combobox la vista attiva
                            modelessForm.AssignValueComboBox();
                            // Salvo ed esco
                            uidoc.SaveAndClose();
                        }
                        break;
                    case "Left":
                        if (nameViewActive != "Left" && nameViewActive != "Right")
                        {
                            uidoc.RequestViewChange(viewElement);
                            _imageViewed = name;
                            // Chiama la Form
                            modelessForm = App.thisApp.RetriveForm();
                            // Mostra nella combobox la vista attiva
                            modelessForm.AssignValueComboBox();
                            // Salvo ed esco
                            uidoc.SaveAndClose();
                        }
                        break;
                    case "Right":
                        if (nameViewActive != "Right")
                        {
                            uidoc.RequestViewChange(viewElement);
                            _imageViewed = name;
                            // Chiama la Form
                            modelessForm = App.thisApp.RetriveForm();
                            // Mostra nella combobox la vista attiva
                            modelessForm.AssignValueComboBox();
                            // Salvo ed esco
                            uidoc.SaveAndClose();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Metodo per l'esportazione della view attiva
        /// </summary>
        private void ExportViewActive(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Recupera la view attiva
            Autodesk.Revit.DB.View viewActive = doc.ActiveView;

            var name = viewActive.Name;

            // Esporta il file immagine a seconda del nome della view
            if (name == "Exterior" || name == "Interior" || name == "Right" || name == "Left")
            {
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Export Image");

                    // Attribuisce al path il nome del file corretto a seconda del nome della view
                    string filepath = "";
                    switch(name)
                    {
                        case "Exterior":
                            filepath = Path.Combine(_desktop_path, _pathName + "_F.png");
                            break;
                        case "Interior":
                            filepath = Path.Combine(_desktop_path, _pathName + "_P.png");
                            break;
                        case "Right":
                            filepath = Path.Combine(_desktop_path, _pathName + "_R.png");
                            break;
                        case "Left":
                            filepath = Path.Combine(_desktop_path, _pathName + "_L.png");
                            break;
                    }

                    // Visualizza l'ultimo file salvato
                    _exportedView = Path.GetFileName(filepath);

                    ImageExportOptions img = new ImageExportOptions();

                    img.ZoomType = ZoomFitType.FitToPage;
                    img.PixelSize = 640;
                    img.ImageResolution = ImageResolution.DPI_600;
                    img.FitDirection = FitDirectionType.Horizontal;
                    img.ExportRange = ExportRange.CurrentView;
                    img.HLRandWFViewsFileType = ImageFileType.PNG;
                    img.FilePath = filepath;
                    img.ShadowViewsFileType = ImageFileType.PNG;

                    // Esporta l'immagine viewActive con le specifiche salvate
                    doc.ExportImage(img);

                    tx.RollBack();

                    //// Cambia l'estensione dell'immagine in .png
                    //filepath = Path.ChangeExtension(filepath, "png");

                    // Mostra l'immagine salvata
                    //Process.Start(filepath);

                    // Chiude il documento nel caso in cui abbia prodotto l'ultima immagine
                    if(name == "Right")
                    {
                        MessageBox.Show("Hai salvato correttamente le 4 Viste del File");
                        _imageViewed = "";
                        CloseDocByCommand(uiapp);
                    }
                }
            }   
        }

        /// <summary>
        ///   Metodo che chiude il documento attivo
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="uiapp">L'oggetto Applicazione di Revit</param>
        private void CloseDocByCommand(UIApplication uiapp)
        {
            Document doc = uiapp.ActiveUIDocument.Document;

            // Dà il comando di chiusura del documento aperto
            RevitCommandId closeDoc
              = RevitCommandId.LookupPostableCommandId(
                PostableCommand.Close);
            uiapp.PostCommand(closeDoc);

            // Assegno alla comboBox della View il valore predefinito
            modelessForm.AssignValueComboBoxDefault();
        }

        /// <summary>
        ///   Metodo che cancella tutti i file modificati contenuti nela cartella di Default
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="uiapp">L'oggetto Applicazione di Revit</param>
        public void DeleteFileModified()
        {
            //_clear = true;

            // Cancella o meno gli eventuali file modificati
            if (!_clear)
            {
                DeleteFile(_dirpath);
            }
        }

    }  // class

}  // namespace

