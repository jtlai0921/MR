﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlFormSize
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            this.Width = 300;//設定視窗的寬度
            this.Height = 150;//設定視窗的高度
            this.FormBorderStyle = FormBorderStyle.FixedDialog;//設定視窗的邊框樣式
        }
    }
}
