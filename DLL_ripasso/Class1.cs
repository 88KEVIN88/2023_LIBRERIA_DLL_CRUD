﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string Lunghezzacampi(string filecsv) //funzione che calcola la lunghezza dei singola campi
        {
            int a = 0;
            int[] maxl=new int[1000];
            string[] linee;
            string[] header;
            string re;
            linee = File.ReadAllLines(filecsv);
            if(linee.Length > 0)
            {
                header = linee[0].Split(delimiter);
                maxl = new int[header.Length];
                
                for(int i = 0; i < maxl.Length; i++)
                {
                    string[] campi = linee[i].Split(delimiter);

                    for(int j = 0; j < campi.Length; j++)
                    {
                        maxl[j] = Math.Max(maxl[j], campi[j].Length);
                        a = campi.Length;
                    }
                }
            }

            int k = maxl.Length;
            return maxl[k].ToString() + " ;"  ;
            k++;

        }
       
        public int GetLenght(string filecsv)   //funzione che calcola la lunghezza massima di una riga per definirla come lunghezza standart del file
        {
            StreamReader rd = new StreamReader(filecsv);
            int Maxrecordlenght = 0;

            while (!rd.EndOfStream)
            {
                string line = rd.ReadLine();      //leggo la linea

                int recordLength = line.Length; // Calcolo della lunghezza del record


                if (recordLength > Maxrecordlenght)//se la precendente é minore di quella attuale le scambio
                {
                    Maxrecordlenght = recordLength; // Aggiornamento della lunghezza massima 
                }
            }
            return Maxrecordlenght;
        }

        public int Standart(string filecsv) //funzione cha imposta la lunghezza massima del record
        {
            using (StreamReader rd = new StreamReader(filecsv))
            {
                StringBuilder newcsv = new StringBuilder(); // Creazione di un oggetto StringBuilder per contenere il nuovo file CSV


                int maxRecordLength = GetLenght(filecsv);// Calcolo della lunghezza massima di ogni record

                // Lettura del file CSV e inserimento degli spazi necessari per rendere fissa la dimensione di ogni record
                rd.BaseStream.Seek(0, SeekOrigin.Begin);
                while (!rd.EndOfStream)
                {
                    string line = rd.ReadLine();


                    line = line.PadRight(maxRecordLength);// Aggiunta degli spazi necessari per rendere fissa la lunghezza del record


                    newcsv.AppendLine(line);// Aggiunta del record al nuovo file CSV

                    File.WriteAllText("output.csv", newcsv.ToString());// Scrittura del nuovo file CSV
                }
                return maxRecordLength;

            }
        }
        public void AggiuntaRecord(string filecsv, string c, string p, string r, string n, string a, string d, string i, string l, string la, string v)         //funzione che aggiunge un record alla fine con i dati dati dall utente
        {

            string value = $"{c.ToUpper()};{p.ToUpper()};{r.ToUpper()};{n.ToUpper()};{a.ToUpper()};{d.ToUpper()};{i.ToUpper()};{l.ToUpper()};{la.ToUpper()};{v.ToUpper()};";

            using (StreamWriter writer = File.AppendText(filecsv))
            {
                writer.WriteLine(value);
            }
        }
        public string[][] Stampacampi(string filecsv)
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
        public string[][] FiltraDati(string filecsv, int scelta, string comune = null, string provincia = null)
        {
            List<string[]> data = new List<string[]>();

            using (StreamReader sr = new StreamReader(filecsv))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] campi = line.Split(';');

                    bool aggiungi = false;

                    switch (scelta)
                    {
                        case 1:
                            if (campi[0].ToUpper().Contains(comune.ToUpper())) // Filtra per campo COMUNE
                            {
                                aggiungi = true;
                            }
                            break;
                        case 2:
                            if (campi[1].ToUpper().Contains(provincia.ToUpper())) // Filtra per PROVINCIA
                            {
                                aggiungi = true;
                            }
                            break;
                    }

                    if (aggiungi)
                    {
                        data.Add(campi);
                    }
                }
            }

            return data.ToArray();
        }
        public int RicercaIndex(string filecsv, string ricercato)
        {
            int retu = -2;
            int index = 0;

            StreamReader sr = new StreamReader(filecsv);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] split = line.Split(';');
                if (index != 0)
                {
                    if (split[0] == ricercato.ToUpper())
                    {
                        retu = index;
                        break;
                    }
                    retu = -1;
                }
                index++;
                line = sr.ReadLine();

            }
            sr.Close();
            return retu;
        }
        public void Modifica(string ric,string filecsv,string c, string p, string r, string n, string a, string d, string i, string l, string la, string v)
        {
            
            int dimensione = 0;
            string[] newvalori = new string[11];
            newvalori[0] = c;//comune
            newvalori[1] = p;//provincia
            newvalori[2] = r;//regione
            newvalori[3] = n;//nome
            newvalori[4] = a;//anno
            newvalori[5] = d;//data
            newvalori[6] = i;//identificatori
            newvalori[7] = l;//longitudine
            newvalori[8] = la;//latidudinestring
            string[] file = File.ReadAllLines(filecsv);
            StreamWriter sw = new StreamWriter(filecsv);
            for (int j = 0; j < RicercaIndex(filecsv, ric); j++)
            {
                if (j == RicercaIndex(filecsv, ric))
                {
                    sw.Write(newvalori[dimensione] + ";");
                    dimensione++;

                }
            }

            sw.Close();

        }
    }
}
