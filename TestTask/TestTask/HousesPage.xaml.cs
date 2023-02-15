using System.Data;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TestTask
{
    public partial class HousesPage : Page
    {
        MainWindow main;
        public string street;

        public HousesPage(string street, MainWindow main)
        {
            this.main = main;
            this.street = street;
            MainWindow.s = street;
            InitializeComponent();
            Initialization();
        }

        public HousesPage()
        {
            InitializeComponent();
            Initialization();
        }

        private void HousesDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)HousesDataGrid.ItemContainerGenerator.ContainerFromItem(HousesDataGrid.SelectedItem);
            var cell = HousesDataGrid.CurrentCell;
            var dataGrid = sender as DataGrid;
            var column = dataGrid.CurrentColumn;
            var columnIndex = dataGrid.Columns.IndexOf(column);
            if (row != null && columnIndex == 1)
            {
                var cellValue = (cell.Column.GetCellContent(row) as TextBlock).Text;
                NavigationService.Navigate(new ApartmentsPage(cellValue, main));
            }
        }

        private void Initialization()
        {
            MainWindow.page = 2;

            HousesDataGrid.Items.Clear();

            string query = $"" +
                $"SELECT Houses.id as house_id, Houses.number as house_number, COUNT(Apartments.id) as number_of_apartments, SUM(Apartments.area) as total_apartment_area   " +
                $"FROM Houses   " +
                $"INNER JOIN Streets ON Houses.street_id = Streets.id   " +
                $"INNER JOIN Apartments ON Houses.id = Apartments.house_id  " +
                $"WHERE Streets.name = '{street}'   " +
                $"GROUP BY Houses.id, Houses.number;";

            var dataTable = MainWindow.Execute(query);

            foreach (DataRow row in dataTable.Rows)
            {
                var hss = new 
                { 
                    id = (int)row["house_id"], 
                    number = (string)row["house_number"], 
                    count = (int)row["number_of_apartments"], 
                    square = (double)row["total_apartment_area"]
                };
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("number", typeof(string));
                dt.Columns.Add("count", typeof(int));
                dt.Columns.Add("square", typeof(double));
                dt.Rows.Add(hss.id, hss.number, hss.count, hss.square);
                HousesDataGrid.Items.Add(dt);
            }
        }
    }
}
