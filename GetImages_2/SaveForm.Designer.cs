
using System;

namespace GetImages_2
{
    partial class SaveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.changeSavebutton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(66, 72);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(520, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "<- Vuoto ->";
            this.textBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Percorso attuale in cui è salvata la cartella Images:";
            // 
            // changeSavebutton
            // 
            this.changeSavebutton.BackColor = System.Drawing.Color.PaleGreen;
            this.changeSavebutton.Location = new System.Drawing.Point(137, 134);
            this.changeSavebutton.Name = "changeSavebutton";
            this.changeSavebutton.Size = new System.Drawing.Size(176, 33);
            this.changeSavebutton.TabIndex = 2;
            this.changeSavebutton.Text = "Cambia Percorso";
            this.changeSavebutton.UseVisualStyleBackColor = false;
            this.changeSavebutton.Click += new System.EventHandler(this.changeSavebutton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.PaleVioletRed;
            this.exitButton.Location = new System.Drawing.Point(422, 134);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(87, 33);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Scegli la Directory in cui vuoi salvare le immagini.";
            this.folderBrowserDialog1.SelectedPath = "C:\\Users\\Bold\\DocumentsC:\\BOLD Software\\GetImages\\Images";
            // 
            // SaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 218);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.changeSavebutton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SaveForm";
            this.Text = "Percorso della Cartella Images";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.saveForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button changeSavebutton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}