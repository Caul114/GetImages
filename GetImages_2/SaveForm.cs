using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetImages_2
{
    public partial class SaveForm : Form
    {
        #region Private data members
        // Dichiarazione della Form principale di GetImages ModelessForm
        private ModelessForm _modelessForm;

        // Inizializza la variabile per il path della cartella Images
        private string _pathImagesFolder = "";

        // Inizializza la variabile per il nuovo path della cartella Images
        private string _newPathImagesFolder = "newPath";

        #endregion

        #region Class public property
        /// <summary>
        /// Proprietà pubblica per accedere al valore della richiesta corrente
        /// </summary>
        public string NewPathImagesFolder
        {
            get { return _newPathImagesFolder; }
        }
        #endregion

        /// <summary>
        /// Costruttore della Form
        /// </summary>
        public SaveForm()
        {
            InitializeComponent();

            // Inizializzo l'istanza di RequestHandler 
            _pathImagesFolder = ModelessForm.thisModForm.ImagesPath;            
            textBox1.Text = _pathImagesFolder;
        }

        /// <summary>
        /// Metodo che definisce cosa accade quando si fa MouseUp sul textBox
        /// </summary>
        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                textBox1.Copy();
            }
        }

        /// <summary>
        /// Metodo che definisce cosa accade al Salvataggio del nuovo percorso
        /// </summary>
        private void changeSavebutton_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostra la FolderBrowserDialog.
                DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    // Ottiene il nuovo Path della cartellaI mages
                    _newPathImagesFolder = folderBrowserDialog1.SelectedPath;

                    // Scrive il nuovo Path nel Path di default
                    _pathImagesFolder = _newPathImagesFolder;

                    // Chiama il metodo in ModelessForm che imposta il nuovo Path delle immagini
                    ModelessForm.thisModForm.SetNewImagesPath();

                    // Scrive il nuovo Path nel File .json di configurazione
                    Json fileJson = new Json();
                    fileJson.UpdateJson(2, 1, "ImagesFolderPath", _pathImagesFolder);

                    // Chiude questa Form e attiva la Form ModelessForm
                    this.Close();
                    _modelessForm = App.thisApp.RetriveForm();
                    _modelessForm.WakeUp();
                    _modelessForm.BringToFront();
                }
            }
            catch (SecurityException ex)
            {
                MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
            }
            
        }


        private void saveForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            // Inizializza ModelessForm e la attiva
            _modelessForm = App.thisApp.RetriveForm();
            _modelessForm.WakeUp();
            _modelessForm.BringToFront();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.Close();

            // Inizializza ModelessForm e la attiva
            _modelessForm = App.thisApp.RetriveForm();
            _modelessForm.WakeUp();
            _modelessForm.BringToFront();
        }
    }
}
