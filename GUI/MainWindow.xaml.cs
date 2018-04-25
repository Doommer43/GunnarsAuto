using System;
using System.Collections.Generic;
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
using Entities;
using DataAccess;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBHandler dbh = new DBHandler(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GunnarsAutoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        List<SalesPersons> Persons;
        List<Car> Cars;
        List<Sale> Sales;
        public MainWindow()
        {
            InitializeComponent();           

            Persons = dbh.GetAllPersons();
            Cars = dbh.GetAllCars();
            Sales = dbh.GetAllSales();

            cBoxEmployeeNames.ItemsSource = Persons;
            cBoxAvailableCars.ItemsSource = Cars;
            dataGridCars.ItemsSource = Cars;
        }

        private void btnNewCar_Click(object sender, RoutedEventArgs e)
        {
            SalesPersons p = (SalesPersons)cBoxEmployeeNames.SelectedItem;

            try
            {
                int stel = Int32.Parse(tBoxNewCarStelnummer.Text);
                int reg = Int32.Parse(tBoxNewCarRegistreringsnummer.Text);
                int trans = Int32.Parse(tBoxTransaktionsbeløb.Text);

                bool ny = (bool)chkBoxNewCarNew.IsChecked;
                bool ejet = (bool)chkBoxEjet.IsChecked;
                Car c = new Car(stel, tBoxNewCarMaerke.Text, tBoxNewCarModel.Text, reg, ny);
                Sale s = new Sale(0, trans, ejet, p, c);

                if (p.ValidateData() && c.ValidateData() && s.ValidateData())
                {
                    if (dbh.AddNewCar(c) && dbh.AddNewSale(c, p, trans, ejet))
                    {
                        MessageBox.Show("En ny transaktion er blevet oprettet.", "succes");
                    }
                    else
                    {
                        MessageBox.Show("Et eller flere problemer opstod under processen.", "Ups!");
                    }
                }
            } catch
            {
                ValidateInput();
            }
        }

        private void ValidateInput()
        {
            try {
                Int32.Parse(tBoxNewCarStelnummer.Text);
                tBoxNewCarStelnummer.BorderBrush = Brushes.Gray;
            }
            catch { tBoxNewCarStelnummer.BorderBrush = Brushes.Red; }

            try {
                Int32.Parse(tBoxNewCarRegistreringsnummer.Text);
                tBoxNewCarRegistreringsnummer.BorderBrush = Brushes.Gray;
            }
            catch { tBoxNewCarRegistreringsnummer.BorderBrush = Brushes.Red; }

            try {
                Int32.Parse(tBoxTransaktionsbeløb.Text);
                tBoxTransaktionsbeløb.BorderBrush = Brushes.Gray;
            }
            catch { tBoxTransaktionsbeløb.BorderBrush = Brushes.Red; }
        }

        private void Solgt(object sender, RoutedEventArgs e)
        {
            Transaktion nyTrans = new Transaktion();
            if(nyTrans.ShowDialog() == true)
            {
                Int32.TryParse(nyTrans.tBoxSalgspris.Text, out int trans);
                dbh.AddNewSale((Car)dataGridCars.SelectedItem, (SalesPersons)cBoxEmployeeNames.SelectedItem, trans, false);
            }
        }
    }
}
