using System;
namespace W2_D2_console
{
        public class CV
        {
            public InformazioniPersonali InformazioniPersonali { get; set; }
            public List<Studi> StudiEffettuati { get; set; }
            public List<Impiego> Impieghi { get; set; }

            public CV()
            {
                StudiEffettuati = new List<Studi>();
                Impieghi = new List<Impiego>();
            }
        }

        public class InformazioniPersonali
        {
            public string Nome { get; set; }
            public string Cognome { get; set; }
            public string Telefono { get; set; }
            public string Email { get; set; }
        }

        public class Studi
        {
            public string Qualifica { get; set; }
            public string Istituto { get; set; }
            public string Tipo { get; set; }
            public DateTime Dal { get; set; }
            public DateTime Al { get; set; }
        }

        public class Impiego
        {
            public Esperienza Esperienza { get; set; }
        }

        public class Esperienza
        {
            public string Azienda { get; set; }
            public string JobTitle { get; set; }
            public DateTime Dal { get; set; }
            public DateTime Al { get; set; }
            public string Descrizione { get; set; }
            public string Compiti { get; set; }
        }
    }

