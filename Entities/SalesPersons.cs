using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SalesPersons
    {
        private string fornavn;
        private string efternavn;
        private string initialer;
        private int id;
        private List<Sale> sale;

        public List<Sale> Sale
        {
            get { return sale; }
            set { sale = value; }
        }


        public SalesPersons(string fornavn, string efternavn, string initialer, int id)
        {
            this.fornavn = fornavn;
            this.efternavn = efternavn;
            this.initialer = initialer;
            this.id = id;
            sale = new List<Sale>();
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        
        public string Initialer
        {
            get { return initialer; }
            set { initialer = value; }
        }


        public string Efternavn
        {
            get { return efternavn; }
            set { efternavn = value; }
        }


        public string Fornavn
        {
            get { return fornavn; }
            set { fornavn = value; }
        }

        public bool ValidateData()
        {
            if(Fornavn != null && Efternavn != null && Initialer.Count() == 4 && Initialer == Initialer.ToUpper())
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Fornavn} {Efternavn}";
        }
    }
}
