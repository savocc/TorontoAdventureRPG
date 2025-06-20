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
using Engine.EventArgs;
using Engine.Models;
using Engine.ViewModels;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Make sure there is no way to assign new object 
        private readonly GameSession _gameSession = new GameSession();

        public MainWindow()
        {
            InitializeComponent();
            _gameSession.OnMessageRaised += OnGameMessageRaised;
            _gameSession.CurrentPlayer.OnLevelUpRaised += OnGameMessageRaised;
            //OnDeath += OnPlayerDeath;

            DataContext = _gameSession;
        }
        private void OnClick_MoveNorth(Object sender, RoutedEventArgs e)
        {
            _gameSession.MoveNorth();
        }
        private void OnClick_MoveEast(Object sender, RoutedEventArgs e)
        {
            _gameSession.MoveEast();
        }
        private void OnClick_MoveSouth(Object sender, RoutedEventArgs e)
        {
            _gameSession.MoveSouth();
        }
        private void OnClick_MoveWest(Object sender, RoutedEventArgs e)
        {
            _gameSession.MoveWest();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        //private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //}

        private void DataGrid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
            Quest item = (Quest)row.Item;
            QuestDetails tradeScreen = new QuestDetails();
            tradeScreen.Owner = this;
            tradeScreen.DataContext = item;
            tradeScreen.ShowDialog();
        }

        private void OnClick_AttackMonster(Object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }

        private void OnClick_DisplayTradeScreen(Object sender, RoutedEventArgs e)
        {
            TradeScreen tradeScreen = new TradeScreen();
            tradeScreen.Owner = this;
            tradeScreen.DataContext = _gameSession;
            tradeScreen.ShowDialog();
        }

        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Executing the Open command");
        }

        public void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}