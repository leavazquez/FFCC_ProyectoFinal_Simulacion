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
        private List<CheckPoint> _recorrido;

        public Traza() { }

        public string NombreTraza
        {
            get { return _nombreTraza; }
            set { _nombreTraza = value; }
        }

        public List<CheckPoint> Recorrido
        {
            get { return _recorrido; }
            set { _recorrido = value; }
        }
    }
}
