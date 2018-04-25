using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data;

namespace DataAccess
{
    public class DBHandler
    {
        private string conString;

        public DBHandler(string conString)
        {
            ConString = conString;
        }

        public string ConString
        {
            get { return conString; }
            set { conString = value; }
        }

        public List<SalesPersons> GetAllPersons()
        {
            List<SalesPersons> sp = new List<SalesPersons>();
            string com = "SELECT * FROM SalesPersons";
            using(SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                using(SqlCommand command = new SqlCommand(com, con))
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter a = new SqlDataAdapter(command);

                    a.Fill(ds);

                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            SalesPersons p = new SalesPersons(row.Field<string>("Fornavn"), row.Field<string>("Efternavn"), row.Field<string>("Initialer"), row.Field<int>("PersonID"));
                            if (p.ValidateData())
                            {
                                sp.Add(p);
                            } else
                            {
                                throw new Exception("A name on the list['SalesPersons'] does not compute.");
                            }
                        }
                    }
                }
            }
            return sp;
        }
        public List<Car> GetAllCars()
        {
            List<Car> cl = new List<Car>();
            string com = "SELECT * FROM Car";
            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(com, con))
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter a = new SqlDataAdapter(command);

                    a.Fill(ds);

                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Car c = new Car(row.Field<int>("Stelnummer"), row.Field<string>("Maerke"), row.Field<string>("Model"), row.Field<int>("Registreringsnummer"), row.Field<bool>("Ny"));
                            if (c.ValidateData())
                            {
                                cl.Add(c);
                            }
                            else
                            {
                                throw new Exception("A name on the list['Car'] does not compute.");
                            }
                        }
                    }
                }
            }
            return cl;
        }
        public List<Sale> GetAllSales()
        {
            List<Sale> sales = new List<Sale>();
            string com = "SELECT * FROM Sale";

            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(com, con))
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter a = new SqlDataAdapter(command);

                    a.Fill(ds);

                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            List<SalesPersons> ps = GetAllPersons();
                            SalesPersons person = null;
                            foreach (SalesPersons p in ps)
                            {
                                if (p.ID == row.Field<int>("PersonID"))
                                {
                                    person = p;
                                }
                            }

                            List<Car> cs = GetAllCars();
                            Car Car = null;
                            foreach (Car c in cs)
                            {
                                if ( c.Stelnummer == row.Field<int>("Stelnummer"))
                                {
                                    Car = c;
                                }
                            }

                            Sale s = new Sale(row.Field<int>("ID"), row.Field<int>("Transaktionsbeløb"), row.Field<bool>("Ejet"), person, Car);
                            if (s.ValidateData())
                            {
                                sales.Add(s);
                            }
                            else
                            {
                                throw new Exception("A name on the list['Sales'] does not compute.");
                            }
                        }
                    }
                }
            }
            return sales;
        }
        public bool AddNewCar(Car c)
        {
            int carRowsAffected;
            string car = $"INSERT INTO Car VALUES ({c.Stelnummer},'{c.Maerke}','{c.Model}',{c.Registreringsnummer},'{c.Ny}')";
            using(SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                using(SqlCommand command = new SqlCommand(car, con))
                {
                    carRowsAffected = command.ExecuteNonQuery();
                }
                
            }
            if (carRowsAffected > 0)
            {
                return true;
            }
            else { return false; }
        }
        public bool AddNewSale(Car c, SalesPersons p, int trans, bool ejet)
        {
            int saleRowsAffected;

            string sale = $"INSERT INTO Sale VALUES ({trans}, '{ejet}', {p.ID}, {c.Stelnummer})";

            using (SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(sale,con)) {

                    saleRowsAffected = com.ExecuteNonQuery();
                }
            }
            if (saleRowsAffected > 0)
            {
                return true;
            }
            else { return false; }
        }

    }
}