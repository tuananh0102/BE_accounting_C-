namespace MISA.Web04.Api
{
    /// <summary>
    /// lớp tính toán
    /// </summary>
    /// Created: ttanh(09/06/2023)
    public class Caculator
    {
    /// <summary>
    /// Hàm cộng 2 số nguyên 
    /// </summary>
    /// <param name="a">Toán hạng 1</param>
    /// <param name="b">Toán hạng 2</param>
    /// <returns>Tổng 2 số nguyên</returns>
    /// Created by: ttanh(09/06/2023)
        public long Add(int a, int b)
        {
            return (long)a+b;
        }

        /// <summary>
        /// Hàn trừ 2 số nguyên
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public long Sub(int a ,int b)
        {
            return (long)a - b;
        }

        /// <summary>
        /// Hàm nhân 2 số nguyên
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public double Mul(int a, int b)
        {
            return (long)a * b;
        }

        public double Div(int a, int b)
        {
            if(b == 0)
            {
                throw new Exception("Không chia được cho 0");
            }
            return (double)a / b;
        }
    }
}
