using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    public class Incidente
    {
        private string _nombreIncidente;
        private int _demoraOcacionada;
        private int _probOcurrencia;

        public Incidente(string nombre, int demora, int probabilidad)
        {
            _nombreIncidente = nombre;

            _demoraOcacionada = demora;

            if ((probabilidad <= 100) || (probabilidad < 0))
                _probOcurrencia = probabilidad;
            else
                throw new System.ArgumentException("Probabilidad de ocurrencia fuera de rango");
        }

        public string NombreIncidente
        {
            get { return _nombreIncidente; }
            set { _nombreIncidente = value; }
        }

        public int DemoraOcacionada
        {
            get { return _demoraOcacionada; }
            set { _demoraOcacionada = value; }
        }

        public int ProbabilidadOcurrencia
        {
            get { return _probOcurrencia; }
        }

        /*Este metodo evalua si este incidente va a ocurrir o no*/
        public bool OcurreIncidente()
        {
            /*Esto genera un numero cualquiera entre 1 y 100*/
            int numeroCualquiera = Util.Rand(1, 100);
            /*Si el numero rando es menor a la probabilidad de ocurrencia entonces el incidente ocurrira*/
            if (numeroCualquiera < _probOcurrencia)
                return true;
            else
                return false;
        }
    }
}
