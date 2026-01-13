using Nodefall.GameData;
using Nodefall.GameLogic;
using Nodefall.Models;
using Nodefall.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nodefall.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CharacterManager Manager { get; set; }
        public Character SelectedCharacter => Manager.SelectedCharacter as Character;
        public MainWindow()
        {
            InitializeComponent();
            Manager = new CharacterManager();
            DataContext = this;
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCharacter == null)
            {
                string message = "Please SELECT a chatacter.";
                MessageBox.Show(message, "Important");
                return; 
            }
            
            var player = new Player(SelectedCharacter);
            var game = new GameManager(player);
            game.StartNewGame();
        }

        private void Customize_Button_Click(object sender, RoutedEventArgs e)
        {
            var customizeWindow = new CustomizeWindow(Manager);
            customizeWindow.ShowDialog();
        }

        private void Info_Button_Click(object sender, RoutedEventArgs e)
        {
            string message = "Welcome to my silly little Project :3" +
                             "\nComing in the future:" +
                             "\n- polished graphics and animations" +
                             "\n- more content" +
                             "\n- soft rebalancing";
            MessageBox.Show(message, "Info");
        }

    }
}