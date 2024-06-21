using System;

public class Contribuente
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public DateTime DataNascita { get; set; }
    public string CodiceFiscale { get; set; }
    public char Sesso { get; set; }
    public string ComuneResidenza { get; set; }
    public decimal RedditoAnnuale { get; set; }

    public Contribuente(string nome, string cognome, DateTime dataNascita, string codiceFiscale, char sesso, string comuneResidenza, decimal redditoAnnuale)
    {
        Nome = nome;
        Cognome = cognome;
        DataNascita = dataNascita;
        CodiceFiscale = codiceFiscale;
        Sesso = sesso;
        ComuneResidenza = comuneResidenza;
        RedditoAnnuale = redditoAnnuale;
    }

    public decimal CalcolaImposta()
    {
        decimal imposta = 0;
        decimal reddito = RedditoAnnuale;

        // Scaglioni di reddito e aliquote
        if (reddito <= 15000)
        {
            imposta += reddito * 0.23m;
        }
        else if (reddito <= 28000)
        {
            imposta += 15000 * 0.23m;
            imposta += (reddito - 15000) * 0.27m;
        }
        else if (reddito <= 55000)
        {
            imposta += 15000 * 0.23m;
            imposta += (28000 - 15000) * 0.27m;
            imposta += (reddito - 28000) * 0.38m;
        }
        else if (reddito <= 75000)
        {
            imposta += 15000 * 0.23m;
            imposta += (28000 - 15000) * 0.27m;
            imposta += (55000 - 28000) * 0.38m;
            imposta += (reddito - 55000) * 0.41m;
        }
        else
        {
            imposta += 15000 * 0.23m;
            imposta += (28000 - 15000) * 0.27m;
            imposta += (55000 - 28000) * 0.38m;
            imposta += (75000 - 55000) * 0.41m;
            imposta += (reddito - 75000) * 0.43m;
        }

        return imposta;
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Inserisci i dati del contribuente:");

            Console.Write("Nome: ");
            string? nome = Console.ReadLine();
            if (string.IsNullOrEmpty(nome)) throw new ArgumentNullException("Nome non può essere vuoto");

            Console.Write("Cognome: ");
            string? cognome = Console.ReadLine();
            if (string.IsNullOrEmpty(cognome)) throw new ArgumentNullException("Cognome non può essere vuoto");

            DateTime dataNascita;
            while (true)
            {
                Console.Write("Data di nascita (gg/mm/aaaa): ");
                string? dataNascitaInput = Console.ReadLine();
                if (DateTime.TryParseExact(dataNascitaInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataNascita))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Formato data non valido. Riprova.");
                }
            }

            Console.Write("Codice Fiscale: ");
            string? codiceFiscale = Console.ReadLine();
            if (string.IsNullOrEmpty(codiceFiscale)) throw new ArgumentNullException("Codice Fiscale non può essere vuoto");

            Console.Write("Sesso (M/F): ");
            string? sessoInput = Console.ReadLine();
            if (string.IsNullOrEmpty(sessoInput)) throw new ArgumentNullException("Sesso non può essere vuoto");
            char sesso = char.Parse(sessoInput);

            Console.Write("Comune di residenza: ");
            string? comuneResidenza = Console.ReadLine();
            if (string.IsNullOrEmpty(comuneResidenza)) throw new ArgumentNullException("Comune di residenza non può essere vuoto");

            Console.Write("Reddito annuale: ");
            string? redditoAnnualeInput = Console.ReadLine();
            if (string.IsNullOrEmpty(redditoAnnualeInput)) throw new ArgumentNullException("Reddito annuale non può essere vuoto");
            decimal redditoAnnuale = decimal.Parse(redditoAnnualeInput);

            Contribuente contribuente = new Contribuente(nome, cognome, dataNascita, codiceFiscale, sesso, comuneResidenza, redditoAnnuale);

            decimal imposta = contribuente.CalcolaImposta();

            Console.WriteLine("==================================================");
            Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE:");
            Console.WriteLine($"Contribuente: {contribuente.Nome} {contribuente.Cognome},");
            Console.WriteLine($"nato il {contribuente.DataNascita:dd/MM/yyyy} ({contribuente.Sesso}),");
            Console.WriteLine($"residente in {contribuente.ComuneResidenza},");
            Console.WriteLine($"codice fiscale: {contribuente.CodiceFiscale}");
            Console.WriteLine($"Reddito dichiarato: {contribuente.RedditoAnnuale:C}");
            Console.WriteLine($"IMPOSTA DA VERSARE: {imposta:C}");
            Console.WriteLine("==================================================");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore: {ex.Message}");
        }
        // Mantiene aperta la console fino a quando l'utente non preme invio
        Console.WriteLine("Premi invio per uscire...");
        Console.ReadLine();
    }
}
