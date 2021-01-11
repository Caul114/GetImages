
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.openViewTextBox = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.viewScaleTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewScaleButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(580, 524);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
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
            this.openFileDialog1.InitialDirectory = "C:\\Users\\Bold\\Documents\\GetImages\\Files da caricare";
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
            this.groupBox3.Size = new System.Drawing.Size(302, 302);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Caricamento Vista";
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
            // openViewTextBox
            // 
            this.openViewTextBox.Location = new System.Drawing.Point(21, 21);
            this.openViewTextBox.Name = "openViewTextBox";
            this.openViewTextBox.Size = new System.Drawing.Size(228, 22);
            this.openViewTextBox.TabIndex = 5;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.openViewTextBox);
            this.groupBox5.Location = new System.Drawing.Point(17, 162);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(267, 58);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ultima Vista caricata";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.viewScaleButton);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.viewScaleTextBox);
            this.groupBox7.Location = new System.Drawing.Point(17, 226);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(267, 58);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Modifica il valore della View Scale";
            // 
            // viewScaleTextBox
            // 
            this.viewScaleTextBox.Location = new System.Drawing.Point(69, 21);
            this.viewScaleTextBox.Name = "viewScaleTextBox";
            this.viewScaleTextBox.Size = new System.Drawing.Size(99, 22);
            this.viewScaleTextBox.TabIndex = 5;
            this.viewScaleTextBox.Text = "30";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Scale 1:";
            // 
            // viewScaleButton
            // 
            this.viewScaleButton.Location = new System.Drawing.Point(174, 21);
            this.viewScaleButton.Name = "viewScaleButton";
            this.viewScaleButton.Size = new System.Drawing.Size(75, 23);
            this.viewScaleButton.TabIndex = 7;
            this.viewScaleButton.Text = "Modifica";
            this.viewScaleButton.UseVisualStyleBackColor = true;
            this.viewScaleButton.Click += new System.EventHandler(this.viewScaleButton_Click);
            // 
            // ModelessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 584);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.exitButton);
            this.MaximizeBox = false;
            this.Name = "ModelessForm";
            this.Text = "BOLD Get Image";
            this.Load += new System.EventHandler(this.ModelessForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
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
        private TextBox openViewTextBox;
        private Button viewScaleButton;
    }
}