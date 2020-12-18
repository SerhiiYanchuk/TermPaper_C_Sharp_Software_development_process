using HotelAppWPF.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HotelAppWPF.Commands
{
    //static public class AddHotelCommand
    //{
    //    static public void AddHotel(object obj)
    //    {
    //        HotelModel hotel = obj as HotelModel;
    //        if (hotel is null)
    //            return;
    //        using (var client = new HttpClient())
    //        {
    //            var response = client.PostAsJsonAsync("https://localhost:44364/api/hotels", hotel).Result;
    //            var result = response.Content.ReadAsStringAsync().Result;
    //        }
    //    }
    //    static public bool CheckHotel(object obj)
    //    {
    //        HotelModel hotel = obj as HotelModel;
    //        if (hotel is null)
    //            return false;
    //        return hotel.IsValid;
    //    }
    //}
}
