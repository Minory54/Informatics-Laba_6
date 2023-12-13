using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace N3_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int[] readIntArray(TextBox textBox, int size) // Проверка на тип int[] (для задания 4)
        {

            //double doubleNum; // используется этот тип потому что 

            //while (!(double.TryParse(textBox.Text, out doubleNum)) || (Convert.ToString(doubleNum)).Length > size)
            //{
            //    MessageBox.Show("Неверно задано число.", "Ошибка!");
            //    textBox.BorderBrush = Brushes.Red;

            //    break;
            //}
            //textBox.BorderBrush = Brushes.Green;

            //string strNum = Convert.ToString(doubleNum);
            string strNum = textBox.Text;

            if (strNum.Length < size)
            {
                while (strNum.Length < size)
                {
                    strNum = "0" + strNum;
                }
            }

            int[] array = new int[size];
            for (int i = 0; i < strNum.Length; i++)
            {
                array[i] = (int)char.GetNumericValue(strNum[i]);
            }

            return array;
        }

        float readFloat(TextBox tb_dec) // проверка вещественноого числа (Задание 1)
        {
            string num = tb_dec.Text.Replace(".", ",");
            float floatNum;

            while (!(float.TryParse(num, out floatNum)))
            {
                MessageBox.Show("Неверно задано вещественное число.", "Ошибка!");
                tb_dec.BorderBrush = Brushes.Red;
                break;
            }
            tb_dec.BorderBrush = Brushes.Green;
            return floatNum;
        }

        int[] intToBin(int n) // Перевод числа в двоичную систему
        {
            int absNum = Math.Abs(n);
            int[] array = new int[8];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = absNum % 2;
                absNum = Convert.ToSByte(absNum / 2);
            }

            int temp;
            for (int i = 0; i < array.Length / 2; i++)
            {
                temp = array[i];
                array[i] = array[array.Length - 1 - i];
                array[array.Length - 1 - i] = temp;
            }

            if (n < 0) // Условие если изначальное число отрицательное
            {
                array = addOne(invers(array));
            }

            return array;
        }

        int[] floatToBin(float n) // Перевод числа в двоичную систему
        {
            float absNum = Math.Abs(n);
            int[] array = new int[20];

            for (int i = 0; i < array.Length; i++)
            {
                absNum = absNum * 2;
                if (absNum == 2) 
                { 
                    absNum = 0; 
                }

                array[i] = Convert.ToInt32(absNum - (absNum % 1));  
                                
                if (absNum > 1)
                {
                    absNum = absNum % 1;
                }

            }

            return array;
        }

        int binToInt(int[] n) // Перевод строки в чило int 
        {
            int[] array = new int[8];
            array = n;
            int result = 0;

            //if (n[0] == 1) // Условие если изначальное число отрицательное
            //{
            //    array = addOne(invers(n));
            //}

            for (int i = 0, j = 7; i < 8 && j >= 0; i++, j--)
            {
                result = result + array[i] * Convert.ToInt32(Math.Pow(2, j));
            }

            //if (n[0] == 1)
            //{
            //    result = Convert.ToInt32(result * (-1));
            //}

            return result;
        }

        double binToFloat (int[] n, int step)
        {
            double result = 0;

            for(int i = 0, j = step; i < n.Length; i++, j--)
            {
                result = result + n[i] * Math.Pow(2, j);
            }
            
            return result; 
        }

        int[] invers(int[] array)
        {

            int[] invArray = new int[8];
            for (int i = 0; i < invArray.Length; i++)
            {
                if (array[i] == 0)
                {
                    invArray[i] = 1;
                }
                else
                {
                    invArray[i] = 0;
                }
            }

            return invArray;
        }

        int[] addOne(int[] n)
        {
            int[] unit = { 0, 0, 0, 0, 0, 0, 0, 1 };
            int[] resArray = new int[8];

            int temp = 0;

            for (int i = resArray.Length - 1; i >= 0; i--)
            {
                if (n[i] + unit[i] + temp >= 2)
                {
                    resArray[i] = n[i] + unit[i] + temp - 2;
                    temp = 1;
                }
                else
                {
                    resArray[i] = n[i] + unit[i] + temp;
                    temp = 0;
                }
            }

            return resArray;
        }

        string printBin(int[] m) // вывод числа в двоичном виде 
        {
            string result = "";

            for (int i = 0; i < m.Length; i++)
            {
                result = result + m[i];
            }
            return result;
        }

        int exponent (int n, float fraction) // подправить!!
        {
            List<int> list = new List<int>();
            int[] arrayIntBin;
            int[] arrayFracBin;
            int exp = 0;

            if (n > 1)
            {
                arrayIntBin = intToBin(n);
                foreach (int i in arrayIntBin)
                {
                    list.Add(i);
                }

                for (int i = 0, j = list.Count-1; i < list.Count; i++, j--)
                {
                    if (list[i] == 1)
                    {
                        exp = j;
                        return exp;
                    }
                }              
                
            }
            else
            {
                arrayFracBin = floatToBin(fraction);
                foreach (int i in arrayFracBin)
                {
                    list.Add(i);
                }

                for (int i = 0, j = 0 - 1; i < list.Count; i++, j--)
                {
                    if (list[i] == 1)
                    {
                        exp = j;
                        return exp;
                    }
                }
            }

            return exp;
        }

        string mantissaInt(int[] mantInt)
        {
            int[] result = new int[8];

            for (int i = mantInt.Length - 1, j = 1; i >= 0; i--, j++)
            {
                if (mantInt[i] == 1)
                {
                    result = shift(mantInt, mantInt.Length - j, result.Length);
                }
            }

            string resultMantis = "";

            for (int i = 0; i < 4; i++)
            {

                if (i == 1)
                {
                    resultMantis = resultMantis + "," + result[i];
                }
                else
                {
                    resultMantis = resultMantis + result[i];
                }

            }

            return resultMantis;
        }

        string mantissaFract(int[] mantFract)
        {
            int[] result = new int[20];

            for (int i = mantFract.Length - 1, j = 1; i >= 0; i--, j++)
            {
                if (mantFract[i] == 1)
                {
                    result = shift(mantFract, mantFract.Length - j, result.Length);
                }
            }

            string resultMantis = "";

            for (int i = 0; i < 20; i++)
            {

                if (i == 1)
                {
                    resultMantis = resultMantis + "," + result[i];
                }
                else
                {
                    resultMantis = resultMantis + result[i];
                }

            }

            return resultMantis;
        }

        private int[] shift(int[] m, int a, int size)
        {
            int[] resArray = new int[size];

            for (int i = 0, j = a; j < resArray.Length; i++, j++)
            {
                resArray[i] = m[j];
            }

            return resArray;
        }

        private void btn_toBin(object sender, RoutedEventArgs e)
        {
            float num = readFloat(tb_dec);
            int intNum = Convert.ToInt32(num - (num % 1));
            float fraction = num % 1;

            int[] arrayIntBin = intToBin(intNum);
            int[] arrayFractBin = floatToBin(fraction);
            int exp = exponent(intNum, fraction);
            string mant = "";

            lb_result.Items.Clear();
            lb_result.Items.Add($"{num} -> Bin");
            lb_result.Items.Add($"{intNum} -> {printBin(arrayIntBin)}");
            lb_result.Items.Add($"{fraction} -> {printBin(arrayFractBin)}");
            lb_result.Items.Add($"exp {exp} -> {exp + 127} -> {printBin(intToBin(exp + 127))}");
            
            if (num >= 1) 
            {
                mant = mantissaInt(arrayIntBin);
                lb_result.Items.Add($"{printBin(arrayIntBin)} -> {mant} {printBin(arrayFractBin)}*2^{printBin(intToBin(exp + 127))}");             
            }
            else 
            {
                mant = mantissaFract(arrayFractBin);
                lb_result.Items.Add($"{printBin(arrayIntBin)} -> {mant}*2^{printBin(intToBin(exp + 127))}");
            }
        }

        private void btn_toDec(object sender, RoutedEventArgs e)
        {
            int[] sign = readIntArray(tb_sign, 1);
            int[] expBin = readIntArray(tb_exp, 8);
            int exp = binToInt(expBin);
            int[] mantisBin = readIntArray(tb_mantissa,23);
            double result = binToFloat(mantisBin, exp - 127);

            lb_result.Items.Clear();
            lb_result.Items.Add($"{printBin(sign)} {printBin(expBin)} {printBin(mantisBin)} -> Dec");
            lb_result.Items.Add($"exp = {printBin(expBin)} -> {exp} -> {exp - 127}");
            lb_result.Items.Add($"result = {result}");
        }

    }
}
