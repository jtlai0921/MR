﻿namespace BeautifulRichTextBox
{
    partial class Frm_Main
    {
        /// <summary>
        /// 必需的設計器變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的資源。
        /// </summary>
        /// <param name="disposing">如果應釋放托管資源，為 true；否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 視窗設計器產生的程式碼

        /// <summary>
        /// 設計器支持所需的方法 - 不要
        /// 使用程式碼編輯器修改此方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.guageRichTextBox1 = new BeautifulRichTextBox.GuageRichTextBox();
            this.SuspendLayout();
            // 
            // guageRichTextBox1
            // 
            this.guageRichTextBox1.BackColor = System.Drawing.Color.Silver;
            this.guageRichTextBox1.CodeShow = true;
            this.guageRichTextBox1.Location = new System.Drawing.Point(12, 9);
            this.guageRichTextBox1.Name = "guageRichTextBox1";
            this.guageRichTextBox1.RulerStyle = BeautifulRichTextBox.GuageRichTextBox.Ruler.Graduation;
            this.guageRichTextBox1.Size = new System.Drawing.Size(336, 268);
            this.guageRichTextBox1.TabIndex = 0;
            this.guageRichTextBox1.UnitStyle = BeautifulRichTextBox.GuageRichTextBox.Unit.Cm;
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 289);
            this.Controls.Add(this.guageRichTextBox1);
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "設計帶行數和標尺的RichTextBox控制元件";
            this.ResumeLayout(false);

        }

        #endregion

        private GuageRichTextBox guageRichTextBox1;


    }
}

