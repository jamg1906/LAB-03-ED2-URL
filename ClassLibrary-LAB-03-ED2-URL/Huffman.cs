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
       // List<Registro> Tabla;
        Heap<Registro> Cola;
        Dictionary<string, Registro> Tabla;
        int Cantidad_nodo;
        int Tam_Original;
        bool Compres;
        public string Resultado_Obtenido()
        {
            string Resul="";
     /*       foreach(Registro Item in Tabla)
            {
                Resul += Item.simbolo;
                Resul += "|" + Item.Cant_Aparicion + "|" + Convert.ToString(Item.probabilidad) +"|"+ Item.prefijo + Environment.NewLine;
           }
     */

        var Contenido=      Tabla.Values;
            foreach (Registro Item in Contenido)
            {
                //Resul += Item.simbolo;
                //Resul += "|" + Item.Cant_Aparicion + "|" + Convert.ToString(Item.probabilidad) + "|" + Item.prefijo + Environment.NewLine;
                Resul += String.Format("{0,5}|{1,20}|{2,30}|{3,35}|" + Environment.NewLine, Item.simbolo, Item.Cant_Aparicion, Convert.ToString(Item.probabilidad), Item.prefijo);
            }
            return Resul;
        }
      
        
        public string Compresion(string Data)
        {
            Compres = true;
            Tam_Original = Data.Length;
            Tabla = new Dictionary<string, Registro>();
            string Result = Send_MetaData(CrearRegistros(Data));
            Crear_Arbol();
            Result += Convert_Ascii(Binario_Convert(Data));
            return Result;
            //string Titular= String.Format("{0,300}|{1,300}|{2,30}|{3,35}|" + Environment.NewLine, Result, Data, Tam_Original, Result.Length);
            //return Titular;
        }
        

        public string Send_MetaData(List<Registro> Data)
        {
            string Resul="";
            int tam = 256;
            int Cant_BFrec = 1;
            int Aux = 0;
            foreach (Registro Item in Data)
            {
                if (tam<Item.Cant_Aparicion)
                {
                    tam += 256;
                    Cant_BFrec++;
                }
            }
            Resul += Convert.ToChar(Data.Count).ToString() + Convert.ToChar(Cant_BFrec).ToString();
            foreach (Registro Item in Data)
            {
                Resul += Item.simbolo;
                Aux = Item.Cant_Aparicion % 256;
                Resul += Convert.ToChar(Aux);
                Aux = Item.Cant_Aparicion - Aux;
                for (int i=1; i< Cant_BFrec; i++)
                {
                    if (Aux >= (256 * i)) 
                    {
                        Resul += Convert.ToChar(256);
                    }
                    else 
                    {
                        Resul += Convert.ToChar(" ");
                    }
                }    
            }
            return Resul;
        }
        public List<Registro> CrearRegistros(string Texto)
        {
            List<Registro> Data = new List<Registro>();
            Cola = new Heap<Registro>();
            Registro Nuevo;
            int Total= Texto.Length;
            while(!Texto.Equals(""))
            {
                string Carct = Convert.ToString(Texto[0]);
                Nuevo = new Registro();
                Nuevo.simbolo = Carct;
                for(int j=0; j < Texto.Length; j++)
                {
                    if(Nuevo.simbolo.Equals(Convert.ToString(Texto[j])))
                    {
                        Nuevo.Cant_Aparicion++;
                    }
                }
                Texto = Texto.Replace(Nuevo.simbolo, string.Empty);
                Nuevo.probabilidad = Convert.ToDouble(Nuevo.Cant_Aparicion) / Convert.ToDouble(Total);
                Data.Add(Nuevo);
                Cola.Agregar(Nuevo, Registro.Determinar_Prioridad);
            }
            return Data;
        }

        public void Crear_Arbol()
        {
            while (Cola.Cant_Nodos > 1)
            {
                Registro Aux = new Registro();
                Aux.simbolo = "Nodo" + Cantidad_nodo++;
                Registro One = Cola.Eliminar(Registro.Comparar_Prioridad, Registro.Determinar_Prioridad, Registro.Comparar_Simbolo);
                Registro Two = Cola.Eliminar(Registro.Comparar_Prioridad, Registro.Determinar_Prioridad, Registro.Comparar_Simbolo);
                Aux.probabilidad = One.probabilidad + Two.probabilidad;
                Aux.Hijo_Der = One;
                Aux.Hijo_Izq = Two;
                Cola.Agregar(Aux, Registro.Determinar_Prioridad);
            }
            Agregar_Prefijos(Cola.raiz.Valor);
        }

        public void Agregar_Prefijos(Registro raiz)
        {
            try
            {
                if (Cola.raiz.Valor != null)
                {
                    if (raiz.Hijo_Izq != null)
                    {
                        Inorder(raiz.Hijo_Izq, Registro.Asignar_Prefijo, "0");
                    }
                        raiz.Asig_Prefijo("1");
                    if (!string.IsNullOrEmpty(raiz.prefijo))
                    {
                        if (Compres) { Tabla.Add(raiz.simbolo, raiz); }
                        else { Tabla.Add(raiz.prefijo, raiz); }
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
                if(Compres) { Tabla.Add(Registro_Nodo.simbolo, Registro_Nodo); }
                else { Tabla.Add(Registro_Nodo.prefijo, Registro_Nodo); }
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
                Binario += Tabla[Convert.ToString(Cadena[i])].prefijo;
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
    
    
        public string Descompresion (string Text)
        {
            Tabla = new Dictionary<string, Registro>();
            Compres = false;
            string Txt_Comprimido=Get_Data(Text);
            Crear_Arbol();
            return Get_OriginalText(Convert_Binario(Txt_Comprimido));
        }

        public string Get_Data(string txt)
        {
            Cola = new Heap<Registro>();
            List<Registro> List_aux = new List<Registro>();
            Registro Nuevo;
            int Total = 0;
            string datos = txt;
            int Cant_Caract = Convert.ToInt32(Convert.ToChar(datos[0]));
            int Tam_BFrec   = Convert.ToInt32(datos[1]);
            datos = datos.Remove(0, 2);
            for (int i=0;i<Cant_Caract;i++)
            {
                Nuevo = new Registro();
                Nuevo.simbolo = Convert.ToString(datos[0]);
                Nuevo.Cant_Aparicion = Convert.ToInt32(datos[1]);
                for(int j=2;j<=Tam_BFrec;j++)
                {
                    if(!datos[j].Equals(Convert.ToChar(" ")))
                    { 
                    Nuevo.Cant_Aparicion += Convert.ToInt32(datos[j]);
                    }
                }
                List_aux.Add(Nuevo);
                Total += Nuevo.Cant_Aparicion;
                datos = datos.Remove(0, 1+Tam_BFrec);
            }
            foreach (Registro Item in List_aux)
            {
                Item.probabilidad =  Convert.ToDouble(Item.Cant_Aparicion) / Convert.ToDouble(Total);
                Cola.Agregar(Item, Registro.Determinar_Prioridad);
            }
            Tam_Original = Total;
            return datos;
        }


        private string Convert_Binario(string txt)
        {
            string texto = txt;
            string txt_binario = "";
            string caract_binario = "";
            foreach (char Caract in texto)
            {
                caract_binario = Convert.ToString(Convert.ToInt32(Caract), 2);
                while (caract_binario.Length < 8)
                {
                    caract_binario = "0"+ caract_binario;
                }
                txt_binario += caract_binario;
            }
            return txt_binario;
        }

        private string Get_OriginalText(string text)
        {
            string data_binaria = text;
            string Resultado = "";
            string aux = "";
            int pos = 0;
            for(int i=0;i<Tam_Original;i++)
            {
                aux = Convert.ToString(data_binaria[pos]);
                while (!Tabla.ContainsKey(aux))
                {
                    pos++;
                    aux += Convert.ToString(data_binaria[pos]);
                }
                Resultado += Tabla[aux].simbolo;
                pos++;
            }
            return Resultado;
        }
    }

}
