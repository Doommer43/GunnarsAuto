using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Car
    {
        private int stelnummer;
        private string maerke;
        private string model;
        private int registreringsnummer;
        private bool ny;

        public Car(int stelnummer, string maerke, string model, int registreringsnummer, bool ny)
        {
            this.stelnummer = stelnummer;
            this.maerke = maerke;
            this.model = model;
            this.registreringsnummer = registreringsnummer;
            this.ny = ny;
        }

        public bool Ny
        {
            get { return ny; }
            set { ny = value; }
        }


        public int Registreringsnummer
        {
            get { return registreringsnummer; }
            set { registreringsnummer = value; }
        }


        public string Model
        {
            get { return model; }
            set { model = value; }
        }


        public string Maerke
        {
            get { return maerke; }
            set { maerke = value; }
        }


        public int Stelnummer
        {
            get { return stelnummer; }
            set { stelnummer = value; }
        }
        public bool ValidateData()
        {
            if (Stelnummer >= 0 && Maerke != null && Model != null && Registreringsnummer >= 0 && (Ny == true || Ny == false))
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{Maerke} {Model} {Stelnummer}";
        }
    }
}
