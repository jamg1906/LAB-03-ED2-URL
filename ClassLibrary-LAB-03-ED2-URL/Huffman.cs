using System;
using System.Collections.Generic;
using System.Text;
using DataStructures;

namespace ClassLibrary_LAB_03_ED2_URL
{
    public class Huffman
    {
        List<Registro> Tabla;
        Heap<Registro> Cerbero;
        Heap<Nodo<Registro>> Can;
       public void CrearRegistros(string Data)
        {
            Tabla = new List<Registro>();
            Cerbero = new Heap<Registro>();
            Registro Nuevo;
            int TOTAL=Data.Length;
            while(!Data.Equals(""))
            {
                string Carct = Convert.ToString(Data[0]);
                Nuevo = new Registro();
                Nuevo.simbolo = Carct;
                for(int j=0; j <Data.Length; j++)
                {
                    if(Nuevo.simbolo.Equals(Convert.ToString(Data[j])))
                    {
                        Nuevo.Cant_Aparicion++;
                    }
                }
                //Data=Data.Replace(Convert.ToString(Nuevo.simbolo), string.Empty);
                Data=Data.Replace(Nuevo.simbolo, string.Empty);
                Nuevo.probabilidad = Convert.ToDouble(Nuevo.Cant_Aparicion) / Convert.ToDouble(TOTAL);
                Tabla.Add(Nuevo);
                Cerbero.Agregar(Nuevo, Registro.PrioMenor);
            }
            Tabla.Sort(Registro.Comparar_Prioridad);
        }
       public string Resultado_Obtenido()
        {
            string Resul="";
            foreach(Registro Item in Tabla)
            {
                Resul += Item.simbolo;
                Resul += "|" + Item.Cant_Aparicion + "|" + Convert.ToString(Item.probabilidad) +"|"+ Item.prefijo + Environment.NewLine;
            }
            
            return Resul;
        }
       public void AsignarPrefijos()
        {
            Cerbero.Add_Prefijo_Inorder(Registro.Add_Prefijo);
        }




    }

}
