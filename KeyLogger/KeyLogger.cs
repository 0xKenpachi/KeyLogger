using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MonProjetCSharp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Créer un token d'annulation pour détecter CTRL + C
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true; // Empêche l'application de se fermer immédiatement
                cts.Cancel();
            };

            // Chemin du fichier de sortie
            string outputFilePath = "OUTPUT.txt";

            // Vider le fichier de sortie s'il existe déjà
            if (File.Exists(outputFilePath))
            {
                File.WriteAllText(outputFilePath, string.Empty);
            }

            // Boucle infinie pour lire l'entrée de l'utilisateur
            while (true)
            {
                try
                {
                    // Demander à l'utilisateur d'entrer du texte
                    Console.WriteLine("Veuillez entrer du texte (appuyez sur CTRL + C pour quitter) :");

                    // Lire l'entrée de l'utilisateur et la stocker dans une variable
                    string input = await Task.Run(() => Console.ReadLine(), cts.Token);

                    // Afficher l'entrée de l'utilisateur
                    Console.WriteLine("Vous avez entré : " + input);

                    // Écrire l'entrée de l'utilisateur dans le fichier de sortie
                    File.AppendAllText(outputFilePath, input + Environment.NewLine);
                }
                catch (OperationCanceledException)
                {
                    // L'utilisateur a appuyé sur CTRL + C
                    Console.WriteLine("Programme arrêté par l'utilisateur.");
                    break;
                }
            }
        }
    }
}
