using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary_LAB_03_ED2_URL
{
    interface Compressor
    {
        public byte[] Compresion(byte[] Texto_Original);
        public byte[] Descompresion(byte[] Texto_Comprimido);
    }
}
