using Autodesk.Revit.UI;
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

        private RequestHandler m_Handler;
        private ExternalEvent m_ExEvent;

        // List dei nomi delle View
        private List<string> viewNames = new List<string>() { "Exterior", "Interior", "Left", "Right" };

        // Valore del path attivo
        private string _filePath;

        // Valore dall'utente nella TextBox della View Scale modificato 
        private int _scaleEdit;

        // Lista dei valori del Livello di Dettaglio della View
        private List<string> _detailLevels = new List<string>() { "Coarse", "Medium", "Fine" };

        // Valore scelto dall'utente nella comboBox del Livello di Dettaglio della View
        private string _detailLevelEdit;
        private int _detailLevelIndex = 1;

        // Lista dei valori dello Stile di Visualizzazione della View
        private List<string> _visualStyles = new List<string>() { "Wireframe", "Hidden Line", "Shaded", "Shaded with Edges", "Consistent Colors", "Realistic" };

        // Valore scelto dall'utente nella comboBox dello Stile di Visualizzazione della View
        private string _visualStyleEdit;
        private int _visualStyleIndex = 2;

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



        private void getFilebutton_Click(object sender, EventArgs e)
        {
            // Apro il nuovo file .rfa
            MakeRequest(RequestId.File);
            foreach (string name in viewNames)
            {
                openViewComboBox.Items.Add(name);
            }
            openViewComboBox.Text = " - ";
        }

        private void openViewButton_Click(object sender, EventArgs e)
        {
            MakeRequest(RequestId.View);
        }

        public void AssignValueComboBox()
        {
            openViewComboBox.Text = m_Handler.ViewShowed;
        }

        public void AssignValueComboBoxDefault()
        {
            openViewComboBox.Items.Clear();
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
        ///   Cancella i file che sono stati modificati nelle operazioni precedenti
        /// </summary>
        private void clearEditFile_Click(object sender, EventArgs e)
        {
            // Cancella i File modificati
            m_Handler.DeleteFileModified();
            MessageBox.Show("Hai cancellato i file modificati.");
        }

        /// <summary>
        ///   Exit - chiude la finestra di dialogo
        /// </summary>
        /// 
        private void exitButton_Click_1(object sender, EventArgs e)
        {
            MakeRequest(RequestId.Esc);
        }

        public void CloseForm()
        {
            Close();
        }
    }  // class
}
