using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    public class CheckPoint
    {
        public Estacion estacion;
        /*Esta variable lo que hace es identificar si el tren va a para en esta estacion o no. Esto sirve para crear
         servicios diferenciales*/
        public bool paraEnEstacion;
        public int tiempoLlgedaEstacion;
        public int tiempoSalidaEstacion;
        public int posicionEstacionEnTraza;
        public decimal distanciaHastaEstacion;
        public int tiempoViaje;
    }
}
