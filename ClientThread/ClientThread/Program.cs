using System;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ClientThread
{
  class Read
   {
    public void read(object obj)
     {
      TcpClient tcpClient = (TcpClient)obj;
      StreamReader sReader = new StreamReader(tcpClient.GetStream());
      while(true)
      {
        try
        {
         // baca pesan masuk
         string message = sReader.ReadLine();
         Console.WriteLine(message);
         
         }
         catch (Exception e)
         {
         Console.WriteLine(e.Message);
         break;
         }
        }
      }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Read reader = new Read();
            try
            {
                //this computer  
                TcpClient tcpClient = new TcpClient("127.0.0.1", 5000);
                Console.WriteLine("Connected to server, now input your name!");

                Thread thread = new Thread(reader.read);
                thread.Start(tcpClient);

                StreamWriter sWriter = new StreamWriter(tcpClient.GetStream());

                while (true)
                {
                    if (tcpClient.Connected)
                    {
                        //kirim pesan
                        string input = Console.ReadLine();
                        sWriter.WriteLine(input);
                        sWriter.Flush();
                    }
                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            Console.ReadKey();
        }
    }
}