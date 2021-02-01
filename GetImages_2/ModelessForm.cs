using Autodesk.Revit.UI;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

using Excel = Microsoft.Office.Interop.Excel;

namespace GetImages_2
{
    /// <summary>
    /// La classe della nostra finestra di dialogo non modale.
    /// </summary>
    /// <remarks>
    /// Oltre ad altri metodi, ha un metodo per ogni pulsante di comando. 
    /// In ognuno di questi metodi non viene fatto nient'altro che il sollevamento
    /// di un evento esterno con una richiesta specifica impostata nel gestore delle richieste.
    /// </remarks>
    /// 
    public partial class ModelessForm : Form
    {
        // In questo esempio, la finestra di dialogo possiede il gestore e gli oggetti evento, 
        // ma non è un requisito. Potrebbero anche essere proprietà statiche dell'applicazione.

        #region Private data members

        private RequestHandler m_Handler;
        private ExternalEvent m_ExEvent;

        // Dichiaro un instanza di questa form
        public static ModelessForm thisModForm = null;

        // Percorso del singolo file .json da importare di default
        private string _pathFileGetImagesConfig = @"\BOLD Software\GetImages\ConfigGetImagesPath.json";
        private string _pathFileTxt = "";

        // List dei nomi delle View
        private List<string> viewNames = new List<string>() { "Exterior", "Interior", "Left", "Right" };

        // Valore del path attivo
        private string _filePath = "";

        // Stabilisce il percorso di salvataggio delle immagini
        //private string _imagesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BOLD Software\GetImages\Images";
        private string _imagesPath = "";

        // Valore dall'utente nella TextBox della View Scale modificato 
        private int _scaleEdit = 0;

        // Lista dei valori del Livello di Dettaglio della View
        private List<string> _detailLevels = new List<string>() { "Coarse", "Medium", "Fine" };

        // Valore scelto dall'utente nella comboBox del Livello di Dettaglio della View
        private string _detailLevelEdit = "";
        private int _detailLevelIndex = 1;

        // Lista dei valori dello Stile di Visualizzazione della View
        private List<string> _visualStyles = new List<string>() { "Wireframe", "Hidden Line", "Shaded", "Shaded with Edges", "Consistent Colors", "Realistic" };

        // Valore scelto dall'utente nella comboBox dello Stile di Visualizzazione della View
        private string _visualStyleEdit = "";
        private int _visualStyleIndex = 2;

        // Dichiaro la Form del cambio percorso della Cartella delle Immagini
        private SaveForm _saveForm;

        // Metodo di controllo booleano nel caso in cui il pecorso della cartella immagini nel file .json non sia corretto
        private bool _notSaveFolder = false;

        #endregion

        #region Class public property
        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public String PathFileGetImagesConfig
        {
            get { return _pathFileGetImagesConfig; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public String ImagesPath
        {
            get { return _imagesPath; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public int ScaleView
        {
            get { return _scaleEdit; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public string DetailLevel
        {
            get { return _detailLevelEdit; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public int DetailLevelIndex
        {
            get { return _detailLevelIndex; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public string VisualStyle
        {
            get { return _visualStyleEdit; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public int VisualStyleIndex
        {
            get { return _visualStyleIndex; }
        }

        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public bool NotSaveFolder
        {
            get { return _notSaveFolder; }
        }
        #endregion

        /// <summary>
        ///   Costruttore della finestra di dialogo
        /// </summary>
        /// 
        public ModelessForm(ExternalEvent exEvent, RequestHandler handler)
        {
            InitializeComponent();
            m_Handler = handler;
            m_ExEvent = exEvent;

            // Riempie l'istanza di questa classe con la Form ModelessForm
            thisModForm = this;

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                @"\OneDrive - BOLD\BOLD Software\Repository for Plugin\GetImages\Files da caricare";

            // Verifica se il Path delle Immagini esista e lo importa
            GetFileTxt();
            if(_imagesPath == "")
            {
                MessageBox.Show("La cartella Images non è stato caricata."
                        + "\nSegui questa procedura per caricarla dal percorso che preferisci.");

                // Apre la Form SaveForm
                OpenSaveForm();
                _notSaveFolder = true;
            }

            // Imposta il TextBox della View Scale
            viewScaleTextBox.Text = Convert.ToString(m_Handler.Scale);

            // Imposta la ComboBox del Detail Level
            foreach (var item in _detailLevels)
            {
                detailLevelComboBox.Items.Add(item);
            }
            _detailLevelEdit = _detailLevels[0];
            detailLevelComboBox.Text = _detailLevelEdit;

            // Imposta la ComboBox del Visual style
            foreach (var item in _visualStyles)
            {
                visualStyleComboBox.Items.Add(item);
            }
            _visualStyleEdit = _visualStyles[1];
            visualStyleComboBox.Text = _visualStyleEdit;
        }

        /// <summary>
        /// Modulo gestore eventi chiuso
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // possediamo sia l'evento che il gestore
            // dovremmo eliminarlo prima di chiudere la finestra

            m_ExEvent.Dispose();
            m_ExEvent = null;
            m_Handler = null;

            // non dimenticare di chiamare la classe base
            base.OnFormClosed(e);
        }

        /// <summary>
        ///   Attivatore / disattivatore del controllo
        /// </summary>
        ///
        private void EnableCommands(bool status)
        {
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = status;
            }
            if (!status)
            {
                this.exitButton.Enabled = true;
            }
        }

        /// <summary>
        ///   Un metodo di supporto privato per effettuare una richiesta 
        ///   e allo stesso tempo mettere la finestra di dialogo in sospensione.
        /// </summary>
        /// <remarks>
        ///   Ci si aspetta che il processo che esegue la richiesta 
        ///   (l'helper Idling in questo caso particolare) 
        ///   riattivi anche la finestra di dialogo dopo aver terminato l'esecuzione.
        /// </remarks>
        ///
        private void MakeRequest(RequestId request)
        {
            App.thisApp.DontShowFormTop();
            m_Handler.Request.Make(request);
            m_ExEvent.Raise();
            DozeOff();
        }


        /// <summary>
        ///   DozeOff -> disabilita tutti i controlli (tranne il pulsante Esci)
        /// </summary>
        /// 
        private void DozeOff()
        {
            EnableCommands(false);
        }

        /// <summary>
        ///   WakeUp -> abilita tutti i controlli
        /// </summary>
        /// 
        public void WakeUp()
        {
            EnableCommands(true);
        }

        /// <summary>
        ///   Metodo di interazione con la finestra di dialogo
        /// </summary>
        /// 
        private void ModelessForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///   Verifica se il file .json di configurazione esista o meno
        /// </summary>
        /// 
        public void GetFileTxt()
        {
            _pathFileTxt = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + _pathFileGetImagesConfig;

            if(File.Exists(_pathFileTxt))
            {
                // Legge il .json dal file
                string jsonText = File.ReadAllText(_pathFileTxt);
                if (jsonText != "")
                {
                    var traduction = JsonConvert.DeserializeObject<IList<JsonData>>(jsonText);
                    JsonData singleItem = traduction.FirstOrDefault(x => x.Id == 2);
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + singleItem.Path))
                    {
                        _imagesPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + singleItem.Path;
                    } 
                    else if(singleItem.Path.Contains(":\\"))
                    {
                        _imagesPath = singleItem.Path;
                    }
                }
            }
            else
            {
                _imagesPath = "";
            }
        }

        /// <summary>
        ///   Metodo che imposta il nuovo Path delle Immagini
        /// </summary>
        /// 
        public void SetNewImagesPath()
        {
            _imagesPath = _saveForm.NewPathImagesFolder;
        }

        /// <summary>
        ///   Metodo che apre il FILE da importare
        /// </summary>
        /// 
        private void getFilebutton_Click(object sender, EventArgs e)
        {
            // Apro il nuovo file .rfa
            MakeRequest(RequestId.File);
        }

        /// <summary>
        ///   Metodo che apre la VIEW da visualizzare
        /// </summary>
        /// 
        private void openViewButton_Click(object sender, EventArgs e)
        {
            MakeRequest(RequestId.View);
        }

        /// <summary>
        ///   Metodo che sceglie l'elemento attivo nella ComboBox e mostra la View relativa
        /// </summary>
        /// 
        private void openViewComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MakeRequest(RequestId.SingleView);
        }

        public string ViewStringRequest()
        {
            // Ottiene la stringa selezionata nella ComboBox
            string selectedItem = (string)openViewComboBox.SelectedItem;
            return selectedItem;
        }


        public void AssignValueComboBox()
        {
            openViewComboBox.Text = m_Handler.ViewShowed;
        }

        public void AssignValueComboBoxDefault()
        {
            openViewComboBox.Items.Clear();
            foreach (string name in viewNames)
            {
                openViewComboBox.Items.Add(name);
            }
            openViewComboBox.Text = " - ";
        }

        private void exportViewbutton_Click(object sender, EventArgs e)
        {
            MakeRequest(RequestId.Export);        
        }

        public string GetPath()
        {
            var fileContent = string.Empty;
            _filePath = string.Empty;
            if(m_Handler.NewPathFamily)
            {
                openFileDialog1.InitialDirectory = m_Handler.PathDirectoryName;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Get the path of specified file
                    _filePath = openFileDialog1.FileName;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
            else
            {
                MessageBox.Show("Non hai selezionato nessun file.");
            }
            return _filePath;
        }

        /// <summary>
        ///   Ritorna l'ultimo path utilizzato
        /// </summary>
        /// 
        public void LastFileOpened()
        {
            getFileTextBox.Text = m_Handler.FileNameWithoutExtension;
            exportViewTextBox.Text = "";
        }

        /// <summary>
        ///   Ritorna l'ultimo path utilizzato
        /// </summary>
        /// 
        public void LastViewOpened()
        {
            //openViewTextBox.Text = m_Handler.ViewShowed;
        }

        /// <summary>
        ///   Ritorna l'ultimo path utilizzato
        /// </summary>
        /// 
        public void LastViewExported()
        {
            exportViewTextBox.Text = m_Handler.ExportedView;
        }

        /// <summary>
        ///   Comando LostFocus relativo alla TextBox della View Scale
        /// </summary>
        private void viewScaleTextBox_LostFocus(object sender, EventArgs e)
        {
            MakeRequest(RequestId.ViewScaleId);
        }


        /// <summary>
        ///   Ritorna ogni modifica fatta nella TextBox della View Scale
        /// </summary>
        public void ViewScaleTextBox()
        {
            string text = viewScaleTextBox.Text;
            int scaleBase = m_Handler.Scale;
            if (int.TryParse(text, out scaleBase))
            {
                _scaleEdit = Convert.ToInt32(text);
            }
            else
            {
                MessageBox.Show("Non hai inserito un valore corretto.\nInserisci un numero > 0.");
            }
        }

        /// <summary>
        ///   Comando LostFocus relativo alla ComboBox Detail Level della View 
        /// </summary>
        private void detailLevelComboBox_LostFocus(object sender, EventArgs e)
        {
            MakeRequest(RequestId.DetailLevelId);
        }

        /// <summary>
        ///   Ritorna ogni modifica fatta nella ComboBox Detail Level della View 
        /// </summary>
        public string DetailLevelComboBox()
        {
            _detailLevelEdit = detailLevelComboBox.SelectedItem as string;
            _detailLevelIndex = detailLevelComboBox.SelectedIndex + 1;
            return _detailLevelEdit;
        }

        /// <summary>
        ///   Comando LostFocus relativo alla ComboBox Visual Style della View 
        /// </summary>
        private void visualStyleComboBox_LostFocus(object sender, EventArgs e)
        {
            MakeRequest(RequestId.VisualStyleId);
        }

        /// <summary>
        ///   Ritorna ogni modifica fatta nella ComboBox Visual Style della View 
        /// </summary>
        public string VisualStyleComboBox()
        {
            _visualStyleEdit = visualStyleComboBox.SelectedItem as string;
            _visualStyleIndex = visualStyleComboBox.SelectedIndex + 1;
            return _visualStyleEdit;
        }

        /// <summary>
        ///   Apre la Form che permette di cambiare la cartella di salvataggio delle immagini
        /// </summary>
        private void saveImagesFolderButton_Click(object sender, EventArgs e)
        {
            // Inizializzo la Form del cambio percorso della Cartella delle Immagini
            this.DozeOff();
            this.SendToBack();
            _saveForm = new SaveForm();
            _saveForm.Show();
            _saveForm.TopMost = true;
        }

        /// <summary>
        ///   Apre la form che permette di caricare la cartella di salvataggio delle immagini
        /// </summary>
        private void OpenSaveForm()
        {
            // Inizializzo la Form del cambio percorso della Cartella delle Immagini
            this.DozeOff();
            this.SendToBack();
            _saveForm = new SaveForm();
            _saveForm.Show();
            _saveForm.TopMost = true;
        }

        /// <summary>
        ///   Apre la cartella Immagini
        /// </summary>
        private void openImagesFolderButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(_imagesPath))
            {
                Process.Start(_imagesPath);
            }
            else
            {
                // Apri il folderBrowserDialog
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if(result == DialogResult.OK)
                {
                    _imagesPath = folderBrowserDialog1.SelectedPath;
                    Process.Start(_imagesPath);
                }
            }
        }

        /// <summary>
        ///   Comando di chiusura della Form
        /// </summary>
        public void CloseForm()
        {
            Close();
        }

        /// <summary>
        ///   Ripristina i percorsi di default del Plug-in
        /// </summary>
        private void defaultImagesFolderButton_Click(object sender, EventArgs e)
        {
            string defaultImagesFolder = @"\OneDrive - BOLD\BOLD Software\Repository for Plugin\GetImages\Images";
            _imagesPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + defaultImagesFolder;

            // Scrive il Path di default della cartella Images nel File .json di configurazione
            Json fileJson = new Json();
            fileJson.UpdateJson(2, 1, "ImagesFolderPath", defaultImagesFolder);

            // avvisa che l'operazione è stata effettuata
            MessageBox.Show("Il ripristino dei Percorsi è andato a buon fine.");
        }

        /// <summary>
        ///   Cancella i file che sono stati modificati nelle operazioni precedenti
        /// </summary>
        private void clearEditFile_Click(object sender, EventArgs e)
        {
            // Cancella i File modificati
            MakeRequest(RequestId.Delete);
        }

        /// <summary>
        ///   Exit - chiude la finestra di dialogo
        /// </summary>
        /// 
        private void exitButton_Click(object sender, EventArgs e)
        {
            MakeRequest(RequestId.Esc);
        }


    }  // class
}
