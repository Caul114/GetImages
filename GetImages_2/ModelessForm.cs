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

        // Valore del TextBox della View Scale modificato 
        private int _scaleEdit;

        #region Class public property
        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public int ScaleView
        {
            get { return _scaleEdit; }
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

        private void getFilebutton_Click(object sender, EventArgs e)
        {
            MakeRequest(RequestId.File);
        }

        private void openViewButton_Click(object sender, EventArgs e)
        {
            MakeRequest(RequestId.View);
        }

        private void exportViewbutton_Click(object sender, EventArgs e)
        {
            MakeRequest(RequestId.Export);        
        }

        public string GetPath()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Get the path of specified file
                    filePath = openFileDialog1.FileName;
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
            return filePath;
        }

        /// <summary>
        ///   Ritorna l'ultimo path utilizzato
        /// </summary>
        /// 
        public void LastFileOpened()
        {
            getFileTextBox.Text = m_Handler.PathName;
            openViewTextBox.Text = "";
            exportViewTextBox.Text = "";
        }

        /// <summary>
        ///   Ritorna l'ultimo path utilizzato
        /// </summary>
        /// 
        public void LastViewOpened()
        {
            openViewTextBox.Text = m_Handler.ViewShowed;
        }

        /// <summary>
        ///   Ritorna l'ultimo path utilizzato
        /// </summary>
        /// 
        public void LastViewExported()
        {
            exportViewTextBox.Text = m_Handler.ExportedView;
        }


        private void viewScaleButton_Click(object sender, EventArgs e)
        {
            string text = viewScaleTextBox.Text;
            _scaleEdit = Convert.ToInt32(text);
        }
    }  // class
}
