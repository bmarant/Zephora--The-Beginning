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
using ZephorasWorld.Classes;
using MySql.Data.MySqlClient;

namespace ZephorasWorld
{
    /// <summary>
    /// Interaction logic for FormCharacterCreator.xaml
    /// </summary>
    public partial class FormCharacterCreator : Window
    {
        int number = 15;

        int nums = 0;


        

        string logginInAs;

        object sender;

       
        

        Random rand = new Random();
        public FormCharacterCreator(string userName)
        {
            InitializeComponent();

            if(number >= 15)
            {
                str_neg1.IsEnabled = false;
                wis_neg2.IsEnabled = false;
                dex_neg3.IsEnabled = false;
                hlt_neg4.IsEnabled = false;
            }
            
         


            List<string> data = new List<string>();
            data.Add("Necromancer");
            data.Add("Magician");
            data.Add("Cleric");
            data.Add("Paladin");
            data.Add("Ranger");
            data.Add("Beastlord");
            data.Add("Rogue");
            data.Add("Druid");
            data.Add("Wizard");
            data.Add("Enchanter");
            data.Add("Warrior");

            cbo_charclass.ItemsSource = data;

            cbo_charclass.SelectedIndex = 0;
           
        

            logginInAs = userName;
            levelstart.Content = 1;


            if (this.cbo_charclass.Text == "Necromancer")
            {

                str_stat.Content = "85";
                dex_stat.Content = "95";
                wis_stat.Content = "104";
                hlt_stat.Content = "75";
                counter(sender, null);
            }
           


        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            int experience = 0;

            string name;

            if(String.IsNullOrEmpty(char_name.Text) || char_name.Text[0] == ' ')
            {
                MessageBox.Show("You must enter a character name" + "Note you cannot start your name with a space");
                return;
            }
            name = char_name.Text;


            EntityGender eGender;


            eGender = EntityGender.Unknown;

            if (rdo_male.IsChecked == true)
            {
               
                eGender = EntityGender.Male;

               
            }
            else if (rdo_female.IsChecked == true)
            {
                
                eGender = EntityGender.Female;
    
            }
            else
            {
                MessageBox.Show("You must select a gender");
            }


            EntityClass eClass;

            eClass = EntityClass.Unknown;

            if (this.cbo_charclass.Text == "Magician")
            
                eClass = EntityClass.Magician;
              

            else if (this.cbo_charclass.Text == "Necromancer")
                eClass = EntityClass.Necromancer;

            else if (this.cbo_charclass.Text == "Cleric")

                eClass = EntityClass.Cleric;

            else if (this.cbo_charclass.Text == "Paladin")
                eClass = EntityClass.Paladin;


            else if (this.cbo_charclass.Text == "Ranger")
                eClass = EntityClass.Ranger;
            else if (this.cbo_charclass.Text == "Beastlord")
                eClass = EntityClass.Beastlord;
            else if (this.cbo_charclass.Text == "Rogue")
                eClass = EntityClass.Rogue;
            else if (this.cbo_charclass.Text == "Druid")
                eClass = EntityClass.Druid;
            else if (this.cbo_charclass.Text == "Wizard")
                eClass = EntityClass.Wizard;
            else if (this.cbo_charclass.Text == "Enchanter")
                eClass = EntityClass.Enchanter;
            else if (this.cbo_charclass.Text == "Warrior")
                eClass = EntityClass.Warrior;
            else
            {
                MessageBox.Show("Please select a gender");
                return;
            }
            Player player1 = new Player(Name, eGender, eClass);

            string conString = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";

            string Query = "insert into zephora.charactercreated(Name,Class,Gender,Strength,Wisdom,Dexterity,Health,character_Username, level, expGained) VALUES ('" + this.char_name.Text + "', ' " + eClass + "', ' " + eGender + "', '" + str_stat.Content.ToString() + "', '" + wis_stat.Content.ToString() + "', '" + dex_stat.Content.ToString() +"', '" + hlt_stat.Content.ToString() + "', '" + logginInAs + "','" + levelstart.Content.ToString() + "', '" + experience + "' ) ;";
            MySqlConnection conDataBase = new MySqlConnection(conString);
            MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;


            cbo_charclass_SelectionChanged(sender, null);



            try
            {
                conDataBase.Open();

                myReader = cmdDataBase.ExecuteReader();
             
               

                MessageBox.Show("Saved");


                while (myReader.Read())
                {
                    MessageBox.Show("Please enter information");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }

        }

        private void cbo_charclass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
            string value = cbo_charclass.SelectedItem as string;
            this.class_select.Content = value;




            if (value == "Magician")
            {
                str_stat.Content = "80";
                dex_stat.Content = "100";
                wis_stat.Content = "110";
                hlt_stat.Content = "65";
                counter(sender, null);
            }
            else if (value == "Necromancer")
            {

                str_stat.Content = "85";
                dex_stat.Content = "95";
                wis_stat.Content = "104";
                hlt_stat.Content = "75";
                counter(sender, null);
            }

            else if (value == "Cleric")
            {
                str_stat.Content = "90";
                dex_stat.Content = "80";
                wis_stat.Content = "100";
                hlt_stat.Content = "80";
                counter(sender, null);
            }

            else if (value == "Paladin")
            {
                str_stat.Content = "95";
                dex_stat.Content = "110";
                wis_stat.Content = "85";
                hlt_stat.Content = "95";
                counter(sender, null);
            }

            else if (value == "Ranger")
            {
                str_stat.Content = "90";
                dex_stat.Content = "115";
                wis_stat.Content = "90";
                hlt_stat.Content = "100";
                counter(sender, null);
            }

            else if (value == "Beastlord")
            {
                str_stat.Content = "110";
                dex_stat.Content = "100";
                wis_stat.Content = "65";
                hlt_stat.Content = "100";
                counter(sender, null);
            }

            else if (value == "Rogue")
            {
                str_stat.Content = "75";
                dex_stat.Content = "100";
                wis_stat.Content = "55";
                hlt_stat.Content = "90";
                counter(sender, null);
            }

            else if (value == "Druid")
            {
                str_stat.Content = "90";
                dex_stat.Content = "97";
                wis_stat.Content = "103";
                hlt_stat.Content = "82";
                counter(sender, null);



            }

            else if (value == "Wizard")
            {
                str_stat.Content = "60";
                dex_stat.Content = "85";
                wis_stat.Content = "115";
                hlt_stat.Content = "75";
                counter(sender, null);
            }

            else if (value == "Enchanter")
            {
                str_stat.Content = "65";
                dex_stat.Content = "95";
                wis_stat.Content = "109";
                hlt_stat.Content = "70";
                counter(sender, null);


            }
            else if (value == "Warrior")
            {
                str_stat.Content = "110";
                dex_stat.Content = "95";
                wis_stat.Content = "50";
                hlt_stat.Content = "115";
                counter(sender, null);


            }

            return;
        }





            public void counter(object sender, RoutedEventArgs e)
            {
                count_add.Text = number.ToString();

                if(number > 15 || number <= 0)
                {
                    str_pos1.Visibility = Visibility.Hidden;
                    str_neg1.Visibility = Visibility.Hidden;
                    wis_pos2.Visibility = Visibility.Hidden;
                    wis_neg2.Visibility = Visibility.Hidden;
                    dex_pos3.Visibility = Visibility.Hidden;
                    dex_neg3.Visibility = Visibility.Hidden;
                    hlt_pos4.Visibility = Visibility.Hidden;
                    hlt_neg4.Visibility = Visibility.Hidden;
                    
                }

               
            }
            //Strength button plus and minus//
            private void str_pos1_Click(object sender, RoutedEventArgs e)
            {
                number--;
                count_add.Text = number.ToString();



                str_stat.Content = (Convert.ToInt32(str_stat.Content) + 1).ToString();
                if(number < 15)
                {
                    str_neg1.IsEnabled = true;
                 
                }
               if(number == 0)
               {
                str_pos1.IsEnabled = false;
               

            }


                
            }

            private void str_neg1_Click(object sender, RoutedEventArgs e)
            {
                number++;
                count_add.Text = number.ToString();

                str_stat.Content = (Convert.ToInt32(str_stat.Content) - 1).ToString();
                if(number == 15)
                {
                   str_neg1.IsEnabled = false;
                   
                  
                }
                if(number > 0)
                {
                    str_pos1.IsEnabled = true;
                   
                }
               
            }
        //Wisdom Button plus and minus //
            private void wis_pos2_Click(object sender, RoutedEventArgs e)
            {
                number--;
                count_add.Text = number.ToString();
                wis_stat.Content = (Convert.ToInt32(wis_stat.Content) + 1).ToString();
                if (number < 15)
                {
                    wis_neg2.IsEnabled = true;

                }
                if (number == 0)
                {
                    wis_pos2.IsEnabled = false;
                   
                }
               
                

            }

            private void wis_neg2_Click(object sender, RoutedEventArgs e)
            {
                number++;
                count_add.Text = number.ToString();

                wis_stat.Content = (Convert.ToInt32(wis_stat.Content) - 1).ToString();
                if (number == 15)
                {
                    wis_neg2.IsEnabled = false;
                }
                if (number > 0)
                {
                    wis_pos2.IsEnabled = true;
                }
            }
        // Dex plus and minus
            private void dex_pos3_Click(object sender, RoutedEventArgs e)
            {
                number--;
                count_add.Text = number.ToString();
                dex_stat.Content = (Convert.ToInt32(dex_stat.Content) + 1).ToString();
                if (number < 15)
                {
                    dex_neg3.IsEnabled = true;

                }
                if (number == 0)
                {
                    dex_pos3.IsEnabled = false;
                    
                }
            }

            private void dex_neg3_Click(object sender, RoutedEventArgs e)
            {
                number++;
                count_add.Text = number.ToString();

                dex_stat.Content = (Convert.ToInt32(dex_stat.Content) - 1).ToString();
                if (number == 15)
                {
                    dex_neg3.IsEnabled = false;
                }
                if (number > 0)
                {
                    dex_pos3.IsEnabled = true;
                }
            }
                                   // Health Buttons //
            private void hlt_pos4_Click(object sender, RoutedEventArgs e)
            {
                number--;
                count_add.Text = number.ToString();
                hlt_stat.Content = (Convert.ToInt32(hlt_stat.Content) + 1).ToString();
                if (number < 15)
                {
                    hlt_neg4.IsEnabled = true;

                }
                if (number == 0)
                {
                    hlt_pos4.IsEnabled = false;
                }
            }

            private void hlt_neg4_Click(object sender, RoutedEventArgs e)
            {
                number++;
                count_add.Text = number.ToString();

                hlt_stat.Content = (Convert.ToInt32(hlt_stat.Content) - 1).ToString();
                if (number == 15)
                {
                    hlt_neg4.IsEnabled = false;
                    
                    
                }
                if (number > 0)
                {
                    hlt_pos4.IsEnabled = true;
                    
                }

            }


        }
        }
    

