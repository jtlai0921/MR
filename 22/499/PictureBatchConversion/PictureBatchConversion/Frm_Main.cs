﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
namespace PictureBatchConversion
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        string[] path1 = null;                 //用於存儲選擇的文件列表
        string path2 = "";                    //用於存儲儲存的路徑
        Bitmap bt;                          //聲明一個轉換圖片格式的Bitmap物件
        Thread td;                          //聲明一個線程
        int Imgtype1;                       //聲明一個變數用於標記ConvertImage方法中轉換的類型
        string OlePath;                     //聲明一個變數用於存儲ConvertImage方法中原始圖片的路徑
        string path;                        //聲明一個變數用於存儲ConvertImage方法中轉換後圖片的儲存路徑
        int flags;                           //用於標記已轉換圖片的數量，用於計算轉換進度

        private void Form2_Load(object sender, EventArgs e)
        {
            tscbType.SelectedIndex = 0;             //設定第一個轉換類型被選中
            CheckForIllegalCrossThreadCalls = false;//屏蔽線程彈出的錯誤提示
        }
        private void toolStripButton3_Click(object sender, EventArgs e)//選擇轉換文件的按鈕
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)        //判斷是否選擇文件
            {
                listView1.Items.Clear();                                //清空listView1
                string[] info = new string[7];                          //存儲每一行資料
                FileInfo fi;                                            //建立一個FileInfo對象，用於取得圖片訊息
                path1 = openFileDialog1.FileNames;                      //取得選擇的圖片集合
                for (int i = 0; i < path1.Length; i++)                  //讀取集合中的內容
                {
                    //取得圖片名稱
                    string ImgName = path1[i].Substring(path1[i].LastIndexOf("\\") + 1, path1[i].Length - path1[i].LastIndexOf("\\") - 1);
                    //取得圖片類型
                    string ImgType = ImgName.Substring(ImgName.LastIndexOf(".") + 1, ImgName.Length - ImgName.LastIndexOf(".") - 1);
                    fi = new FileInfo(path1[i].ToString());             //實例化FileInfo物件
                    //將每一行資料第一個位置的圖標新增到imageList1中
                    imageList1.Images.Add(ImgName, Properties.Resources.圖標__23_);
                    info[1] = ImgName;                      //圖片名稱
                    info[2] = ImgType;                      //圖片類型
                    info[3] = fi.LastWriteTime.ToShortDateString();//圖片最後修改日期
                    info[4] = path1[i].ToString();                  //圖片位置
                    info[5] = (fi.Length / 1024) + "KB";                //圖片大小
                    info[6] = "未轉換";                                //圖片狀態
                    ListViewItem lvi = new ListViewItem(info, ImgName);  //實例化ListViewItem物件
                    listView1.Items.Add(lvi);                              //將訊息新增到listView1控制元件中
                }
                tsslFileNum.Text = "目前共有" + path1.Length.ToString() + "個文件";//狀態欄中顯示圖片數量
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e) //關閉按鈕
        {
            Application.Exit();                                         //退出系統
        }

        private void toolStripButton5_Click(object sender, EventArgs e) //清空列表的按鈕
        {
            listView1.Items.Clear();                                        //清空列表
            path1 = null;                                                   //清空圖片的集合
            tsslFileNum.Text = "目前沒有文件";                                 //狀態欄中提示
            tsslPlan.Text = "";                                                 //清空進度數字
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (path1 == null)                                              //判斷是否選擇圖片
            {
                MessageBox.Show("請選擇圖片！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (path2.Length == 0)                                      //判斷是否選擇儲存位置
                {
                    MessageBox.Show("請選擇儲存位置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    flags = 1;                                              //初始化flags變數為1，用於計算進度
                    toolStrip1.Enabled = false;                             //當轉換開始時，禁用工具欄
                    int flag = tscbType.SelectedIndex;                      //判斷將圖片轉換為何種格式
                    switch (flag)                                           //根據不同的格式進行轉換
                    {
                        case 0:
                            Imgtype1 = 0;                                   //如果選擇第一項則轉換為BMP格式
                            td = new Thread(new ThreadStart(ConvertImage)); //透過線程呼叫ConvertImage方法進行轉換
                            td.Start();
                            break;
                        case 1:                                             //如果選擇第二項則轉換為JPG格式
                            Imgtype1 = 1;
                            td = new Thread(new ThreadStart(ConvertImage));//透過線程呼叫ConvertImage方法進行轉換
                            td.Start();
                            break;
                        case 2:                                            //如果選擇第三項則轉換為PNG格式
                            Imgtype1 = 2;
                            td = new Thread(new ThreadStart(ConvertImage));//透過線程呼叫ConvertImage方法進行轉換
                            td.Start();
                            break;
                        case 3:                                             //如果選擇第四項則轉換為GIF格式
                            Imgtype1 = 3;
                            td = new Thread(new ThreadStart(ConvertImage));//透過線程呼叫ConvertImage方法進行轉換
                            td.Start();
                            break;
                        default: td.Abort(); break;
                    }
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)//選擇儲存路徑按鈕
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)   //判斷是否選擇儲存路徑
            {
                path2 = folderBrowserDialog1.SelectedPath;              //取得儲存路徑
            }
        }

        private void ConvertImage()
        {
            flags = 1;
            switch (Imgtype1)
            {
                case 0:
                    for (int i = 0; i < path1.Length; i++)
                    {
                        string ImgName = path1[i].Substring(path1[i].LastIndexOf("\\") + 1, path1[i].Length - path1[i].LastIndexOf("\\") - 1);
                        ImgName = ImgName.Remove(ImgName.LastIndexOf("."));
                        OlePath = path1[i].ToString();
                        bt = new Bitmap(OlePath);
                        path = path2 + "\\" + ImgName + ".bmp";
                        bt.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                        listView1.Items[flags - 1].SubItems[6].Text = "已轉換";
                        tsslPlan.Text = "正在轉換" + flags * 100 / path1.Length + "%";
                        if (flags == path1.Length)
                        {
                            toolStrip1.Enabled = true;
                            tsslPlan.Text = "圖片轉換全部完成";
                        }
                        flags++;
                    }
                    break;
                case 1:
                    for (int i = 0; i < path1.Length; i++)
                    {
                        string ImgName = path1[i].Substring(path1[i].LastIndexOf("\\") + 1, path1[i].Length - path1[i].LastIndexOf("\\") - 1);
                        ImgName = ImgName.Remove(ImgName.LastIndexOf("."));
                        OlePath = path1[i].ToString();
                        bt = new Bitmap(OlePath);
                        path = path2 + "\\" + ImgName + ".jpeg";
                        bt.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                        listView1.Items[flags - 1].SubItems[6].Text = "已轉換";
                        tsslPlan.Text = "正在轉換" + flags * 100 / path1.Length + "%";
                        if (flags == path1.Length)
                        {
                            toolStrip1.Enabled = true;
                            tsslPlan.Text = "圖片轉換全部完成";
                        }
                        flags++;
                    }
                    break;
                case 2:
                    for (int i = 0; i < path1.Length; i++)
                    {
                        string ImgName = path1[i].Substring(path1[i].LastIndexOf("\\") + 1, path1[i].Length - path1[i].LastIndexOf("\\") - 1);
                        ImgName = ImgName.Remove(ImgName.LastIndexOf("."));
                        OlePath = path1[i].ToString();
                        bt = new Bitmap(OlePath);
                        path = path2 + "\\" + ImgName + ".png";
                        bt.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                        listView1.Items[flags - 1].SubItems[6].Text = "已轉換";
                        tsslPlan.Text = "正在轉換" + flags * 100 / path1.Length + "%";
                        if (flags == path1.Length)
                        {
                            toolStrip1.Enabled = true;
                            tsslPlan.Text = "圖片轉換全部完成";
                        }
                        flags++;
                    }
                    break;
                case 3:
                    for (int i = 0; i < path1.Length; i++)
                    {
                        string ImgName = path1[i].Substring(path1[i].LastIndexOf("\\") + 1, path1[i].Length - path1[i].LastIndexOf("\\") - 1);
                        ImgName = ImgName.Remove(ImgName.LastIndexOf("."));
                        OlePath = path1[i].ToString();
                        bt = new Bitmap(OlePath);
                        path = path2 + "\\" + ImgName + ".gif";
                        bt.Save(path, System.Drawing.Imaging.ImageFormat.Gif);
                        listView1.Items[flags - 1].SubItems[6].Text = "已轉換";
                        tsslPlan.Text = "正在轉換" + flags * 100 / path1.Length + "%";
                        if (flags == path1.Length)
                        {
                            toolStrip1.Enabled = true;
                            tsslPlan.Text = "圖片轉換全部完成";
                        }
                        flags++;
                    }
                    break;
                default: bt.Dispose(); break;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)//關閉視窗時要關閉線程
        {
            if (td != null)                                                 //判斷是否存在線程
            {
                if (td.ThreadState == ThreadState.Running)                  //然後判斷線程是否正在執行
                {
                    td.Abort();                                             //如果執行則關閉線程
                }
            }
        }
    }
}
