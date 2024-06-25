
using System;
using W2_D2_console;

namespace W2_D2_console
{
    class Program
    {
        static void Main(string[] args)
        {
            CV cv = new CV
            {
                InformazioniPersonali = new InformazioniPersonali
                {
                    Nome = "Marco",
                    Cognome = "Puccio",
                    Telefono = "+393288317446",
                    Email = "info@marcopuccio.com"
                }
            };

            cv.StudiEffettuati.Add(new Studi
            {
                Istituto = "UniPa",
                Qualifica = "",
                Tipo = "",
                Dal = new DateTime(2016, 9, 12),
                Al = new DateTime(2019, 7, 22)
            });

            cv.StudiEffettuati.Add(new Studi
            {
                Istituto = "Microsoft Center Milan",
                Qualifica = "",
                Tipo = "Specializzazione sviluppo software",
                Dal = new DateTime(2016, 9, 12),
                Al = new DateTime(2019, 7, 22)
            });

            cv.Impieghi.Add(new Impiego
            {
                Esperienza = new Esperienza
                {
                    Azienda = "Libero Professionista",
                    JobTitle = "IT Manager",
                    Descrizione = "",
                    Compiti = "Sviluppatore software",
                    Dal = new DateTime(2016, 9, 12),
                    Al = new DateTime(2016, 9, 12)
                }
            });

            cv.Impieghi.Add(new Impiego
            {
                Esperienza = new Esperienza
                {
                    Azienda = "Libero Professionista",
                    JobTitle = "Docenza e Formazione",
                    Descrizione = "",
                    Compiti = "Docente e Formatore",
                    Dal = new DateTime(2016, 9, 12),
                    Al = new DateTime(2016, 9, 12)
                }
            });

            StampaDettagliCVSuSchermo(cv);
        }

        static void StampaDettagliCVSuSchermo(CV cv)
        {
            Console.WriteLine($"CV di {cv.InformazioniPersonali.Nome} {cv.InformazioniPersonali.Cognome}");
            Console.WriteLine("++++ INIZIO Informazioni Personali: ++++");
            Console.WriteLine($"Nome: {cv.InformazioniPersonali.Nome}");
            Console.WriteLine($"Cognome: {cv.InformazioniPersonali.Cognome}");
            Console.WriteLine($"Email: {cv.InformazioniPersonali.Email}");
            Console.WriteLine($"Telefono: {cv.InformazioniPersonali.Telefono}");
            Console.WriteLine("++++ FINE Informazioni Personali: ++++");

            Console.WriteLine("++++ INIZIO Studi e Formazione: ++++");
            foreach (var studio in cv.StudiEffettuati)
            {
                Console.WriteLine($"Istituto: {studio.Istituto}");
                Console.WriteLine($"Qualifica: {studio.Qualifica}");
                Console.WriteLine($"Tipo: {studio.Tipo}");
                Console.WriteLine($"Dal: {studio.Dal} al {studio.Al}");
            }
            Console.WriteLine("++++ FINE Studi e Formazione: ++++");

            Console.WriteLine("++++ INIZIO Esperienze professionali: ++++");
            foreach (var impiego in cv.Impieghi)
            {
                Console.WriteLine($"Presso: {impiego.Esperienza.Azienda}");
                Console.WriteLine($"Tipo di lavoro: {impiego.Esperienza.JobTitle}");
                Console.WriteLine($"Compito: {impiego.Esperienza.Compiti}");
                Console.WriteLine($"Dal: {impiego.Esperienza.Dal} al {impiego.Esperienza.Al}");
            }
            Console.WriteLine("++++ FINE Esperienze professionali: ++++");
        }
    }
}
