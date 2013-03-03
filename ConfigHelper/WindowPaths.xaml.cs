using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Initialize
{
    public class SystemVariable
    {
        //public string Name { get; set; }
        public string Value { get; set; }
    }
    /// <summary>
    /// Interaction logic for WindowPaths.xaml
    /// </summary>
    public partial class WindowPaths : Window
    {
        public ObservableCollection<SystemVariable> Paths { get; set; }
        public WindowPaths()
        {
            Paths = new ObservableCollection<SystemVariable>();
            this.DataContext = this;

            GetPaths();

            InitializeComponent();
        }
        public void GetPaths()
        {
            Paths.Clear();
            RegistryKey localMachine = Registry.LocalMachine;
            string str = (string)localMachine.OpenSubKey(@"SYSTEM\ControlSet001\Control\Session Manager\Environment", false).GetValue("Path");
            foreach (string s in str.Split(';'))
            {
                if (s != "")
                    Paths.Add(new SystemVariable() { Value = s });
            }
        }
        public void UpdatePaths()
        {
            string str = "";
            foreach (SystemVariable s in Paths)
                str += s.Value + ";";
            RegistryKey localMachine = Registry.LocalMachine;
            localMachine.OpenSubKey(@"SYSTEM\ControlSet001\Control\Session Manager\Environment", true).SetValue("Path", str);
        }

        private void DataGrid_CellEditEnding_1(object sender, DataGridCellEditEndingEventArgs e)
        {
            UpdatePaths();
            GetPaths();
        }
    }
}
