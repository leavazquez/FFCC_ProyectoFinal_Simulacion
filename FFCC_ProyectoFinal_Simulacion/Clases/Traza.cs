using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    /*Las trazas representan el recorrido del tren desde una estacion inicio a una estacion fin*/
    public class Traza
    {
        private string _nombreTraza;
        private List<Estacion> _estaciones;

        public Traza() { }

        public string NombreTraza
        {
            get { return _nombreTraza; }
            set { _nombreTraza = value; }
        }

        public List<Estacion> Estaciones
        {
            get { return _estaciones; }
            set { _estaciones = value; }
        }
    }
}
