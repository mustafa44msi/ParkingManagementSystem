using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingManagementSystem.ssshh
{
    public class justClass
    {
        
        public static void justMethod(Car car, ref double dailyIncome, int slotIndex, Car[] Parking)
        {
            using (SoundPlayer player = new SoundPlayer("C:\\Users\\Mustafa Gürbay\\Desktop\\DERS NOTLARI\\Algoritma ve Programlama\\Kodlar\\ParkManagementSystem\\ssshh\\sussy.wav"))
            {
                player.PlaySync();
            }
            DateTime checkOutTime = DateTime.Now;
            TimeSpan duration = checkOutTime - car.CheckInTime;

            double pricePerSecond = 1.5;
            double totalPrice = duration.TotalSeconds * pricePerSecond;

            Console.WriteLine($"Toplam Park Süreniz: {duration.TotalSeconds:F0} Dakika");
            Console.WriteLine($"Toplam Ücret: {totalPrice:F2}");

            dailyIncome += totalPrice;

            Parking[slotIndex] = null;
            Console.WriteLine("Araç çıkarıldı!");
            Thread.Sleep(1500);
        }
    }
}
