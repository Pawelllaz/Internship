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
        private static int port;
        private static string data;
        private static string address;
        private static int sleepMilisec;

        public static UdpClient udpClient;


        static void Main(string[] args)
        {
            filePath = @"C:\Users\Praktyka\Desktop\data2.txt";
            port = 1234;
            address = "127.0.0.4";
            sleepMilisec = 200;

            if (args.Length > 2)
            {
                address = args[0];
                port = Int32.Parse(args[1]);
                filePath = args[2];
            }
            if(args.Length == 4)
            {
                sleepMilisec = Int32.Parse(args[3]);
            }

            udpClient = new UdpClient(address, port);

            data = ReadDataFromFile(filePath);

            string tempDate = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 'S' && data[i + 1] == 'I' && data[i + 2] == 'M')
                {
                    i += 3;
                    SendData(tempDate);
                    Console.WriteLine(tempDate);
                    Thread.Sleep(sleepMilisec);
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
