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
        public void Add(bool fatto,string filecsv) //Funzione che aggiunge un campo alla fine del record
        {
            if(fatto == false)
            {
                string[] csvline = File.ReadAllLines(filecsv);

                for(int i = 0; i < csvline.Length; i++) 
                {
                    csvline[i] += $";Mio valore:{rand.Next(10, 21)};0,";
                }
                fatto = true;
                File.WriteAllLines(filecsv, csvline);

            }
        }
    }
}
