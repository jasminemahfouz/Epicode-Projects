using System;

public class ContoCorrente
{
    private string numeroConto;
    private string intestatario;
    private double saldo;
    private bool contoAperto;

    public ContoCorrente(string numeroConto, string intestatario)
    {
        this.numeroConto = numeroConto;
        this.intestatario = intestatario;
        this.saldo = 0.0;
        this.contoAperto = false;
    }

    public void ApriConto(double versamentoIniziale)
    {
        if (!contoAperto)
        {
            if (versamentoIniziale >= 1000)
            {
                saldo = versamentoIniziale;
                contoAperto = true;
                Console.WriteLine("Conto aperto con successo.");
            }
            else
            {
                Console.WriteLine("Il versamento iniziale deve essere almeno di 1000 euro.");
            }
        }
        else
        {
            Console.WriteLine("Il conto è già stato aperto.");
        }
    }

    public void FaiVersamento(double importo)
    {
        if (contoAperto)
        {
            saldo += importo;
            Console.WriteLine("Versamento effettuato con successo.");
        }
        else
        {
            Console.WriteLine("Il conto non è ancora aperto.");
        }
    }

    public void FaiPrelevamento(double importo)
    {
        if (contoAperto)
        {
            if (saldo >= importo)
            {
                saldo -= importo;
                Console.WriteLine("Prelevamento effettuato con successo.");
            }
            else
            {
                Console.WriteLine("Saldo insufficiente.");
            }
        }
        else
        {
            Console.WriteLine("Il conto non è ancora aperto.");
        }
    }

    public double OttieniSaldo()
    {
        return saldo;
    }
}
