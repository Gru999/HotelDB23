using HotelDB23.Interfaces;
using HotelDB23.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDB23.Services
{
    public class RoomService : Connection, IRoomService {
        private string listRooms = "select * from Room where Hotel_No = @hotelNr";
        //implement Room_No check so two rooms can't have the same room nr.
        private string createRoom = "if exists (select * from Room where Hotel_No = @hotelNr) " +
                                    "begin insert into Room (Room_No, Hotel_No, Types, Price) values (@roomNr, @hotelNr, @type, @price); " +
                                    "end";
        private string deleteRoom = "if exists (select * from Room where Hotel_No = @hotelNr and Room_No = @roomNr)" +
                                    "begin delete from Booking where Hotel_No = @hotelNr and Room_No = @roomNr" +
                                          "delete from Room where Hotel_No = @hotelNr and Room_No = @roomNr" +
                                    "end";

        
        public List<Room> GetAllRoom(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(listRooms, connection);
                    command.Parameters.AddWithValue("@hotelNr", hotelNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int roomNo = reader.GetInt32(0);
                        int hotelNo = reader.GetInt32(1);
                        //problem with getchar
                        char type = reader.GetString(2).First();
                        double price = reader.GetDouble(3);
                        Room r = new Room(roomNo, hotelNo, type, price);
                        rooms.Add(r);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
            }
            return rooms;
        }

        public Room GetRoomFromId(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public bool CreateRoom(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(createRoom, connection);
                    command.Parameters.AddWithValue("@roomNr", room.RoomNr);
                    command.Parameters.AddWithValue("@hotelNr", room.HotelNr);
                    command.Parameters.AddWithValue("@type", room.Types);
                    command.Parameters.AddWithValue("@price", room.Pris);
                    command.Connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
            }
            return false;
        }

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Room DeleteRoom(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteRoom, connection);
                    command.Parameters.AddWithValue("@roomNr", roomNr);
                    command.Parameters.AddWithValue("@hotelNr", hotelNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return GetRoomFromId(hotelNr);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
            }
            return null;
        }
    }
}
