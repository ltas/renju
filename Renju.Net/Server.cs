using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Collections;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Renju.Net
{
    /// <summary>
    /// Клас серверу гри.
    /// </summary>
    class Server
    {
        public static Hashtable clientsList = new Hashtable();
        public static Hashtable clientsColorsList = new Hashtable();

        List<Chess.Color> avaliableColors = new List<Chess.Color>() { Chess.Color.Black, Chess.Color.White, Chess.Color.Green, Chess.Color.Blue };
                
        private int counter = 0;
        private TcpClient clientSocket;
        private TcpListener serverSocket;
        private bool stopCondition = true;
        public static Game _game;

        private string[] playersNames = new string[] { "", "", "", "" };
        private int iPlayersNames = -1;



        private int maxClientsNumber = 0 ;

        /// <summary>
        /// Ініціалізація серверу.
        /// </summary>
        /// <param name="maxClients">Максимальна кількість клієнтів.</param>
        /// <param name="serverPort">Порт сервера.</param>
        public Server(int maxClients, int serverPort)
        {
            serverSocket = new TcpListener(serverPort);
            clientSocket = default(TcpClient); 
            
            serverSocket.Start();
            counter = 0;
            maxClientsNumber = maxClients;            
        }

        public static Thread serverThread;

        ~Server()
        {
            //serverThread.Interrupt();
            //clientSocket.Close();
            //serverSocket.Stop();
        }

        /// <summary>
        /// Запуск прослуховування порта сервера в окремуму потоці.
        /// </summary>
        public void ServerLoop()
        {
            serverThread = new Thread(this.ServerListnening);
            serverThread.IsBackground = true;
            serverThread.Start();
        }


        /// <summary>
        /// Проcлуховування порту сервера.
        /// </summary>
        void ServerListnening()
        {
            bool sendStart = false;
            while ((stopCondition))
            {
                if (clientsList.Keys.Count < maxClientsNumber)
                {
                    counter += 1;
                    clientSocket = serverSocket.AcceptTcpClient();

                    byte[] bytesFrom = new byte[10025];
                    string dataFromClient = null;

                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.UTF8.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    clientsList.Add(dataFromClient, clientSocket);
                    clientsColorsList.Add(dataFromClient, avaliableColors.First());
                    avaliableColors.Remove(avaliableColors.First());

                    playersNames[++iPlayersNames] = dataFromClient;


                    broadcast(dataFromClient + " долучився", dataFromClient, false);
                    System.Threading.Thread.Sleep(100);

                    // якщо всі гравці підєднались
                    if (clientsList.Keys.Count == maxClientsNumber && !sendStart)
                    {
                        sendStart = true;
                        _game = new Game();
                        _game.reset(maxClientsNumber);

                        string players = "_players ";
                        players += Convert.ToString(maxClientsNumber) + " ";

                        foreach (string Item in playersNames)
                        {
                            players += (string)Item + " ";
                        }
                        //broadcast(players, "", true);

                        
                        

                        foreach (DictionaryEntry Item in clientsList)
                        {
                            string gameBeginMsg = players;
                            TcpClient broadcastSocket;
                            broadcastSocket = (TcpClient)Item.Value;

                            gameBeginMsg += "_gameBegin " + Convert.ToString(clientsColorsList[(string)Item.Key] );
                            localcast(gameBeginMsg, broadcastSocket);
                            
                            //broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                            //broadcastStream.Flush();
                            //System.Threading.Thread.Sleep(100);
                        }
                        
                    }
                    handleClinet client = new handleClinet();
                    client.startClient(clientSocket, dataFromClient, clientsList);
                }
            }
            //clientSocket.Close();
            serverSocket.Stop();            
        }

        private static void localcast(string msg, TcpClient client)
        {
            TcpClient broadcastSocket;
            broadcastSocket = client;
            NetworkStream broadcastStream = broadcastSocket.GetStream();
            Byte[] broadcastBytes = null;
            broadcastBytes = Encoding.UTF8.GetBytes(msg);
            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
            broadcastStream.Flush();
        }
        
        /// <summary>
        /// Відправлення всім клієнтам повідомлення.
        /// </summary>
        /// <param name="msg">Текст повідомлення.</param>
        /// <param name="uName">Імя гравця.</param>
        /// <param name="flag">false - гравець тільки підєднався, інакше true.</param>
        public static void broadcast(string msg, string uName, bool flag)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                if (flag == true)
                {                    
                    broadcastBytes = Encoding.UTF8.GetBytes(msg);
                }
                else
                {                    
                    broadcastBytes = Encoding.UTF8.GetBytes("connect " + msg);
                }

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }

        /// <summary>
        /// Зупинка прослуховування порта сервером.
        /// </summary>
        public void stopServer()
        {
            stopCondition = false;
            serverThread.Interrupt();
            //clientSocket.Close();
            serverSocket.Stop();

        }

    }

    /// <summary>
    /// Клас для роботи з клієнтом.
    /// </summary>
    public class handleClinet
    {
        private Chess.Color _currentColor;
        TcpClient clientSocket;
        string clNo;
        Hashtable clientsList;
        private static char[] sep = new char[] { ',', '-', ' ' };
        private Thread ctThread;

        /// <summary>
        /// Ініціалізація клієнта. Запуск очікування повідомлень від клієнта в окремому потоці.
        /// </summary>
        /// <param name="inClientSocket">Сокет клієнта.</param>
        /// <param name="clineNo">Імя клієнта.</param>
        /// <param name="cList">Список клієнтів.</param>
        public void startClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            this.clientsList = cList;
            ctThread = new Thread(doChat);
            ctThread.IsBackground = true;
            ctThread.Start();
        }
        
        /// <summary>
        /// Очікування та обробка повідомлень від клієнта.
        /// </summary>
        private void doChat()
        {   
            int requestCount = 0;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;
            string rCount = null;
            requestCount = 0;
            bool cond = true;
            while ((cond))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.UTF8.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));                    
                    rCount = Convert.ToString(requestCount);

                    dataFromClient = parseMessage(dataFromClient);

                    Server.broadcast(dataFromClient, clNo, true);
                }
                catch (Exception)
                {
                    cond = false;
                    ctThread.Interrupt();
                }
            }
            
        }        

        /// <summary>
        /// Обробка повідомлення від клієнта.
        /// </summary>
        /// <param name="mes">Текст повідомлення.</param>
        /// <returns>Відповідь сервера.</returns>
        private string parseMessage(string mes)
        {
            //broadcastBytes = Encoding.UTF8.GetBytes("chat" + uName + " написав: " + msg);
            string[] lexems = mes.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string pref = lexems[0];

            switch (pref)
            {
                case "chat":
                    return mes;
                case "_put":
                    if (String.Compare(lexems[1],"Black") == 0)
                        _currentColor = Chess.Color.Black;
                    if (String.Compare(lexems[1], "White") == 0)
                        _currentColor = Chess.Color.White;
                    if (String.Compare(lexems[1], "Blue") == 0)
                        _currentColor = Chess.Color.Blue;
                    if (String.Compare(lexems[1], "Green") == 0)
                        _currentColor = Chess.Color.Green;
                    int xG = Convert.ToInt32(lexems[2]);
                    int yG = Convert.ToInt32(lexems[3]);
                    if (Server._game.putChess(_currentColor, xG, yG))
                    {                       
                        return mes;
                    }
                    else
                        return "simplyTrying";
                default:
                    return mes;                    
            }                
        }
    }


   
}
