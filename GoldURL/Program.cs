using System;

namespace GoldURL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 創建 QRCode 生成服務
            QRCodeGeneratorService qrCodeService = new QRCodeGeneratorService();

            // 請使用者輸入要生成的 GoldURLID 範圍
            Console.WriteLine("請輸入開始的 GoldURLID：");
            if (int.TryParse(Console.ReadLine(), out int startId))
            {
                Console.WriteLine("請輸入結束的 GoldURLID：");
                if (int.TryParse(Console.ReadLine(), out int endId))
                {
                    // 確保 startId <= endId
                    if (startId > endId)
                    {
                        Console.WriteLine("開始的 GoldURLID 不能大於結束的 GoldURLID。");
                        return;
                    }

                    // 依次生成指定範圍內的 QRCode
                    for (int goldUrlId = startId; goldUrlId <= endId; goldUrlId++)
                    {
                        Console.WriteLine($"正在生成 GoldURLID: {goldUrlId} 的 QRCode...");
                        qrCodeService.GenerateQRCode(goldUrlId);
                    }

                    Console.WriteLine($"已成功生成 GoldURLID 範圍從 {startId} 到 {endId} 的 QRCode。");
                }
                else
                {
                    Console.WriteLine("結束的 GoldURLID 輸入無效，請輸入有效的數字。");
                }
            }
            else
            {
                Console.WriteLine("開始的 GoldURLID 輸入無效，請輸入有效的數字。");
            }
        }
    }
}
