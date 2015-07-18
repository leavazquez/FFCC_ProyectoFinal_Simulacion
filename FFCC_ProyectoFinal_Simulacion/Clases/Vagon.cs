using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCC_ProyectoFinal_Simulacion.Clases
{
    /*Los coches son los vagones del tren. Una formacion (o tren) esta compuesto por un conjunto de coches*/
    public class Vagon
    {
        private string _modelo;
        /*Cantidad de pasajeros maxima que entre en el coche*/
        private int _capacidadPasajerosCoche;

        public Vagon (string modelo, int cantPasajeros)
        {
            _modelo = modelo;
            _capacidadPasajerosCoche = cantPasajeros;
        }

        public string Modelo
        {
            get { return _modelo; }
        }

        public int CapacidadPasajerosCoche
        {
            get { return _capacidadPasajerosCoche; }
        }
    }
}
