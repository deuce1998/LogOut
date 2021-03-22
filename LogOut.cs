using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace МатюшкинАлексейLogTest
{
    class LogOut :ILog
    {
        string dirPath = Directory.GetCurrentDirectory() + "\\Logs\\" + DateTime.Now.ToShortDateString().ToString();
        string fileName = DateTime.Now.ToShortDateString().ToString()+".txt";
        public void Debug(string message)
        {
            WritingMsg(message, "Debug");
            
        }

        public void Debug(string message, Exception e)
        {

            WritingMsg(message, e, "Debug");
        }

        public void DebugFormat(string message, params object[] args)
        {
            WritingMsg(message, "DebugFormat", args);
        }

        public void Error(string message)
        {
            WritingMsg(message, "Error");

        }

        public void Error(string message, Exception e)
        {
            WritingMsg(message, e, "Error");
        }

        public void Error(Exception ex)
        {
            WritingMsgUnique(ex, "Error");
        }

        public void ErrorUnique(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        //Критичная ошибка:приложение не может далее функционировать
        public void Fatal(string message)
        {
            WritingMsg(message, "Fatal");
        }

        public void Fatal(string message, Exception e)
        {

            WritingMsg(message,e, "Fatal");
        }

        public void Info(string message)
        {
            WritingMsg(message, "Info");
        }

        public void Info(string message, Exception e)
        {
            WritingMsg(message, e, "Info");
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
            WritingMsg(message, "Warning");
        }

        public void Warning(string message, Exception e)
        {

            WritingMsg(message, e, "Warning");
            //Успешное завершение
        }

        public void WarningUnique(string message)
        {
            WritingMsgUnique(message, "Warning");
        }



        public void WritingMsg(string message, Exception e, string TypeOfError)
        {
            //Проверка на существование пути
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                File.Create(dirPath + @"\" + fileName);
            }

            //Проверка на существование файла
            if (!File.Exists(dirPath + @"\" + fileName))
            {/*все пропало, файла нет, надо срочно что-то делать*/
                File.Create(dirPath + @"\" + fileName);
            }

            System.Threading.Thread.Sleep(100);

            using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                // чтение из файла
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                message = message.Insert(0, "  " + e.StackTrace);
                message = message.Insert(0, "  " + TypeOfError);
                message = message.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  ");
                message = message.Insert(message.Length, "\n");
                fstream.Write(Encoding.Default.GetBytes(message), 0, Encoding.Default.GetBytes(message).Length);
            }
        }

        public void WritingMsg(string message, string TypeOfError)
        {
            //Проверка на существование пути
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                File.Create(dirPath + @"\" + fileName);
            }

            //Проверка на существование файла
            if (!File.Exists(dirPath + @"\" + fileName))
            {/*все пропало, файла нет, надо срочно что-то делать*/
                File.Create(dirPath + @"\" + fileName);
            }

            System.Threading.Thread.Sleep(100);

            using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.Write))
            {
                // чтение из файла
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                
                message = message.Insert(0, "  " + TypeOfError);
                message = message.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  ");
                message = message.Insert(message.Length, "\n");
                fstream.Write(Encoding.Default.GetBytes(message), 0, Encoding.Default.GetBytes(message).Length);
            }
        }

        public void WritingMsgUnique(string message, string TypeOfError)
        {
                    //Проверка на существование пути
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                        File.Create(dirPath + @"\" + fileName);
                    }

                    //Проверка на существование файла
                    if (!File.Exists(dirPath + @"\" + fileName))
                    {/*все пропало, файла нет, надо срочно что-то делать*/
                        File.Create(dirPath + @"\" + fileName);
                    }

                    System.Threading.Thread.Sleep(100);

                    using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
                    {
                        // чтение из файла

                        // преобразуем строку в байты
                        byte[] array = new byte[fstream.Length];
                        // считываем данные
                        fstream.Read(array, 0, array.Length);
                        // декодируем байты в строку
                        string textFromFile = System.Text.Encoding.Default.GetString(array);

                        if (!textFromFile.Contains(message))
                        {
                            
                            message = message.Insert(0, "  " + TypeOfError);
                            message = message.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  ");
                            message = message.Insert(message.Length, "\n");

                            fstream.Write(Encoding.Default.GetBytes(message), 0, Encoding.Default.GetBytes(message).Length);
                            fstream.Flush();

                        }


                    }
        }

        public void WritingMsgUnique(Exception e, string TypeOfError)
        {
            //Проверка на существование пути
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                File.Create(dirPath + @"\" + fileName);
            }

            //Проверка на существование файла
            if (!File.Exists(dirPath + @"\" + fileName))
            {/*все пропало, файла нет, надо срочно что-то делать*/
                File.Create(dirPath + @"\" + fileName);
            }

            System.Threading.Thread.Sleep(100);

            using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                string message = "";
                // чтение из файла
                
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);

                if (!textFromFile.Contains(e.StackTrace))
                {
                    message = message.Insert(0, "  " + e.StackTrace);
                    message = message.Insert(0, "  " + TypeOfError);
                    message = message.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  ");
                    message = message.Insert(message.Length, "\n");

                    fstream.Write(Encoding.Default.GetBytes(message), 0, Encoding.Default.GetBytes(message).Length);
                    fstream.Flush();

                }


            }
        }

        public void WritingMsg(string message,string TypeOfError, params object[] args )
        {
            //Проверка на существование пути
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                File.Create(dirPath + @"\" + fileName);
            }

            //Проверка на существование файла
            if (!File.Exists(dirPath + @"\" + fileName))
            {/*все пропало, файла нет, надо срочно что-то делать*/
                File.Create(dirPath + @"\" + fileName);
            }

            System.Threading.Thread.Sleep(100);

            using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                // чтение из файла
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                
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
            //Проверка на существование пути
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                File.Create(dirPath + @"\" + fileName);
            }

            //Проверка на существование файла
            if (!File.Exists(dirPath + @"\" + fileName))
            {/*все пропало, файла нет, надо срочно что-то делать*/
                File.Create(dirPath + @"\" + fileName);
            }

            System.Threading.Thread.Sleep(100);

            using (FileStream fstream = new FileStream(dirPath + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                // чтение из файла
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                if(!(properties == null))
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
