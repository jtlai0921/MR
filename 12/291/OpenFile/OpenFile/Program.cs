﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenFile
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主入口點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_Main());
        }
    }
}