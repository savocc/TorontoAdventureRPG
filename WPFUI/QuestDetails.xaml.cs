using Engine.Factories;
using Engine.Models;
using Engine.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for QuestInformation.xaml
    /// </summary>
    public partial class QuestDetails : Window
    {
        public Quest quest => DataContext as Quest;
        //public List<Quest> questList = new List<Quest>{ quest };
        List<Quest> questList = new List<Quest>();

        public QuestDetails()
        {

            InitializeComponent();
        }
        void OnLoad(object sender, RoutedEventArgs e)
        {
            questList.Add(quest);
            dataGrid.Items.Clear();
            dataGrid.ItemsSource = questList;
            dataGrid.Columns.Clear();
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Name",
                Binding = new Binding("Name"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Items Needed To Complete",
                Binding = new Binding("AllItemsToComplete"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Reward Items",
                Binding = new Binding("AllRewardItems"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });
            //dataGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn
            //{
            //    Header = "Name",
            //    Binding = new System.Windows.Data.Binding("Name")
            //});
            //dataGrid.Columns.Add(new System.Windows.Controls.DataGridTextColumn
            //{
            //    Header = "Age",
            //    Binding = new System.Windows.Data.Binding("Age")
            //});
        }
        //dataGrid.Items.Clear();
        //dataGrid.ItemsSource = new List<Quest> { quest };
    }

}
