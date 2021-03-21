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
        string dirPath = Directory.GetCurrentDirectory() + "\\" + DateTime.Now.ToShortDateString().ToString();
        string fileName = DateTime.Now.ToShortDateString().ToString()+".txt";
        public void Debug(string message)
        {
            

            throw new NotImplementedException();
        }

        public void Debug(string message, Exception e)
        {

            throw new NotImplementedException();
        }

        public void DebugFormat(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        public void Error(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void ErrorUnique(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        //Критичная ошибка:приложение не может далее функционировать
        public void Fatal(string message)
        {
            //Warning(message);
            Environment.Exit(1);
        }

        public void Fatal(string message, Exception e)
        {
            Warning(message,e);
            System.Environment.FailFast("При работе программы, возникла фатальная ошибка!",e);
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void SystemInfo(string message, Dictionary<object, object> properties = null)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            //Проверка на существование пути
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                File.Create(dirPath + @"\" + fileName);
            }

            //Проверка на существование файла
            if (!(File.Exists(dirPath + @"\" + fileName)))
            {/*все пропало, файла нет, надо срочно что-то делать*/
                File.Create(dirPath + @"\" + fileName);
            }

            //Запись сообщения в файл
            using (StreamWriter sw = new StreamWriter(dirPath + @"\" + fileName, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(message);
            }
            
            //Успешное завершение
        }

        public void Warning(string message, Exception e)
        {

            WritingMsg(message, e, "Warning");
            //Успешное завершение
        }

        public void WarningUnique(string message)
        {
            //WritingMsg(message, e, "Warning");
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

            System.Threading.Thread.Sleep(1000);
            
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
                    message = message.Insert(0, "  " + e.StackTrace);
                    message = message.Insert(0, "  " + TypeOfError);
                    message = message.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  ");
                    message = message.Insert(message.Length, "\n");

                    fstream.Write(Encoding.Default.GetBytes(message), 0, Encoding.Default.GetBytes(message).Length);
                    fstream.Flush();

                }


            }
        }
    }
}
