using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFCC_ProyectoFinal_Simulacion.Clases;

namespace FFCC_ProyectoFinal_Simulacion
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Vagon v1 = new Vagon("v1", 100);
            Vagon v2 = new Vagon("v2", 1000);
            List<Vagon> formacion = new List<Vagon>();
            formacion.Add(v1);
            formacion.Add(v2);
            
            Tren tren = new Tren(formacion, 50);

            List<Incidente> incidentes = new List<Incidente>();
            incidentes.Add(new Incidente("inc1", 100, 10));
            incidentes.Add(new Incidente("inc1", 1000, 20));
            
            Estacion e1 = new Estacion("est1", incidentes);
            Estacion e2 = new Estacion("est2", incidentes);
            Estacion e3 = new Estacion("est3", incidentes);

            CheckPoint c1 = new CheckPoint();
            c1.estacion = e1;
            c1.posicionEstacionEnTraza = 1;
            c1.paraEnEstacion = true;
            CheckPoint c2 = new CheckPoint();
            c2.estacion = e2;
            c2.posicionEstacionEnTraza = 2;
            c2.paraEnEstacion = true;
            
            CheckPoint c3 = new CheckPoint();
            c3.estacion = e3;
            c3.posicionEstacionEnTraza = 3;
            c3.paraEnEstacion = true;

            List<CheckPoint> recorrido = new List<CheckPoint>();
            recorrido.Add(c1);
            recorrido.Add(c2);
            recorrido.Add(c3);
            
            Traza traza = new Traza();
            traza.Recorrido = recorrido;
            
            Simulador silvia = new Simulador();
            silvia.Tren = tren;
            silvia.Traza = traza;
            silvia.Simulacion();
        }
    }
}
