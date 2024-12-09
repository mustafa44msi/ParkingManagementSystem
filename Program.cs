using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;


namespace ParkingManagementSystem
{
    internal class Program
    {
        static Car[] Parking = new Car[25];

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int x = 0, y = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Otopark Yönetim Sistemi ====");
                Console.WriteLine("Yön Tuşları 'Gezin' | Enter 'İşlem Yap' | ESC 'Çıkış'");

                Print(x, y);

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape) break;
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        x = Math.Max(0, x - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        x = Math.Min(4, x + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        y = Math.Max(0, y - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        y = Math.Min(4, y + 1);
                        break;
                    case ConsoleKey.Enter:
                        Process(x, y);
                        break;
                }

            }

        }

        static void Print(int cursorX, int cursorY)
        {

            Console.WriteLine($"+{new string('-', 20)}+");
            for (int i = 0; i < 25; i++)
            {
                int currentX = i % 5;
                int currentY = i / 5;

                if (currentX == 0)
                {
                    Console.Write("|");
                }
                if (currentX == cursorX && currentY == cursorY)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                else if (Parking[i] == null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write(Parking[i] == null ? "[🟩]" : "[🅿️]");
                Console.ResetColor();

                if (currentX == 4) Console.WriteLine("|");

            }
            Console.WriteLine($"+{new string('-', 20)}+");



        }

        static void Process(int x, int y)
        {
            int slotIndex = y * 5 + x;
            if (Parking[slotIndex] == null)
            {
                Console.WriteLine("Boş slot. Araç eklemek ister misiniz? (E/H)");
                char select = Console.ReadKey(true).KeyChar;
                if (char.ToUpper(select) == 'E')
                {
                    Console.Write("Araç Plakası: ");
                    string plate = Console.ReadLine();
                    Console.Write("Araç Markası: ");
                    string brandName = Console.ReadLine();

                    Parking[slotIndex] = new Car
                    {
                        Plate = plate,
                        BrandName = brandName,
                        CheckInTime = DateTime.Now
                    };
                    Console.Clear();
                    Console.WriteLine("Araç eklendi!");
                    Thread.Sleep(1000);
                }
                else if (char.ToUpper(select) == 'H') { }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Geçersiz seçim!");
                    Thread.Sleep(1000);
                }

            }
            else
            {
                Console.Clear();
                var car = Parking[slotIndex];
                Console.WriteLine($"Bu slot dolu. Araç bilgilerin\nPlaka: {car.Plate}\nMarka: {car.BrandName}\nGiriş Zamanı: {car.CheckInTime}\n\nAraç çıkarmak ister misiniz (E/H)");
                char secim = Console.ReadKey(true).KeyChar;
                if (char.ToUpper(secim) == 'E')
                {
                    Random random = new Random();
                    int tempSecCode = random.Next(1000, 9999);
                    int secCode = tempSecCode;
                    MessageBox.Show($"Güvenlik Kodunuz: {secCode}");
                    Console.Write("Güvenlik Kodunu Girin: ");
                    int checkSecCode;

                    if (int.TryParse(Console.ReadLine(), out checkSecCode))
                    {
                        if (checkSecCode == secCode)
                        {
                            Parking[slotIndex] = null;
                            Console.WriteLine("Araç çıkarıldı!");
                            Thread.Sleep(1000);
                        }
                        else Console.WriteLine("Geçersiz Güvenlik Kodu");
                    }
                    else Console.WriteLine("Geçersiz giriş! Lütfen bir sayı girin.");
                }
                else if (char.ToUpper(secim) == 'H') { }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Geçersiz seçim!");
                    Thread.Sleep(1000);
                }
            }
        }
    }

    class Car
    {
        public string? Plate { get; set; }
        public string? BrandName { get; set; }
        public DateTime CheckInTime { get; set; }
    }
}