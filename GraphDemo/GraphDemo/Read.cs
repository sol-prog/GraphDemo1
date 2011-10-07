using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GraphDemo
{
    class Read
    {
        private string[] header;
        private float[,] data;
        private int nLines;
        private int nColumns;

        public Read(Stream myStream)
        {
            string aux;
            string[] pieces;

            //read the file line by line
            StreamReader sr = new StreamReader(myStream);
            aux = sr.ReadLine();
            header = aux.Split(',');
            nColumns = header.Length;
            nLines = 0;
            while ((aux = sr.ReadLine()) != null)
            {
                if (aux.Length > 0) nLines++;
            }

            //read the numerical data from file in an array
            data = new float[nLines, nColumns];
            sr.BaseStream.Seek(0, 0);
            sr.ReadLine();
            for (int i = 0; i < nLines; i++)
            {
                aux = sr.ReadLine();
                pieces = aux.Split(',');
                for (int j = 0; j < nColumns; j++) data[i, j] = float.Parse(pieces[j]);
            }
            sr.Close();
        }

        //functions used for retrieving the data
        public int get_nLines()
        {
            return nLines;
        }

        public int get_nColumns()
        {
            return nColumns;
        }

        public float[,] get_Data()
        {
            return data;
        }

        public string[] get_Header()
        {
            return header;
        }
    }
}
