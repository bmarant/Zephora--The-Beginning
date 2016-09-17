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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Net.Sockets;
using ZephorasWorld.Classes;
using System.Windows.Threading;

namespace ZephorasWorld
{
    /// <summary>
    /// Interaction logic for LoadCharacter.xaml
    /// </summary>
    public partial class LoadCharacter : Window
    {

        string expThrough;
        int levels;
        int experiences;
        LevelingEntitiy newLevel;
        public string usernames;
        public DispatcherTimer autoSaving;
        

        public LoadCharacter(string userName)
        {
            InitializeComponent();

            FillListBox(userName);

            checkConnection();

            usernames = userName;

            


        }

       

        public LoadCharacter()
        {
            InitializeComponent();
        }

        private void FillListBox(string userNames)
        {
            string conString = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";
            string Query = "select * from zephora.charactercreated where character_Username = '" + userNames + "';";
            MySqlConnection conDataBase = new MySqlConnection(conString);
            MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;





            try
            {
                conDataBase.Open();

                myReader = cmdDataBase.ExecuteReader();




                while (myReader.Read())
                {
                    string sName = myReader.GetString("Name");
                    char_list.Items.Add(sName);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }















        private void char_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            string conString = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";
            string Query = "select * from zephora.charactercreated where Name= '" + char_list.SelectedValue + "';";
            MySqlConnection conDataBase = new MySqlConnection(conString);
            MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;

            if (char_list.SelectedValue.ToString() == "")
            {
            }
            else
            {
                try
                {
                    conDataBase.Open();

                    myReader = cmdDataBase.ExecuteReader();





                    while (myReader.Read())
                    {
                        string sName = myReader.GetString("Name");
                        string sGender = myReader.GetString("Gender");
                        string sClass = myReader.GetString("Class");
                        string sStrength = myReader.GetInt32("Strength").ToString();
                        string sWisdom = myReader.GetInt32("Wisdom").ToString();
                        string sDexterity = myReader.GetInt32("Dexterity").ToString();
                        string sHealth = myReader.GetInt32("Health").ToString();
                        string level_character = myReader.GetInt32("level").ToString();
                        string expGain = myReader.GetInt32("expGained").ToString();



                        name_txt.Content = sName;
                        gen_txt.Content = sGender;
                        class_txt.Content = sClass;
                        str_txt.Content = sStrength;
                        wis_txt.Content = sWisdom;
                        dex_txt.Content = sDexterity;
                        hlt_txt.Content = sHealth;
                        lvl_character.Content = level_character;
                        expGaining.Content = expGain;





                        int experience = Convert.ToInt32(expGain);



                        int level = Convert.ToInt32(level_character);


                        newLevel = new LevelingEntitiy(experiences, levels);

                        newLevel.level = Convert.ToInt32(level_character);

                        double mathematicsSucks = newLevel.Experience * 0.022 * newLevel.leveling * newLevel.leveling * 2 + 160;

                        double percentages = Convert.ToDouble(experience) / mathematicsSucks * 100;


                        expGaining.Visibility = Visibility.Hidden;

                        int percent = Convert.ToInt32(percentages);

                        Progress.Content = "You are: " + percent + "%" + " through level " + level_character;

                    }

                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex + "");
                }
            }
        }

        public void checkConnection()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)




        {

            string name = char_list.SelectedValue.ToString();

          

               
                string classes = class_txt.Content.ToString();
                string genders = gen_txt.Content.ToString();
                string str = str_txt.Content.ToString();
                string wisdom = wis_txt.Content.ToString();
                string health = hlt_txt.Content.ToString();
                string dex = dex_txt.Content.ToString();
                string level = lvl_character.Content.ToString();
                string exp = expGaining.Content.ToString();







                World newWorld = new World(name, classes, genders, str, wisdom, health, dex, level, exp, usernames);




                this.Close();
                newWorld.ShowDialog();


            


        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            Zephora newZephora = new Zephora(usernames);
            this.Close();
            newZephora.ShowDialog();
        }

        private void delete_Button_Click(object sender, RoutedEventArgs e)
        {

           

                string conString = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";
                string Query = "DELETE from zephora.charactercreated where Name= '" + char_list.SelectedValue + "';";
                MySqlConnection conDataBase = new MySqlConnection(conString);
                MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                MySqlDataReader myReader;
                char_list.Items.Remove(char_list.SelectedValue);

                try
                {
                    conDataBase.Open();

                    myReader = cmdDataBase.ExecuteReader();

                    while (myReader.Read())
                    {
                      
                    }



                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            
           

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

     

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}

        
    

    

        
    

