using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace TestTask
{
    public partial class MainWindow : Window
    {
        public static DB dB = new DB("Task");
        public static int page = 0;
        public static string h = "";
        public static string s = "";
        public static string c = "";
        public MainWindow()
        {
            InitializeComponent();
            butback.Click += Back;
            MainFrame.Content = new CitiesPage(this);
            Sort.Visibility = Visibility.Collapsed;
        }
        public static DataTable Execute(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand(query, dB.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            return dataTable;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Sort.Visibility = Visibility.Collapsed;
            switch (--page)
            {
                case 0:
                    MainFrame.Content = new CitiesPage(this);
                    break;
                case 1:
                    MainFrame.Content = new StreetsPage(c, this);
                    break;
                case 2:
                    MainFrame.Content = new HousesPage(s, this);
                    break;
            }
        }

        private void ButtonSort(object sender, RoutedEventArgs e)
        {
            int from = int.Parse(TextBox_from.Text);
            int to = int.Parse(TextBox_to.Text);
            string q = $"SELECT id, area \r\nFROM Apartments \r\nWHERE house_id = {h} \r\nAND area >= {from} \r\nAND area <= {to} \r\nORDER BY area;";
            MainFrame.Content = new ApartmentsPage(h, this, q);
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ApartmentsPage(h, this);
        }
    }
}
