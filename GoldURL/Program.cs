using System;

namespace GoldURL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 創建 QRCode 生成服務
            QRCodeGeneratorService qrCodeService = new QRCodeGeneratorService();

            while (true)
            {
                // 請使用者輸入要生成的 GoldURLID 範圍
                Console.WriteLine();
                Console.WriteLine("請輸入開始的 GoldURLID：");
                if (int.TryParse(Console.ReadLine(), out int startId))
                {
                    Console.WriteLine();
                    Console.WriteLine("請輸入結束的 GoldURLID：");
                    if (int.TryParse(Console.ReadLine(), out int endId))
                    {
                        // 確保 startId <= endId
                        if (startId > endId)
                        {
                            Console.WriteLine();
                            Console.WriteLine("開始的 GoldURLID 不能大於結束的 GoldURLID。");
                            break;
                        }

                        // 依次生成指定範圍內的 QRCode
                        for (int goldUrlId = startId; goldUrlId <= endId; goldUrlId++)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"正在生成 GoldURLID: {goldUrlId} 的 QRCode...");
                            qrCodeService.GenerateQRCode(goldUrlId);
                        }

                        Console.WriteLine();
                        Console.WriteLine($"已成功生成 GoldURLID 範圍從 {startId} 到 {endId} 的 QRCode。");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("結束的 GoldURLID 輸入無效，請輸入有效的數字。");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("開始的 GoldURLID 輸入無效，請輸入有效的數字。");
                    continue;
                }

                // 詢問用戶是否要繼續生成更多的 QRCode
                Console.WriteLine();
                Console.WriteLine("是否要生成更多的 QRCode？(y/n)");
                string response = Console.ReadLine();
                if (response?.ToLower() != "y")
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("按任意鍵退出...");
            Console.ReadKey();
        }
    }
}
