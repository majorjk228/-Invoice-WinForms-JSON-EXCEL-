﻿namespace CourseWork15
{
    partial class ChooseCompany
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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbFrom = new System.Windows.Forms.RadioButton();
            this.rbTo = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(52, 180);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 44);
            this.button1.TabIndex = 109;
            this.button1.Text = "Подтвердить (Submit)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(25, 76);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(206, 28);
            this.comboBox1.TabIndex = 108;
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(34, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.MaximumSize = new System.Drawing.Size(200, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 34);
            this.label1.TabIndex = 107;
            this.label1.Text = "Выбрать компанию (Choose Company)\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rbFrom
            // 
            this.rbFrom.AutoSize = true;
            this.rbFrom.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbFrom.Checked = true;
            this.rbFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbFrom.Location = new System.Drawing.Point(64, 109);
            this.rbFrom.MaximumSize = new System.Drawing.Size(150, 30);
            this.rbFrom.Name = "rbFrom";
            this.rbFrom.Size = new System.Drawing.Size(130, 20);
            this.rbFrom.TabIndex = 106;
            this.rbFrom.TabStop = true;
            this.rbFrom.Text = "От кого (Sent by)";
            this.rbFrom.UseVisualStyleBackColor = false;
            // 
            // rbTo
            // 
            this.rbTo.AutoSize = true;
            this.rbTo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbTo.Location = new System.Drawing.Point(64, 141);
            this.rbTo.MaximumSize = new System.Drawing.Size(150, 30);
            this.rbTo.Name = "rbTo";
            this.rbTo.Size = new System.Drawing.Size(116, 20);
            this.rbTo.TabIndex = 110;
            this.rbTo.TabStop = true;
            this.rbTo.Text = "Кому (Sent To)";
            this.rbTo.UseVisualStyleBackColor = false;
            // 
            // ChooseCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 250);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbFrom);
            this.Controls.Add(this.rbTo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ChooseCompany";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить компанию (Choose Company)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbFrom;
        private System.Windows.Forms.RadioButton rbTo;
    }
}