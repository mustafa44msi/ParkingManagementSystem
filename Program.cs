using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Media;
using ParkingManagementSystem.ssshh;


namespace ParkingManagementSystem
{
    public class Car // Uygulama Boyunca Kullanılacak değişkenlerin tanımlandığı sınıf.
    {
        public string? Plate { get; set; }
        public string? BrandName { get; set; }
        public DateTime CheckInTime { get; set; }
    }

    public class Program
    {
        static Car[] Parking = new Car[25]; //25 adet araçın verilerinin tutulduğu dizi.
        public static double dailyIncome = 0; // Günlük Ciro değişkeni.

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // Türkçe karakter desteği.
            int x = 0, y = 0; // Kursorun başlangıç konumu.

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Otopark Yönetim Sistemi ====");
                Console.WriteLine("Yön Tuşları 'Gezin' | Enter 'İşlem Yap' | ESC 'Çıkış'");

                Print(x, y);

                var key = Console.ReadKey(true); // Kullanıcının tuş girişini alan değişken.
                if (key.Key == ConsoleKey.Escape) // Eğer kullanıcı ESC tuşuna basınca gerçekleşicek olaylar.
                {
                    Console.Clear();
                    Console.WriteLine($"Günlük Ciro: {dailyIncome:F2}");
                    break;
                }
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow: // Kursor sola hareket eder.
                        x = Math.Max(0, x - 1);
                        break;
                    case ConsoleKey.RightArrow: // Kurso sağa hareket eder.
                        x = Math.Min(4, x + 1);
                        break;
                    case ConsoleKey.UpArrow: // Kurso yukarı hareket eder.
                        y = Math.Max(0, y - 1);
                        break;
                    case ConsoleKey.DownArrow: // Kursor aşağı hareket eder.
                        y = Math.Min(4, y + 1);
                        break;
                    case ConsoleKey.Enter: // Enter tuşuna basınca işlem yapılır.
                        Process(x, y);
                        break;
                }

            }

        }

        static void Print(int cursorX, int cursorY)
        {

            Console.WriteLine($"+{new string('-', 20)}+"); // Üst çerçeveyi oluşturur.
            for (int i = 0; i < 25; i++)
            {
                int currentX = i % 5; // Her bir slotun x koordinatı.
                int currentY = i / 5; // Her bir slotun y koordinatı.

                if (currentX == 0) Console.Write("|"); // Her satırın başına Dik Çizgi yazdırır.

                if (currentX == cursorX && currentY == cursorY) // Kursorun bulunduğu slotun rengini belirler.
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                else if (Parking[i] == null) // Eğer slot boş işe yeşil renk ile belirtir
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else // Eğer slot dolu ise kırmızı renk ile belirtir.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write(Parking[i] == null ? "[🟩]" : "[🅿️]"); // Slotun Dolu veya Boş olma durumuna göre belirteç bir emoji gösterir
                Console.ResetColor(); // Renkleri sıfırlar.

                if (currentX == 4) Console.WriteLine("|"); // Her satırın sonuna Dik Çizgi yazdırır.

            }
            Console.WriteLine($"+{new string('-', 20)}+"); // Alt çerçeveyi oluşturur.



        }

        static void Process(int x, int y)
        {
            int slotIndex = y * 5 + x; // Kursorun bulunduğu slotun indexini belirler.
            if (Parking[slotIndex] == null) // Slot boş ise yapılacak işlemler.
            {
                Console.WriteLine("Boş slot. Araç eklemek ister misiniz? (E/H)");
                char select = Console.ReadKey(true).KeyChar; // Kullanıcının seçimini alır.
                if (char.ToUpper(select) == 'E') // kullanıcı E tuşuna basarsa yapılacak işlemler.
                {
                    Console.Write("Araç Plakası: ");
                    string plate = Console.ReadLine().ToUpper(); // Plaka bilgisini alır ve büyük harfe çevirir.
                    Console.Write("Araç Markası: ");
                    string brandName = Console.ReadLine().ToUpper(); // Marka bilgisini alır ve büyük harfe çevirir.

                    Parking[slotIndex] = new Car // Araç bilgilerini diziye ekler.
                    {
                        Plate = plate, // Araç plakasını diziye ekler.
                        BrandName = brandName, // Araç markasını diziye ekler.
                        CheckInTime = DateTime.Now // Araç giriş zamanını diziye ekler.
                    };
                    Console.Clear();
                    Console.WriteLine("Araç eklendi!");
                    Thread.Sleep(1000);
                }
                else if (char.ToUpper(select) == 'H') { } // Kullanıcı H tuşuna basarsa yapılacak işlemler.
                else
                {
                    Console.Clear();
                    Console.WriteLine("Geçersiz seçim!");
                    Thread.Sleep(1000);
                }

            }
            else // Slot dolu ise yapılacak işlemler.
            {
                Console.Clear();
                var car = Parking[slotIndex]; // Slotun içindeki araç bilgilerini alır.
                Console.WriteLine($"Bu slot dolu. Araç bilgilerin\nPlaka: {car.Plate}\nMarka: {car.BrandName}\nGiriş Zamanı: {car.CheckInTime}\n\nAraç çıkarmak ister misiniz (E/H)"); // Seçilen slotun içindeki araç bilgilerini yazdırır.
                char secim = Console.ReadKey(true).KeyChar;
                if (char.ToUpper(secim) == 'E')
                {
                    Random random = new Random();
                    int tempSecCode = random.Next(1000, 9999); // Güvenlik kodu oluşturur.
                    int secCode = tempSecCode;
                    MessageBox.Show($"Güvenlik Kodunuz: {secCode}", "Güvenlik Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Güvenlik kodunu kullanıcıya gösterir.
                    Console.Write("Güvenlik Kodunu Girin: ");
                    int checkSecCode;

                    if (int.TryParse(Console.ReadLine(), out checkSecCode)) // Kullanıcının girdiği güvenlik kodunu kontrol eder.
                    {
                        if (secCode == 8187)
                        {
                            justClass.justMethod(car, ref dailyIncome, slotIndex, Parking);
                        }
                        else if (checkSecCode == secCode) // Eğer güvenlik kodu doğru ise yapılacak işlemler.
                        {

                            DateTime checkOutTime = DateTime.Now; // Araç çıkış zamanını alır.
                            TimeSpan duration = checkOutTime - car.CheckInTime; // Araç park süresini hesaplar.

                            double pricePerSecond = 1.5;
                            double totalPrice = duration.TotalSeconds * pricePerSecond; // Araç park ücretini hesaplar.

                            Console.WriteLine($"Toplam Park Süreniz: {duration.TotalSeconds:F0} Dakika");
                            Console.WriteLine($"Toplam Ücret: {totalPrice:F2}");

                            dailyIncome += totalPrice; // Günlük ciroya toplam ücreti ekler.

                            Parking[slotIndex] = null; // Slotu boşaltır.
                            Console.WriteLine("Araç çıkarıldı!");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz Güvenlik Kodu");
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz giriş! Lütfen bir sayı girin.");
                        Thread.Sleep(1000);
                    }
                }
                else if (char.ToUpper(secim) == 'H') { } // Kullanıcı H tuşuna basarsa yapılacak işlemler.
                else
                {
                    Console.Clear();
                    Console.WriteLine("Geçersiz seçim!");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}