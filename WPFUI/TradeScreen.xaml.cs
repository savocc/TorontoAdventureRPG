using System.Windows;
using Engine.Models;
using Engine.ViewModels;
namespace WPFUI
{
    /// <summary>
    /// Interaction logic for TradeScreen.xaml
    /// </summary>
    public partial class TradeScreen : Window
    {
        public GameSession Session => DataContext as GameSession;
        public TradeScreen()
        {
            InitializeComponent();
        }
        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedtem = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            if (groupedtem != null)
            {
                Session.CurrentPlayer.Gold += groupedtem.Item.Price;
                Session.CurrentTrader.AddItemToInventory(groupedtem.Item);
                Session.CurrentPlayer.RemoveItemFromInventory(groupedtem.Item);
            }
        }
        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem groupedtem = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            if (groupedtem != null)
            {
                if (Session.CurrentPlayer.Gold >= groupedtem.Item.Price)
                {
                    Session.CurrentPlayer.Gold -= groupedtem.Item.Price;
                    Session.CurrentTrader.RemoveItemFromInventory(groupedtem.Item);
                    Session.CurrentPlayer.AddItemToInventory(groupedtem.Item);
                }
                else
                {
                    MessageBox.Show("You do not have enough gold");
                }
            }
        }
        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}