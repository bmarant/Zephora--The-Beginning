using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Net.Sockets;
using System.Net;
using ZephorasWorld.Classes;
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using System.Xml;
using ZephoraWorld.UDPCLientServer;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace ZephorasWorld
{
    /// <summary>
    /// Interaction logic for World.xaml
    /// </summary>
    public partial class World : Window
    {
        int timeLeft = 30;
        int attacking = 0;

        public string classes;
        private const string hostName = "74.80.233.85";
        public string userName;
        public static double hpsstill = 0;
        public static double hitpointsdrop = 0;
        public static string message = "";
        public int level;
        public int experience;
        public string healths;
        public string wisdoms;
        public int enemyHitpointdrop;
        public static string charLevels;
        public double exper;
      
        public int levelup;
        public double leveledup;
        public double addExps;
      
     
        LevelingEntitiy newlevel;
        ArmorClass armor;
        public double speed = 0.05 * 100;
        public double multiplier = 0.02;
        DispatcherTimer timer;
        DispatcherTimer autoSaving;
        DispatcherTimer attacktimer;
        DispatcherTimer camping;
        DispatcherTimer regeneration;
        public string randomising;
        public int strValue;
        int g_count = 5;
        int l_count = 5;
        int m_count = 5;
        double manaLoss = 0;
        double manaStill = 0;
        double AccNumbers = 0;
        int NorthMovement = 0;
        int SouthMovement = 0;
        int EastMovement = 0;
        int WestMovement = 0;
        List<string> lootList = new List<string>();

        string playName;
        string sName;
        List<string> namesOfFriends = new List<string>();

        Player newClass = new Player();

        IPEndPoint server;






        #region Private Members

        // Client socket
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
        Entity prefs;
        public List<Entity> Entities = new List<Entity>() {
                                                            new Player() };
        const int WORLD_DIMENSION = 100;
        WorldCell[,] _cells = new WorldCell[WORLD_DIMENSION, WORLD_DIMENSION];
        


        #endregion
        public World()
        {

        }



        public World(string str_value, string classValue, string genders, string strength, string wisdom, string health, string dexterity, string charLevel, string expG, string user)
        {
            InitializeComponent();


           
                        ipaddresses.Content = GetIPAddress().ToString();

            userName = user;
             

            try
            {

              
                



                //  game_screen.AppendText(classValue + Environment.NewLine);



                UdpClient newClient = new UdpClient(3389);




                this.name = str_value;

                // Initialise a packet object to store the data to be sent
                Packet sendData = new Packet();
                sendData.ChatName = str_value;
                sendData.ChatMessage = null;
                sendData.ChatDataIdentifier = DataIdentifier.LogIn;


                // Initialise socket
                this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // Initialise server IP
                IPAddress serverIP = IPAddress.Parse("74.80.233.83");
                
                // Initialise the IPEndPoint for the server and use port 30000
                server = new IPEndPoint(serverIP, 4545);

                // Initialise the EndPoint for the server
                epServer = (EndPoint)server;

                // Get packet as byte array
                byte[] data = sendData.GetDataStream();

                // Send data to server
                clientSocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, epServer, new AsyncCallback(this.SendData), null);

                // Initialise data stream
                this.dataStream = new byte[1024];


                // Begin listening for broadcasts


                clientSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref epServer, new AsyncCallback(this.ReceiveData), null);





                strValue = Convert.ToInt32(strength);


                North.Content = "North: " + NorthMovement;
                South.Content = "South: " + SouthMovement;
                East.Content =  "East: " +  EastMovement;
                West.Content =  "West: " +  WestMovement;


                Console.WriteLine(classValue);

                


            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error: " + ex.Message, "UDP Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }




            game_screen.AppendText("Welcome to the Land of Zephora" + Environment.NewLine);




            //  game_screen.AppendText("You are sitting on your bed, just waking up from a deep sleep. You notice there is a [SWORD] and a [BREASTPLATE] sitting on a chair in the corner, to equip the items type /equip" + Environment.NewLine);
            //   game_screen.AppendText("If you wish to move your character, please enter [EAST] or [NORTH] to move your character around the house, or to exit the door." + Environment.NewLine);










            newlevel = new LevelingEntitiy(experience, level);



            expGained.Content = exper.ToString() + "/";



            newlevel.leveling = Convert.ToInt32(charLevel);


            charLevels = charLevel;


            double mathematicsSucks = newlevel.Experience * 0.40 * newlevel.leveling * newlevel.leveling * 2 + 160;
            string experiences = Convert.ToString(mathematicsSucks);


            expStill.Content = experiences;
            expValue.Maximum = mathematicsSucks;

            double percent = Convert.ToDouble(expG) / mathematicsSucks * 100;

            int percentage = Convert.ToInt32(percent);
            exper = Convert.ToDouble(expG);
            expGained.Content = expG;
            expPercentage.Content = percentage.ToString() + "%";
            expValue.Value = exper;





            pcexp();



            if (newlevel.level <= 1)
            {

                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(PCattack_Tick);
                timer.Interval = TimeSpan.FromSeconds(2.00);
                manaLoss = Convert.ToInt32(wisdom) / 2;

                manaStill = Convert.ToInt32(wisdom) / 2;

                mana_pot.Content = manaLoss.ToString() + "/" + manaStill.ToString();



                armor = new ArmorClass();
                ACNumbers.Content = armor.ACNumber / armor.ACNumber + 10 + 20 * newlevel.leveling;







                int percentagesss = Convert.ToInt32(ACNumbers.Content) / Convert.ToInt32(hpsstill + 5);

                percentageofAc.Content = percentagesss * 100 + "%";
            }

            if (newlevel.level >= 2 && newlevel.level <= 10)
            {
                
                
               
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(PCattack_Tick);
                timer.Interval = TimeSpan.FromSeconds(1.92);

                int healthAdd = 2;

                int healthIncrease = Convert.ToInt32(health) + 2;

                health = healthIncrease.ToString();

                hitpointsdrop = Convert.ToInt32(health) / 2;

                hpsstill = Convert.ToInt32(health) / 2;


                hitpointsdrop *= healthAdd;

                hpsstill *= healthAdd;



                char_hlt.Content = health.ToString();


                int manaIncrease = 4;

                int manaAdd = Convert.ToInt32(wisdom) + 3;

                wisdom = manaAdd.ToString();


                manaLoss = Convert.ToInt32(wisdom) / 2;

                manaStill = Convert.ToInt32(wisdom) / 2;


                manaLoss *= manaIncrease;

                manaStill *= manaIncrease;

                mana_pot.Content = manaLoss.ToString() + "/" + manaStill.ToString();



                hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();



                armor = new ArmorClass();
                ACNumbers.Content = armor.ACNumber / armor.ACNumber + 10 + 20 * newlevel.leveling;







                double percentages = Convert.ToDouble(ACNumbers.Content) / hpsstill;

                percentageofAc.Content = Convert.ToInt32(percentages * 100) + "%";


                game_screen.AppendText(percentages + Environment.NewLine);








            }
            if (newlevel.level >= 11 && newlevel.level <= 19)
            {


                double speeding = 1.92;

                Console.WriteLine(speeding);
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(PCattack_Tick);
                timer.Interval = TimeSpan.FromSeconds(1.84);


                int healthAdd = 2;

                int healthIncrease = Convert.ToInt32(health) + 3;

                health = healthIncrease.ToString();

                hitpointsdrop = Convert.ToInt32(health) / 2;

                hpsstill = Convert.ToInt32(health) / 2;


                hitpointsdrop *= healthAdd;

                hpsstill *= healthAdd;



                char_hlt.Content = health.ToString();


                int manaIncrease = 3;

                int manaAdd = Convert.ToInt32(wisdom) + 3;

                wisdom = manaAdd.ToString();


                manaLoss = Convert.ToInt32(wisdom) / 2;

                manaStill = Convert.ToInt32(wisdom) / 2;


                manaLoss *= manaIncrease;

                manaStill *= manaIncrease;

                mana_pot.Content = manaLoss.ToString() + "/" + manaStill.ToString();



                hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();



                armor = new ArmorClass();
                ACNumbers.Content = armor.ACNumber / armor.ACNumber + 10 + 20 * newlevel.leveling;






                double percentages = Convert.ToDouble(ACNumbers.Content) / hpsstill;

                percentageofAc.Content = Convert.ToInt32(percentages * 100) + "%";

                game_screen.AppendText(ACNumbers.Content + Environment.NewLine);








            }

            else if (newlevel.level >= 20 && newlevel.level <= 30)
            {



                double speeding = 1.92;

                Console.WriteLine(speeding);
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(PCattack_Tick);
                timer.Interval = TimeSpan.FromSeconds(1.50);


                int upper = 2;


                int healthIncreases = Convert.ToInt32(health) + 3;

                health = healthIncreases.ToString();

                hitpointsdrop = Convert.ToInt32(health) / 2 + 17;

                hpsstill = Convert.ToInt32(health) / 2 + 17;

                hitpointsdrop *= upper;

                hpsstill *= upper;




                char_hlt.Content = health.ToString();

                int manaIncrease = 7;

                int manaAdd = Convert.ToInt32(wisdom) + 3;

                wisdom = manaAdd.ToString();


                manaLoss = Convert.ToInt32(wisdom) / 2;

                manaStill = Convert.ToInt32(wisdom) / 2;


                manaLoss *= manaIncrease;

                manaStill *= manaIncrease;

                mana_pot.Content = manaLoss.ToString() + "/" + manaStill.ToString();


                hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();

                ArmorClass armor = new ArmorClass();
                ACNumbers.Content = armor.ACNumber / armor.ACNumber + 10 + 20 * newlevel.leveling;


                double percentages = Convert.ToDouble(ACNumbers.Content) / hpsstill;

                percentageofAc.Content = Convert.ToInt32(percentages * 100) + "%";


            }
            else if (newlevel.level >= 30 && newlevel.level <= 40)
            {


                double speeding = 1.92;

                Console.WriteLine(speeding);
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(PCattack_Tick);
                timer.Interval = TimeSpan.FromSeconds(1.37);

                int incresing = 4;


                int healthIncrease = Convert.ToInt32(health) + 3;

                health = healthIncrease.ToString();

                hitpointsdrop = Convert.ToInt32(health) / 2;

                hpsstill = Convert.ToInt32(health) / 2;

                hpsstill *= incresing;
                hitpointsdrop *= incresing;




                char_hlt.Content = health.ToString();

                int manaIncrease = 13;

                int manaAdd = Convert.ToInt32(wisdom) + 3;

                wisdom = manaAdd.ToString();


                manaLoss = Convert.ToInt32(wisdom) / 2;

                manaStill = Convert.ToInt32(wisdom) / 2;


                manaLoss *= manaIncrease;

                manaStill *= manaIncrease;

                mana_pot.Content = manaLoss.ToString() + "/" + manaStill.ToString();


                hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();

                ArmorClass armor = new ArmorClass();
                ACNumbers.Content = armor.ACNumber / armor.ACNumber + 10 + 20 * newlevel.leveling;


                double percentages = Convert.ToDouble(ACNumbers.Content) / hpsstill;

                percentageofAc.Content = Convert.ToInt32(percentages * 100) + "%";

            }
            else if (newlevel.level >= 40 && newlevel.level <= 50)
            {

               

             
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(PCattack_Tick);
                timer.Interval = TimeSpan.FromSeconds(1.42);


                int healt = 20;

                int healthIncrease = Convert.ToInt32(health);

                health = healthIncrease.ToString();

                hitpointsdrop = Convert.ToInt32(health) / 2;

                hpsstill = Convert.ToInt32(health) / 2;



                hitpointsdrop *= healt;

                hpsstill *= healt;




                char_hlt.Content = health.ToString();

                int manaIncrease = 23;

                int manaAdd = Convert.ToInt32(wisdom) + 3;

                wisdom = manaAdd.ToString();


                manaLoss = Convert.ToInt32(wisdom) / 2;

                manaStill = Convert.ToInt32(wisdom) / 2;


                manaLoss *= manaIncrease;

                manaStill *= manaIncrease;

                mana_pot.Content = manaLoss.ToString() + "/" + manaStill.ToString();


                hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();

                ArmorClass armor = new ArmorClass();
                ACNumbers.Content = armor.ACNumber / armor.ACNumber + 10 + 20 * newlevel.leveling;





                double percentages = Convert.ToDouble(ACNumbers.Content) / hpsstill;

                percentageofAc.Content = Convert.ToInt32(percentages * 100) + "%";


            }

            else if (newlevel.level < 2)
            {
                hitpointsdrop = Convert.ToInt32(health) / 2;

                hpsstill = Convert.ToInt32(health) / 2;


                char_hlt.Content = health.ToString();


                hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();

                ArmorClass armor = new ArmorClass();
                ACNumbers.Content = armor.ACNumber / armor.ACNumber + 10 + 20 * newlevel.leveling;



                int percentagesss = Convert.ToInt32(ACNumbers.Content) / Convert.ToInt32(hpsstill);

                percentageofAc.Content = percentagesss  + "%";


            }



            greater_count.Content = g_count.ToString();
            lesser_count.Content = l_count.ToString();
            minor_count.Content = m_count.ToString();

            EntityGender gender;

            char_name.Content = str_value;
            char_class.Content = classValue;
            char_str.Content = strength.ToString();
            char_wis.Content = wisdom.ToString();
            char_dex.Content = dexterity.ToString();
            char_level.Content = newlevel.leveling.ToString();

            int dex = Convert.ToInt32(dexterity);
            int str = Convert.ToInt32(strength);
            int wis = Convert.ToInt32(wisdom);
            int hlt = Convert.ToInt32(health);

            newClass = new Player(str_value, dex, str, wis, hlt);



         




            for (int i = 0; i < WORLD_DIMENSION; i++)
            {
                for (int j = 0; j < WORLD_DIMENSION; j++)
                {
                    _cells[i, j] = new WorldCell(i, j);
                }
            }

            Random random = new Random();



            foreach (var entity in Entities)
            {
                int x = random.Next(WORLD_DIMENSION);
                int y = random.Next(WORLD_DIMENSION);

                TextRange trss = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                trss.Text = $"spawning '{entity.Identity(str_value, hlt)}' at {x},{y}" + Environment.NewLine;


                trss.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);



                Debug.WriteLine($"spawning '{entity.Identity(str_value, hlt)}' at {x},{y}");
                _cells[x, y].AddEntity(entity);


                Packet sendData = new Packet();
                sendData.ChatName = name;

                sendData.ChatMessage = trss.Text;

                sendData.ChatDataIdentifier = DataIdentifier.Message;






                // Get packet as byte array
                byte[] byteData = sendData.GetDataStream();

                // Send packet to the server
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(this.SendData), null);
            }

            newClass = Entities.OfType<Player>().First();



            TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
            trs.Text = $"Welcome {name}" + Environment.NewLine;

            trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);



            classes = classValue;


            healths = health;
            wisdoms = wisdom;





            Client_Load();



         


            attacktimer = new DispatcherTimer();
            attacktimer.Tick += new EventHandler(attackTimer_Tick);
            attacktimer.Interval = new TimeSpan(0, 0, 2);

            camping = new DispatcherTimer();
            camping.Tick += new EventHandler(tmr_timer_Tick);
            camping.Interval = new TimeSpan(0, 0, 1);

            regeneration = new DispatcherTimer();
            regeneration.Tick += new EventHandler(regenration_tick);
            regeneration.Interval = new TimeSpan(0, 0, 6);

            autoSaving = new DispatcherTimer();
            autoSaving.Tick += new EventHandler(autoSaving_tick);
            autoSaving.Interval = new TimeSpan(0, 3, 0);

            autoSaving.Start();





        }

        private void autoSaving_tick(object sender, EventArgs e)
        {
            chat_box.ScrollToEnd();

           chat_box.AppendText("Auto Saving......." + Environment.NewLine);
            string conString = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";

            string Query = "Update zephora.charactercreated set level= '" + newlevel.leveling + "'  ,Health= '" + char_hlt.Content + "' ,expGained= '" + expGained.Content + "' WHERE Name = '" + char_name.Content + "';";
            MySqlConnection conDataBase = new MySqlConnection(conString);
            MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;


            try
            {
                conDataBase.Open();

                myReader = cmdDataBase.ExecuteReader();



                while (myReader.Read())
                {
                    game_screen.AppendText("Character was saved" + Environment.NewLine);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }




        }

        private void Client_Load()
        {
            // Initialise delegate
            this.displayMessageDelegate = new DisplayMessageDelegate(this.DisplayMessage);

        }

        private void regenration_tick(object sender, EventArgs e)
        {
            if(hitpointsdrop <= -1)
            {
                regeneration.Stop();
            }

            if (hitpointsdrop < hpsstill)
            {

                hitpointsdrop++;
                hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();

            }
            else
            {
                regeneration.Stop();
            }
        }



        private void tmr_timer_Tick(object sender, EventArgs e)
        {


            timeLeft--;


            if (timeLeft == 25)
            {
                chat_box.AppendText("You have " + timeLeft.ToString() + " to camp." + Environment.NewLine);
            }
            else if (timeLeft == 20)
            {
                chat_box.AppendText("You have " + timeLeft.ToString() + " to camp." + Environment.NewLine);
            }
            else if (timeLeft == 15)
            {
                chat_box.AppendText("You have " + timeLeft.ToString() + " to camp. " + Environment.NewLine);
            }
            else if (timeLeft == 10)
            {
                chat_box.AppendText("You have " + timeLeft.ToString() + " to camp. " + Environment.NewLine);
            }
            else if (timeLeft == 5)
            {
                chat_box.AppendText("You have " + timeLeft.ToString() + " to camp. " + Environment.NewLine);
            }
            else if (timeLeft == 0)
            {
                chat_box.AppendText("You have " + timeLeft.ToString() + " to camp. " + Environment.NewLine);

                 Client_FormClosing();
            
               
                LoadCharacter loadScreen = new LoadCharacter(userName);
                this.Close();
                loadScreen.ShowDialog();
               


            }
            chat_box.ScrollToEnd();
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chat_box.AppendText("You have 30 Seconds left" + Environment.NewLine);
            DispatcherTimer dispatchTimer = new DispatcherTimer();
            dispatchTimer.Tick += new EventHandler(tmr_timer_Tick);
            dispatchTimer.Start();


            





        }
        private void message_commands(object sender, RoutedEventArgs e)
        {


            if (commandline.Text == "/north" || commandline.Text == "north")
            {
                MovePlayerNorth();
                TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                trs.Text = "You have moved North" + Environment.NewLine;
                trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkBlue);
            }

            if (commandline.Text == "/south" || commandline.Text == "south")
            {



                

                MovePlayerSouth();


             
                TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
               
                trs.Text = "You have moved South " + Environment.NewLine;
               
                trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkRed);

              

               

                game_screen.ScrollToEnd();
            }
            if (commandline.Text == "/east" || commandline.Text == "east")
            {
                MovePlayerEast();
                TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                trs.Text = "You have moved East" + Environment.NewLine;
                trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkViolet);
            }
            if (commandline.Text == "/west" || commandline.Text == "west")
            {
                MovePlayerWest();
                TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                trs.Text = "You have moved West" + Environment.NewLine;
                trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Fuchsia);
            }


            //movements of character through world-- Enumerators/State Machine

            if (commandline.Text == "Start" || commandline.Text == "start" || commandline.Text == "go")
            {
                XmlTextReader text = new XmlTextReader("C:/Users/Brad/Documents/Visual Studio 2015/WebSites/Zephora/ParserMove.xml");
                while (text.Read())
                {
                    if (text.IsStartElement())
                    {
                        switch (text.Name)
                        {
                            case "Beginning":
                                Console.WriteLine("Beginning of Adventure<Beginning>");
                                break;


                            case "room":
                                string rooms = text["Starting"];
                                if (rooms != null)
                                {
                                    Console.WriteLine("Has room in game" + rooms);
                                }
                                if (text.Read())
                                {
                                    game_screen.AppendText(text.Value + Environment.NewLine);
                                }
                                break;


                        }
                    }

                }
            }

            if (commandline.Text == "North")
            {
                XmlTextReader moving = new XmlTextReader("C:/Users/Brad/Documents/Visual Studio 2015/WebSites/Zephora/ParserMove.xml");

                while (moving.Read())
                {
                    if (moving.IsStartElement())
                    {
                        switch (moving.Name)
                        {
                            case "Beginning":
                                break;
                            case "Woods":
                                string woods = moving["North"];
                                if (woods != null)
                                {
                                    Console.WriteLine("Has attribute of:" + woods);
                                }
                                if (moving.Read())
                                {

                                    game_screen.AppendText(moving.Value + Environment.NewLine);
                                }
                                break;
                        }
                    }
                }


            }
            if (commandline.Text == "East")
            {
                XmlTextReader movingEast = new XmlTextReader("C:/Users/Brad/Documents/Visual Studio 2015/WebSites/Zephora/ParserMove.xml");
                while (movingEast.Read())
                {
                    if (movingEast.IsStartElement())
                    {
                        switch (movingEast.Name)
                        {
                            case "Beginning":
                                Console.WriteLine("Heading east");
                                break;

                            case "Eastmovement":
                                string easts = movingEast["East"];

                                if (easts != null)
                                {
                                    Console.WriteLine("Headed east" + easts);
                                }
                                if (movingEast.Read())
                                {
                                    game_screen.AppendText(movingEast.Value + Environment.NewLine);
                                }
                                break;
                        }
                    }
                }
            }

            var experience = commandline.Text.Split(' ');
            int experienceAdded = 0;
            if (experience[0] == "#Exp")
            {

                if (experienceAdded >= 1 || experienceAdded <= 20000)
                {

                    experienceAdded = Convert.ToInt32(experience[1]);

                    exper += experienceAdded;

                  
                    expGained.Content = exper.ToString();

                    pcexp();



                }
            }




            //Sending Information to Chatbox/UDP Server



            string t = commandline.Text;






            try
            {
                // Initialise a packet object to store the data to be sent
                Packet sendData = new Packet();
                sendData.ChatName = this.name;

                sendData.ChatMessage = t;

                sendData.ChatDataIdentifier = DataIdentifier.Message;






                // Get packet as byte array
                byte[] byteData = sendData.GetDataStream();

                // Send packet to the server
                clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(this.SendData), null);







            }
            catch (Exception ex)
            {
                MessageBox.Show("Send Error: " + ex.Message, "UDP Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            var cmds = commandline.Text.Split(' ');






            if (cmds[0] == "/friend")
            {






                if (playName == char_name.Content.ToString())
                {
                    game_screen.AppendText("Cannot add yourself" + Environment.NewLine);
                }
                else
                {

                    try
                    {
                        playName = cmds[1];


                        string conString = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";

                        string Query = "SELECT * FROM `zephora`.charactercreated WHERE Name= '" + playName + "';";
                        MySqlConnection conDataBase = new MySqlConnection(conString);
                        MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                        MySqlDataReader myReader;






                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();

                        int count = 0;






                        while (myReader.Read())
                        {
                            count = count + 1;

                        }

                        if (count == 1)
                        {


                            string conStrings = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";

                            string Querys = "insert into zephora.friendslist(friend,character_name) VALUES ('" + playName + "', '" + char_name.Content.ToString() + "' ) ;";
                            MySqlConnection conDataBases = new MySqlConnection(conStrings);
                            MySqlCommand cmdDataBases = new MySqlCommand(Querys, conDataBases);


                            MySqlDataReader myReaders;

                            conDataBases.Open();
                            myReaders = cmdDataBases.ExecuteReader();


                            chat_box.AppendText(playName + " Was Added to your friends" + Environment.NewLine);





                        }
                        else if (count == 0)
                        {
                            chat_box.AppendText(playName + " Does not exist" + Environment.NewLine);
                        }

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex + "");
                    }
                }
            }

            if (commandline.Text == "/camp")
            {
                chat_box.AppendText("You have 30 Seconds left" + Environment.NewLine);


                camping.Start();



                chat_box.ScrollToEnd();

              

            }


            //Gets friends that are added to your list in the database.
            var cmdFriends = commandline.Text.Split(' ');

            if (cmdFriends[0] == "/friends")
            {
                string conStringes = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";
                string Queryes = "select * from zephora.friendslist where character_name= '" + char_name.Content.ToString() + "';";
                MySqlConnection conDataBasees = new MySqlConnection(conStringes);
                MySqlCommand cmdDataBasees = new MySqlCommand(Queryes, conDataBasees);
                MySqlDataReader myReaderes;


                try
                {
                    conDataBasees.Open();

                    myReaderes = cmdDataBasees.ExecuteReader();

                    int countName = 0;


                    while (myReaderes.Read())
                    {
                        sName = myReaderes.GetString("friend");

                        countName = countName + 1;


                    }
                    // Receive friends list from database
                    // List them if exist
                    if (countName == 1)
                    {
                        game_screen.AppendText("-----------Friends----------" + Environment.NewLine);
                        game_screen.AppendText(sName + Environment.NewLine);
                    }
                    else if (countName > 1)
                    {
                        game_screen.AppendText("You cannot add the same friend twice!" + Environment.NewLine);
                    }
                    else
                    {
                        game_screen.AppendText("awww how sad you have no friends!" + Environment.NewLine);
                    }



                }
                catch (Exception p)
                {

                }

            }


            if (commandline.Text == "#GM")
            {
                char_name.Foreground = System.Windows.Media.Brushes.Red;




               // bool GMstatus = false;

             

            }







            if (commandline.Text == "Heal1" || commandline.Text == "heal1")
            {




                if (greater_count.Content.ToString() == "0")
                {
                    TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    healing.Text = "You do not have any Greater Potions to use" + Environment.NewLine;
                    healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                    game_screen.ScrollToEnd();

                }
                else if (hitpointsdrop == hpsstill)
                {
                    game_screen.AppendText("You cannot heal yourself yet " + Environment.NewLine);
                    game_screen.ScrollToEnd();
                }
                else if (hitpointsdrop < hpsstill)
                {

                    if (newlevel.level >= 1 && newlevel.level <= 5)
                    {

                        g_count--;

                        double greaterPotion = 25;

                        greater_count.Content = g_count.ToString();

                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 25)
                        {
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 25)
                        {
                            greaterPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);

                        game_screen.ScrollToEnd();




                    }


                    if (newlevel.level >= 6 && newlevel.level <= 9)
                    {

                        g_count--;

                        double greaterPotion = 35;

                        greater_count.Content = g_count.ToString();




                        hitpointsdrop = hitpointsdrop + greaterPotion;

                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                        hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        game_screen.ScrollToEnd();

                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 35)
                        {
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 35)
                        {
                            greaterPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                    }

                    if (newlevel.level >= 10 && newlevel.level <= 15)
                    {

                        g_count--;

                        double greaterPotion = 38;

                        greater_count.Content = g_count.ToString();

                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 38)
                        {
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 38)
                        {
                            greaterPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                        game_screen.ScrollToEnd();
                    }
                    if (newlevel.level >= 16 && newlevel.level <= 21)
                    {

                        g_count--;

                        double greaterPotion = 41;

                        greater_count.Content = g_count.ToString();

                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 41)
                        {
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 41)
                        {
                            greaterPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                        game_screen.ScrollToEnd();
                    }
                    if (newlevel.level >= 22 && newlevel.level <= 25)
                    {

                        g_count--;

                        double greaterPotion = 44;

                        greater_count.Content = g_count.ToString();


                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 44)
                        {
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 44)
                        {
                            greaterPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }

                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                        game_screen.ScrollToEnd();
                    }
                    if (newlevel.level >= 26 && newlevel.level <= 31)
                    {

                        g_count--;

                        double greaterPotion = 47;

                        greater_count.Content = g_count.ToString();




                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 47)
                        {
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 47)
                        {
                            greaterPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + greaterPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                        game_screen.ScrollToEnd();
                    }

                }
                if (newlevel.level >= 31 && newlevel.level <= 35)
                {

                    g_count--;

                    double greaterPotion = 50;

                    greater_count.Content = g_count.ToString();




                    double differenceHps = hpsstill - hitpointsdrop;

                    if (differenceHps > 50)
                    {
                        hitpointsdrop = hitpointsdrop + greaterPotion;
                        hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                    }
                    else if (differenceHps < 50)
                    {
                        greaterPotion = differenceHps;
                        hitpointsdrop = hitpointsdrop + greaterPotion;
                        hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                    }
                    TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                    healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                    game_screen.ScrollToEnd();
                }
                if (newlevel.level >= 36 && newlevel.level <= 42)
                {

                    g_count--;

                    double greaterPotion = 54;

                    greater_count.Content = g_count.ToString();




                    double differenceHps = hpsstill - hitpointsdrop;

                    if (differenceHps > 54)
                    {
                        hitpointsdrop = hitpointsdrop + greaterPotion;
                        hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                    }
                    else if (differenceHps < 54)
                    {
                        greaterPotion = differenceHps;
                        hitpointsdrop = hitpointsdrop + greaterPotion;
                        hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                    }
                    TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                    healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                    game_screen.ScrollToEnd();
                }



                if (newlevel.level >= 42 && newlevel.level <= 46)
                {

                    g_count--;

                    double greaterPotion = 57;

                    greater_count.Content = g_count.ToString();




                    double differenceHps = hpsstill - hitpointsdrop;

                    if (differenceHps > 57)
                    {
                        hitpointsdrop = hitpointsdrop + greaterPotion;
                        hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                    }
                    else if (differenceHps < 57)
                    {
                        greaterPotion = differenceHps;
                        hitpointsdrop = hitpointsdrop + greaterPotion;
                        hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                    }
                    TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                    healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                    game_screen.ScrollToEnd();
                }

                if (newlevel.level >= 47 && newlevel.level <= 50)
                {

                    g_count--;

                    double greaterPotion = 60;

                    greater_count.Content = g_count.ToString();




                    double differenceHps = hpsstill - hitpointsdrop;

                    if (differenceHps > 60)
                    {
                        hitpointsdrop = hitpointsdrop + greaterPotion;
                        hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                    }
                    else if (differenceHps < 60)
                    {
                        greaterPotion = differenceHps;
                        hitpointsdrop = hitpointsdrop + greaterPotion;
                        hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                    }
                    TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    healing.Text = "You have healed yourself for " + greaterPotion + Environment.NewLine;
                    healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                    game_screen.ScrollToEnd();
                }





            }

            if (commandline.Text == "Heal2" || commandline.Text == "heal2")
            {


                if (lesser_count.Content.ToString() == "0")
                {

                    TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    healing.Text = "You do not have any Lesser Potions to use" + Environment.NewLine;
                    healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
                }
                else if (hitpointsdrop == hpsstill)
                {
                    game_screen.AppendText("You cannot heal yourself yet" + Environment.NewLine);
                }
                else if (hitpointsdrop < hpsstill)
                {



                    if (newlevel.level >= 1 && newlevel.level <= 5)
                    {

                        l_count--;

                        double lesserPotion = 15;

                        lesser_count.Content = l_count.ToString();




                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 15)
                        {
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 15)
                        {
                            lesserPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }

                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + lesserPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);


                    }
                    else if (newlevel.level >= 5 && newlevel.level <= 9)
                    {



                        l_count--;

                        double lesserPotion = 25;

                        lesser_count.Content = l_count.ToString();




                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 25)
                        {
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 25)
                        {
                            lesserPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + lesserPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);




                    }
                    else if (newlevel.level >= 10 && newlevel.level <= 14)
                    {



                        l_count--;

                        double lesserPotion = 28;


                        lesser_count.Content = l_count.ToString();

                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 28)
                        {
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 28)
                        {
                            lesserPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + lesserPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                    }


                    else if (newlevel.level >= 15 && newlevel.level <= 20)
                    {



                        l_count--;

                        double lesserPotion = 31;

                        lesser_count.Content = l_count.ToString();
                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 31)
                        {
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 31)
                        {
                            lesserPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + lesserPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                        game_screen.ScrollToEnd();

                    }
                    else if (newlevel.level >= 21 && newlevel.level <= 23)
                    {



                        l_count--;

                        double lesserPotion = 31;

                        lesser_count.Content = l_count.ToString();
                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 31)
                        {
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 31)
                        {
                            lesserPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + lesserPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + lesserPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);
                        game_screen.ScrollToEnd();

                    }



                



            }


            }


            if (commandline.Text == "Heal3" || commandline.Text == "heal3")
            {


                if (minor_count.Content.ToString() == "0")
                {

                    TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    healing.Text = "You do not have any Minor Potions to use" + Environment.NewLine;
                    healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkOrange);
                }
                else if (hitpointsdrop == hpsstill)
                {
                    game_screen.AppendText("You cannot heal yourself yet" + Environment.NewLine);
                }
                else if (hitpointsdrop < hpsstill)
                {


                    if (newlevel.level >= 1 && newlevel.level <= 5)
                    {
                        m_count--;

                        double minorPotion = 10;

                        minor_count.Content = m_count.ToString();
                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 10)
                        {
                            hitpointsdrop = hitpointsdrop + minorPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 10)
                        {
                            minorPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + minorPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + minorPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);



                    }
                    else if (newlevel.level >= 5 && newlevel.level <= 9)
                    {

                        m_count--;

                        double minorPotion = 13;

                        minor_count.Content = m_count.ToString();
                        double differenceHps = hpsstill - hitpointsdrop;

                        if (differenceHps > 13)
                        {
                            hitpointsdrop = hitpointsdrop + minorPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        else if (differenceHps < 13)
                        {
                            minorPotion = differenceHps;
                            hitpointsdrop = hitpointsdrop + minorPotion;
                            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                        }
                        TextRange healing = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                        healing.Text = "You have healed yourself for " + minorPotion + Environment.NewLine;
                        healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkGreen);

                    }
                }

            }








            if (commandline.Text == "resurrect")
            {
                if (hitpointsdrop == hpsstill)
                {
                    game_screen.AppendText("You are full health why would you want to resurrect yourself?" + Environment.NewLine);
                }
                else
                {
                    game_screen.AppendText("You have been raised from the dead!, at a cost though!" + Environment.NewLine);
                    hitpointsdrop = +40;

                    hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                }


            }


            if (commandline.Text == "/clear")
            {
                chat_box.Document.Blocks.Clear();
                commandline.Clear();


            }


            if (commandline.Text == "stats" || commandline.Text == "Stats")
            {



                game_screen.AppendText(char_name.Content + Environment.NewLine + " Level " + char_level.Content);
                game_screen.AppendText(char_class.Content + Environment.NewLine);
                game_screen.AppendText(char_str.Content + Environment.NewLine);
                game_screen.AppendText(char_wis.Content + Environment.NewLine);
                game_screen.AppendText(char_dex.Content + Environment.NewLine);
                game_screen.AppendText(char_hlt.Content + Environment.NewLine);




            }




            enemy bats;

            bats = enemy.unknown;

            if (commandline.Text == "/target")
            {
                Random rdm = new Random();

                string mobs = ((enemy)(rdm.Next(0, 4))).ToString();

                Random healthChange = new Random();


                if (newlevel.leveling >= 1 && newlevel.leveling <= 5)
                {



                    enemyHitpointdrop = healthChange.Next(15, 30);


                }


                else if (newlevel.leveling >= 6 && newlevel.leveling <= 15)
                {
                    enemyHitpointdrop = healthChange.Next(50, 100);


                }

                else if (newlevel.leveling >= 16 && newlevel.leveling <= 23)
                {
                    enemyHitpointdrop = healthChange.Next(100, 250);


                }

                else if (newlevel.leveling >= 24 && newlevel.leveling <= 31)
                {
                    enemyHitpointdrop = healthChange.Next(150, 400);


                }

                else if (newlevel.leveling >= 32 && newlevel.leveling <= 44)
                {
                    enemyHitpointdrop = healthChange.Next(250, 450);


                }
                else if (newlevel.leveling >= 45 && newlevel.leveling <= 50)
                {
                    enemyHitpointdrop = healthChange.Next(300, 500);


                }

                AttackMob.Content = mobs;


                if (AttackMob.Content.ToString() == "bat")
                {
                    bats = enemy.bat;



                    enemyHps.Content = enemyHitpointdrop.ToString();

                }
                if (AttackMob.Content.ToString() == "wolf")
                {
                    bats = enemy.wolf;

                    enemyHps.Content = enemyHitpointdrop.ToString();
                }
                if (AttackMob.Content.ToString() == "beetle")
                {
                    bats = enemy.beetle;

                    enemyHps.Content = enemyHitpointdrop.ToString();
                }
                if (AttackMob.Content.ToString() == "drachnid")
                {
                    bats = enemy.drachnid;

                    enemyHps.Content = enemyHitpointdrop.ToString();
                }
                if (AttackMob.Content.ToString() == "scarab")
                {
                    bats = enemy.scarab;

                    enemyHps.Content = enemyHitpointdrop.ToString();
                }


            }

            if (commandline.Text == "loot")
            {

                if (AttackMob.Content.ToString() == "")
                {
                    game_screen.AppendText("Cannot loot there is no enemy" + Environment.NewLine);
                }
                else if(enemyHitpointdrop >= 0)
                {
                    TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    trs.Text = "The Enemy is still alive!";
                    trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Aquamarine);
                }
                else
                {



                   


                    int count = 0;

                    AttackMob.Content = "";
                    enemyHps.Content = "";
                    LootSystem newSystem = new LootSystem();

                    randomising = newSystem.randoms;
                    ConcurrentBag<string> lootedItem = new ConcurrentBag<string>();
                    lootedItem.Add(newSystem.randoms);

                   
                   

                    string items;
                    while(!lootedItem.IsEmpty)
                    {
                        if (lootedItem.TryTake(out items))
                        {



                          

                                Console.WriteLine(items);
                            TextRange lootedItems = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                     
                     

                          
                           
                            lootedItems.Text = items;
                                lootedItems.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);



                           Loots.Items.Add(items);
                         
                           

                        
                           
                        }
                        else
                        {
                            Console.WriteLine("The bag is empty");
                        }
                          
                    }



                 


                    


                }


            }

            if (commandline.Text == "Save" || commandline.Text == "save")
            {
                string conString = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";

                string Query = "Update zephora.charactercreated set level= '" + newlevel.leveling + "'  ,Health= '" + char_hlt.Content + "' ,expGained= '" + expGained.Content + "' WHERE Name = '" + char_name.Content + "';";
                MySqlConnection conDataBase = new MySqlConnection(conString);
                MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                MySqlDataReader myReader;



                try
                {
                    conDataBase.Open();

                    myReader = cmdDataBase.ExecuteReader();

                    game_screen.AppendText("Character was saved" + Environment.NewLine);

                    while (myReader.Read())
                    {
                        game_screen.AppendText("Character was saved" + Environment.NewLine);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex + "");
                }


            }

            if (commandline.Text == "attack")
            {



                if (AttackMob.Content.ToString() == "")
                {
                    TextRange tr = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    tr.Text = "Your target is blank, what are you attacking?" + Environment.NewLine;
                    tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);

                }
                else
                {

                    timer.Start();
                    attacktimer.Start();




                }
            }






            //    chat_box.AppendText(label1.Text + " : " + commandline.Text + Environment.NewLine);
            commandline.Clear();
            //  byte[] byteTimes = Encoding.ASCII.GetBytes(Environment.NewLine + label1.Text + ": " + t);

            // ns.Write(byteTimes, 0, byteTimes.Length);








        }

        private void SendButton_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (commandline.Text.Length == 0)
                {
                    SendButton.IsEnabled = false;
                }
                else
                {
                    SendButton.IsEnabled = true;
                    message_commands(sender, null);
                }
            }
        }
        private void attackTimer_Tick(object sender, EventArgs e)
        {

       //    pcexp();
            attack newMessages = new attack(message, attacking, newlevel.leveling);

            attacking = newMessages.damage;






            TextRange tr = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
            tr.Text = "You are being attack by a " + AttackMob.Content + " for " + attacking + Environment.NewLine;
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);







            string health = hitpointsdrop.ToString();

            int ac = Convert.ToInt32(ACNumbers.Content.ToString());

            double number = Convert.ToDouble(health) - newMessages.damage;





            newMessages.damage = 1 - 5099 / newlevel.level * ac + 5098;




            int mitigation = newMessages.damage;


            hitpointsdrop = number;



            hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();


            if (hitpointsdrop <= 0)
            {
                game_screen.AppendText("You have died!" + Environment.NewLine);
                game_screen.AppendText("You have died so sad, you will be buried with the correct burial, if you wish to avoid this. Please type ressurect now!" + Environment.NewLine);

                DeathbyFire();


                timer.Stop();
                attacktimer.Stop();


            }






        }


        private void DeathbyFire()
        {

            double expLosses = 50;

            exper -= expLosses;

            if (expLosses <= 0)
            {
                newlevel.level--;
                LevelingEntitiy newleveling = new LevelingEntitiy(experience, level);
                double math = newleveling.Experience * 0.022 * newlevel.leveling * newlevel.leveling * 2 + 160;



                double experienceLosses = math - exper;



                expGained.Content = Convert.ToString(experienceLosses);
                expStill.Content = math.ToString();

                char_level.Content = newlevel.level;


                double percentage = exper / Convert.ToDouble(expStill.Content) * 100;

                expPercentage.Content = Convert.ToInt32(percentage).ToString() + "%";

            }
            else
            {

                double expLoss = exper - 50;
                expGained.Content = expLoss.ToString();
            }
        }


        private void PCattack_Tick(object sender, EventArgs e)
        {
            attack newAttack = new attack(message, attacking, newlevel.leveling);
            TextRange tr = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
            int attackMobs = newlevel.level * strValue / 50 + newAttack.dmgtowardsenemy / 2;
            tr.Text = char_name.Content + " attacks a " + AttackMob.Content + " for " + attackMobs + Environment.NewLine;
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Indigo);
            game_screen.ScrollToEnd();

          //   pcexp();



            string healths = enemyHitpointdrop.ToString();

            int numbers = Convert.ToInt32(healths) - attackMobs;




            enemyHitpointdrop = numbers;

            enemyHps.Content = enemyHitpointdrop.ToString();


            if (enemyHitpointdrop <= 0)
            {

                timer.Stop();
                attacktimer.Stop();
                regeneration.Start();





                game_screen.AppendText("You have slained a " + AttackMob.Content + Environment.NewLine);

                Random rdm = new Random();



                AttackMob.Content = AttackMob.Content + " Corpse";
                enemyHps.Content = "0";
                
                game_screen.AppendText(" [LOOT] " + Environment.NewLine);



                if (newlevel.leveling >= 1 && newlevel.leveling <= 5)
                {


                    int expAdds = rdm.Next(50, 75);
                    TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    trs.Text = "You have gained " + "[" + expAdds + "] " + "points of experience!" + Environment.NewLine;
                    trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Yellow);

                    //  game_screen.AppendText("You have gained " + "[" + expAdds + "] " + "points of experience!" + Environment.NewLine);
                    exper += expAdds;
                 

                   
                    
                   

                }
                else if (newlevel.leveling >= 6 && newlevel.leveling <= 10)
                {


                    int expAdd = rdm.Next(1, 40);
                    TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    trs.Text = "You have gained " + "[" + expAdd + "] " + "points of experience!" + Environment.NewLine;
                    trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Yellow);
                    //  game_screen.AppendText("You have gained " + "[" + expAdd + "] " + " points of experience!" + Environment.NewLine);
                    exper += expAdd;
                }
                else if (newlevel.leveling >= 11 && newlevel.leveling <= 20)
                {


                    int expAdd = rdm.Next(40, 110);
                    TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    trs.Text = "You have gained " + "[" + expAdd + "] " + "points of experience!" + Environment.NewLine;
                    trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Yellow);
                    exper += expAdd;
                }
                else if (newlevel.leveling >= 21 && newlevel.leveling <= 35)
                {


                    int expAdd = rdm.Next(111, 140);
                    game_screen.AppendText("You have gained " + "[" + expAdd + "] " + " points of experience! " + Environment.NewLine);
                    exper += expAdd;
                }

                else if (newlevel.leveling >= 36 && newlevel.leveling <= 50)
                {


                    int expAdd = rdm.Next(140, 175);
                    game_screen.AppendText("You have gained " + "[" + expAdd + "] " + " points of experience! " + Environment.NewLine);
                    exper += expAdd;
                }




                expGained.Content = exper.ToString();

                expValue.Value = exper;

                double percentage = exper / Convert.ToDouble(expStill.Content) * 100;

                expPercentage.Content = Convert.ToInt32(percentage).ToString() + "%";

               

               
                   pcexp();

                

            }





            if (newAttack.dmgtowardsenemy == 0)
            {
                game_screen.AppendText("You have missed the enemy!" + Environment.NewLine);
            }



        }


        #region Leveling
        public void pcexp()
        {


            LevelingEntitiy newlevels = new LevelingEntitiy(experience, level);

            double expStills = Convert.ToDouble(expStill.Content);



            if (exper >= expStills)
            {


                exper = 0;

                expGained.Content = exper.ToString();

                expValue.Value = exper;

                expPercentage.Content = "0%";


                // levels++;

                newlevel.leveling++;

                char_level.Content = newlevel.leveling.ToString();

                double math = newlevels.Experience * 0.40 * newlevel.leveling * newlevel.leveling * 2 + 160;
                string experi = Convert.ToString(math);
                game_screen.AppendText(" Ding!!!  You have gained level " + newlevel.leveling + Environment.NewLine);




                expStill.Content = experi;







                if (newlevel.level >= 2 && newlevel.level <= 10)
                {

                    int hitpointIncrement = 2;


                    int healthIncreases = Convert.ToInt32(healths) + 3;

                    healths = healthIncreases.ToString();

                    hitpointsdrop = Convert.ToInt32(healths) / 2;

                    hpsstill = Convert.ToInt32(healths) / 2;

                    char_hlt.Content = healths.ToString();


                    hpsstill *= hitpointIncrement;
                    hitpointsdrop *= hitpointIncrement;






                    hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();





                    int manaIncrease = 4;

                    int manaAdd = Convert.ToInt32(wisdoms) + 3;

                    wisdoms = manaAdd.ToString();


                    manaLoss = Convert.ToInt32(wisdoms) / 2;

                    manaStill = Convert.ToInt32(wisdoms) / 2;


                    manaLoss *= manaIncrease;

                    manaStill *= manaIncrease;

                    mana_pot.Content = manaLoss.ToString() + "/" + manaStill.ToString();



                    hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();



                    ArmorClass armor = new ArmorClass();
                    ACNumbers.Content = armor.ACNumber / armor.ACNumber + 10 + 20 * newlevel.leveling;







                    int percentagesss = Convert.ToInt32(ACNumbers.Content) / Convert.ToInt32(hpsstill);

                    percentageofAc.Content = percentagesss + "%";


                    game_screen.AppendText(percentagesss + Environment.NewLine);
                }
                else if (newlevel.level >= 11 && newlevel.level <= 20)
                {

                    int hitpointIncrement = 2;
                   

                    int healthIncreases = Convert.ToInt32(healths) + 3;

                    healths = healthIncreases.ToString();

                    hitpointsdrop = Convert.ToInt32(healths) / 2;

                    hpsstill = Convert.ToInt32(healths) / 2;

                    char_hlt.Content = healths.ToString();


                    hpsstill *= hitpointIncrement;
                    hitpointsdrop *= hitpointIncrement;






                    hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();





                    int manaIncrease = 4;

                    int manaAdd = Convert.ToInt32(wisdoms) + 3;

                    wisdoms = manaAdd.ToString();


                    manaLoss = Convert.ToInt32(wisdoms) / 2;

                    manaStill = Convert.ToInt32(wisdoms) / 2;


                    manaLoss *= manaIncrease;

                    manaStill *= manaIncrease;

                    mana_pot.Content = manaLoss.ToString() + "/" + manaStill.ToString();



                    hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();



                    ArmorClass armor = new ArmorClass();
                    ACNumbers.Content = armor.ACNumber / armor.ACNumber + 10 + 20 * newlevel.leveling;







                    int percentagesss = Convert.ToInt32(ACNumbers.Content) / Convert.ToInt32(hpsstill);

                    percentageofAc.Content = percentagesss + "%";


                    game_screen.AppendText(ACNumbers.Content + Environment.NewLine);
                }
                else if (newlevel.level >= 30 && newlevel.level <= 40)
                {
                    int hitpointIncrem = 3;


                    int healthIncreases = Convert.ToInt32(healths) + 3;

                    healths = healthIncreases.ToString();

                    hitpointsdrop = Convert.ToInt32(healths) / 2;

                    hpsstill = Convert.ToInt32(healths) / 2;

                    char_hlt.Content = healths.ToString();


                    hpsstill *= hitpointIncrem;
                    hitpointsdrop *= hitpointIncrem;







                    hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                }
                else if (newlevel.level >= 40 && newlevel.level <= 50)
                {
                    int hitpoint = 4;


                    int healthIncreases = Convert.ToInt32(healths) + 3;

                    healths = healthIncreases.ToString();

                    hitpointsdrop = Convert.ToInt32(healths) / 2;

                    hpsstill = Convert.ToInt32(healths) / 2;

                    char_hlt.Content = healths.ToString();


                    hpsstill *= hitpoint;
                    hitpointsdrop *= hitpoint;







                    hp.Content = hitpointsdrop.ToString() + "/" + hpsstill.ToString();
                }



            }




              







            

        }

        public void MovePlayer(int toX, int toY)
        {
            try
            {
                int count = 0;

                _cells[toX, toY].AddEntity(newClass);
                TextRange trs = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                trs.Text = toX.ToString() + " , "+ toY.ToString() + Environment.NewLine;
                trs.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.DarkBlue);
                
               
               

               
            }
            catch (IndexOutOfRangeException ex)
            {
               
                Console.WriteLine("Cannot move there");
            }
        }

            public void MovePlayerNorth()
        {
               
                MovePlayer(newClass.Cell.X, newClass.Cell.Y - 1);
           NorthMovement++;

            Console.WriteLine(NorthMovement);

            North.Content = "North:" + NorthMovement.ToString();


            game_screen.ScrollToEnd();
            





        }

        public void MovePlayerSouth()
        {
           
            MovePlayer(newClass.Cell.X, newClass.Cell.Y + 1);


           SouthMovement++;

            Console.WriteLine(SouthMovement);

            South.Content = "South:" + SouthMovement.ToString();

            if(SouthMovement == 4)
            {

         MessageBoxResult result =   MessageBox.Show("There is an enemy lurking in your area.. Would you like to go defend the territory?", "Warning!", MessageBoxButton.OKCancel, MessageBoxImage.Hand);

                if (result == MessageBoxResult.OK)
                {
                    TextRange newText = new TextRange(game_screen.Document.ContentEnd, game_screen.Document.ContentEnd);
                    newText.Text = "You have arrived at the location of the enemy, do you wish to attack?" + Environment.NewLine;
                    newText.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Cyan);
                   
                    commandline.Text = "/target";
                }
                else if(result == MessageBoxResult.Cancel)
                {
                    game_screen.AppendText("You resume your usual travels!");
                    MovePlayer(newClass.Cell.X, newClass.Cell.Y + 1);
                }
                
             
            }


        }

        public void MovePlayerWest()
        {
            MovePlayer(newClass.Cell.X - 1, newClass.Cell.Y);
            WestMovement++;

            Console.WriteLine(WestMovement);

            West.Content = "West:" + WestMovement.ToString();
            game_screen.ScrollToEnd();
        }

        public void MovePlayerEast()
        {
            MovePlayer(newClass.Cell.X + 1, newClass.Cell.Y);

            EastMovement++;

            Console.WriteLine(EastMovement);

            East.Content = "East:" + EastMovement.ToString();
            game_screen.ScrollToEnd();
        }

    

        #endregion

        
        #region UDP
        private void Client_FormClosing()
        {

            try
            {
                if (this.clientSocket != null)
                {
                    // Initialise a packet object to store the data to be sent
                    Packet sendData = new Packet();
                    sendData.ChatDataIdentifier = DataIdentifier.LogOut;
                    sendData.ChatName = this.name;
                    sendData.ChatMessage = null;

                    // Get packet as byte array
                    byte[] byteData = sendData.GetDataStream();

                    // Send packet to the server
                    this.clientSocket.SendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer);

                    // Close the socket
                    this.clientSocket.Close();
                }
              //  LoadCharacter logout = new LoadCharacter();
             //   this.Close();
              //  logout.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Closing Error: " + ex.Message, "UDP Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


        #region Send And Receive

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



        private void ReceiveData(IAsyncResult ar)
        {
            try
            {




                // Receive all data
                this.clientSocket.EndReceive(ar);

                // Initialise a packet object to store the received data
                Packet receivedData = new Packet(dataStream);

                Packet sendData = new Packet();

              



                // Update display through a delegate
                if (receivedData.ChatMessage != null)

                    this.Dispatcher.BeginInvoke(this.displayMessageDelegate, new object[] { receivedData.ChatMessage });

                
                





                // Reset data stream
                this.dataStream = new byte[1024];

              

                // Continue listening for broadcasts

                clientSocket.BeginReceiveFrom(this.dataStream, 0, this.dataStream.Length, SocketFlags.None, ref epServer, new AsyncCallback(this.ReceiveData), null);



                this.displayMessageDelegate = new DisplayMessageDelegate(DisplayMessage);


            }
            catch (ObjectDisposedException)
            { }

            catch (Exception ex)
            {
                MessageBox.Show("Receive Data: " + ex.Message, "UDP Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion
        #endregion


        #region Other Methods

        private void DisplayMessage(string messge)
        {



            TextRange healing = new TextRange(chat_box.Document.ContentEnd, chat_box.Document.ContentEnd);
            healing.Text = messge + "\n";
           
            healing.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Crimson);

            


            chat_box.ScrollToEnd();
            chat_box.IsReadOnly = true;
            game_screen.IsReadOnly = true;
            game_screen.ScrollToEnd();



        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Client_FormClosing();

            autoSaving.Stop();
           
          
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Bag newBag = new Bag();
            newBag.Show();
        }

        private void commands_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Welcome to Zephora 2d version; here you will experience something of a role playing game.");

        }

        private void LogoutOnClose(object sender, EventArgs e)
        {
          //  Client_FormClosing();
        }

        public static IPAddress GetIPAddress()
        {
            IPAddress[] hostAddresses = Dns.GetHostAddresses("");

            foreach (IPAddress hostAddress in hostAddresses)
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(hostAddress) &&  // ignore loopback addresses
                    !hostAddress.ToString().StartsWith("169.254."))  // ignore link-local addresses
                    return hostAddress;
            }
            return null; // or IPAddress.None if you prefer
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                chat_box.AppendText("You have 30 Seconds left" + Environment.NewLine);


                camping.Start();



                chat_box.ScrollToEnd();

             
            }
        }
    }
}

        #endregion