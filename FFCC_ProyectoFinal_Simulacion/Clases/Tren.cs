using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    public class Tren
    {
        private List<Coche> _formacion;
        private decimal _volocidadPromedio;
        private int _pasajerosABordo;
        private Estacion _proximaEstacion;
        private Estacion _estacionActual;
        private Traza _trazaTren;

        public Tren(List<Coche> f,decimal v)
        {
            _formacion = f;
            _volocidadPromedio = v;
            _pasajerosABordo = 0;
        }

        public Estacion EstacionActual
        { 
            get { return _estacionActual; } 
        }

        public Estacion ProximaEstacion
        {
            get { return _proximaEstacion; }
        }

        public decimal VelocidadPromedio
        {
            get { return _volocidadPromedio; }
        }

        public int PasajerosABordo
        {
            get { return _pasajerosABordo; }
        }

        public int CapacidadMaximaPasajeros
        {
            get
            {
                int total = 0;
                foreach (Coche c in _formacion)
                    total = total + c.CapacidadPasajerosCoche;

                return total;
            }
        }

        /*Esta funcion retorna que cantidad de pasajeros es la que baja del tren*/
        public int PasajerosBajanTren()
        {
            /*Por ahora la cantidad de pasajeros que baja es fija en 30. Luego se reeplazara por una fdp*/
            return 30;
        }

        public void DesabordarTren()
        {
            int pasajerosBajan = this.PasajerosBajanTren();

            if (pasajerosBajan >= _pasajerosABordo)
                _pasajerosABordo = 0;
            else
                _pasajerosABordo = _pasajerosABordo - pasajerosBajan;
        }

        public void AbordarTren(int pasajerosNuevos)
        {
            if (pasajerosNuevos <= this.CapacidadMaximaPasajeros)
                _pasajerosABordo = _pasajerosABordo + pasajerosNuevos;
            /*aca habria que poner que si sale por falso arroje una expepcion*/
        }
    }
}
