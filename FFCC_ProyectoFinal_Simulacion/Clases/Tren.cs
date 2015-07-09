using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    public class Tren
    {
        private List<Vagon> _formacion;
        private decimal _volocidadPromedio;
        private decimal _consumoCombustiblePromedioMovimiento;
        private decimal _consumoCombustiblePromedioParado;
        private decimal _totalConsumoCombustibleMovimiento = 0;
        private decimal _totalCosumoCombustibleParado = 0;
        private int _pasajerosABordo;
        private Traza _trazaTren;
        private int _totalDemoraIncidente = 0;
        private int _totalDemoraEstacion = 0;
        private int _vecesDemoradoIncidente = 0;
        private int _vecesDemoradoEstacion = 0;

        public Tren(List<Vagon> f,decimal v)
        {
            _formacion = f;
            _volocidadPromedio = v;
            _pasajerosABordo = 0;
        }

        public decimal ConsumoCombustiblePromedioMovimiento
        {
            get { return _consumoCombustiblePromedioMovimiento; }
        }

        public decimal ConsumoCombustiblePromedoParado
        {
            get { return _consumoCombustiblePromedioParado; }
        }

        public decimal TotalConsumoCombustibleMovimiento
        {
            get { return _totalConsumoCombustibleMovimiento; }
        }

        public decimal TotalConsumoCombustibleParado
        {
            get { return _totalCosumoCombustibleParado; }
        }

        public decimal ConsumoTotalCombustible
        {
            get { return _totalConsumoCombustibleMovimiento + _totalCosumoCombustibleParado; }
        }

        public int TotalDemoraIncidene
        {
            get { return _totalDemoraIncidente; }
            set { _totalDemoraIncidente = value; }
        }

        public int TotalDemoraEstacion
        {
            get { return _totalDemoraEstacion; }
            set { _totalDemoraEstacion = value; }
        }

        public int VecesDemoradoIncidente
        {
            get { return _vecesDemoradoIncidente; }
            set { _vecesDemoradoIncidente = value; }
        }

        public int VecesDemoradoEstacion
        {
            get { return _vecesDemoradoEstacion; }
            set { _vecesDemoradoEstacion = value; }
        }

        public decimal VelocidadPromedio
        {
            get { return _volocidadPromedio; }
        }

        public int PasajerosABordo
        {
            get { return _pasajerosABordo; }
        }

        public Traza TrazaTren
        {
            get { return _trazaTren; }
            set { _trazaTren = value; }
        }

        public int CapacidadMaximaPasajeros
        {
            get
            {
                int total = 0;
                foreach (Vagon c in _formacion)
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
            else
                throw new System.ArgumentException("La cantidad de pasajeros es mayor a la capacidad del tren");
        }

        public void CalcularConsumoCombustibleMovimiento(decimal distancia)
        {
            _totalConsumoCombustibleMovimiento += distancia * _consumoCombustiblePromedioMovimiento;
        }

        public void CalcularConsumoCombustibleParado(decimal tiempoParado)
        {
            _totalCosumoCombustibleParado += tiempoParado * _consumoCombustiblePromedioParado;
        }
    }
}
