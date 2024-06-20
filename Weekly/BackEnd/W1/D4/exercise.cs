using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    static class Utente
    {
        private static string username;
        private static string password;
        private static DateTime loginTime;
        private static List<DateTime> accessHistory = new List<DateTime>();

        public static bool IsLoggedIn { get; private set; } = false;

        public static void Login(string user, string pass, string confirmPass)
        {
            if (!IsLoggedIn)
            {
                if (!string.IsNullOrEmpty(user) && pass == confirmPass)
                {
                    username = user;
                    password = pass;
                    loginTime = DateTime.Now;
                    accessHistory.Add(loginTime);
                    IsLoggedIn = true;
                    Console.WriteLine("Login effettuato con successo.");
                }
                else
                {
                    Console.WriteLine("Errore: le password non coincidono o username non valido.");
                }
            }
            else
            {
                Console.WriteLine("Errore: Utente già loggato.");
            }
        }

        public static void Logout()
        {
            if (IsLoggedIn)
            {
                username = null;
                password = null;
                IsLoggedIn = false;
                Console.WriteLine("Logout effettuato con successo.");
            }
            else
            {
                Console.WriteLine("Errore: Nessun utente loggato.");
            }
        }

        public static void VerificaLogin()
        {
            if (IsLoggedIn)
            {
                Console.WriteLine($"L'utente è loggato dal: {loginTime}");
            }
            else
            {
                Console.WriteLine("Errore: Nessun utente loggato.");
            }
        }

        public static void ListaAccessi()
        {
            if (accessHistory.Count > 0)
            {
                Console.WriteLine("Storico degli accessi:");
                foreach (var accesso in accessHistory)
                {
                    Console.WriteLine(accesso);
                }
            }
            else
            {
                Console.WriteLine("Nessun accesso registrato.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("===============OPERAZIONI==============");
                Console.WriteLine("Scegli l'operazione da effettuare:");
                Console.WriteLine("1.: Login");
                Console.WriteLine("2.: Logout");
                Console.WriteLine("3.: Verifica ora e data di login");
                Console.WriteLine("4.: Lista degli accessi");
                Console.WriteLine("5.: Esci");
                Console.WriteLine("========================================");

                string scelta = Console.ReadLine();

                switch (scelta)
                {
                    case "1":
                        Console.Write("Inserisci username: ");
                        string username = Console.ReadLine();
                        Console.Write("Inserisci password: ");
                        string password = Console.ReadLine();
                        Console.Write("Conferma password: ");
                        string confirmPassword = Console.ReadLine();
                        Utente.Login(username, password, confirmPassword);
                        break;
                    case "2":
                        Utente.Logout();
                        break;
                    case "3":
                        Utente.VerificaLogin();
                        break;
                    case "4":
                        Utente.ListaAccessi();
                        break;
                    case "5":
                        Console.WriteLine("Uscita...");
                        return;
                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }
        }
    }
}
