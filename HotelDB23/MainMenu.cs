using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDB23.Services;
using HotelDB23.Models;

namespace HotelDB23
{
    public static class MainMenu
    {
        //Lav selv flere menupunkter til at vælge funktioner for Rooms, bookings m.m.
        public static void showOptions()
        {
            Console.Clear();
            Console.WriteLine("\tVælg et menupunkt\n");

            Console.WriteLine("\t1)\t List hoteller");
            Console.WriteLine("\t1a)\t List hoteller async");
            Console.WriteLine("\t2)\t Opret nyt Hotel");
            Console.WriteLine("\t3)\t Fjern Hotel");
            Console.WriteLine("\t4)\t Søg efter hotel udfra hotelnr");
            Console.WriteLine("\t5)\t Opdater et hotel");
            Console.WriteLine("\t6)\t List hoteller udfra navn");

            Console.WriteLine("\t7)\t List alle værelser til et bestemt hotel");
            Console.WriteLine("\t8)\t List et bestemt værelser til et bestemt hotel");
            Console.WriteLine("\t9)\t Opret nyt værelse");

            Console.WriteLine("\tQ)\t Afslut");
            Console.Write("\tIndtast valg: ");
        }

        public static bool Menu()
        {
            showOptions();
            switch (Console.ReadLine())
            {
                case "1":
                    ShowHotels();
                    return true;
                //case "1a":
                //    ShowHotelsAsync();
                //    DoSomething();
                //    return true;
                case "2":
                    CreateHotel();
                    return true;
                case "3":
                    DeleteHotel();
                    return true;
                case "4":
                    GetHotel();
                    return true;
                case "5":
                    UpdateHotel();
                    return true;
                case "6":
                    GetHotelByName();
                    return true;
                case "7":
                    GetRoom();
                    return true;
                case "8":
                    GetRoomById();
                    return true;
                case "9":
                    CreateRoom();
                    return true;
                case "Q":
                case "q": return false;
                default: return true;
            }

        }
        private static void ShowHotels()
        {
            Console.Clear();
            HotelService hs = new HotelService();
            List<Hotel> hotels = hs.GetAllHotel();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.Navn} Address {hotel.Adresse}");
            }
            Console.ReadKey();
        }

        private static void CreateHotel()
        {
            //Indlæs data
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs hotelnavn");
            string navn = Console.ReadLine();
            Console.WriteLine("Indlæs hotel adresse");
            string adresse = Console.ReadLine();

            //Kald hotelservice og vis resultatet
            HotelService hs = new HotelService();
            bool ok = hs.CreateHotel(new Hotel(hotelnr, navn, adresse));
            if (ok)
            {
                Console.WriteLine("Hotellet blev oprettet!");
            }
            else
            {
                Console.WriteLine("Fejl. Hotellet blev ikke oprettet!");
            }
        }

        private static void DeleteHotel()
        {
            HotelService hs = new HotelService();
            Console.Clear();
            Console.WriteLine("Indlæs hotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());

            Hotel deletedHotel = hs.GetHotelFromId(hotelNr);
            hs.DeleteHotel(hotelNr);
            if (deletedHotel != null)
            {
                Console.WriteLine("Fjernet: " + deletedHotel.ToString());
            }
            else
            {
                Console.WriteLine("Hotellet findes ikke");
            }
            Console.ReadKey();
        }

        private static void GetHotel()
        {
            Console.WriteLine("Indtast hotel nummer som du ønsker at finde:");
            int hotelNo = int.Parse(Console.ReadLine());
            HotelService hs = new HotelService();
            Hotel foundHotel = hs.GetHotelFromId(hotelNo);
            if (foundHotel != null)
            {
                Console.WriteLine($"Hotel fundet: {foundHotel.ToString()}");
            }
            else
            {
                Console.WriteLine("Hotellet findes ikke");
            }
            Console.ReadKey();
        }

        private static void UpdateHotel()
        {
            Console.WriteLine("Indtast hotel nummer som du ønsker at opdatere:");
            int hotelNo = int.Parse(Console.ReadLine());

            Console.WriteLine("Indtast nummeret på det nye hotel");
            int opHotelNo = int.Parse(Console.ReadLine());

            Console.WriteLine("Indtast navnet på det nye hotel");
            string hotelNavn = Console.ReadLine();

            Console.WriteLine("Indtast adressen på det nye hotel");
            string hotelAdr = Console.ReadLine();

            HotelService hs = new HotelService();
            int hotelNr = hotelNo;
            bool ok = hs.UpdateHotel(new Hotel(opHotelNo, hotelNavn, hotelAdr), hotelNr);
            if (ok)
            {
                Console.WriteLine("Hotellet blev opdateret!");
            }
            else
            {
                Console.WriteLine("Fejl. Hotellet blev ikke opdateret!");
            }

        }

        private static void GetHotelByName()
        {
            Console.Clear();
            Console.WriteLine("Indtast navn på hotel:");
            string name = Console.ReadLine();
            HotelService hs = new HotelService();
            List<Hotel> foundHotels = hs.GetHotelsByName(name);

            //if statement tjek
            Console.ReadKey();
        }

        private static void GetRoom()
        {
            Console.Clear();
            Console.WriteLine("Indtast hotel nummert for at finde værelserne");
            int hotelNo = int.Parse(Console.ReadLine());
            RoomService rs = new RoomService();
            List<Room> rooms = rs.GetAllRoom(hotelNo);
            foreach (Room room in rooms)
            {
                Console.WriteLine(room.ToString());
            }
            Console.ReadKey();
        }

        private static void GetRoomById() {
        
        }

        private static void CreateRoom() {
            Console.Clear();
            Console.WriteLine("Indtast værelse nummer");
            int roomnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indtast hotel nummer");
            int hotelnr = int.Parse(Console.ReadLine());
            Console.WriteLine("Indtast værelsets type");
            char type = Console.ReadLine().First();
            Console.WriteLine("Indtast pris på værelset");
            double pris = double.Parse(Console.ReadLine());

            
            RoomService rs = new RoomService();
            bool ok = rs.CreateRoom(hotelnr, new Room(roomnr, hotelnr, type, pris));
            if (ok)
            {
                Console.WriteLine("værelset blev oprettet!");
            }
            else
            {
                Console.WriteLine("Fejl. værelset blev ikke oprettet!");
            }
            Console.ReadKey();
        }

        


        private async static Task ShowHotelsAsync()
        {
            //Console.Clear();
            //HotelServiceAsync hs = new HotelServiceAsync();
            //List<Hotel> hotels = await hs.GetAllHotelAsync();
            //foreach (Hotel hotel in hotels)
            //{
            //    Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.HotelNr} Address {hotel.Adresse}");
            //}
        }

        private static void DoSomething()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i + " i GUI i main thread");
            }
        }
    }
}
