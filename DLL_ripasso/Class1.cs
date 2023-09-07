using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_ripasso
{
    public class Class1
    {
        public Random rand= new Random();
        public char delimiter =  ';';
        public int Add(int fatto,string filecsv) //Funzione che aggiunge un campo alla fine del record
        {
            if(fatto == 0)
            {
                string[] csvline = File.ReadAllLines(filecsv);

                for(int i = 0; i < csvline.Length; i++) 
                {
                    csvline[i] += $";Mio valore:{rand.Next(10, 21)};0,";
                }
                fatto = 1;
                File.WriteAllLines(filecsv, csvline);

            }
            return fatto;
        }   
        public int Contatore(string filecsv) //funzione che conta i campi del file
        {
             int cont = 0;
             using(StreamReader sr = new StreamReader(filecsv))
             {
                string line = sr.ReadLine();
                if(!String.IsNullOrEmpty(line))
                {
                   cont= line.Split(delimiter).Length;
                }
             }
             return cont;
        }
        public int Lunghezza(string filecsv) //funzione che calcola la lunghezza massima di un record
        {
            int[] recordl=new int[1000];
            int Maxverstappen;
            using(StreamReader sr = new StreamReader(filecsv))
            {
                string linea;
                int i = 0;
                while((linea = sr.ReadLine()) != null)
                {
                    recordl[i]=linea.Length; 
                    i++;
                }
                Maxverstappen = recordl.Max();
                return Maxverstappen;
            }
        }

    }
}
