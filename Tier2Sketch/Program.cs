using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Tier2Sketch
{
    class Program
    {
        //Socket stuff
        private static TcpClient _tcpClient;
        private static Stream _stream;

        //Dummy data to send to tier 3
        public static DummyObject dummy1 = new DummyObject() {name = "Troels, maybe?", age = 41, salaray = 4000.00};

        static void Main(string[] args)
        {
            //______________METHOD FOR RECIEVING DATA FROM TIER 3 _____________________________

            getConnection();
            
            string helloWorld = JsonSerializer.Serialize(new Request
            {
                EnumRequest = EnumRequest.GetFromTier3
            });

            byte[] getHelloWorld = Encoding.ASCII.GetBytes(helloWorld);
            _stream.Write(getHelloWorld, 0, getHelloWorld.Length);

            byte[] fromServerHelloWorld = new byte[1024 * 1024];
            int readHelloWorld = _stream.Read(fromServerHelloWorld, 0, fromServerHelloWorld.Length);
            string recievedHelloWorld = Encoding.ASCII.GetString(fromServerHelloWorld, 0, readHelloWorld);

            DummyObject dummy = JsonSerializer.Deserialize<DummyObject>(recievedHelloWorld);

            Console.WriteLine($"Name: {dummy.name}, Age: {dummy.age}, Salary: {dummy.salaray}");
            
            closeConnection();
            
            //______________METHOD FOR SENDING DATA TO TIER 3 _____________________________
            
            
            
            getConnection();

            string worldHello = JsonSerializer.Serialize(new Request
            {
                dummyObject = dummy1,
                EnumRequest = EnumRequest.SendToTier3
            });
            
            byte[] WorldHelloSend = Encoding.ASCII.GetBytes(worldHello);
            _stream.Write(WorldHelloSend, 0, WorldHelloSend.Length);
            
        }

        public static void getConnection()
        {
            _tcpClient = new TcpClient("localhost", 1236);
            _stream = _tcpClient.GetStream();
        }

        public static void closeConnection()
        {
            _tcpClient.Close();
            _stream.Close();
        }
    }
}