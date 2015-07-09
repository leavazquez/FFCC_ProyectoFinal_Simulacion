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
            int pos = rnd.Next(0, _formacionesDisponibles.Count);

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
            int tiempoFinal = 1440;
            int cantidadTrenes = 0;

            while (tiempoActual <= tiempoFinal)
            {
                unTren = this.GetFormacion();
                unTren.TrazaTren = this.GetTraza();
                unTren.TrazaTren.Recorrido.OrderBy(x => x.posicionEstacionEnTraza);
                cantidadTrenes++;
                tiempoActual = tiempoActual + TiempoProximaSalida();

                /*Se setean los tiempos en la estacion inicial*/
                unTren.TrazaTren.Recorrido[0].tiempoLlgedaEstacion = 0;
                unTren.TrazaTren.Recorrido[0].tiempoSalidaEstacion = tiempoActual;

                for (int i = 1; i <= unTren.TrazaTren.Recorrido.Count - 1; i++)
                {
                    tiempoViaje = this.GetTiempoViaje(unTren.TrazaTren.Recorrido[i], false);
                    unTren.CalcularConsumoCombustibleMovimiento(unTren.TrazaTren.Recorrido[i].distanciaHastaEstacion);
                    tiempoTotalIncidentes = unTren.TrazaTren.Recorrido[i].estacion.DemoraTotalIncidente();

                    /*si el tiempo de demora es mayor a 0 quiere decir que el tren sufrio por lo menos un incidente*/
                    if(tiempoTotalIncidentes!=0)
                    {
                        unTren.VecesDemoradoIncidente++;
                        unTren.TotalDemoraIncidene = unTren.TotalDemoraIncidene + tiempoTotalIncidentes;
                        unTren.CalcularConsumoCombustibleParado(tiempoTotalIncidentes);
                    }

                    /*El tiempo de llegada de la siguiente estacion sera la suma de el tiempo de salida de la 
                     estacion anterior mas el tiempo de viaje mas el tiempo de las demoras por incidente*/
                    unTren.TrazaTren.Recorrido[i].tiempoLlgedaEstacion = unTren.TrazaTren.Recorrido[i - 1].tiempoSalidaEstacion 
                                                                            + tiempoViaje
                                                                            + tiempoTotalIncidentes;

                    if (unTren.TrazaTren.Recorrido[i].paraEnEstacion)
                    {
                        tiempoAtencionEstacion = unTren.TrazaTren.Recorrido[i].estacion.TiempoAtencionTren();
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
                        unTren.CalcularConsumoCombustibleParado(tiempoAtencionEstacion + unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido);

                        /*se ajusta el tiempo comprometido de la estacion segun el tiempo ya acumulado y segun el tiempo de atencion del tren actual*/
                        unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido = +tiempoAtencionEstacion;
                    }
                    else
                        unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido = unTren.TrazaTren.Recorrido[i].tiempoLlgedaEstacion
                                                                                    + tiempoAtencionEstacion;

                    unTren.TrazaTren.Recorrido[i].tiempoSalidaEstacion = unTren.TrazaTren.Recorrido[i].tiempoLlgedaEstacion
                                                                                + unTren.TrazaTren.Recorrido[i].estacion.TiempoComprometido;
                }

                /*A partir de aca se hacen los calculos para las variables resultado*/
            }
        }
    }
}
