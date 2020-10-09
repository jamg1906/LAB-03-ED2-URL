﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using System.Text;
using DataStructures;
using LAB_03_ED2_URL.Models;
using System.Text.Json;

namespace ClassLibrary_LAB_03_ED2_URL
{
    public class Huffman
    {
        Heap<Registro> Cola;
        Dictionary<byte, Registro> Tabla;
        Dictionary<string, Registro> Tabla_Decompres;

        int Tam_Original;
        bool Compres;
        public string Resultado_Obtenido()
        {
            string Resul="";
             var Contenido=      Tabla.Values;
            foreach (Registro Item in Contenido)
            {
                     Resul += String.Format("{0,5}|{1,5}|{2,20}|{3,30}|{4,35}|" + Environment.NewLine, Convert.ToChar(Item.simbolo), Item.simbolo, Item.Cant_Aparicion, Convert.ToString(Item.probabilidad), Item.prefijo);
            }
            return Resul;
        }
      
        
        public byte[] Compresion(byte[] Data)
        {
            Compres = true;
            Tam_Original = Data.Length;
            Tabla = new Dictionary<byte, Registro>();
            CrearRegistros(Data);
            byte[] Meta_Data = Send_MetaData();
            int Tam_Data = Meta_Data.Length;
            Crear_Arbol();
            Agregar_Prefijos(Cola.raiz.Valor);
            byte[] Result = Convert_Ascii(Binario_Convert(Data));
            Array.Resize(ref Meta_Data, Meta_Data.Length + Result.Length);
            Result.CopyTo(Meta_Data, Tam_Data);
            return Meta_Data;
        }

        public void WriteRegistry(string OriginalName, string CompressedFilePath, double CompressionRatio, double CompressionFactor, double ReductionPercentage)
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\") + "\\LAB-03-ED2-URL\\test.json");
            using (var reader = new StreamReader(path))
            {
                List<Compression> All = new List<Compression>();
                JsonSerializerOptions rule = new JsonSerializerOptions { IgnoreNullValues = true };
                All = JsonSerializer.Deserialize<List<Compression>>(reader.ReadToEnd(), rule);
                Compression NewRegistry = new Compression();
                NewRegistry.originalName = OriginalName;
                NewRegistry.compressedFilePath = CompressedFilePath;
                NewRegistry.compressionRatio = CompressionRatio;
                NewRegistry.compressionFactor = CompressionRatio;
                NewRegistry.reductionPercentage = ReductionPercentage;
                All.Add(NewRegistry);
                reader.Close();
                using (var writer = new StreamWriter(path))
                {
                    var AllRegistries = JsonSerializer.Serialize<List<Compression>>(All, rule);
                    writer.Write(AllRegistries);
                }
            }
        }


        public byte[] Send_MetaData()
        {
            int tam = 256;
            int Cant_BFrec = 1;
            int Aux = 0;
            var Data = Tabla.Values;
            foreach (Registro Item in Data)
            {
                    while (tam < Item.Cant_Aparicion)
                    {
                        tam += 256;
                        Cant_BFrec++;
                    }
            }
            byte[] Resul = new byte[2+(Cant_BFrec+1)*Data.Count];
            int posicion = 0;
            Resul[posicion++] = Convert.ToByte(Convert.ToChar(Data.Count));
            Resul[posicion++] = Convert.ToByte(Convert.ToChar(Cant_BFrec));
            int cantidaD = 0;
            foreach (Registro Item in Data)
            {
                cantidaD += Item.Cant_Aparicion;
                Resul[posicion++] = Item.simbolo;
                Aux = Item.Cant_Aparicion % 256;
                Resul[posicion++] = Convert.ToByte(Convert.ToChar(Aux));
                Aux = Item.Cant_Aparicion - Aux;
                for (int i=1; i< Cant_BFrec; i++)
                {
                    if (Aux >= (256 * i)) 
                    {
                        Resul[posicion++] = Convert.ToByte(Convert.ToChar(255));
                    }
                    else 
                    {
                        Resul[posicion++] = Convert.ToByte(Convert.ToChar(" "));
                    }
                }    
            }
            return Resul;
        }
        public void CrearRegistros(byte[] Texto)
        {

            Cola = new Heap<Registro>();
            Registro Nuevo;
            int Total = Texto.Length;
            for (int i = 0; i < Total; i++)
            {
                Nuevo = new Registro();
                Nuevo.simbolo = Texto[i];
                if (Tabla.ContainsKey(Nuevo.simbolo))
                {
                    Tabla[Nuevo.simbolo].Cant_Aparicion++;
                }
                else
                {
                    Tabla.Add(Texto[i], Nuevo);
                    Tabla[Nuevo.simbolo].Cant_Aparicion++;
                }
            }
            var Contenido = Tabla.Values;
            foreach (Registro Item in Contenido)
            {
                Item.probabilidad = Convert.ToDouble(Item.Cant_Aparicion) / Convert.ToDouble(Total);
                Cola.Agregar(Item, Registro.Determinar_Prioridad);
            }
        }

        public void Crear_Arbol()
        {
            while (Cola.Cant_Nodos > 1)
            {
                Registro Aux = new Registro();
                Aux.simbolo = 0;
                Registro One = Cola.Eliminar(Registro.Comparar_Prioridad, Registro.Determinar_Prioridad, Registro.Comparar_Simbolo);
                Registro Two = Cola.Eliminar(Registro.Comparar_Prioridad, Registro.Determinar_Prioridad, Registro.Comparar_Simbolo);
                Aux.probabilidad = One.probabilidad + Two.probabilidad;
                Aux.Hijo_Der = One;
                Aux.Hijo_Izq = Two;
                Cola.Agregar(Aux, Registro.Determinar_Prioridad);
            }
        }

        private void Agregar_Prefijos(Registro raiz)
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
                        if (Compres) {
                            Tabla[raiz.simbolo].Asig_Prefijo("1");

                        }
                        else { Tabla_Decompres.Add(raiz.prefijo, raiz); }
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
                if(Compres) { 
                             Tabla[Registro_Nodo.simbolo].Asig_Prefijo(Prefijo_Binario);
                }
                else {
                    Tabla_Decompres.Add(Registro_Nodo.prefijo, Registro_Nodo);
                }
            }
            if (Registro_Nodo.Hijo_Der != null)
            {
                Inorder(Registro_Nodo.Hijo_Der, Asig_Prefijo, Prefijo_Binario + "1");
            }
        }

        private string Binario_Convert(byte[] Cadena)
        {
            string Binario = "";
            for(int i=0; i<Cadena.Length;i++)
            {
                Registro aux = new Registro();
                Binario += Tabla[Cadena[i]].prefijo;
            }
            return Binario;
        }

        private byte[] Convert_Ascii(string Text_Binario)
        {
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
            byte[] Comprimido = new byte[Cantidad_Byte];
            for (int i=0;i<Cantidad_Byte;i++)
            {
                //Duda
                Caracteres_Resul[i] = Convert.ToChar(Convert.ToInt32(Byte[i], 2));
                Comprimido[i] = Convert.ToByte(Caracteres_Resul[i]); 
            }
            return Comprimido;
        }
    
    
        public byte[] Descompresion (byte[] Text)
        {
            Tabla_Decompres = new Dictionary<string, Registro>();
            Compres = false;
            byte[] Txt_Comprimido=Get_Data(Text);
            Crear_Arbol();
            Agregar_Prefijos(Cola.raiz.Valor);
            return Get_OriginalText(Convert_Binario(Txt_Comprimido));
        }

        private byte[] Get_Data(byte[] txt)
        {
            Cola = new Heap<Registro>();
            List<Registro> List_aux = new List<Registro>();
            Registro Nuevo;
            int Total = 0;
            byte[] datos = txt;
            int Cant_Caract = Convert.ToInt32(Convert.ToChar(datos[0]));
            int Tam_BFrec   = Convert.ToInt32(datos[1]);
            int posicion= 2;
            for (int i=0;i<Cant_Caract;i++)
            {
                Nuevo = new Registro();
                Nuevo.simbolo = datos[posicion++];
                Nuevo.Cant_Aparicion = Convert.ToInt32(datos[posicion++]);
                for(int j=2;j<=Tam_BFrec;j++)
                {
                    if(datos[posicion]!=32)
                    { 
                    Nuevo.Cant_Aparicion += 256;
                    }
                    posicion++;
                }
                List_aux.Add(Nuevo);
                Total += Nuevo.Cant_Aparicion;
            }
            foreach (Registro Item in List_aux)
            {
                Item.probabilidad = Convert.ToDouble(Item.Cant_Aparicion) / Convert.ToDouble(Total);
                Cola.Agregar(Item, Registro.Determinar_Prioridad);
            }
            Tam_Original = Total;
            byte[] Data_retorna = new byte[datos.Length-posicion];
            Array.Copy(datos, posicion, Data_retorna, 0, Data_retorna.Length);
            return Data_retorna;
        }


        private string Convert_Binario(byte[] txt)
        {
            byte[] texto = txt;
            string txt_binario = "";
            string caract_binario = "";
            foreach (byte Caract in texto)
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

        private byte[] Get_OriginalText(string text)
        {
            string data_binaria = text;
            byte[] Resultado = new byte[Tam_Original];
            string aux = "";
            int pos = 0;
            for(int i=0;i<Tam_Original;i++)
            {
                aux = Convert.ToString(data_binaria[pos]);
                while (!Tabla_Decompres.ContainsKey(aux))
                {
                    pos++;
                    aux += Convert.ToString(data_binaria[pos]);
                }
                Resultado[i] = Tabla_Decompres[aux].simbolo;
                pos++;
            }
            return Resultado;
        }
    }

}
