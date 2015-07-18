using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    /*Representa a una estacion de la traza (del recorrido del tren). Una lista de estaciones formaran una traza*/
    public class Estacion
    {
        private string _nombre;
        private int _tiempoComprometido;
        private int _personasEsperandoTren;
        private List<Incidente> _incidentesPosibles;


        public Estacion(string nombre, List<Incidente> incidentes)
        {
            _nombre = nombre;
            _tiempoComprometido = 0;
            _personasEsperandoTren = 0;
            _incidentesPosibles = incidentes;
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int TiempoComprometido
        {
            get { return _tiempoComprometido; }
            set { _tiempoComprometido = value; }
        }

        public int PersonasEsperandoTren
        {
            get { return _personasEsperandoTren; }
            set { _personasEsperandoTren = value; }
        }

        /*Conjunto de incidentes posibles que puede sufrir el tren antes de llegar a la estacion*/
        public List<Incidente> IncidentesPosibles
        {
            get { return _incidentesPosibles; }
            set { _incidentesPosibles = value; }
        }

        /*Esta funcion lo que hace es devolver un entero que simboliza la cantidad de gente nueva que acaba de llegar
         a la estacion y que espera al tren.*/
        public int CantidadPersonasArrivoAEstacion()
        {
            /*por el momento retorna siemrpe 30, mas adelante sera reemplazado por alguna fdp*/
            return 30;
        }

        /*Esta funcion devuelve el tiempo que tarda el tren en ser despachado de la estacion (el tiempo que tarda la gente
         en bajar del tren y subir al mismo*/
        public int TiempoAtencionTren()
        {
            /*por el momento retorna siemrpe 5 minutos, mas adelante sera reemplazado por alguna fdp*/
            return 5;
        }

        public void ActualizarPersonasEsperandoTren()
        {
            int personas = this.CantidadPersonasArrivoAEstacion();

            _personasEsperandoTren = _personasEsperandoTren + personas;
        }

        /*Retorna la lista de incidentes que le van a ocurrir al tren hasta que llegue a esta estacion*/
        public List<Incidente> GetIncidentesActivos()
        {
            List<Incidente> incidentesActivos = new List<Incidente>();

            foreach(Incidente i in _incidentesPosibles)
            {
                if (i.OcurreIncidente())
                    incidentesActivos.Add(i);
            }

            return incidentesActivos;
        }

        /*Retorna la demora total que va a sufrir el tren por incidentes sufridos hasta que llegue a la estacion*/
        public int DemoraTotalIncidente()
        {
            int totalDemora = 0;
            List<Incidente> incidentesActivos = this.GetIncidentesActivos();

            foreach (Incidente i in incidentesActivos)
                totalDemora = totalDemora + i.DemoraOcacionada;

            return totalDemora;
        }

        /*Cuando se atiende un tren lo que sucede es que bajan pasajeros del mismo y luego suben personas de la estacion al tren*/
        public void AtencionTren(Tren unTren)
        {
            int personasAbordan;

            unTren.DesabordarTren();

            if(_personasEsperandoTren + unTren.PasajerosABordo > unTren.CapacidadMaximaPasajeros)
            {
                personasAbordan = unTren.CapacidadMaximaPasajeros - unTren.PasajerosABordo;
                _personasEsperandoTren = _personasEsperandoTren - personasAbordan;
            }
            else
            {
                personasAbordan = _personasEsperandoTren;
                _personasEsperandoTren = 0;
            }

            unTren.AbordarTren(personasAbordan);
        }
    }
}
