using System;
using System.Drawing;
using System.Windows.Forms;

namespace ComputingIntelligence
{
    public partial class DrawForm : Form
    {
        /// <summary>
        /// 绘制的数据
        /// </summary>
        public Matrix Data { set; get; }

        /// <summary>
        /// 创建一个绘图窗口
        /// </summary>
        /// <param name="matrix">要绘图的数据矩阵</param>
        public DrawForm(Matrix matrix)
        {
            InitializeComponent();
            Data = matrix;
        }

        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">参数</param>
        private void DrawForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">绘图参数</param>
        private void DrawForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = Pens.Black;
            // 获取每行数据
            Matrix[] datas = Data.GetRows();
            for (int i = 0; i < Data.Row; i++)
            {
                datas[i] = datas[i].Abs;
            }
            // 第i行
            for (int i = 0; i < Data.Row; i++)
            {
                // 第j列
                for (int j = 0; j < Data.Column; j++)
                {
                    Point A = new Point(Width / Data.Row * i, this.Height - (int)(datas[i].Data[j] * 500) - 50);
                    Point B = new Point(A.X, A.Y + 1);
                    // 绘制(点)直线
                    g.DrawLine(pen, A, B);
                }
            }
        }
    }
}
