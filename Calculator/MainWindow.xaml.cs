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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNum, result;
        SelectedOp selectedOp;
        public MainWindow()
        {
            InitializeComponent();
            //res_lbl.Content = "1995";
            neg_btn.Click += Neg_btn_Click;
            ac_btn.Click += Ac_btn_Click;
            perc_btn.Click += Perc_btn_Click;
        }

        private void Perc_btn_Click(object sender, RoutedEventArgs e)
        {
            double temp;
            if (double.TryParse(res_lbl.Content.ToString(), out temp))
            {
                temp = temp / 100;
                if (lastNum != 0)
                {
                    temp *= lastNum;
                }
                res_lbl.Content = temp.ToString();
            }
        }

        private void Ac_btn_Click(object sender, RoutedEventArgs e)
        {
            res_lbl.Content = "0";
            lastNum = 0;
            result = 0;
        }

        private void Neg_btn_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(res_lbl.Content.ToString(), out lastNum))
            {
                lastNum *= -1;
                res_lbl.Content = lastNum.ToString();
            }
        }

        private void Num_btn_Click(object sender, RoutedEventArgs e)
        {
            int presedNum = int.Parse((sender as Button).Content.ToString());
            
            if (res_lbl.Content.ToString() != "0")
            {
                res_lbl.Content = $"{res_lbl.Content.ToString()}{presedNum}";
            }
            else
            {
                res_lbl.Content = $"{presedNum}";
            }
        }

        private void Point_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!res_lbl.Content.ToString().Contains("."))
            {
                res_lbl.Content = $"{res_lbl.Content.ToString()}.";
            }
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(res_lbl.Content.ToString(), out lastNum))
            {
                res_lbl.Content = "0";
                if(sender == add_btn)
                    selectedOp = SelectedOp.Addition;
                if (sender == sub_btn)
                    selectedOp = SelectedOp.Substraction;
                if (sender == div_btn)
                    selectedOp = SelectedOp.Division;
                if (sender == multiply_btn)
                    selectedOp = SelectedOp.Multiplication;
            }
        }

        private void Res_btn_Click(object sender, RoutedEventArgs e)
        {
            double newNum;
            if(double.TryParse(res_lbl.Content.ToString(), out newNum))
            {
                switch (selectedOp)
                {
                    case SelectedOp.Addition:
                        result = SimpleMath.Add(lastNum, newNum);
                        break;
                    case SelectedOp.Substraction:
                        result = SimpleMath.Substract(lastNum, newNum);
                        break;
                    case SelectedOp.Multiplication:
                        result = SimpleMath.Multiply(lastNum, newNum);
                        break;
                    case SelectedOp.Division:
                        result = SimpleMath.Divid(lastNum, newNum);
                        break;
                }
                res_lbl.Content = $"{result}";
            }
        }

        public enum SelectedOp
        {
            Addition,
            Substraction,
            Multiplication,
            Division
        }

        public class SimpleMath
        {
            public static double Add(double num1, double num2)
            {
                return num1 + num2;
            }
            public static double Substract(double num1, double num2)
            {
                return num1 - num2;
            }
            public static double Multiply(double num1, double num2)
            {
                return num1 * num2;
            }
            public static double Divid(double num1, double num2)
            {
                if(num2 == 0)
                {
                    MessageBox.Show("Division by ZERO is not Supported", "Dividing By Zero", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return 0;
                }
                return num1 / num2;
            }
        }
    }
}
