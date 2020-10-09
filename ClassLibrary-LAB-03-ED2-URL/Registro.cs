using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace ClassLibrary_LAB_03_ED2_URL
{
    public class Registro
    {
        public byte simbolo;
        public int Cant_Aparicion;
        public double probabilidad;
        public string prefijo;
        public Registro Hijo_Izq;
        public Registro Hijo_Der;

        public static Comparison<Registro> Comparar_Prioridad = delegate (Registro Simb1, Registro Simb2)
        {
                return Simb1.probabilidad > Simb2.probabilidad ? 1 : Simb1.probabilidad < Simb2.probabilidad ? -1 : 0;
        };


        public static Func<Registro, Registro, bool> Determinar_Prioridad = delegate (Registro Simb1, Registro Simb2)
        {
                return (Simb1.probabilidad > Simb2.probabilidad);
        };

        public static Comparison<Registro> Comparar_Simbolo = delegate (Registro Simb1, Registro Simb2)
        {
            return Simb1.simbolo.CompareTo(Simb2.simbolo);
        };

        public static Func<Registro,String, Registro> Asignar_Prefijo = delegate (Registro Simb, string Prefijo_Binario)
        {
            if(Simb.simbolo != 0)
            {
            Simb.prefijo = Prefijo_Binario;
            }
            return Simb;
        };
        public void Asig_Prefijo(string Prefijo_Binario)
        {
            if (simbolo!= 0)
            {
                prefijo = Prefijo_Binario;
            }
        }
    }
}
