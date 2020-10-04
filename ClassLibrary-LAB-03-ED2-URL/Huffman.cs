using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using DataStructures;

namespace ClassLibrary_LAB_03_ED2_URL
{
    public class Huffman
    {
        List<Registro> Tabla;
        Heap<Registro> Cerbero;
        Dictionary<string, Registro> Contenedor;
        int Cantidad_nodo;
        int Tam_Original;
        public string Resultado_Obtenido()
        {
            string Resul="";
     /*       foreach(Registro Item in Tabla)
            {
                Resul += Item.simbolo;
                Resul += "|" + Item.Cant_Aparicion + "|" + Convert.ToString(Item.probabilidad) +"|"+ Item.prefijo + Environment.NewLine;
           }
     */

        var Contenido=      Contenedor.Values;
            foreach (Registro Item in Contenido)
            {
                //Resul += Item.simbolo;
                //Resul += "|" + Item.Cant_Aparicion + "|" + Convert.ToString(Item.probabilidad) + "|" + Item.prefijo + Environment.NewLine;
                Resul += String.Format("{0,5}|{1,20}|{2,30}|{3,35}|" + Environment.NewLine, Item.simbolo, Item.Cant_Aparicion, Convert.ToString(Item.probabilidad), Item.prefijo);
            }
            return Resul;
        }
      
        
        public string Proceso_Prefijado(string Data)
        {
            Tam_Original = Data.Length;
            Contenedor = new Dictionary<string, Registro>();
            CrearRegistros(Data);
            Crear_Arbol();
            String Result = Convert_Ascii(Binario_Convert(Data));
            string Titular= String.Format("{0,300}|{1,300}|{2,30}|{3,35}|" + Environment.NewLine, Result, Data, Tam_Original, Result.Length);
            return Titular;
        }
        
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
                Data=Data.Replace(Nuevo.simbolo, string.Empty);
                Nuevo.probabilidad = Convert.ToDouble(Nuevo.Cant_Aparicion) / Convert.ToDouble(TOTAL);
                Tabla.Add(Nuevo);
                Cerbero.Agregar(Nuevo, Registro.Determinar_Prioridad);
            }
            Tabla.Sort(Registro.Comparar_Prioridad);
        }

        public void Crear_Arbol()
        {
            while (Cerbero.Cant_Nodos > 1)
            {
                Registro Aux = new Registro();
                Aux.simbolo = "Nodo" + Cantidad_nodo++;
                Registro One = Cerbero.Eliminar(Registro.Comparar_Prioridad, Registro.Determinar_Prioridad, Registro.Comparar_Simbolo);
                Registro Two = Cerbero.Eliminar(Registro.Comparar_Prioridad, Registro.Determinar_Prioridad, Registro.Comparar_Simbolo);
                Aux.probabilidad = One.probabilidad + Two.probabilidad;
                Aux.Hijo_Der = One;
                Aux.Hijo_Izq = Two;
                Cerbero.Agregar(Aux, Registro.Determinar_Prioridad);
            }
            Agregar_Prefijos(Cerbero.raiz.Valor);
        }

        public void Agregar_Prefijos(Registro raiz)
        {
            try
            {
                if (Cerbero.raiz.Valor != null)
                {
                    if (raiz.Hijo_Izq != null)
                    {
                        Inorder(raiz.Hijo_Izq, Registro.Asignar_Prefijo, "0");
                    }
                        raiz.Asig_Prefijo("1");
                    if (!string.IsNullOrEmpty(raiz.prefijo))
                    {
                        Contenedor.Add(raiz.simbolo, raiz);
                    }
                    if (raiz.Hijo_Der != null)
                    {
                        Inorder(raiz.Hijo_Der, Registro.Asignar_Prefijo, "1");
                    }
                }
            }
            catch
            {
                //Falta añadir la excepción
            }
        }

        private void Inorder(Registro Registro_Nodo, Delegate Asig_Prefijo, string Prefijo_Binario)
        {
            if (Registro_Nodo.Hijo_Izq != null)
            {
                Inorder(Registro_Nodo.Hijo_Izq, Asig_Prefijo, Prefijo_Binario + "0");
            }
            Registro_Nodo.Asig_Prefijo(Prefijo_Binario);
            if(!string.IsNullOrEmpty(Registro_Nodo.prefijo))
            {
                Contenedor.Add(Registro_Nodo.simbolo, Registro_Nodo);
            }
            if (Registro_Nodo.Hijo_Der != null)
            {
                Inorder(Registro_Nodo.Hijo_Der, Asig_Prefijo, Prefijo_Binario + "1");
            }
        }

        private string Binario_Convert(string Cadena)
        {
            string Binario = "";
            for(int i=0; i<Cadena.Length;i++)
            {
                Registro aux = new Registro();
                Binario += Contenedor[Convert.ToString(Cadena[i])].prefijo;
            }
            return Binario;
        }

        private string Convert_Ascii(string Text_Binario)
        {
            string Comprimido = "";
            int Cantidad_Byte = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Text_Binario.Length) / 8));
            string[] Byte = new string[Cantidad_Byte];
            int posicion = 0;
            while(!string.IsNullOrEmpty(Text_Binario))
            {
                 string aux ="";
                 if(Text_Binario.Length>=8)
                {
                     aux =Text_Binario.Substring(0,8);
                    Text_Binario = Text_Binario.Remove(0, 8);
                    Byte[posicion] = aux;
                }
                else
                {
                    aux = Text_Binario;
                    Text_Binario ="";
                    while (aux.Length < 8)
                    {   
                        aux += "0";
                    }
                    Byte[posicion] = aux;
                }
                posicion++;
            }
            char[] Caracteres_Resul = new char[Cantidad_Byte];
            for(int i=0;i<Cantidad_Byte;i++)
            {
                Caracteres_Resul[i] = Convert.ToChar(Convert.ToInt32(Byte[i], 2));
                Comprimido += Caracteres_Resul[i].ToString(); 
            }
            return Comprimido;
        }
    }

}
