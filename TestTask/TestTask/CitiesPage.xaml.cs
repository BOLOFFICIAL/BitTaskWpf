using System.Data;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestTask
{
    public partial class CitiesPage : Page
    {
        public MainWindow main;

        public CitiesPage(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            Initialization();
        }

        private void CitiesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)CitiesDataGrid.ItemContainerGenerator.ContainerFromItem(CitiesDataGrid.SelectedItem);
            var cell = CitiesDataGrid.CurrentCell;
            var dataGrid = sender as DataGrid;
            var column = dataGrid.CurrentColumn;
            var columnIndex = dataGrid.Columns.IndexOf(column);
            if (row != null && columnIndex == 1)
            {
                var cellValue = (cell.Column.GetCellContent(row) as TextBlock).Text;
                NavigationService.Navigate(new StreetsPage(cellValue, main));
            }
        }

        private void Initialization()
        {
            MainWindow.page = 0;

            CitiesDataGrid.Items.Clear();

            string query = $"" +
                $"SELECT Cities.id, Cities.name, COUNT(Streets.id) as street_count   " +
                $"FROM Cities LEFT JOIN Streets   ON Cities.id = Streets.city_id  " +
                $"GROUP BY Cities.id, Cities.name";

            var dataTable = MainWindow.Execute(query);

            foreach (DataRow row in dataTable.Rows)
            {
                var city = new
                {
                    id = (int)row["id"],
                    name = (string)row["name"],
                    count = (int)row["street_count"]
                };
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("name", typeof(string));
                dt.Columns.Add("count", typeof(int));
                dt.Rows.Add(city.id, city.name, city.count);
                CitiesDataGrid.Items.Add(dt);
            }
        }
    }
}
