using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Initialize
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int PATH_NUM = 4;

        string[] description = new string[PATH_NUM];
        string[] values = new string[PATH_NUM];

        Register[] registers = new Register[PATH_NUM];

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            for (int i = 0; i < registers.Length; i++)
            {
                registers[i] = new Register();
            }
            registers[0].Key = Registry.CurrentUser;
            registers[0].Path = @"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders";
            registers[0].Name = "Favorites";
            values[0] = @"E:\My\Favorites";
            description[0] = "收藏夹位置=";


            registers[1].Key = Registry.CurrentUser;
            registers[1].Path = @"SOFTWARE\Microsoft\Internet Explorer\MAIN";
            registers[1].Name = "Start Page";
            values[1] = @"file:///E:/My/Programming/GitHub/home/home.html";
            description[1] = "homepage=";

            registers[2].Key = Registry.LocalMachine;
            registers[2].Path = @"SYSTEM\ControlSet001\Control\Session Manager\Environment";
            registers[2].Name = "Path";
            values[2] = @"E:\My\Path";
            description[2] = "Path=";

            registers[3].Key = Registry.CurrentUser;
            registers[3].Path = @"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders";
            registers[3].Name = "Desktop";
            values[3] = @"E:\My\Desktop";
            description[3] = "Desktop=";

            sp1.Children.Clear();

            for (int i = 0; i < PATH_NUM; i++)
            {
                Border border = new Border();
                border.BorderThickness = new Thickness(0);
                border.Height = 16;
                border.Width = this.Width;
                border.Margin = new Thickness(0, 10, 0, 0);
                border.Name = "border" + i.ToString();

                TextBlock tb = new TextBlock();
                tb.Background = Brushes.Silver;// Color.FromRgb(192, 192, 192);
                tb.Height = 16;
                tb.Width = this.Width;
                tb.Text = description[i] + registers[i].GetValue();

                border.Child = tb;

                if (-1 != ((string)registers[i].GetValue()).IndexOf(values[i]))
                {
                    border.ToolTip = "设置正确";
                    border.Opacity = 0.3;
                }
                else
                {
                    if (2 == i)
                    {
                        border.ToolTip = "单击增加" + values[i];
                    }
                    else
                    {
                        border.ToolTip = "单击修改为" + values[i];
                    }
                    border.MouseDown += new MouseButtonEventHandler(border_MouseDown);

                }
                sp1.Children.Add(border);
            }
        }
        private void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            int index = Convert.ToInt32(border.Name.Substring(6));
            SetValue(index);
        }

        private void SetValue(int index)
        {
            if (2 == index)
            {
                string oldValue = registers[index].GetValue() as string;
                registers[index].SetValue(oldValue + @";" + values[index]);
            }
            else
            {
                registers[index].SetValue(values[index]);
            }
            Initialize();
        }

        private void buttonPaths_Click(object sender, RoutedEventArgs e)
        {
            WindowPaths window = new WindowPaths();
            window.Show();
        }
    }
}
