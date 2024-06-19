using System;

class Program
{
    static void Main()
    {
        string[] nomi = {"Mario", "Luca", "Giulia", "Anna", "Francesco"};
        
        Console.WriteLine("Inserisci il nome da cercare:");
        string nomeDaCercare = Console.ReadLine();
        
        bool trovato = false;
        foreach (string nome in nomi)
        {
            if (nome.Equals(nomeDaCercare, StringComparison.OrdinalIgnoreCase))
            {
                trovato = true;
                break;
            }
        }
        
        if (trovato)
        {
            Console.WriteLine("Il nome è presente nell'array.");
        }
        else
        {
            Console.WriteLine("Il nome non è presente nell'array.");
        }
    }
}
