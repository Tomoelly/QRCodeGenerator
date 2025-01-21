using QRCoder;
using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace GoldURL
{
    public class QRCodeGeneratorService
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public QRCodeGeneratorService()
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Retrieve connection string
            connectionString = configuration.GetConnectionString("DatabaseConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");
        }

        /// <summary>
        /// 根據資料表中的指定 GoldURLID 生成 QRCode 圖片並保存為 PNG 檔案。
        /// </summary>
        /// <param name="goldUrlId">資料表中的 GoldURLID</param>
        public void GenerateQRCode(int goldUrlId)
        {
            // 從資料庫中查詢指定 GoldURLID 的資料
            string fullUrl = GetFullUrlFromDatabase(goldUrlId);

            if (!string.IsNullOrEmpty(fullUrl))
            {
                // 定義保存路徑
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string folderPath = Path.Combine(desktopPath, "GoldQRCode");
                Directory.CreateDirectory(folderPath); // 確保資料夾存在
                string savePath = Path.Combine(folderPath, $"QRCode_{goldUrlId}.png"); // 保存圖片的路徑，根據 GoldURLID 命名

                // 初始化 QRCode 產生器
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    // 產生 QRCode 資料
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(fullUrl, QRCodeGenerator.ECCLevel.Q);

                    // 使用 PngByteQRCode 生成 QR Code 圖片
                    PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                    byte[] qrCodeImage = qrCode.GetGraphic(20);

                    // 保存 QR Code 圖片到指定路徑
                    File.WriteAllBytes(savePath, qrCodeImage);
                    Console.WriteLine($"QRCode 已成功保存到 {savePath}");
                }
            }
            else
            {
                Console.WriteLine("指定的 GoldURLID 找不到或已使用。");
            }
        }

        /// <summary>
        /// 根據 GoldURLID 從資料庫查詢 FullURL。
        /// </summary>
        /// <param name="goldUrlId">資料表中的 GoldURLID</param>
        /// <returns>對應的 FullURL 或 null</returns>
        private string GetFullUrlFromDatabase(int goldUrlId)
        {
            string query = "SELECT FullURL FROM LineAt_EG_GoldURL WHERE GoldURLID = @GoldURLID AND UsedDate IS NULL"; // 查詢未使用過的 URL

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GoldURLID", goldUrlId);
                connection.Open();

                var result = command.ExecuteScalar();
                return result?.ToString();
            }
        }
    }
}
