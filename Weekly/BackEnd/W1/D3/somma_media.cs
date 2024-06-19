using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Inserisci la dimensione dell'array:");
        int dimensione = int.Parse(Console.ReadLine());
        
        int[] numeri = new int[dimensione];
        
        for (int i = 0; i < dimensione; i++)
        {
            Console.WriteLine($"Inserisci il numero {i + 1}:");
            numeri[i] = int.Parse(Console.ReadLine());
        }
        
        int somma = 0;
        for (int i = 0; i < numeri.Length; i++)
        {
            somma += numeri[i];
        }
        
        double media = (double)somma / numeri.Length;
        
        Console.WriteLine($"La somma di tutti i numeri è: {somma}");
        Console.WriteLine($"La media aritmetica di tutti i numeri è: {media}");
    }
}
