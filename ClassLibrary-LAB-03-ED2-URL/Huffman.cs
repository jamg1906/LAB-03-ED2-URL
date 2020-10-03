using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary_LAB_03_ED2_URL
{
    public class Huffman
    {
        List<Registro> Tabla;

       public void CrearRegistros(string Data)
        {
            Tabla = new List<Registro>();
            Registro Nuevo =new Registro();
            int TOTAL=Data.Length;
            char[] listado;
            foreach(char Carct in Data)
            {
                Nuevo.simbolo = Convert.ToString(Carct);
                for(int j=0; j <Data.Length; j++)
                {
                    if(Nuevo.simbolo.Equals(Data[j]))
                    {
                        Nuevo.Cant_Aparicion++;
                    }
                }
                Data.Trim(Convert.ToChar(Nuevo.simbolo));
                Nuevo.probabilidad = (Nuevo.Cant_Aparicion / TOTAL);
            }
        }
       public string Resultado_Obtenido()
        {
            string Resul="";
            foreach(Registro Item in Tabla)
            {
                Resul += Item.simbolo + "|" + Item.Cant_Aparicion + "|" + Item.probabilidad + Enviroment.Newline;
            }
            return Resul;
        }


    }

}
