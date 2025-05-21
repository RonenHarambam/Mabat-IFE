
namespace DLLTester
{
    partial class UserControlEnumParametersField
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
            this.lblParametersName = new System.Windows.Forms.Label();
            this.lblParametersType = new System.Windows.Forms.Label();
            this.cbEnumValues = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblParametersName
            // 
            this.lblParametersName.AutoSize = true;
            this.lblParametersName.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblParametersName.Location = new System.Drawing.Point(222, 7);
            this.lblParametersName.Name = "lblParametersName";
            this.lblParametersName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblParametersName.Size = new System.Drawing.Size(144, 25);
            this.lblParametersName.TabIndex = 1;
            this.lblParametersName.Text = "very very long";
            // 
            // lblParametersType
            // 
            this.lblParametersType.AutoSize = true;
            this.lblParametersType.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblParametersType.Location = new System.Drawing.Point(372, 7);
            this.lblParametersType.Name = "lblParametersType";
            this.lblParametersType.Size = new System.Drawing.Size(190, 25);
            this.lblParametersType.TabIndex = 2;
            this.lblParametersType.Text = "very very very long";
            // 
            // cbEnumValues
            // 
            this.cbEnumValues.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEnumValues.FormattingEnabled = true;
            this.cbEnumValues.Location = new System.Drawing.Point(3, 3);
            this.cbEnumValues.Name = "cbEnumValues";
            this.cbEnumValues.Size = new System.Drawing.Size(213, 33);
            this.cbEnumValues.TabIndex = 3;
            // 
            // UserControlEnumParametersField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbEnumValues);
            this.Controls.Add(this.lblParametersType);
            this.Controls.Add(this.lblParametersName);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlEnumParametersField";
            this.Size = new System.Drawing.Size(570, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblParametersName;
        private System.Windows.Forms.Label lblParametersType;

        public string GetParameterValue()
        {
            return cbEnumValues.SelectedItem.ToString();
        }

        public void SetParameterType(string type)
        {
            lblParametersType.Text = type;
        }

        public void SetParameterName(string name)
        {
            lblParametersName.Text = name;
        }

        private System.Windows.Forms.ComboBox cbEnumValues;
    }
}
