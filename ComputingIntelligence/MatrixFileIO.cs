using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{

    /// <summary>
    /// 筛选器接口
    /// </summary>
    interface Filter
    {
        /// <summary>
        /// 筛选
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>结果矩阵</returns>
        Matrix Filter(string data);
    }

    /// <summary>
    /// 鸢尾花筛选器
    /// </summary>
    class IrisFilter : Filter
    {
        public Matrix Filter(string data)
        {
            float[] matData = { 0, 0, 0 };
            switch (data)
            {
                case "Iris-setosa":
                    matData[0] = 1;
                    break;
                case "Iris-versicolor":
                    matData[1] = 1;
                    break;
                case "Iris-virginica":
                    matData[2] = 1;
                    break;
                default:
                    break;
            }
            Matrix matrix = new Matrix(1,3,matData);
            return matrix;
        }
    }

    static class MatrixFileIO
    {
        /// <summary>
        /// 从文件中读取一个数组
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <returns>读取的矩阵</returns>
        public static Matrix ReadMatrixOfFile(string filePath, Filter filter = null)
        {
            // 提示信息
            Console.WriteLine("正在打开文件" + filePath);
            // 打开文件
            FileStream inStream = new FileStream(filePath, FileMode.Open);
            // 包装为reader
            StreamReader reader = new StreamReader(inStream);
            // 读取一行
            String dataLine;
            // 储存一行的数据数组
            String[] dataStrings;
            // 定义空白字符
            char[] block = { ' ', '\r', '\n', ',', '，', ';', '；', '\t' };
            // 数据链表
            List<float> data = new List<float>();
            // 行和列
            int row = 0, colunm = 0;
            // 提示信息
            Console.WriteLine("正在读取数据");
            // 读取一行数据
            while ((dataLine = reader.ReadLine()) != null)
            {
                // 行数加一
                row++;
                // 分割字符串
                dataStrings = dataLine.Split(block);
                // 获取列数
                colunm = dataStrings.Length;
                // 依次解析数据
                for (int i = 0; i < colunm; i++)
                {
                    if (float.TryParse(dataStrings[i], out float parseData))
                    {
                        // 添加到数据链表
                        data.Add(parseData);
                    }
                    else
                    {
                        data.AddRange(filter.Filter(dataStrings[i]).Data);
                    }

                }
            }
            // 提示信息
            Console.WriteLine("读取数据完毕");
            // 关闭流
            reader.Close();
            // 创建矩阵
            float[] matData = data.ToArray();
            Matrix matrix = new Matrix(row, matData.Length / row, matData);
            return matrix;
        }
           
        /// <summary>
        /// 创建文件并将矩阵数据写入文件
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <param name="matrix">要写入的矩阵</param>
        public static void WriteMatrixToFile(String filePath, Matrix matrix)
        {
            // 打开文件
            FileStream outStream = new FileStream(filePath, FileMode.Create);
            // 包装为writer
            StreamWriter writer = new StreamWriter(outStream);
            // 写入数据
            writer.WriteLine(matrix.ToString());
            // 关闭流
            writer.Close();
        }
    }
}
