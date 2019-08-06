using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDP
{
    class Program
    {
        private static string filePath;
        private int port;
        private static string data;
        public static UdpClient udpClient;


        static void Main(string[] args)
        {
            udpClient = new UdpClient("127.0.0.1", 1234);

            filePath = @"C:\Users\Praktyka\Desktop\data2.txt";
            data = ReadDataFromFile(filePath);

            string tempDate = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 'S' && data[i + 1] == 'I' && data[i + 2] == 'M')
                {
                    i += 3;
                    SendData(tempDate);
                    Console.WriteLine(tempDate);
                    Thread.Sleep(200);
                    tempDate = "SIM";
                }

                if(data[i] != '\n')         // tu moze byc blad
                    tempDate += data[i];
            }
        }


        private static void SendData(string tempDate)
        {
            byte[] array = Encoding.UTF8.GetBytes(tempDate);
            udpClient.Send(array, array.Length);
        }

        private static string ReadDataFromFile(string path)
        {
            string dateReaded = null;

            if (File.Exists(path))
            {
                try
                {
                    dateReaded = File.ReadAllText(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading file error: {0}", e.ToString());
                }
            }
            return dateReaded;
        }
    }
}
