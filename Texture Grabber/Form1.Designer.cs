namespace Texture_Grabber
{
    partial class Form1
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
            this.listViewTags = new System.Windows.Forms.ListView();
            this.buttonSearchTag = new System.Windows.Forms.Button();
            this.textBoxSearchTag = new System.Windows.Forms.TextBox();
            this.textBoxStringTag = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelSearchTag = new System.Windows.Forms.Label();
            this.radioButtonProduction = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.pictureBoxItem = new Texture_Grabber.InterpolatedBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItem)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewTags
            // 
            this.listViewTags.Location = new System.Drawing.Point(12, 36);
            this.listViewTags.MultiSelect = false;
            this.listViewTags.Name = "listViewTags";
            this.listViewTags.Size = new System.Drawing.Size(271, 199);
            this.listViewTags.TabIndex = 0;
            this.listViewTags.UseCompatibleStateImageBehavior = false;
            this.listViewTags.View = System.Windows.Forms.View.List;
            this.listViewTags.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // buttonSearchTag
            // 
            this.buttonSearchTag.Location = new System.Drawing.Point(426, 5);
            this.buttonSearchTag.Name = "buttonSearchTag";
            this.buttonSearchTag.Size = new System.Drawing.Size(118, 27);
            this.buttonSearchTag.TabIndex = 1;
            this.buttonSearchTag.Text = "Search";
            this.buttonSearchTag.UseVisualStyleBackColor = true;
            this.buttonSearchTag.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxSearchTag
            // 
            this.textBoxSearchTag.Location = new System.Drawing.Point(50, 7);
            this.textBoxSearchTag.Name = "textBoxSearchTag";
            this.textBoxSearchTag.Size = new System.Drawing.Size(370, 23);
            this.textBoxSearchTag.TabIndex = 2;
            // 
            // textBoxStringTag
            // 
            this.textBoxStringTag.Location = new System.Drawing.Point(289, 212);
            this.textBoxStringTag.Name = "textBoxStringTag";
            this.textBoxStringTag.ReadOnly = true;
            this.textBoxStringTag.Size = new System.Drawing.Size(255, 23);
            this.textBoxStringTag.TabIndex = 4;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "png";
            this.saveFileDialog1.Filter = "PNG files|*.png|All files|*.*";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // labelSearchTag
            // 
            this.labelSearchTag.AutoSize = true;
            this.labelSearchTag.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSearchTag.Location = new System.Drawing.Point(12, 10);
            this.labelSearchTag.Name = "labelSearchTag";
            this.labelSearchTag.Size = new System.Drawing.Size(32, 15);
            this.labelSearchTag.TabIndex = 5;
            this.labelSearchTag.Text = "Tag: ";
            // 
            // radioButtonProduction
            // 
            this.radioButtonProduction.AutoSize = true;
            this.radioButtonProduction.Location = new System.Drawing.Point(12, 241);
            this.radioButtonProduction.Name = "radioButtonProduction";
            this.radioButtonProduction.Size = new System.Drawing.Size(80, 19);
            this.radioButtonProduction.TabIndex = 12;
            this.radioButtonProduction.TabStop = true;
            this.radioButtonProduction.Text = "Production";
            this.radioButtonProduction.UseVisualStyleBackColor = true;
            this.radioButtonProduction.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 266);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(63, 19);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Testing";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // pictureBoxItem
            // 
            this.pictureBoxItem.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pictureBoxItem.BackgroundImage = global::TextureGrabber.Properties.Resources.Transparent;
            this.pictureBoxItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxItem.Interpolation = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.pictureBoxItem.Location = new System.Drawing.Point(289, 37);
            this.pictureBoxItem.Name = "pictureBoxItem";
            this.pictureBoxItem.Size = new System.Drawing.Size(255, 167);
            this.pictureBoxItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxItem.TabIndex = 11;
            this.pictureBoxItem.TabStop = false;
            this.pictureBoxItem.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonSearchTag;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 289);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButtonProduction);
            this.Controls.Add(this.pictureBoxItem);
            this.Controls.Add(this.labelSearchTag);
            this.Controls.Add(this.textBoxStringTag);
            this.Controls.Add(this.textBoxSearchTag);
            this.Controls.Add(this.buttonSearchTag);
            this.Controls.Add(this.listViewTags);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Texture Grabber";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewTags;
        private System.Windows.Forms.Button buttonSearchTag;
        private System.Windows.Forms.TextBox textBoxSearchTag;
        private System.Windows.Forms.TextBox textBoxStringTag;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label labelSearchTag;
        private InterpolatedBox pictureBoxItem;
        private System.Windows.Forms.RadioButton radioButtonProduction;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}

