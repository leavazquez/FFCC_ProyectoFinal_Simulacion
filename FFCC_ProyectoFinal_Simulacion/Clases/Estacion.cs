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
        private DateTime _tiempoComprometido;
        private int _personasEsperandoTren;


        public Estacion(string n)
        {
            _nombre = n;
            _tiempoComprometido = new DateTime(0);
            _personasEsperandoTren = 0;
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

        /*Esta funcion lo que hace es devolver un entero que simbolisa la cantidad de gente nueva que acava de llegar
         a las estacion y que espera al tren.*/
        public int CantidadPersonasArrivoAEstacion()
        {
            /*por el momento retorna siemrpe 30, mas adelante sera reemplazado por alguna fdp*/
            return 30;
        }

        /*Esta funcion devuelve el tiempo que tarda el tren en ser despachado de la estacion (el tiempo que tarda la gente
         en bajar del tren y subir al mismo*/
        public int TiempoAtencionTren()
        {
            /*por el momento retorna siemrpe 30, mas adelante sera reemplazado por alguna fdp*/
            return 45;
        }

        public void ActualizarPersonasEsperandoTren()
        {
            int personas = this.CantidadPersonasArrivoAEstacion();
            _personasEsperandoTren = _personasEsperandoTren + personas;
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
