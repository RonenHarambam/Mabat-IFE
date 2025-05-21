
namespace DLLTester
{
    partial class UserControlParametersField
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mtValue = new System.Windows.Forms.MaskedTextBox();
            this.lblParametersName = new System.Windows.Forms.Label();
            this.lblParametersType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mtValue
            // 
            this.mtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.mtValue.Location = new System.Drawing.Point(4, 4);
            this.mtValue.Name = "mtValue";
            this.mtValue.Size = new System.Drawing.Size(212, 31);
            this.mtValue.TabIndex = 0;
            // 
            // lblParametersName
            // 
            this.lblParametersName.AutoSize = true;
            this.lblParametersName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblParametersName.Location = new System.Drawing.Point(222, 7);
            this.lblParametersName.Name = "lblParametersName";
            this.lblParametersName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblParametersName.Size = new System.Drawing.Size(147, 25);
            this.lblParametersName.TabIndex = 1;
            this.lblParametersName.Text = "very very long";
            // 
            // lblParametersType
            // 
            this.lblParametersType.AutoSize = true;
            this.lblParametersType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblParametersType.Location = new System.Drawing.Point(375, 7);
            this.lblParametersType.Name = "lblParametersType";
            this.lblParametersType.Size = new System.Drawing.Size(194, 25);
            this.lblParametersType.TabIndex = 2;
            this.lblParametersType.Text = "very very very long";
            // 
            // UserControlParametersField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblParametersType);
            this.Controls.Add(this.lblParametersName);
            this.Controls.Add(this.mtValue);
            this.Name = "UserControlParametersField";
            this.Size = new System.Drawing.Size(570, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox mtValue;
        private System.Windows.Forms.Label lblParametersName;
        private System.Windows.Forms.Label lblParametersType;

        public string GetParameterValue()
        {
            return mtValue.Text;
        }

        public void SetParameterType(string type)
        {
            lblParametersType.Text = type;
        }

        public void SetParameterName(string name)
        {
            lblParametersName.Text = name;
        }


    }
}
