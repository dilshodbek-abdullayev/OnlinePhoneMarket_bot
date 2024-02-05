using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlinePhoneMarket_bot.TelegramBotHandler
{
    public class Phones
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public long price { get; set; }

        public static string path = @"C:\Users\ACER\Desktop\GitHub\TelegramBot\OnlinePhoneMarket\OnlinePhoneMarket_bot\OnlinePhoneMarket_bot\Phone.json";

        public static void Create(Phones bk)
        {
            List<Phones> Phones = DeserializeSerialize<Phones>.GetAll(path);
            if (Phones.Any(c => c.Model == bk.Model))
            {
                return;
            }
            Phones.Add(bk);
            DeserializeSerialize<Phones>.Save(Phones, path);
        }
        public static string Read()
        {
            StringBuilder builder = new StringBuilder();
            List<Phones> Phones = DeserializeSerialize<Phones>.GetAll(path);
            foreach (Phones c in Phones)
            {
                builder.Append($"Phones Model:{c.Model}\n" +
                    $"Phones Brand: {c.Brand}\n" +
                    $"Phones price: {c.price}\n" + "\n---------------------" + "\n");
            }
            return builder.ToString();
        }

        public static string GetRead(Phones c)
        {
            return ($"Phone Model:{c.Model}\n" +
                     $"Phone Brand: {c.Brand}\n" +
                     $"Phone price: {c.price}\n" + "\n----------------------" + "\n");

        }

        public static void Update(string last_Model, string new_price, string new_Model, string new_Brand)
        {
            try
            {
                List<Phones> Phones = DeserializeSerialize<Phones>.GetAll(path);
                if (Phones != null)
                {
                    int index = Phones.FindIndex(Model => Model.Model == last_Model);
                    if (index != -1)
                    {
                        Phones[index].Model = new_Model;
                        Phones[index].Brand = new_Brand;
                        Phones[index].price = Convert.ToUInt16(new_price);
                       
                        DeserializeSerialize<Phones>.Save(Phones, path);
                    }
                }
            }
            catch { }
        }

        public static void Delete(string del_Model)
        {
            try
            {
                List<Phones> Phones = DeserializeSerialize<Phones>.GetAll(path);
                var catchToRemove = Phones.Find(ct => ct.Model == del_Model);

                if (catchToRemove != null)
                {
                    Phones.Remove(catchToRemove);
                    DeserializeSerialize<Phones>.Save(Phones, path);
                }
            }
            catch { }
        }
    }
}
