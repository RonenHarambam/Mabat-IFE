
namespace DLLTester
{
    partial class s
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
            this.cbFunctionList = new System.Windows.Forms.ComboBox();
            this.flpParameters = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExecuteFunction = new System.Windows.Forms.Button();
            this.lblResponse = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbFunctionList
            // 
            this.cbFunctionList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cbFunctionList.FormattingEnabled = true;
            this.cbFunctionList.Location = new System.Drawing.Point(13, 22);
            this.cbFunctionList.Name = "cbFunctionList";
            this.cbFunctionList.Size = new System.Drawing.Size(350, 33);
            this.cbFunctionList.TabIndex = 0;
            // 
            // flpParameters
            // 
            this.flpParameters.Location = new System.Drawing.Point(369, 13);
            this.flpParameters.Name = "flpParameters";
            this.flpParameters.Size = new System.Drawing.Size(676, 338);
            this.flpParameters.TabIndex = 1;
            // 
            // btnExecuteFunction
            // 
            this.btnExecuteFunction.Location = new System.Drawing.Point(12, 305);
            this.btnExecuteFunction.Name = "btnExecuteFunction";
            this.btnExecuteFunction.Size = new System.Drawing.Size(173, 46);
            this.btnExecuteFunction.TabIndex = 2;
            this.btnExecuteFunction.Text = "Execute";
            this.btnExecuteFunction.UseVisualStyleBackColor = true;
            this.btnExecuteFunction.Click += new System.EventHandler(this.BtnExecuteFunction_Click);
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblResponse.Location = new System.Drawing.Point(186, 364);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(33, 25);
            this.lblResponse.TabIndex = 3;
            this.lblResponse.Text = "---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(8, 364);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Result:";
            // 
            // s
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 505);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblResponse);
            this.Controls.Add(this.btnExecuteFunction);
            this.Controls.Add(this.flpParameters);
            this.Controls.Add(this.cbFunctionList);
            this.Name = "s";
            this.Text = "GUI";
            this.Load += new System.EventHandler(this.FrmGUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFunctionList;
        private System.Windows.Forms.FlowLayoutPanel flpParameters;
        private System.Windows.Forms.Button btnExecuteFunction;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel pnEnums;
    }
}