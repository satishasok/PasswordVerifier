using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PasswordVerifierErrors;

namespace PasswordVerifier
{
    class Program
    {
        static void Main(string[] args)
        {
            // Handling command-line arguments
            string inputFilePath = "";
                // show command-line usage help
            if (args.Length != 1 || args[0] == "-h" || args[0] == "-help" || args[0] == "usage") {
                ShowUsage();
                return;
            } else {
                    // extract the input file name from the command line argument
                inputFilePath = args[0];
                    // If the input file does not exist display an error message
                if(!File.Exists(inputFilePath)) {
                    System.Console.WriteLine("PasswordVerifier: Input file \"" + inputFilePath + "\" does not exist. Please enter a valid file path.");
                    return;
                }
                
            } 
            
            try {

                VerifyPassword(inputFilePath);

            } catch(InputError e) {
                    // error parsing the input file, display error message
                System.Console.WriteLine("PasswordVerifier: Input file \"" + inputFilePath + "\" is not in the correct format. Please enter a valid input file path.");
            } catch(Exception e) {
                    // error verifying password. Display error message.
                System.Console.WriteLine("PasswordVerifier: Error while verifying password specified in \"" + inputFilePath + "\"\n" + "Error details: " + e.Message);
            }
        }

        static void ShowUsage()
        {
            System.Console.WriteLine("Usage:");
            System.Console.WriteLine("  PasswordVerifier <inputFilePath> ");
            System.Console.WriteLine("     Where,");
            System.Console.WriteLine("        inputFilePath is full or relative path to the input file.");
        }

        static void VerifyPassword(string inputFilePath)
        {
            PasswordVerifier passwordVerifier = new PasswordVerifier(inputFilePath);

                // generate the outputFilePath from the input file path. Replaces the file extension with ".out"
            string outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), Path.GetFileNameWithoutExtension(inputFilePath) + ".out");

            passwordVerifier.VerifyPasswordAndOutputResults(outputFilePath);

        }
    }
}
