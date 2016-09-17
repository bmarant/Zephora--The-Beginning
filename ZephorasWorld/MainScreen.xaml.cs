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

namespace ZephorasWorld
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Window
    {

       



        public MainScreen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            int ageVerification = 0;

            if (this.name_txt.Text == "" || surname_txt.Text == "" || username_txt.Text == "" || password_txt.Text == "" || age_txt.Text == " ")
            {
                MessageBox.Show("Please check your information, there might have been something forgotten!", "Uh oh!", MessageBoxButton.OKCancel);

            }
            else
            {

                ageVerification = Convert.ToInt32(age_txt.Text);


                if (ageVerification <= 13)
                {


                    MessageBox.Show("You must be over the age of 13 or of the age of 13 to play this game");
                }
                else
                {


                    string conString = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";

                    string Query = "insert into zephora.character(name,surname,age, Username,Password) VALUES ('" + this.name_txt.Text + "', ' " + this.surname_txt.Text + "', '" + this.age_txt.Text + "', '" + this.username_txt.Text + "', '" + this.password_txt.Text + "') ;"; //Account Status is 20 for normal users unless Admin assigns higher value
                    MySqlConnection conDataBase = new MySqlConnection(conString);
                    MySqlCommand cmdDataBase = new MySqlCommand(Query, conDataBase);
                    MySqlDataReader myReader;




                    try
                    {
                        conDataBase.Open();

                        myReader = cmdDataBase.ExecuteReader();

                        MessageBox.Show("Saved");

                        this.name_txt.Clear();
                        this.surname_txt.Clear();
                        this.age_txt.Clear();
                        this.username_txt.Clear();
                        this.password_txt.Clear();
                        this.Close();
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


            }
        
        }
    }
}
