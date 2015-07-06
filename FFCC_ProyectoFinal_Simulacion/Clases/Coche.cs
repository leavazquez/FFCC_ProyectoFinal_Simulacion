using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    /*Los coches son los vagones del tren. Una formacion (o tren) esta compuesto por un conjunto de coches*/
    public class Coche
    {
        private string _modelo;
        /*Cantidad de pasajeros maxima que entre en el coche*/
        private int _capacidadPasajerosCoche;

        public Coche(string m,int p)
        {
            _modelo = m;
            _capacidadPasajerosCoche = p;
        }

        public string Modelo
        {
            get { return _modelo; }
            //set { _modelo = value; }
        }

        public int CapacidadPasajerosCoche
        {
            get { return _capacidadPasajerosCoche; }
            //set { _capacidadPasajerosCoche = value; }
        }
    }
}
