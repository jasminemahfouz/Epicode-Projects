using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<int, (string, double)> menu = new Dictionary<int, (string, double)>
        {
            { 1, ("Coca Cola 150 ml", 2.50) },
            { 2, ("Insalata di pollo", 5.20) },
            { 3, ("Pizza Margherita", 10.00) },
            { 4, ("Pizza 4 Formaggi", 12.50) },
            { 5, ("Pz patatine fritte", 3.50) },
            { 6, ("Insalata di riso", 8.00) },
            { 7, ("Frutta di stagione", 5.00) },
            { 8, ("Pizza fritta", 5.00) },
            { 9, ("Piadina vegetariana", 6.00) },
            { 10, ("Panino Hamburger", 7.90) }
        };

        List<int> ordine = new List<int>();
        double costoTotale = 0.0;
        const double servizio = 3.00;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("==============MENU==============");
            foreach (var voce in menu)
            {
                Console.WriteLine($"{voce.Key}: {voce.Value.Item1} (€ {voce.Value.Item2})");
            }
            Console.WriteLine("11: Stampa conto finale e conferma");
            Console.WriteLine("==============MENU==============");

            Console.Write("Seleziona un'opzione: ");
            if (int.TryParse(Console.ReadLine(), out int scelta))
            {
                if (scelta == 11)
                {
                    Console.Clear();
                    Console.WriteLine("==============CONTO==============");
                    foreach (var item in ordine)
                    {
                        var voce = menu[item];
                        Console.WriteLine($"{voce.Item1} - € {voce.Item2}");
                        costoTotale += voce.Item2;
                    }
                    Console.WriteLine($"Servizio al tavolo: € {servizio}");
                    costoTotale += servizio;
                    Console.WriteLine($"Costo totale: € {costoTotale}");
                    Console.WriteLine("Grazie per aver ordinato!");
                    Console.WriteLine("================================");
                    break;
                }
                else if (menu.ContainsKey(scelta))
                {
                    ordine.Add(scelta);
                    Console.WriteLine($"{menu[scelta].Item1} aggiunto al tuo ordine.");
                }
                else
                {
                    Console.WriteLine("Opzione non valida. Riprova.");
                }
            }
            else
            {
                Console.WriteLine("Inserisci un numero valido.");
            }

            Console.WriteLine("Premi un tasto per continuare...");
            Console.ReadKey();
        }
    }
}
