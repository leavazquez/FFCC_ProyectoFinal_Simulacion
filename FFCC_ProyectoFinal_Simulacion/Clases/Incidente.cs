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

        public Incidente(string n,int demora,int prob)
        {
            _nombreIncidente = n;
            _demoraOcacionada = demora;

            if (prob <= 100)
                _probOcurrencia = prob;
            else
                throw new System.ArgumentException("La probabilidad de ocurrencia no puede ser mayor a 100");
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
            Random rnd = new Random();
            /*Esto genera un numero cualquiera entre 1 y 100*/
            int numeroCualquiera = rnd.Next(1, 100);

            /*Si el numero rando es menor a la probabilidad de ocurrencia entonces el incidente ocurrira*/
            if (numeroCualquiera < _probOcurrencia)
                return true;
            else
                return false;
        }
    }
}
