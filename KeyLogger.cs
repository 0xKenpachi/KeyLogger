using System;

namespace keylogger
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demander à l'utilisateur d'entrer du texte
            Console.WriteLine("Veuillez entrer du texte :");

            // Lire l'entrée de l'utilisateur et la stocker dans une variable
            string input = Console.ReadLine();

            // Afficher l'entrée de l'utilisateur
            Console.WriteLine("Vous avez entré : " + input);
        }
    }
}
