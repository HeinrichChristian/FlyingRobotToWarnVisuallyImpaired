namespace FollowMe
{
    partial class HuePickerForm
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
            this.huePicker1 = new AForge.Controls.HuePicker();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // huePicker1
            // 
            this.huePicker1.Location = new System.Drawing.Point(38, 26);
            this.huePicker1.Name = "huePicker1";
            this.huePicker1.Size = new System.Drawing.Size(209, 209);
            this.huePicker1.TabIndex = 12;
            this.huePicker1.Text = "huePicker";
            this.huePicker1.Type = AForge.Controls.HuePicker.HuePickerType.Range;
            // 
            // HuePickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 264);
            this.Controls.Add(this.huePicker1);
            this.Name = "HuePickerForm";
            this.Text = "Hue Picker";
            this.ResumeLayout(false);

        }

        #endregion

        private AForge.Controls.HuePicker huePicker1;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}