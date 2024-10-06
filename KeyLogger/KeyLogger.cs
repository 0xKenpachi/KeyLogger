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

            // Boucle infinie pour capturer les touches pressées
            while (true)
            {
                try
                {
                    // Vérifier si une touche a été pressée
                    if (Console.KeyAvailable)
                    {
                        // Lire la touche pressée
                        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                        // Écrire la touche pressée dans le fichier de sortie
                        File.AppendAllText(outputFilePath, keyInfo.KeyChar.ToString());
                    }

                    // Attendre un court moment pour éviter de surcharger le CPU
                    await Task.Delay(100, cts.Token);
                }
                catch (OperationCanceledException)
                {
                    // L'utilisateur a appuyé sur CTRL + C
                    Console.WriteLine("\nProgramme arrêté par l'utilisateur.");
                    break;
                }
            }
        }
    }
}
