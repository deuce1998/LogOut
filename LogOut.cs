using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace МатюшкинАлексейLogTest
{

    class LogOut : ILog
    {

        List<string> Unique = new List<string>() { "Запись пошла" };
        
        public void Debug(string message)
        {
            WritingMsg(message, "Debug");
            
        }

        public void Debug(string message, Exception e)
        {

            WritingMsg(message, "Debug", e);
        }

        public void DebugFormat(string message, params object[] args)
        {
            WritingMsg(message, "DebugFormat", args);
        }

        public void Error(string message)
        {
            WritingMsg( "Error",message);

        }

        public void Error(string message, Exception e)
        {
            WritingMsg(message, "Error", e);
        }

        public void Error(Exception ex)
        {
            
            if (Unique != null && !Unique.Contains(DateTime.Now.ToString("dd:MM:yyyy") + "      " + ex.StackTrace))
            {
                Unique.Add(DateTime.Now.ToString("dd:MM:yyyy") + "      " + ex.StackTrace);
                WritingMsgUnique("Error",ex.StackTrace);
            }
        }

        public void ErrorUnique(string message, Exception e)
        {
            if(Unique != null && !Unique.Contains(DateTime.Now.ToString("dd:MM:yyyy") + "   " + "Error" + "   " + e.StackTrace))
            {
                Unique.Add(DateTime.Now.ToString("dd:MM:yyyy") + "   " + "Error" + "   " + e.StackTrace);
                WritingMsgUnique("ErrorUnique",message,e.StackTrace);
            } 
        }

        
        public void Fatal(string message)
        {
            WritingMsg("Fatal",message);
        }

        public void Fatal(string message, Exception e)
        {

            WritingMsg(message, "Fatal",e);
        }

        public void Info(string message)
        {
            WritingMsg("Info", message);
        }

        public void Info(string message, Exception e)
        {
            WritingMsg(message, "Info", e);
        }

        public void Info(string message, params object[] args)
        {
            WritingMsg(message, "Info", args);
        }

        public void SystemInfo(string message, Dictionary<object, object> properties = null)
        {
                WritingMsg(message, "SystemInfo", properties);
        }

        public void Warning(string message)
        {
            WritingMsg("Warning", message);
        }

        public void Warning(string message, Exception e)
        {

            WritingMsg(message, "Warning", e);
            
        }

        public void WarningUnique(string message)
        {
            if (Unique!=null && !Unique.Contains(DateTime.Now.ToString("dd:MM:yyyy") + "   " + "WarningUnique") )
            {
                Unique.Add(DateTime.Now.ToString("dd:MM:yyyy") + "   " + "WarningUnique");
                WritingMsgUnique( "Warning",message);
            }
            
        }

        public bool CheckingForExistence()
        {
            try
            { 
                string dirPath = Directory.GetCurrentDirectory() + "\\Logs\\" + DateTime.Now.ToShortDateString().ToString();
                string fileName = DateTime.Now.ToShortDateString().ToString() + ".txt";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                    File.Create(dirPath + @"\" + fileName);
                }            
                if (!File.Exists(dirPath + @"\" + fileName))
                {
                    File.Create(dirPath + @"\" + fileName);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void WritingMsg(string message, string TypeOfError, Exception e)
        {
            string dirPath = Directory.GetCurrentDirectory() + "\\Logs\\" + DateTime.Now.ToShortDateString().ToString();
            string fileName = DateTime.Now.ToShortDateString().ToString() + ".txt";
            if (CheckingForExistence())
            using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                message = message.Insert(0, "  " + e.StackTrace);
                message = message.Insert(0, "  " + TypeOfError);
                message = message.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  ");
                message = message.Insert(message.Length, "\n");
                fstream.Write(Encoding.Default.GetBytes(message), 0, Encoding.Default.GetBytes(message).Length);
            }
            else
            {
                MessageBox.Show("Проверьте существование пути файла и папки!");
            }
        }

        
        public void WritingMsgUnique(string TypeOfError, string message ="", string e = "")
        {
            string dirPath = Directory.GetCurrentDirectory() + "\\Logs\\" + DateTime.Now.ToShortDateString().ToString();
            string fileName = DateTime.Now.ToShortDateString().ToString() + ".txt";

            if (CheckingForExistence())
            using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                    message = message.Insert(0, "  " + e);
                    message = message.Insert(0, "  " + TypeOfError);
                    message = message.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  ");
                    message = message.Insert(message.Length, "\n");
                    fstream.Write(Encoding.Default.GetBytes(message), 0, Encoding.Default.GetBytes(message).Length);
                    fstream.Flush();
            }
        }

       

        public void WritingMsg(string TypeOfError,string message, params object[] args )
        {
            string dirPath = Directory.GetCurrentDirectory() + "\\Logs\\" + DateTime.Now.ToShortDateString().ToString();
            string fileName = DateTime.Now.ToShortDateString().ToString() + ".txt";
            if (CheckingForExistence())
            using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                if(args!=null)
                for(int i=1;i<=args.Length;i++)
                {
                    message = message.Insert(0, args[args.Length-i] as string) +" ";
                }
                message = message.Insert(0, "  " + TypeOfError);
                message = message.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  ");
                message = message.Insert(message.Length, "\n");
                fstream.Write(Encoding.Default.GetBytes(message), 0, Encoding.Default.GetBytes(message).Length);
            }
        }

        public void WritingMsg(string message, string TypeOfError, Dictionary<object, object> properties = null)
        {
            string dirPath = Directory.GetCurrentDirectory() + "\\Logs\\" + DateTime.Now.ToShortDateString().ToString();
            string fileName = DateTime.Now.ToShortDateString().ToString() + ".txt";

            if (CheckingForExistence())
            using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                
                if(properties != null)
                foreach(var s in properties)
                {
                    message = message.Insert(0, s.Value.ToString()) + " "; 
                }
                message = message.Insert(0, "  " + TypeOfError);
                message = message.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  ");
                message = message.Insert(message.Length, "\n");
                fstream.Write(Encoding.Default.GetBytes(message), 0, Encoding.Default.GetBytes(message).Length);
            }
        }
    }
}
