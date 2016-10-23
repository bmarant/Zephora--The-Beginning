using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ZephorasWorld.Classes
{


    public enum RandomLoot { }


    public enum Money { Gold, Silver, Copper }


    public class LootSystem
   {

        #region FieldRegion
      



        protected Money _money;
        protected RandomLoot _random_loot;
        string randoming;
        

        protected Random rdm = new Random();

        #endregion


        #region Property Region


       



       

        public Money money
        {
            get
            {
                return _money;
            }
            set
            {
                _money = value;
            }
        }


        public string randoms
        {

            get
            {
                string connections = "datasource=74.80.233.83;port=3306;uid=barant2003;password=onedollar2;";
                string randomLoot = "SELECT * FROM `zephora`.zephloot WHERE Id = '" +rdm.Next(1,12)+"';";
                MySqlConnection newConnection = new MySqlConnection(connections);
                MySqlCommand newCmd = new MySqlCommand(randomLoot,newConnection);
                MySqlDataReader myReader;

               

                newConnection.Open();


                myReader = newCmd.ExecuteReader();

                while (myReader.Read())
                {


                    randoming = myReader.GetString("item_name");
                    
              

                }
                return randoming;


            }
            set
            {
               randoming = value;



            }
        }

        #endregion



   }

    


    


}
