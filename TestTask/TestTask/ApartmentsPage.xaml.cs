using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace TestTask
{
    public partial class ApartmentsPage : Page
    {
        MainWindow main;
        string house;
        string query;

        public ApartmentsPage(string house, MainWindow main, string query = "0")
        {
            this.query = query;
            this.main = main;
            this.house = house;
            MainWindow.h = house;
            InitializeComponent();
            Initialization();
        }

        private void Initialization()
        {
            MainWindow.page = 3;

            ApartmentsDataGrid.Items.Clear();

            main.Sort.Visibility = Visibility.Visible;
            
            if (query.Length == 1)
            {
                query = $"" +
                    $"SELECT Apartments.id, Apartments.area " +
                    $"FROM Apartments   " +
                    $"INNER JOIN Houses ON Apartments.house_id = Houses.id  " +
                    $"WHERE Houses.number = '{house}'";
            }

            var dataTable = MainWindow.Execute(query);

            foreach (DataRow row in dataTable.Rows)
            {
                var aprt = new 
                { 
                    id = (int)row["id"], 
                    area = (double)row["area"]
                };
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("area", typeof(double));
                dt.Rows.Add(aprt.id, aprt.area);
                ApartmentsDataGrid.Items.Add(dt);
            }
        }
    }
}
