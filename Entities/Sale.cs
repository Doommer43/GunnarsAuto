using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sale
    {
        private int id;
        private int transaktionsbeløb;
        private bool ejet;
        private SalesPersons person;
        private Car car;

        public Sale(int id, int transaktionsbeløb, bool ejet, SalesPersons person, Car car)
        {
            this.id = id;
            this.transaktionsbeløb = transaktionsbeløb;
            this.ejet = ejet;
            this.person = person;
            this.car = car;
        }

        public Car Car
        {
            get { return car; }
            set { car = value; }
        }


        public SalesPersons Person
        {
            get { return person; }
            set { person = value; }
        }


        public bool Ejet
        {
            get { return ejet; }
            set { ejet = value; }
        }


        public int Transaktionsbeløb
        {
            get { return transaktionsbeløb; }
            set { transaktionsbeløb = value; }
        }


        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public bool ValidateData()
        {
            if ((Ejet == true || Ejet == false) && Person != null && Car != null)
            {
                return true;
            }
            return false;
        }
    }
}
