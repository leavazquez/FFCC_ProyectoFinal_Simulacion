using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    /*Esta clase es la que se encarga de hacer la simulacion propiamente dicha. Aca va a estar el modelo*/
    public class Simulador
    {
        /*representa los distintos tipos de formaciones que se utilizaran durante la simulacion*/
        private List<Tren> _formacionesDisponibles;
        /*representa los distintos tipos de trazas recorreran */
        private List<Traza> _trazasDisponibles;

        /*solo se creo un tren y una traza para probar la funcionalidad*/
        private Tren _tren;

        public Tren Tren
        {
            get { return _tren; }
            set { _tren = value; }
        }

        private Traza _traza;

        public Traza Traza
        {
            get { return _traza; }
            set { _traza = value; }
        }


        /*las variable de esta clase seran las variables resultado*/

        /*Retorna una formacion cualquiera de las disponibles para la simulacion*/
        private Tren GetFormacion()
        {
            Random rnd = new Random();
            int pos = rnd.Next(0, _formacionesDisponibles.Count);

            return _formacionesDisponibles[pos];
        }

        /*Retorna una traza cualquiera de las disponibles para la simulacion*/
        private Traza GetTraza()
        {
            Random rnd = new Random();
            int pos = rnd.Next(0, _trazasDisponibles.Count);

            return _trazasDisponibles[pos];
        }

        /*Representa el intervalo de tiempo que tarda de salir una formacion (tren) en minutos*/
        private int TiempoProximaSalida()
        {
            /*Por ahora se supone que cada tren sale cada 10 minutos, pero luego debera ser reeplazado por una fdp*/
            return 10;
        }

        public int GetTiempoViaje(CheckPoint cp, bool tipoCalculo = true, Tren unTren = null)
        {
            if (tipoCalculo && unTren != null)
                return Convert.ToInt32(cp.distanciaHastaEstacion * unTren.VelocidadPromedio / 60);
            else
                return cp.tiempoViaje;
        }
        public void Simulacion()
        {
            Tren unTren;
            int tiempoViaje = 0;
            int tiempoTotalIncidentes = 0;
            int tiempoActual = 0;
            int tiempoAtencionEstacion = 0;
            /*Son la cantidad de minutos por dia. Se simulan 24 horas=1440 minutos*/
            int tiempoFinal = 100;
            int cantidadTrenes = 0;

            while (tiempoActual <= tiempoFinal)
            {
                unTren = Tren; //this.GetFormacion();

                unTren.TrazaTren = Traza; //this.GetTraza();

                unTren.TrazaTren.Recorrido.OrderBy(x => x.posicionEstacionEnTraza);

                cantidadTrenes++;

                tiempoActual = tiempoActual + TiempoProximaSalida();
                Console.WriteLine("tiempo actual = {0}",tiempoActual);
                /*Se setean los tiempos en la estacion inicial*/
                unTren.TrazaTren.Recorrido[0].tiempoLlgedaEstacion = 0;

                unTren.TrazaTren.Recorrido[0].tiempoSalidaEstacion = tiempoActual;

                for (int i = 1; i <= unTren.TrazaTren.Recorrido.Count - 1; i++)
                {
                    tiempoViaje = this.GetTiempoViaje(unTren.TrazaTren.Recorrido[i], false);
                    Console.WriteLine("{0}, {1}", i, unTren.TrazaTren.Recorrido[i].estacion.Nombre);
                    
                    //unTren.CalcularConsumoCombustibleMovimiento(unTren.TrazaTren.Recorrido[i].distanciaHastaEstacion);

                    tiempoTotalIncidentes = unTren.TrazaTren.Recorrido[i].estacion.DemoraTotalIncidente();

                    /*si el tiempo de demora es mayor a 0 quiere decir que el tren sufrio por lo menos un incidente*/
                    if(tiempoTotalIncidentes != 0)
                    {
                        unTren.VecesDemoradoIncidente++;
                        Console.WriteLine("Incidente");
                        unTren.TotalDemoraIncidente = unTren.TotalDemoraIncidente + tiempoTotalIncidentes;

                        //unTren.CalcularConsumoCombustibleParado(tiempoTotalIncidentes);
                    }

                    /*El tiempo de llegada de la siguiente estacion sera la suma de el tiempo de salida de la 
                     estacion anterior mas el tiempo de viaje mas el tiempo de las demoras por incidente*/
                    unTren.TrazaTren.Recorrido[i].tiempoLlgedaEstacion = unTren.TrazaTren.Recorrido[i - 1].tiempoSalidaEstacion 
                                                                            + tiempoViaje
                                                                            + tiempoTotalIncidentes;

                    if (unTren.TrazaTren.Recorrido[i].paraEnEstacion)
                    {
                        tiempoAtencionEstacion = unTren.TrazaTren.Recorrido[i].estacion.TiempoAtencionTren();
                        Console.WriteLine("Paro Estacion {0}", unTren.TrazaTren.Recorrido[i].estacion.Nombre);
                        /*se hace el asenso y desenso de pasajeros*/
                        unTren.TrazaTren.Recorrido[i].estacion.AtencionTren(unTren);
                    }
                    else
                        tiempoAtencionEstacion = 0;

                    /*Si el if da verdadero quiere decir que la estacion esta demorada y por lo tanto el tiempo de salida de la estacion
                     sera igual al tiempo de llegada de la estacion mas la demora del tiempo comprometido de la estacion*/
                    if (unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido >= unTren.TrazaTren.Recorrido[i].tiempoLlgedaEstacion)
                    {
                        unTren.VecesDemoradoEstacion++;
                        
                        unTren.TotalDemoraEstacion = unTren.TotalDemoraEstacion + unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido;
                        
                        //unTren.CalcularConsumoCombustibleParado(tiempoAtencionEstacion + unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido);

                        /*se ajusta el tiempo comprometido de la estacion segun el tiempo ya acumulado y segun el tiempo de atencion del tren actual*/
                        unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido = +tiempoAtencionEstacion;
                    }
                    else
                        unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido = unTren.TrazaTren.Recorrido[i].tiempoLlgedaEstacion
                                                                                    + tiempoAtencionEstacion;

                    unTren.TrazaTren.Recorrido[i].tiempoSalidaEstacion = unTren.TrazaTren.Recorrido[i].tiempoLlgedaEstacion
                                                                                + unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido;
                }                
            }
            /*A partir de aca se hacen los calculos para las variables resultado*/
            
        }
    }
}
