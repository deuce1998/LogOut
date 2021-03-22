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

namespace МатюшкинАлексейLogTest
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

        private void start_Click(object sender, RoutedEventArgs e)
        {
            //string dirPath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToShortDateString().ToString();
            //string fileName = DateTime.Now.ToShortDateString().ToString() + ".txt";
            //textBox.Text+= Directory.Exists(dirPath).ToString();
            //textBox.Text += Directory.GetCurrentDirectory(); 
            //if (!Directory.Exists(dirPath))
            //{
            //    Directory.CreateDirectory(dirPath);
            //    File.Create(dirPath + @"\" + fileName);
            //}

            //if (!(File.Exists(dirPath + @"\" + fileName)))
            //{/*все пропало, файла нет, надо срочно что-то делать*/
            //    File.Create(dirPath + @"\" + fileName);
            //}t

            Exception ex=  new Exception ("An error occurred pi doing something");
            string[] ars = new string[] {"Alexey","Maximovish","Matyushkin" };
            LogOut logOut = new LogOut();
            logOut.Warning("New Error3445",ex);
            logOut.WarningUnique("Error145 Message");
            logOut.WritingMsg("WritingStore", "Orn", ars);
            logOut.SystemInfo("System info and ATC",null);

        }
    }
}
