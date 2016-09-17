using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;

using System.Net.Sockets;
using System.Net;
using ZephorasWorld.UDPCLientServer.ZephorasWorld.UDPCLientServer;
using System.Threading;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace ZephorasWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members


        IPEndPoint server;


        private Socket clientSocket;

        // Client name
        private string name;

        // Server End Point
        private EndPoint epServer;

        // Data stream
        private byte[] dataStream = new byte[1024];

        // Display message delegate
        private delegate void DisplayMessageDelegate(string message);
        private DisplayMessageDelegate displayMessageDelegate = null;
        private object e;

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            progressBar1.Visibility = Visibility.Hidden;

           
        }


        private void Button_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {


            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
                Console.WriteLine("Hello");
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {





                string myConnection = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";
            MySqlConnection myConn = new MySqlConnection(myConnection);

            MySqlCommand connection = new MySqlCommand("SELECT * FROM `zephora`.character WHERE Username= '" + this.Username.Text + "' AND Password= '" + this.Password.Password + "';", myConn);



            MySqlDataReader myReader;
            myConn.Open();
            myReader = connection.ExecuteReader();

            int count = 0;


            while (myReader.Read())
            {
                count = count + 1;

            }


            if (count == 1)
            {
                OnLoading();



                progressBar1.Visibility = Visibility.Visible;







            }
            else if (count > 1)
            {
                label3.Content = "Duplicate usernames";
              
            }
            else
            {

              
                label3.Content = "Invalid connection";
            
                myConn.Close();


            }



        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {



            percentage.Content = e.ProgressPercentage.ToString() + "%";

            progressBar1.Value = e.ProgressPercentage;

            try
            {

                if (e.ProgressPercentage == 100)
                {
                    label3.Content = "Connected";


                    string username = Username.Text;
                    Zephora fs = new Zephora(username);
                    this.Close();
                    fs.ShowDialog();
                }
            }
            catch(Exception z)
            {
                MessageBox.Show("There was a problem connecting!" + z.ToString());
            }



        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {

           

            for (int i = 0; i < 100; i++)
            {
                

                
                (sender as BackgroundWorker).ReportProgress(i * 5);
                Thread.Sleep(100);
               
            }
        }

        public void OnLoading()
        {
            BackgroundWorker worker = new BackgroundWorker();


            
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;


                worker.RunWorkerAsync();


            

        }




        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                // Receive all data
                this.clientSocket.EndReceive(ar);

                // Initialise a packet object to store the received data
                LoginPacket receivedData = new LoginPacket(dataStream);







                // Update display through a delegate
                if (receivedData.ChatMessage != null)

                    this.Dispatcher.BeginInvoke(this.displayMessageDelegate, new object[] { receivedData.ChatMessage });









                // Reset data stream
                this.dataStream = new byte[1024];

                // Continue listening for broadcasts

                clientSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref epServer, new AsyncCallback(this.ReceiveData), null);



                this.displayMessageDelegate = new DisplayMessageDelegate(DisplayMessage);
            }
            catch (Exception e)
            {

            }
        }

        private void DisplayMessage(string message)
        {

        }

        private void SendData(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send Data: " + ex.Message, "UDP Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainScreen newForm = new MainScreen();
            newForm.ShowDialog();

        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            MainScreen newScreens = new MainScreen();
            newScreens.ShowDialog();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
               this.DragMove();
            
        }
    }
    }

        
    

