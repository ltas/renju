using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Renju.Net
{
    /// <summary>
    /// Клас, що реалізує, графіку для клієнтом та звязок з сервером.
    /// </summary>
    public partial class Renju : Form
    {

        #region privateFielsds
        private string playerName;
        private int playersQuantity = 0;

        private int serverPort; // server
        private string ip;      // server
        private string port;
        private Server lServer;        

        private string localIP = "";
        private int localPort;

        private Chess.Color _currentColor;
        private Chess.Color _currentPlayer = Chess.Color.Black;

        private List<Chess> _chesses = new List<Chess>();
        #endregion

        /// <summary>
        /// Містить останнє отримане повідомлення.
        /// </summary>
        public string lastChatMessage;        

        /// <summary>
        /// Ініціалізація гри.
        /// </summary>
        public Renju()
        {
            InitializeComponent();
            playersQuantityBox.SelectedIndex = 0;
                        
            //ListViewItem li = new ListViewItem();
            //li.ForeColor = Color.Gray;
            //li.Text = "Sample";
            //listView1.Items.Add(li);            
        }

        /// <summary>
        /// Логіка відображення компонентів інтерфейсу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Simple interface
                        
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        /// <summary>
        /// Відображення меню створення нової гри.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newGame(object sender, EventArgs e)
        {
            startMenu.Hide();
            createNewGameBox.Show();
            localPortTextBox.Text = "8888";
            localIP = GetLocalIP();
            label6.Text = "IP: " + localIP;
        }


        /// <summary>
        /// Визначення IP
        /// </summary>
        /// <returns>IP</returns>
        private string GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }

        

        /// <summary>
        /// Створення нового серверу гри.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (serverNameTextBox.Text == "")
                {
                    MessageBox.Show("Введіть ім'я");
                    serverNameTextBox.Focus();
                    return;
                }
                if (localPortTextBox.Text == "")
                {
                    MessageBox.Show("Введіть порт");
                    localPortTextBox.Focus();
                    return;
                }
                else
                {
                    localPort = Convert.ToInt32(localPortTextBox.Text);

                    playersQuantity = Convert.ToInt32(playersQuantityBox.SelectedItem);
                    playerName = serverNameTextBox.Text;
                    login = playerName;
                    //create server                    
                    lServer = new Server(Convert.ToInt32(playersQuantityBox.SelectedItem), localPort);
                    lServer.ServerLoop();                    
                    connectToServer(localIP, localPort);
                    
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message + "\n\n Програма завершить роботу.");
                Application.Exit();
            }
            
                
            createNewGameBox.Hide();
            playersGroupBox.Show();
            
                    
            
        }

        /// <summary>
        /// Відображення меню для підключення до існуючого сервера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectBtn_Click(object sender, EventArgs e)
        {
            startMenu.Hide();
            connectBox.Show();

            ipTextBox.Text = GetLocalIP();
            portTextBox.Text = "8888";
        }   

        /// <summary>
        /// Підєднання до вже існуючого сервера.
        /// </summary>        
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                serverPort = Convert.ToInt32(portTextBox.Text);

                if (playerNameTextBox.Text == "")
                {
                    MessageBox.Show("Введіть ім'я");
                    playerNameTextBox.Focus();
                    return;
                }
                if (ipTextBox.Text == "")
                {
                    MessageBox.Show("Введіть IP");
                    ipTextBox.Focus();
                    return;
                }
                if (portTextBox.Text == "")
                {
                    MessageBox.Show("Введіть порт");
                    portTextBox.Focus();
                    return;
                }
                else
                {
                    ip = ipTextBox.Text;
                    port = portTextBox.Text;
                    playerName = playerNameTextBox.Text;
                    connectBox.Hide();
                    playersGroupBox.Show();
                    login = playerName;
                    
                    connectToServer(ip, serverPort);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Завершення роботи серверу при виході з додатку.
        /// </summary>        
        private void Renju_FormClosing(object sender, FormClosingEventArgs e)
        {            
            if (serverNameTextBox.Text != "")         
                lServer.stopServer();
            _gameBegin = false;
        }

        /// <summary>
        /// Выдправлення повыдомлення по чату.
        /// </summary>        
        private void sendMessageBtn_Click(object sender, EventArgs e)
        {
            try
            {
                sendMessage("chat " + playerName + ": " + msgBox.Text);
            }
                catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                msgBox.Text = "";
            }
            msgBox.Text = "";
        }
        #endregion       

        #region Client

        private System.Net.Sockets.TcpClient clientSocket;
        private NetworkStream serverStream = default(NetworkStream);
        private string readData = null;
        private string login;
        private static char[] sep = new char[] { ',', '-', ' ' };
        private bool exCondition = true;


        /// <summary>
        /// Містить останнє повідомлення
        /// </summary>
        public string message;

        /// <summary>
        /// Відправка повідомлення на сервер.
        /// </summary>
        /// <param name="msgText">Текст повідомлення.</param>
        public void sendMessage(string msgText)
        {
            try
            {

                byte[] outStream = System.Text.Encoding.UTF8.GetBytes(msgText + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(msgText);
            }
        }

        private Thread ctThread;

        /// <summary>
        /// З'єднання з сервером.
        /// </summary>
        private void connectToServer(string ip, int port)
        {
            readData = "Під'єднання до серверу ...";
            msg();
            clientSocket = new System.Net.Sockets.TcpClient();
            clientSocket.Connect(ip, port);
            serverStream = clientSocket.GetStream();

            byte[] outStream = System.Text.Encoding.UTF8.GetBytes(login + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            ctThread = new Thread(getMessage);
            ctThread.IsBackground = true;
            ctThread.Start();
        }

        /// <summary>
        /// Прослуховування порта для обмын повыдленнями з сервером.
        /// </summary>
        private void getMessage()
        {            
            while (exCondition)
            {
                try
                {
                    serverStream = clientSocket.GetStream();
                    int buffSize = 0;
                    byte[] inStream = new byte[10025];
                    buffSize = clientSocket.ReceiveBufferSize;
                    serverStream.Read(inStream, 0, buffSize);
                    string returndata = System.Text.Encoding.UTF8.GetString(inStream);
                    readData = "" + returndata;
                    parseMessage(readData);
                }
                catch (Exception )
                {
                    //exCondition = false;
                    //MessageBox.Show(ex.Message);
                    //_gameBegin = false;
                    //_isGameOver = false;
                    //playersGroupBox.Hide();
                    //startMenu.Show();
                    //listView1.Clear();
                    //Invalidate(false);
                    //if (serverNameTextBox.Text != "")
                    //    lServer.stopServer();                    
                    //serverNameTextBox.Text = "";
                    //playerNameTextBox.Text = "";

                }
            }
        }
        


        /// <summary>
        /// Аналіз вхідного повідомлення.
        /// </summary>
        /// <param name="mes">Текст повідомлення.</param>
        private void parseMessage(string mes)
        {
            string mesCopy = mes;
            string[] lexems = mesCopy.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string pref = lexems[0];
            int xPut, yPut;
            Chess newChess;

            switch (pref)
            {   
                case "chat":
                    readData = readData.Replace("chat ", "");
                    msg();
                    break;
                case "connect":
                    readData = readData.Replace("connect ", "");
                    msg();
                    break;
                case "_put":
                    {
                        try
                        {
                            if (String.Compare(lexems[1], "Black") == 0)
                                _currentPlayer = Chess.Color.Black;
                            if (String.Compare(lexems[1], "White") == 0)
                                _currentPlayer = Chess.Color.White;
                            if (String.Compare(lexems[1], "Blue") == 0)
                                _currentPlayer = Chess.Color.Blue;
                            if (String.Compare(lexems[1], "Green") == 0)
                                _currentPlayer = Chess.Color.Green;
                            xPut = Convert.ToInt32(lexems[2]);
                            yPut = Convert.ToInt32(lexems[3]);
                            newChess = new Chess();
                            newChess.XLoc = xPut;
                            newChess.YLoc = yPut;
                            newChess.ChessColor = _currentPlayer;
                            _chesses.Add(newChess);
                            Invalidate(false);
                            _currentPlayer = Chess.Switch(_currentPlayer, playersQuantity);
                            isGameOver();
                            if (_isGameOver)
                            {
                                readData = "Виграє гравець з коліром: " + lexems[1];
                                MessageBox.Show(readData);
                                Application.Restart();
                                //playAgain();

                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("For fun");
                        }

                    }; break;
               case "_players":
                    playersString = mes;
                    playersQuantity = Convert.ToInt32(lexems[1]);
                    showPlayersNames();                           
                    //MessageBox.Show(mes + Convert.ToString(lexems.Length) + " AAAAAAAAAAAA");
                    _gameBegin = true;
                    _isGameOver = false;
                    exCondition = true;
                    //this._game.reset(playersQuantity);                                              
                    Invalidate(false);
                    
                    

                    if (String.Compare(lexems[lexems.Length-1], "Black") == 0)
                        _currentColor = Chess.Color.Black;
                    if (String.Compare(lexems[lexems.Length-1], "White") == 0)
                        _currentColor = Chess.Color.White;
                    if (String.Compare(lexems[lexems.Length-1], "Blue") == 0)
                        _currentColor = Chess.Color.Blue;
                    if (String.Compare(lexems[lexems.Length-1], "Green") == 0)
                        _currentColor = Chess.Color.Green;
                    

                    
                    break;
                    
            }
        }

        private void playAgain()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(playAgain));
            else
            {                
                _gameBegin = false;
                _isGameOver = false;
                playersGroupBox.Hide();
                startMenu.Show();
                listView1.Clear();
                Invalidate(false);
                if (serverNameTextBox.Text != "")
                    lServer.stopServer();
                ctThread.Interrupt();
                if (playerNameTextBox.Text != "")
                    exCondition = false;
                serverNameTextBox.Text = "";
                playerNameTextBox.Text = "";
                clientSocket.Close();               
            }

        }
        private string playersString = "";
        
        /// <summary>
        /// Виводить список активних гравців.
        /// </summary>
        private void showPlayersNames()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(showPlayersNames));
            else
            {
                string mesCopy = playersString.Replace("_players ", "");
                string[] lexems = mesCopy.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                //MessageBox.Show(playersString);

                ListViewItem li = new ListViewItem();
                li.ForeColor = Color.Black;
                li.Text = lexems[1];
                listView1.Items.Add(li);

                li = new ListViewItem();
                li.ForeColor = Color.White;
                li.Text = lexems[2];
                listView1.Items.Add(li);

                //MessageBox.Show(mesCopy+ " " + Convert.ToString(playersQuantity));

                if (playersQuantity > 2)
                {

                    try
                    {
                        ListViewItem li3 = new ListViewItem();
                        li3.ForeColor = Color.Blue;
                        li3.Text = lexems[3];
                        listView1.Items.Add(li3);

                    }
                    catch (Exception)
                    {
                    }
                    if (playersQuantity > 3)
                    {
                        try
                        {
                            ListViewItem li4 = new ListViewItem();
                            li4.ForeColor = Color.Green;
                            li4.Text = lexems[4];
                            listView1.Items.Add(li4);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Перевірка чи завершилась гра.
        /// </summary>
        public void isGameOver()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(isGameOver));
            else
            {
                try
                {
                    bool somef = false;
                    if (this._chesses.Count < 9)
                        return;

                    Chess last = this._chesses[this._chesses.Count - 1];

                    int xLoc = last.XLoc;
                    int yLoc = last.YLoc;
                    int count = 0;

                    // vertical
                    while (yLoc-- > 0)
                    {
                        somef = false;
                        foreach (Chess ch in this._chesses)
                        {
                            if (ch.XLoc == xLoc && ch.YLoc == yLoc && ch.ChessColor == last.ChessColor)
                            {
                                somef = true;
                                break;
                            }
                        }

                        if (somef)
                        {
                            count++;
                            if (4 == count)
                            {
                                _isGameOver = true;
                                return;
                            }
                        }
                        else
                            break;
                    }
                    yLoc = last.YLoc;
                    while (yLoc++ < LINE_COUNT)
                    {
                        somef = false;
                        foreach (Chess ch in this._chesses)
                        {
                            if (ch.XLoc == xLoc && ch.YLoc == yLoc && ch.ChessColor == last.ChessColor)
                            {
                                somef = true;
                                break;
                            }
                        }
                        if (somef)
                        {
                            count++;
                            if (4 == count)
                            {
                                _isGameOver = true;
                                return;
                            }

                        }
                        else
                            break;
                    }

                    // horizontal
                    count = 0;
                    xLoc = last.XLoc;
                    yLoc = last.YLoc;
                    while (xLoc-- > 0)
                    {
                        somef = false;
                        foreach (Chess ch in this._chesses)
                        {
                            if (ch.XLoc == xLoc && ch.YLoc == yLoc && ch.ChessColor == last.ChessColor)
                            {
                                somef = true;
                                break;
                            }
                        }
                        if (somef)
                        {
                            count++;
                            if (4 == count)
                            {
                                _isGameOver = true;
                                return;
                            }
                        }
                        else
                            break;
                    }
                    xLoc = last.XLoc;
                    while (xLoc++ < LINE_COUNT)
                    {
                        somef = false;
                        foreach (Chess ch in this._chesses)
                        {
                            if (ch.XLoc == xLoc && ch.YLoc == yLoc && ch.ChessColor == last.ChessColor)
                            {
                                somef = true;
                                break;
                            }
                        }
                        if (somef)
                        {
                            count++;
                            if (4 == count)
                            {
                                _isGameOver = true;
                                return;
                            }
                        }
                        else
                            break;
                    }

                    // 1-3 half splitter
                    count = 0;
                    xLoc = last.XLoc;
                    yLoc = last.YLoc;
                    while (xLoc++ < LINE_COUNT && yLoc-- > 0)
                    {
                        somef = false;
                        foreach (Chess ch in this._chesses)
                        {
                            if (ch.XLoc == xLoc && ch.YLoc == yLoc && ch.ChessColor == last.ChessColor)
                            {
                                somef = true;
                                break;
                            }
                        }
                        if (somef)
                        {
                            count++;
                            if (4 == count)
                            {
                                _isGameOver = true;
                                return;
                            }

                        }
                        else
                            break;
                    }
                    xLoc = last.XLoc;
                    yLoc = last.YLoc;
                    while (xLoc-- > 0 && yLoc++ < LINE_COUNT)
                    {
                        somef = false;
                        foreach (Chess ch in this._chesses)
                        {
                            if (ch.XLoc == xLoc && ch.YLoc == yLoc && ch.ChessColor == last.ChessColor)
                            {
                                somef = true;
                                break;
                            }
                        }
                        if (somef)
                        {
                            count++;
                            if (4 == count)
                            {
                                _isGameOver = true;
                                return;
                            }
                        }
                        else
                            break;
                    }

                    // 2-4 half splitter
                    count = 0;
                    xLoc = last.XLoc;
                    yLoc = last.YLoc;
                    while (xLoc-- > 0 && yLoc-- > 0)
                    {
                        somef = false;
                        foreach (Chess ch in this._chesses)
                        {
                            if (ch.XLoc == xLoc && ch.YLoc == yLoc && ch.ChessColor == last.ChessColor)
                            {
                                somef = true;
                                break;
                            }
                        }
                        if (somef)
                        {
                            count++;
                            if (4 == count)
                            {
                                _isGameOver = true;
                                return;
                            }

                        }
                        else
                            break;
                    }
                    xLoc = last.XLoc;
                    yLoc = last.YLoc;
                    while (xLoc++ < LINE_COUNT && yLoc++ < LINE_COUNT)
                    {
                        somef = false;
                        foreach (Chess ch in this._chesses)
                        {
                            if (ch.XLoc == xLoc && ch.YLoc == yLoc && ch.ChessColor == last.ChessColor)
                            {
                                somef = true;
                                break;
                            }
                        }
                        if (somef)
                        {
                            count++;
                            if (4 == count)
                            {
                                _isGameOver = true;
                                return;
                            }
                        }
                        else
                            break;
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Не роби так)");
                }
            
            }
            
        }

        /// <summary>
        /// Виведення повыдомлення чату.
        /// </summary>
        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
                chatBox.Items.Add(readData);
        }

        #endregion

        #region Board

        #region Private fields
        private const int CELL_WIDTH = 32;
        private const int MIN_DISTANCE = 10;
        private const int LINE_COUNT = 15;

        private readonly Pen _borderPen = new Pen(Color.Black, 3);
        private readonly Pen _linePen = new Pen(Color.Black, 1);
        private readonly Point _startPoint = new Point(50, 60);
        private readonly Size _boardSize = new Size(CELL_WIDTH * (LINE_COUNT - 1), CELL_WIDTH * (LINE_COUNT - 1));
        
        private Game _game = new Game();
        private bool _isGameOver = false;

        private bool _gameBegin = false;

        #endregion

        /// <summary>
        /// Перемалювання поля.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            try
            {
                if (_gameBegin)
                {
                    drawBoard(e.Graphics);
                    drawChess(e.Graphics);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Малюєм поле");
            }
        }

        #region for on paint methods
        private void drawBoard(Graphics graph)
        {
            // draw lines.
            Point up = new Point();
            Point down = new Point();
            Point left = new Point();
            Point right = new Point();
            for (int i = 1; i <= LINE_COUNT; ++i)
            {
                up.X = _startPoint.X + (i - 1) * CELL_WIDTH - 16;
                up.Y = _startPoint.Y - 16;
                down.X = up.X;
                down.Y = up.Y + _boardSize.Height;

                left.X = _startPoint.X - 16;
                left.Y = _startPoint.Y + (i - 1) * CELL_WIDTH - 16;
                right.X = left.X + _boardSize.Width;
                right.Y = left.Y;

                graph.DrawString(i.ToString(), SystemFonts.DefaultFont, Brushes.Black,
                    up.X - (int)(SystemFonts.DefaultFont.Size / 2),
                    up.Y - SystemFonts.DefaultFont.Size * 2,
                    StringFormat.GenericDefault);
                if ((i == 1) || (i == LINE_COUNT))
                {
                    graph.DrawLine(_borderPen, up, down);
                }
                else
                {
                    graph.DrawLine(_linePen, up, down);
                }

                if (i > 1)
                {
                    graph.DrawString(i.ToString(), SystemFonts.DefaultFont, Brushes.Black,
                        left.X - SystemFonts.DefaultFont.Size * 2,
                        left.Y - (int)(SystemFonts.DefaultFont.Size / 2),
                        StringFormat.GenericDefault);
                }
                if ((i == 1) || (i == LINE_COUNT))
                {
                    graph.DrawLine(_borderPen, left, right);
                }
                else
                {
                    graph.DrawLine(_linePen, left, right);
                }

                // draw points.
                int wid = 10;
                Rectangle rect = new Rectangle();
                rect.Width = rect.Height = wid;
                int xLoc = _startPoint.X + i * CELL_WIDTH;
                int yLoc = _startPoint.Y + i * CELL_WIDTH;
                if (i == LINE_COUNT / 3 - 2)
                {
                    rect.Location = reviseLocation(wid / 2, new Point(xLoc - CELL_WIDTH / 2, yLoc - CELL_WIDTH / 2));
                    graph.FillPie(Brushes.Black, rect, 0, 360);

                    rect.Location = reviseLocation(wid / 2, new Point(xLoc - CELL_WIDTH / 2, yLoc + (LINE_COUNT / 2 + 1) * CELL_WIDTH - CELL_WIDTH / 2));
                    graph.FillPie(Brushes.Black, rect, 0, 360);
                }
                else if (i == LINE_COUNT / 2)
                {
                    rect.Location = reviseLocation(wid / 2, new Point(xLoc - CELL_WIDTH / 2, yLoc - CELL_WIDTH / 2));
                    graph.FillPie(Brushes.Black, rect, 0, 360);
                }
                else if (i == LINE_COUNT - LINE_COUNT / 3 + 1)
                {
                    rect.Location = reviseLocation(wid / 2, new Point(xLoc - CELL_WIDTH / 2, yLoc - (LINE_COUNT / 2 + 1) * CELL_WIDTH - CELL_WIDTH / 2));
                    graph.FillPie(Brushes.Black, rect, 0, 360);

                    rect.Location = reviseLocation(wid / 2, new Point(xLoc - CELL_WIDTH / 2, yLoc - CELL_WIDTH / 2));
                    graph.FillPie(Brushes.Black, rect, 0, 360);
                }
            }
        }

        private Point reviseLocation(int revise, Point point)
        {
            return new Point(point.X - revise, point.Y - revise);
        }

        private void drawChess(Graphics graph)
        {
            //foreach (Chess ch in this._game.getChesses())
            foreach (Chess ch in _chesses)
            {
                Rectangle rect = new Rectangle();
                rect.Width = rect.Height = CELL_WIDTH - 2;
                rect.Location = makeLocation(ch);
                Brush fillBrush;
                switch (ch.ChessColor)
                {
                    case Chess.Color.Black:
                        fillBrush = Brushes.Black;
                        break;
                    case Chess.Color.White:
                        fillBrush = Brushes.White;
                        break;
                    case Chess.Color.Blue:
                        fillBrush = Brushes.Blue;
                        break;
                    case Chess.Color.Green:
                        fillBrush = Brushes.Green;
                        break;
                    default:
                        fillBrush = Brushes.Black;
                        break;
                }
                graph.FillPie(fillBrush, rect, 0, 360);
            }
        }

        private Point makeLocation(Chess ch)
        {
            Point pt = new Point(
                _startPoint.X + ch.XLoc * CELL_WIDTH - 15,
                _startPoint.Y + ch.YLoc * CELL_WIDTH - 15);
            return pt;
        }
        #endregion

        /// <summary>
        /// Перевірка можливості ходу поточного клієнта.
        /// </summary>
        /// <returns></returns>
        private bool notMyStep()
        {
            if ((_currentColor == Chess.Color.Black && _currentPlayer == Chess.Color.Green
                || _currentColor == Chess.Color.White && _currentPlayer == Chess.Color.Black
                || _currentColor == Chess.Color.Blue && _currentPlayer == Chess.Color.White
                || _currentColor == Chess.Color.Green && _currentPlayer == Chess.Color.Blue)&& playersQuantity == 4)
                return false;
            if ((_currentColor == Chess.Color.Black && _currentPlayer == Chess.Color.White
                || _currentColor == Chess.Color.White && _currentPlayer == Chess.Color.Black) && playersQuantity == 2)
                return false;
            if ((_currentColor == Chess.Color.Black && _currentPlayer == Chess.Color.Blue
                || _currentColor == Chess.Color.White && _currentPlayer == Chess.Color.Black
                || _currentColor == Chess.Color.Blue && _currentPlayer == Chess.Color.White) && playersQuantity == 3)
                return false;

            return true;
        }

        /// <summary>
        /// Обробка кліку мишкою.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (_gameBegin)
            {
                if (((e.X < _startPoint.X)
                    || (e.Y < _startPoint.Y)
                    || (e.X > (_startPoint.X + _boardSize.Width))
                    || (e.Y > (_startPoint.Y + _boardSize.Height))
                    || _isGameOver) && notMyStep())
                    return;

                int locX = 0;
                int locY = 0;

                if (tryGetLocation(e, ref locX, ref locY))
                {
                    sendMessage("_put " + Convert.ToString(_currentColor) + " " + Convert.ToString(locX) + " " + Convert.ToString(locY));
                    Invalidate(false);
                }
            }
        }

        #region for on mouse Click

        /// <summary>
        /// Перевірка чи клік мишкою відбувся в межах ігрового поля.
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns></returns>
        private bool tryGetLocation(MouseEventArgs e, ref int x, ref int y)
        {
            int xCnt = (int)((e.X - _startPoint.X) / CELL_WIDTH);
            int yCnt = (int)((e.Y - _startPoint.Y) / CELL_WIDTH);

            // bottom-right corner within a cross
            //    |
            //----|-----
            //    | here     
            int crossX = _startPoint.X + CELL_WIDTH * xCnt;
            int crossY = _startPoint.Y + CELL_WIDTH * yCnt;
            if (e.X >= crossX && e.Y >= crossY)
            {
                if (e.X - crossX <= MIN_DISTANCE && e.Y - crossY <= MIN_DISTANCE)
                {
                    x = xCnt;
                    y = yCnt;
                    return true;
                }
            }

            // up-right 
            int crossX2 = _startPoint.X + CELL_WIDTH * xCnt;
            int crossY2 = _startPoint.Y + CELL_WIDTH * (yCnt + 1);
            if (e.X >= crossX2 && e.Y <= crossY2)
            {
                if (e.X - crossX2 <= MIN_DISTANCE && crossY2 - e.Y <= MIN_DISTANCE)
                {
                    x = xCnt;
                    y = yCnt + 1;
                    return true;
                }
            }

            // up-left 
            int crossX3 = _startPoint.X + CELL_WIDTH * (xCnt + 1);
            int crossY3 = _startPoint.Y + CELL_WIDTH * (yCnt + 1);
            if (e.X <= crossX3 && e.Y <= crossY3)
            {
                if (crossX3 - e.X <= MIN_DISTANCE && crossY3 - e.Y <= MIN_DISTANCE)
                {
                    x = xCnt + 1;
                    y = yCnt + 1;
                    return true;
                }
            }

            // bottom-left
            int crossX4 = _startPoint.X + CELL_WIDTH * (xCnt + 1);
            int crossY4 = _startPoint.Y + CELL_WIDTH * yCnt;
            if (e.X <= crossX4 && e.Y >= crossY4)
            {
                if (crossX4 - e.X <= MIN_DISTANCE && e.Y - crossY4 <= MIN_DISTANCE)
                {
                    x = xCnt + 1;
                    y = yCnt;
                    return true;
                }
            }

            return false;
        }

        #endregion

        private void OnNewGame(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //this._gameBegin = false;
            //playersQuantity = 0;
            //Invalidate(false);
        }   
        
        /// <summary>
        /// Виклик довідки про гру.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAbout(object sender, EventArgs e)
        {
            (new About()).ShowDialog();
        }

        #endregion      


    }

}
