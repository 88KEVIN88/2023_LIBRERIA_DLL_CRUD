using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DLL_ripasso
{
    public class Class1
    {
        public Random rand= new Random();
        public char delimiter =  ';';
        public string filecsv = @"relitti.csv";
        public void Add()
        {
            string[] CsvLines = File.ReadAllLines(filecsv); // Legge tutte le linee del file CSV

            
            for (int i = 0; i < CsvLines.Length; i++)
            {
                // Aggiunge il tuo campo con un numero casuale tra 10 e 20 a ciascuna riga + un campo per la cancellazione logica ed un campo univoco
                CsvLines[i] += $";Mio valore:{rand.Next(10, 21)};0;{i}";

            }
            // Sovrascrive il file CSV con le linee aggiornate
            File.WriteAllLines(filecsv, CsvLines);

           
        }
        public int Contatore() //funzione che conta i campi del file
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
        public int Lunghezza() //funzione che calcola la lunghezza massima di un record
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
        public int[] Lunghezzacampi()
        {
            int[] maxl = new int[1000];
            string[] linee;
            string[] header;

            linee = File.ReadAllLines(filecsv);

            if (linee.Length > 0)
            {
                header = linee[0].Split(delimiter);
                maxl = new int[header.Length];

                for (int i = 0; i < maxl.Length; i++)
                {
                    string[] campi = linee[i].Split(delimiter);

                    for (int j = 0; j < campi.Length; j++)
                    {
                        maxl[j] = Math.Max(maxl[j], campi[j].Length);
                    }
                }
            }

            return maxl;
        }

        public int GetLenght()   //funzione che calcola la lunghezza massima di una riga per definirla come lunghezza standart del file
        {
            int Maxrecordlenght = 0;

            using (StreamReader rd = new StreamReader(filecsv))
            {
                while (!rd.EndOfStream)
                {
                    string line = rd.ReadLine(); // leggi la linea
                    int recordLength = line.Length; // Calcola la lunghezza del record

                    if (recordLength > Maxrecordlenght)
                    {
                        Maxrecordlenght = recordLength; // Aggiorna la lunghezza massima 
                    }
                }
            }

            return Maxrecordlenght;
        }

        public int Standart()
        {
            int maxRecordLength = GetLenght(); // Calcolo della lunghezza massima di ogni record

            using (StreamReader rd = new StreamReader(filecsv))
            {
                using (StreamWriter sw = new StreamWriter("output.csv")) // Apertura del nuovo file CSV in modalità scrittura
                {
                    while (!rd.EndOfStream)
                    {
                        string line = rd.ReadLine();
                        line = line.PadRight(maxRecordLength); // Aggiunta degli spazi necessari per rendere fissa la lunghezza del record
                        sw.WriteLine(line); // Scrittura del record nel nuovo file CSV
                    }
                    sw.Close();
                }
                rd.Close();
            }
            

            // Sovrascrive il file CSV originale con il nuovo formato
            File.Copy("output.csv", filecsv, true);
            File.Delete("output.csv"); // Rimuovi il file temporaneo

            return maxRecordLength;
        }
        public void AggiuntaRecord( string c, string p, string r, string n, string a, string d, string i, string l, string la, string v)         //funzione che aggiunge un record alla fine con i dati dati dall utente
        {

            string value = $"{c.ToUpper()};{p.ToUpper()};{r.ToUpper()};{n.ToUpper()};{a.ToUpper()};{d.ToUpper()};{i.ToUpper()};{l.ToUpper()};{la.ToUpper()};{v.ToUpper()};";

            using (FileStream csvRanWriter = new FileStream(filecsv, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                csvRanWriter.Seek(0, SeekOrigin.End);
                csvRanWriter.Write(bytes, 0, bytes.Length);
                csvRanWriter.Close();
            }

        }
        public string[][] Stampacampi()
        {
            List<string[]> data = new List<string[]>();

            using (StreamReader rd = new StreamReader(filecsv))
            {
                while (!rd.EndOfStream)
                {
                    var line = rd.ReadLine();       // Leggo la linea
                    var valori = line.Split(';');   // La divido nei campi
                    data.Add(valori);               // Aggiungo i dati all'elenco
                }
            }

            return data.ToArray();
        }
        
    
       
        public  List<string[]> Ricerca(string comune, string provincia, int scelta)
        {
            List<string[]> risultati = new List<string[]>();

            using (StreamReader sr = new StreamReader(filecsv))
            {
                string line;
              

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string[] campi = line.Split(';');

                    switch (scelta)
                    {
                        case 1:
                            if (campi[0].ToUpper().Contains(comune.ToUpper())) //solo campo COMUNE
                            {
                                risultati.Add(campi);

                            }
                            break;


                        case 2:
                            if (campi[1].ToUpper().Contains(provincia.ToUpper()))//solo PROVINCIA
                            {
                                risultati.Add(campi);
                                break;
                            }

                            break;
                        case 3:
                            if (campi[0].ToUpper().Contains(comune.ToUpper()) && campi[1].ToUpper().Contains(provincia.ToUpper()))//solo REGIONE
                            {
                                risultati.Add(campi);
                                break;
                            }

                            break;


                    }


                }
                
                
                

            }
            return risultati;
        }
        public void Modifica(int indice,string c, string p,string r,string n,string a, string d,string i,string l,string la,string v)
        {
             string[] record=File.ReadAllLines(filecsv);
             record[indice] = $"{c.ToUpper()};{p.ToUpper()};{r.ToUpper()};{n.ToUpper()};{a.ToUpper()};{d.ToUpper()};{i.ToUpper()};{l.ToUpper()};{la.ToUpper()};{v.ToUpper()};0;{indice}";
             File.WriteAllLines(filecsv, record);

        }
        public void Cancellazione(int indice)
        {
            string[] record = File.ReadAllLines(filecsv);
            for(int i = 0; i < indice; i++)
            {
                string[] linea;
                if(indice == i) 
                { 
                    
                }
            }
        }

    }
    
   
}
