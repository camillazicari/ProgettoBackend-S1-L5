using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgettoBackend_S1_L5.models
{
    internal class Contribuente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateOnly DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public char Sesso { get; set; }
        public string ComuneResidenza { get; set; }
        public decimal RedditoAnnuale { get; set; }
        public decimal Imposta { get; set; }

        public Contribuente() { }

        public void NomeContribuente()
        {
            Console.WriteLine("Inserire il nome del contribuente:");
            string nome = Console.ReadLine();
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrWhiteSpace(nome))
            {
                Nome = nome.ToUpper();
                CognomeContribuente();
            }
            else
            {
                Console.WriteLine("Inserire un nome corretto.");
                NomeContribuente();
            }
        }

        public void CognomeContribuente()
        {
            Console.WriteLine("Inserire il cognome del contribuente:");
            string cognome = Console.ReadLine();
            if (!string.IsNullOrEmpty(cognome) && !string.IsNullOrWhiteSpace(cognome))
            {
                Cognome = cognome.ToUpper();
                NascitaContribuente();
            }
            else
            {
                Console.WriteLine("Inserire un cognome corretto.");
                CognomeContribuente();
            }
            
        }

        public void NascitaContribuente()
        {
            Console.WriteLine("Inserire la data di nascita del contibuente " + Nome + " " + Cognome + " rispettando il formato aaaa/mm/gg (es. 2001-03-21):");
            string data = Console.ReadLine();
            Regex regex = new Regex(@"^\d{4}-\d{2}-\d{2}$");
            if (regex.IsMatch(data))
            {
                if (DateOnly.TryParse(data, out DateOnly nascita))
                {
                    DataNascita = nascita;
                    CFContribuente();
                }
                else
                {
                    Console.WriteLine("Data non valida. Verifica che il mese e il giorno siano corretti.");
                    NascitaContribuente();
                }
            }
            else
            {
                Console.WriteLine("Inserire la data di nascita nel formato corretto. Attenzione ad inserire anche - tra l'anno, il giorno ed il mese");
                NascitaContribuente();
            }
        }

        public void ValidazioneCF(string cf)
        {
            
            if (string.IsNullOrWhiteSpace(cf) ||
                   cf.Length != 16 ||
                   !System.Text.RegularExpressions.Regex.IsMatch(cf, "^[A-Z0-9]+$"))
            {
                Console.WriteLine("Inserire un codice fiscale di 16 caratteri nel formato corretto.");
                CFContribuente();
            } 
        }
        public void CFContribuente()
        {
            Console.WriteLine("Inserire il codice fiscale del contibuente " + Nome + " " + Cognome + " :");
            string cf = Console.ReadLine().ToUpper();
            ValidazioneCF(cf);
            CodiceFiscale = cf;
            SessoContribuente();
        }

        public void SessoContribuente()
        { 
            Console.WriteLine("Inserire il sesso del contribuente " + Nome + " " + Cognome + " (f/m):");
            string sex = Console.ReadLine().ToUpper();
            bool sm = false;
            while (!sm)
            {
                if(sex == "M" || sex == "F")
                {
                    sm = true;
                    Sesso = sex[0];
                    ResidenzaContribuente();
                } else
                {
                    Console.WriteLine("Sesso non valido. Inserire 'f' per femmina o 'm' per maschio.");
                    SessoContribuente();
                }
            }
        }

        public void ResidenzaContribuente()
        {
            Console.WriteLine("Inserire il comune di residenza del contribuente " + Nome + " " + Cognome + ":");
            string comune = Console.ReadLine();
            if (!string.IsNullOrEmpty(comune) && !string.IsNullOrWhiteSpace(comune))
            {
                ComuneResidenza = comune.ToUpper();
                RedditoContribuente();
            }
            else
            {
                Console.WriteLine("Inserire un cognome corretto.");
                ResidenzaContribuente();
            }

        }

        public void RedditoContribuente()
        {
            Console.WriteLine("Inserire il reddito del contribuente " + Nome + " " + Cognome + ":");
            bool right = decimal.TryParse(Console.ReadLine(), out decimal reddito);
            if (right)
            {
                RedditoAnnuale = reddito;
                CalcoloImposta();
            } else
            {
                Console.WriteLine("Inserire un reddito valido.");
                RedditoContribuente();
            }

        }

        public void CalcoloImposta() { 
            
            switch (RedditoAnnuale)
            {
                case <= 15000:
                    decimal imposta = RedditoAnnuale * 23 / 100;
                    Imposta = Math.Truncate(imposta * 100) / 100;
                    break;

                case <= 28000:
                    decimal imposta2 = 3450 + ((RedditoAnnuale - 15000) * 27 / 100);
                    Imposta = Math.Truncate(imposta2 * 100) / 100;
                    break;

                case <= 55000:
                    decimal imposta3 = 6960 + ((RedditoAnnuale - 28000) * 38 / 100);
                    Imposta = Math.Truncate(imposta3 * 100) / 100;
                    break;

                case <= 75000:
                    decimal imposta4 = 17220 + ((RedditoAnnuale - 55000) * 41 / 100);
                    Imposta = Math.Truncate(imposta4 * 100) / 100;
                    break;

                case > 75001:
                    decimal imposta5 = 25420 + ((RedditoAnnuale - 75000) * 43 / 100);
                    Imposta = Math.Truncate(imposta5 * 100) / 100;
                    break;
            }

            Riassunto();
        
        }

        public void Riassunto()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE:");
            Console.WriteLine("Contribuente: " + Nome + " " + Cognome);
            Console.WriteLine("Nato/a il: " + DataNascita + " (" + Sesso + ")");
            Console.WriteLine("Residente in: " + ComuneResidenza);
            Console.WriteLine("Codice fiscale: " + CodiceFiscale);
            Console.WriteLine("Reddito dichiarato: " + RedditoAnnuale);
            Console.WriteLine("IMPOSTA DA VERSARE: " + Imposta);
            Console.WriteLine("==================================================");
            back:
            Console.WriteLine("Premi:");
            Console.WriteLine("0. Per effettuare un altro calcolo;");
            Console.WriteLine("1. Per uscire.");
            if (int.TryParse(Console.ReadLine(), out int scelta))
            {
                if (scelta == 0)
                {
                    NomeContribuente();  
                }
                else if (scelta == 1)
                {
                    Console.WriteLine("Arrivederci!");
                }
                else
                {
                    Console.WriteLine("Inserisci un numero valido.");
                    goto back;
                }
            }
            else
            {
                Console.WriteLine("Effettuare una scelta valida tra 0 e 1.");
                goto back;
            }
        }
    }
}