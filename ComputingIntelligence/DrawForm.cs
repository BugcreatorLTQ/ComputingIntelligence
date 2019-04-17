using System;
using System.Drawing;
using System.Windows.Forms;

namespace ComputingIntelligence
{
    public partial class DrawForm : Form
    {

        /// <summary>
        /// 横坐标起点
        /// </summary>
        private readonly int start_x = 5;

        /// <summary>
        /// 纵坐标起点
        /// </summary>
        private readonly int start_y = 45;

        /// <summary>
        /// 最大值
        /// </summary>
        private float min;

        /// <summary>
        /// 最小值
        /// </summary>
        private float max;

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
            SetVal();
        }

        /// <summary>
        /// 设置最大值与最小值
        /// </summary>
        private void SetVal()
        {
            // 数据最小值
            min = Data.Data[0];
            // 数据最大值
            max = Data.Data[0];
            for (int i = 1; i < Data.Data.Length; i++)
            {
                if (Data.Data[i] < min)
                {
                    min = Data.Data[i];
                }
                if (Data.Data[i] > max)
                {
                    max = Data.Data[i];
                }
            }
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
        /// 获取纵坐标值
        /// </summary>
        /// <param name="min">值域最小值</param>
        /// <param name="max">值域最大值</param>
        /// <param name="val">值</param>
        /// <returns>纵坐标</returns>
        private int GetY(float min,float max,float val)
        {
            return Height - (int)((val - min) / (max - min) * Height * 5 / 6) - start_y;
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
            // 绘制横坐标
            g.DrawLine(Pens.Violet, new Point(0, GetY(min, max, 0)),
                new Point(Width, GetY(min, max, 0)));
            // 绘制纵坐标
            //g.DrawLine(Pens.Violet, new Point(start_x, 0), new Point(start_x, this.Height));
            // 获取每列数据
            Matrix[] datas = Data.GetColumns();
            // 第i列
            for (int i = 0; i < Data.Column; i++)
            {
                // 第j行
                for (int j = 0; j < Data.Row; j++)
                {
                    // 决定点绘制的位置
                    Point A = new Point(Width * i / Data.Column + start_x, GetY(min, max, datas[i].Data[j]));
                    Point B = new Point(A.X - 1, A.Y - 1);
                    Point C = new Point(A.X - 1, A.Y);
                    Point D = new Point(A.X, A.Y - 1);
                    // 绘制(点)直线
                    g.DrawLine(pen, A, B);
                    g.DrawLine(pen, C, D);
                }
            }
        }
    }
}
