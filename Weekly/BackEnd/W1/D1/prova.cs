using System;

namespace ConsoleApp
{
    // Classe Atleta
    class Atleta
    {
        public string? Nome { get; set; }
        public int? Età { get; set; }
        public string? Sport { get; set; }

        public void MostraDettagli()
        {
            Console.WriteLine($"Nome: {Nome}, Età: {Età}, Sport: {Sport}");
        }
    }

    // Classe Dipendendente
    class Dipendendente
    {
        public string? Nome { get; set; }
        public int? Età { get; set; }
        public string? Posizione { get; set; }

        public void MostraDettagli()
        {
            Console.WriteLine($"Nome: {Nome}, Età: {Età}, Posizione: {Posizione}");
        }
    }

    // Classe Animale
    class Animale
    {
        public string? Nome { get; set; }
        public int? Età { get; set; }
        public string? Specie { get; set; }

        public void MostraDettagli()
        {
            Console.WriteLine($"Nome: {Nome}, Età: {Età}, Specie: {Specie}");
        }
    }

    // Classe Veicolo
    class Veicolo
    {
        public string? Marca { get; set; }
        public string? Modello { get; set; }
        public int? Anno { get; set; }

        public void MostraDettagli()
        {
            Console.WriteLine($"Marca: {Marca}, Modello: {Modello}, Anno: {Anno}");
        }
    }

    // Dichiarazione parziale della classe Program
    partial class Program
    {
        static void Main(string[] args)
        {
            // Creare istanze di Atleta
            Atleta atleta1 = new Atleta { Nome = "Ariana Grande", Età = 25, Sport = "Calcio" };
            Atleta atleta2 = new Atleta { Nome = "Luca Bennet", Età = 30, Sport = "Tennis" };

            // Creare istanze di Dipendendente
            Dipendendente dip1 = new Dipendendente { Nome = "Laura Pasuini", Età = 35, Posizione = "Manager" };
            Dipendendente dip2 = new Dipendendente { Nome = "Achille Neri", Età = 40, Posizione = "Ingegnere" };

            // Creare istanze di Animale
            Animale anim1 = new Animale { Nome = "Fido", Età = 3, Specie = "Cane" };
            Animale anim2 = new Animale { Nome = "Merlo", Età = 2, Specie = "Gatto" };

            // Creare istanze di Veicolo
            Veicolo veic1 = new Veicolo { Marca = "Toyota", Modello = "Corolla", Anno = 2019 };
            Veicolo veic2 = new Veicolo { Marca = "Ford", Modello = "Mustang", Anno = 2021 };

            // Stampare dettagli a schermo
            Console.WriteLine("Dettagli degli Atleti:");
            atleta1.MostraDettagli();
            atleta2.MostraDettagli();

            Console.WriteLine("\nDettagli dei Dipendenti:");
            dip1.MostraDettagli();
            dip2.MostraDettagli();

            Console.WriteLine("\nDettagli degli Animali:");
            anim1.MostraDettagli();
            anim2.MostraDettagli();

            Console.WriteLine("\nDettagli dei Veicoli:");
            veic1.MostraDettagli();
            veic2.MostraDettagli();
        }
    }
}
