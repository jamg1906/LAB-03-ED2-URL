using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary_LAB_03_ED2_URL
{
    public class Registro
    {
        public string simbolo;
        public int Cant_Aparicion;
        public double probabilidad;
        public string prefijo;

        public static Comparison<Registro> Comparar_Prioridad = delegate (Registro simbolo1, Registro simbolo2)
        {
                return simbolo1.probabilidad > simbolo2.probabilidad ? 1 : simbolo1.probabilidad < simbolo2.probabilidad ? -1 : 0;
        };


        public static Func<Registro, Registro, bool> EsPrioritario = delegate (Registro simbolo1, Registro simbolo2)
        {
                return (simbolo1.probabilidad > simbolo2.probabilidad);
        };


    }
}
