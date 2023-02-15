using System.Data;
using System.Windows.Controls;

namespace TestTask
{
    public partial class StreetsPage : Page
    {
        MainWindow main;
        public string citie;

        public StreetsPage(string citie, MainWindow main)
        {
            this.main = main;
            this.citie = citie;
            MainWindow.c = citie;
            InitializeComponent();
            Initialization();
        }

        public StreetsPage()
        {
            InitializeComponent();
            Initialization();
        }

        private void StreetsDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)StreetsDataGrid.ItemContainerGenerator.ContainerFromItem(StreetsDataGrid.SelectedItem);
            var cell = StreetsDataGrid.CurrentCell;
            var dataGrid = sender as DataGrid;
            var column = dataGrid.CurrentColumn;
            var columnIndex = dataGrid.Columns.IndexOf(column);
            if (row != null && columnIndex == 1)
            {
                var cellValue = (cell.Column.GetCellContent(row) as TextBlock).Text;
                NavigationService.Navigate(new HousesPage(cellValue, main));
            }
        }

        private void Initialization()
        {
            MainWindow.page = 1;

            StreetsDataGrid.Items.Clear();

            string query = $"" +
                $"SELECT Streets.id, Streets.name, COUNT(Houses.id) AS num_houses    " +
                $"FROM Streets  " +
                $"INNER JOIN Cities ON Streets.city_id = Cities.id  " +
                $"INNER JOIN Houses ON Streets.id = Houses.street_id    " +
                $"WHERE Cities.name = '{citie}' " +
                $"GROUP BY Streets.id, Streets.name";

            var dataTable = MainWindow.Execute(query);

            foreach (DataRow row in dataTable.Rows)
            {
                var strt = new 
                { 
                    id = (int)row["id"],
                    name = (string)row["name"], 
                    count = (int)row["num_houses"]
                };
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("name", typeof(string));
                dt.Columns.Add("count", typeof(int));
                dt.Rows.Add(strt.id, strt.name, strt.count);
                StreetsDataGrid.Items.Add(dt);
            }
        }
    }
}
