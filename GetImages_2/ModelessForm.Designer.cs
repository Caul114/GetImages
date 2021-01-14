
using System.Drawing;
using System.Windows.Forms;

namespace GetImages_2
{
    partial class ModelessForm
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
            this.exitButton = new System.Windows.Forms.Button();
            this.getFilebutton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openViewButton = new System.Windows.Forms.Button();
            this.exportViewbutton = new System.Windows.Forms.Button();
            this.getFileTextBox = new System.Windows.Forms.TextBox();
            this.exportViewTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.visualStyleComboBox = new System.Windows.Forms.ComboBox();
            this.detailLevelComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.viewScaleTextBox = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.openViewComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.clearEditFile = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(187, 63);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(93, 32);
            this.exitButton.TabIndex = 0;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click_1);
            // 
            // getFilebutton
            // 
            this.getFilebutton.Location = new System.Drawing.Point(197, 20);
            this.getFilebutton.Name = "getFilebutton";
            this.getFilebutton.Size = new System.Drawing.Size(228, 121);
            this.getFilebutton.TabIndex = 1;
            this.getFilebutton.Text = "Scegli il File";
            this.getFilebutton.UseVisualStyleBackColor = true;
            this.getFilebutton.Click += new System.EventHandler(this.getFilebutton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "rfa files (*.rfa)|*.rfa|All files (*.*)|*.*";
            this.openFileDialog1.InitialDirectory = "C:\\Users\\Bold\\Documents\\Bold Software\\GetImages\\Files da caricare";
            // 
            // openViewButton
            // 
            this.openViewButton.Location = new System.Drawing.Point(38, 31);
            this.openViewButton.Name = "openViewButton";
            this.openViewButton.Size = new System.Drawing.Size(228, 121);
            this.openViewButton.TabIndex = 2;
            this.openViewButton.Text = "Apri una Vista";
            this.openViewButton.UseVisualStyleBackColor = true;
            this.openViewButton.Click += new System.EventHandler(this.openViewButton_Click);
            // 
            // exportViewbutton
            // 
            this.exportViewbutton.Location = new System.Drawing.Point(33, 31);
            this.exportViewbutton.Name = "exportViewbutton";
            this.exportViewbutton.Size = new System.Drawing.Size(228, 121);
            this.exportViewbutton.TabIndex = 3;
            this.exportViewbutton.Text = "Esporta l\'immagine";
            this.exportViewbutton.UseVisualStyleBackColor = true;
            this.exportViewbutton.Click += new System.EventHandler(this.exportViewbutton_Click);
            // 
            // getFileTextBox
            // 
            this.getFileTextBox.Location = new System.Drawing.Point(21, 21);
            this.getFileTextBox.Name = "getFileTextBox";
            this.getFileTextBox.Size = new System.Drawing.Size(538, 22);
            this.getFileTextBox.TabIndex = 4;
            // 
            // exportViewTextBox
            // 
            this.exportViewTextBox.Location = new System.Drawing.Point(17, 21);
            this.exportViewTextBox.Name = "exportViewTextBox";
            this.exportViewTextBox.Size = new System.Drawing.Size(228, 22);
            this.exportViewTextBox.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.getFilebutton);
            this.groupBox1.Location = new System.Drawing.Point(38, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(617, 218);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Caricamento File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.getFileTextBox);
            this.groupBox2.Location = new System.Drawing.Point(17, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(580, 55);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ultimo file caricato";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.openViewButton);
            this.groupBox3.Location = new System.Drawing.Point(38, 270);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 363);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Caricamento Vista";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.visualStyleComboBox);
            this.groupBox7.Controls.Add(this.detailLevelComboBox);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.viewScaleTextBox);
            this.groupBox7.Location = new System.Drawing.Point(17, 226);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(267, 124);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Modifica i Valori della View";
            // 
            // visualStyleComboBox
            // 
            this.visualStyleComboBox.FormattingEnabled = true;
            this.visualStyleComboBox.Location = new System.Drawing.Point(117, 89);
            this.visualStyleComboBox.Name = "visualStyleComboBox";
            this.visualStyleComboBox.Size = new System.Drawing.Size(132, 24);
            this.visualStyleComboBox.TabIndex = 12;
            this.visualStyleComboBox.LostFocus += new System.EventHandler(this.visualStyleComboBox_LostFocus);
            // 
            // detailLevelComboBox
            // 
            this.detailLevelComboBox.FormattingEnabled = true;
            this.detailLevelComboBox.Location = new System.Drawing.Point(117, 58);
            this.detailLevelComboBox.Name = "detailLevelComboBox";
            this.detailLevelComboBox.Size = new System.Drawing.Size(132, 24);
            this.detailLevelComboBox.TabIndex = 11;
            this.detailLevelComboBox.LostFocus += new System.EventHandler(this.detailLevelComboBox_LostFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "- Visual Style";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "- Detail Level";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "- Scale     1:";
            // 
            // viewScaleTextBox
            // 
            this.viewScaleTextBox.Location = new System.Drawing.Point(117, 29);
            this.viewScaleTextBox.Name = "viewScaleTextBox";
            this.viewScaleTextBox.Size = new System.Drawing.Size(72, 22);
            this.viewScaleTextBox.TabIndex = 5;
            this.viewScaleTextBox.Text = "30";
            this.viewScaleTextBox.LostFocus += new System.EventHandler(this.viewScaleTextBox_LostFocus);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.openViewComboBox);
            this.groupBox5.Location = new System.Drawing.Point(17, 162);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(267, 58);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ultima Vista caricata";
            // 
            // openViewComboBox
            // 
            this.openViewComboBox.FormattingEnabled = true;
            this.openViewComboBox.Location = new System.Drawing.Point(21, 21);
            this.openViewComboBox.Name = "openViewComboBox";
            this.openViewComboBox.Size = new System.Drawing.Size(228, 24);
            this.openViewComboBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.exportViewbutton);
            this.groupBox4.Location = new System.Drawing.Point(353, 270);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(302, 235);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Salvataggio File";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.exportViewTextBox);
            this.groupBox6.Location = new System.Drawing.Point(16, 162);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(266, 58);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Ultimo File salvato";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.groupBox9);
            this.groupBox8.Controls.Add(this.exitButton);
            this.groupBox8.Location = new System.Drawing.Point(355, 514);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(300, 119);
            this.groupBox8.TabIndex = 10;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Utilities";
            // 
            // clearEditFile
            // 
            this.clearEditFile.Location = new System.Drawing.Point(32, 42);
            this.clearEditFile.Name = "clearEditFile";
            this.clearEditFile.Size = new System.Drawing.Size(93, 32);
            this.clearEditFile.TabIndex = 1;
            this.clearEditFile.Text = "Cancella";
            this.clearEditFile.UseVisualStyleBackColor = true;
            this.clearEditFile.Click += new System.EventHandler(this.clearEditFile_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.clearEditFile);
            this.groupBox9.Location = new System.Drawing.Point(14, 21);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(158, 85);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Cancella File modificati";
            // 
            // ModelessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 643);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "GetImages";
            this.Text = "BOLD Get Images";
            this.Load += new System.EventHandler(this.ModelessForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Button exitButton;
        private Button getFilebutton;
        private OpenFileDialog openFileDialog1;
        private Button openViewButton;
        private Button exportViewbutton;
        private TextBox getFileTextBox;
        private TextBox exportViewTextBox;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private Label label1;
        private TextBox viewScaleTextBox;
        private GroupBox groupBox5;
        private Label label3;
        private Label label2;
        private ComboBox visualStyleComboBox;
        private ComboBox detailLevelComboBox;
        private ComboBox openViewComboBox;
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private Button clearEditFile;
    }
}