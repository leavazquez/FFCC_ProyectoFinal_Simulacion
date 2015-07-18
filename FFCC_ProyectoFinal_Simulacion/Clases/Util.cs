using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    class Util
    {
        public static int Rand (int minValue, int maxValue)
        {
            Random r = new Random();

            return r.Next(minValue, maxValue);
        }
    }
}
