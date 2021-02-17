using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTravelApp.Model;

namespace WpfTravelApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string local = "https://localhost:44317/";
        Repository repository = new Repository(local);
        public MainWindow()
        {
            InitializeComponent();
            listFrom.ItemsSource = Repository.Cityes;
            listTo.ItemsSource = Repository.Cityes;
            listCar.ItemsSource = Repository.GetCars;
            txtDate.SelectedDate = DateTime.Now;
            LoadData();
        }
        private void LoadData()
        {
            repository.ReadData();
            listRides.ItemsSource = Repository.RidesList;
        }

        private void Refrech_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            List<string> msgError = new List<string>();

            int from = 0; if(listFrom.SelectedValue != null) from = (int)listFrom.SelectedValue;
            int to = 0; if (listTo.SelectedValue != null) to = (int)listTo.SelectedValue;
            int car = 0; if (listCar.SelectedValue != null) car = (int)listCar.SelectedValue;
            bool result = repository.ValidItems(from,to, car, txtDate.SelectedDate.Value.ToString(), 
                txtFree.Text, txtPrice.Text, out  msgError);

            if (!result)
            {
                listError.ItemsSource = msgError;
                string message = string.Empty;
                message = String.Join(" \n ",msgError);
                MessageBox.Show($"{message}");
            }
            LoadData();
        }
    }
}
