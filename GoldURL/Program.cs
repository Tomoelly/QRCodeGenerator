using System;

namespace QRCodeGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create QRCode generator service
            QRCodeGeneratorService qrCodeService = new QRCodeGeneratorService();

            while (true)
            {
                // Prompt user to enter the range of IDs to generate QRCode
                Console.WriteLine();
                Console.WriteLine("Please enter the start ID:");
                if (int.TryParse(Console.ReadLine(), out int startId))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the end ID:");
                    if (int.TryParse(Console.ReadLine(), out int endId))
                    {
                        // Ensure startId <= endId
                        if (startId > endId)
                        {
                            Console.WriteLine();
                            Console.WriteLine("The start ID cannot be greater than the end ID.");
                            break;
                        }

                        // Generate QRCode for each ID in the specified range
                        for (int id = startId; id <= endId; id++)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Generating QRCode for ID: {id}...");
                            qrCodeService.GenerateQRCode(id);
                        }

                        Console.WriteLine();
                        Console.WriteLine($"Successfully generated QRCode for IDs from {startId} to {endId}.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid end ID, please enter a valid number.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid start ID, please enter a valid number.");
                    continue;
                }

                // Ask user if they want to generate more QRCode
                Console.WriteLine();
                Console.WriteLine("Do you want to generate more QRCode? (y/n)");
                string response = Console.ReadLine();
                if (response?.ToLower() != "y")
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
