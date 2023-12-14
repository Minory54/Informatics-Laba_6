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

namespace N1
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

        float readFloat (TextBox tb_dec) // проверка вещественноого числа (Задание 1)
        {
            string num = tb_dec.Text.Replace(".", ",");
            float floatNum;

            while (!(float.TryParse(num, out floatNum)))
            {
                MessageBox.Show("Неверно задано вещественное число.", "Ошибка!");
                tb_dec.BorderBrush = Brushes.Red;
                return 0;
            }
            tb_dec.BorderBrush = Brushes.Green;
            return floatNum;
        }

        string readStr(TextBox tb_dec) // проверка двоичного числа (Задание 1)
        {
            string strNum = tb_dec.Text.Replace(" ", "");
            double doubleNum;

            while (!(double.TryParse(tb_bin.Text, out doubleNum)) || (strNum.Length != 32))
            {
                MessageBox.Show("Неверно задано двоичное число.", "Ошибка!");
                tb_bin.BorderBrush = Brushes.Red;
                return "";
            }
            tb_dec.BorderBrush = Brushes.Green;

            return strNum;
        }

        static int[] intToBin(int n) // Перевод числа в двоичную систему
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

        static int binToInt(int[] n) // Перевод строки в чило int 
        {
            int[] array = new int[8];
            array = n;
            int result = 0;

            for (int i = 0, j = 7; i < 8 && j >= 0; i++, j--)
            {
                result = result + array[i] * Convert.ToInt32(Math.Pow(2, j));
            }

            return result;
        }

        static int[] invers(int[] array)
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

        static int[] addOne(int[] n) 
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

        void print1(byte[] tmp, ListBox lb_result) //вывод решения (задание 1)
        {
            string result = "";

            for (int i = tmp.Length-1; i >= 0; i--)
            {
                lb_result.Items.Add($"{i} byte[{tmp[i]}] -> {printBin(intToBin(tmp[i]))}");
                result = result + printBin(intToBin(tmp[i])) + " ";
            }

            lb_result.Items.Add($"result -> {result}");
        }

        int[] divisionString(string numStr, int i)
        {
            int j;
            int[] result = new int[8];
            
            for (i = 8*i, j = 0; j < result.Length; i++, ++j) 
            {
                result[j] = (int)char.GetNumericValue(numStr[i]);                
            }
            return result;
        }


        void print2(string numStr, ListBox lb_result)
        {

            byte[] tmp = new byte[4];

            for (int i = tmp.Length - 1, j = 0; i >= 0; i--, j++)
            {
                lb_result.Items.Add($"{i} byte[{(printBin(divisionString(numStr, j)))}] -> {binToInt(divisionString(numStr, j))} ");
                tmp[i] = Convert.ToByte(binToInt(divisionString(numStr, j)));
            }

            lb_result.Items.Add($"result -> [{tmp[0]}][{tmp[1]}][{tmp[2]}][{tmp[3]}]");
            float result = BitConverter.ToSingle(tmp, 0);
            lb_result.Items.Add($"[{tmp[0]}][{tmp[1]}][{tmp[2]}][{tmp[3]}] -> ToSingle -> {result}");

        }

        private void clickDoIt(object sender, RoutedEventArgs e)
        {
            float num = readFloat(tb_dec);
            byte[] tmp = BitConverter.GetBytes(num);

            lb_result.Items.Clear();
            lb_result.Items.Add($"GetBytes result -> [{tmp[0]}][{tmp[1]}][{tmp[2]}][{tmp[3]}]");
            print1(tmp, lb_result);
        }

        private void clickUndoIt(object sender, RoutedEventArgs e)
        {
            string numStr = readStr(tb_bin);

            lb_result.Items.Clear();
            lb_result.Items.Add($"GetBytes result -> {numStr}");
            print2(numStr, lb_result);
        }
    }
}
