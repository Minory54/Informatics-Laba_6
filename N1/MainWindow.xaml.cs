using System;
using System.Collections.Generic;
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

        float readFloat (TextBox tb_dec)
        {
            string num = tb_dec.Text.Replace(".", ",");
            float floatNum;

            while (!(float.TryParse(num, out floatNum)))
            {
                MessageBox.Show("Неверно задано число.", "Ошибка!");
                tb_dec.BorderBrush = Brushes.Red;
                return 0;
            }
            tb_dec.BorderBrush = Brushes.Green;
            return floatNum;
        }

        static int[] intToBin(int n) // Перевод числа в двоичную систему (для задания 3)
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

        static void print(int[] m, ListBox lb_result)
        {
            string result = "";

            for (int i = 0; i < m.Length; i++)
            {
                if (i == 4)
                {
                    result = result + " " + m[i];
                }
                else
                {
                    result = result + m[i];
                }
            }
            lb_result.Items.Add(result);
        }

        private void clickDoIt(object sender, RoutedEventArgs e)
        {
            float num = readFloat(tb_dec);
            byte[] tmp = BitConverter.GetBytes(num);

            lb_result.Items.Clear();
            lb_result.Items.Add($"GetBytes result -> [{tmp[0]}][{tmp[1]}][{tmp[2]}][{tmp[3]}]");
            lb_result.Items.Add($"3 byte {tmp[3]} -> {intToBin(tmp[3])}");
            //lb_result.Items.Add($"{num} -> Bin");
            //lb_result.Items.Add (Convert.ToInt32(num));
            //lb_result.Items.Add(num % 10);
            print(intToBin(tmp[3]), lb_result);
            

        }

        private void clickUndoIt(object sender, RoutedEventArgs e)
        {

        }
    }
}
