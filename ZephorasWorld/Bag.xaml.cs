using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ZephorasWorld
{
    /// <summary>
    /// Interaction logic for Bag.xaml
    /// </summary>
    public partial class Bag : Window
    {

       public ObservableCollection<string> newItems = new ObservableCollection<string>();
        
        LootSystem newSystem = new LootSystem();
        World newWorld = new World();
        

        public Bag()
        {
            InitializeComponent();

         
        

            

        }


     
           
            

          

        
    }
}
