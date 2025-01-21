using QRCoder;
using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace QRCodeGenerator
{
    public class QRCodeGeneratorService
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public QRCodeGeneratorService()
        {
            try
            {
                // Build configuration
                configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Retrieve connection string
                connectionString = configuration.GetConnectionString("DatabaseConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing configuration: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Generates a QRCode image based on the specified ID from the database and saves it as a PNG file.
        /// </summary>
        /// <param name="id">The ID from the database</param>
        public void GenerateQRCode(int id)
        {
            try
            {
                // Retrieve data from the database based on the specified ID
                string fullUrl = GetFullUrlFromDatabase(id);

                if (!string.IsNullOrEmpty(fullUrl))
                {
                    // Define save path
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string folderPath = Path.Combine(desktopPath, "QRCodeImages");
                    Directory.CreateDirectory(folderPath); // Ensure the folder exists
                    string savePath = Path.Combine(folderPath, $"QRCode_{id}.png"); // Save path for the image, named based on the ID

                    // Initialize QRCode generator
                    using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                    {
                        // Generate QRCode data
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(fullUrl, QRCodeGenerator.ECCLevel.Q);

                        // Generate QR Code image using PngByteQRCode
                        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                        byte[] qrCodeImage = qrCode.GetGraphic(20);

                        // Save QR Code image to the specified path
                        File.WriteAllBytes(savePath, qrCodeImage);
                        Console.WriteLine($"QRCode successfully saved to {savePath}");
                    }
                }
                else
                {
                    Console.WriteLine("The specified ID was not found or has already been used.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating QRCode: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves the FullURL from the database based on the specified ID.
        /// </summary>
        /// <param name="id">The ID from the database</param>
        /// <returns>The corresponding FullURL or null</returns>
        private string GetFullUrlFromDatabase(int id)
        {
            try
            {
                string query = "SELECT FullURL FROM YourTable WHERE ID = @ID AND UsedDate IS NULL"; // Query for unused URLs

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();

                    var result = command.ExecuteScalar();
                    return result?.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving FullURL from database: {ex.Message}");
                return null;
            }
        }
    }
}
