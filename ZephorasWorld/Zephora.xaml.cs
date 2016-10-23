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

namespace ZephorasWorld
{
    /// <summary>
    /// Interaction logic for Zephora.xaml
    /// </summary>
    public partial class Zephora : Window
    {

        string loginName;

        public Zephora(string username)
        {
            InitializeComponent();

            loginName = username;

        }
        public Zephora ()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FormCharacterCreator newForm = new FormCharacterCreator(loginName);
            newForm.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadCharacter newCharacter = new LoadCharacter(loginName);
            this.Close();
            newCharacter.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
