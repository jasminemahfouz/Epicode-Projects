using System;

namespace ConsoleApp
{
    // Classe Persona
    class Persona
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int Eta { get; set; }

        // Costruttore
        public Persona(string nome, string cognome, int eta)
        {
            Nome = nome;
            Cognome = cognome;
            Eta = eta;
        }

        // Metodi per ottenere i dettagli
        public string GetNome()
        {
            return Nome;
        }

        public string GetCognome()
        {
            return Cognome;
        }

        public int GetEta()
        {
            return Eta;
        }

        public string GetDettagli()
        {
            return $"Nome: {Nome}, Cognome: {Cognome}, Età: {Eta}";
        }
    }

    // Dichiarazione parziale della classe Program
    partial class Program
    {
        static void Main(string[] args)
        {
            // Creare istanze di Persona utilizzando il costruttore
            Persona persona1 = new Persona("Yasmin", "Mahfouz", 25);
            Persona persona2 = new Persona("Elodie", "Bianchi", 30);

            // Stampare dettagli a schermo usando i metodi della classe Persona
            Console.WriteLine("Dettagli della Persona 1:");
            Console.WriteLine(persona1.GetDettagli());
            Console.WriteLine($"Nome: {persona1.GetNome()}");
            Console.WriteLine($"Cognome: {persona1.GetCognome()}");
            Console.WriteLine($"Età: {persona1.GetEta()}");

            Console.WriteLine("\nDettagli della Persona 2:");
            Console.WriteLine(persona2.GetDettagli());
            Console.WriteLine($"Nome: {persona2.GetNome()}");
            Console.WriteLine($"Cognome: {persona2.GetCognome()}");
            Console.WriteLine($"Età: {persona2.GetEta()}");
        }
    }
}
