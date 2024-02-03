using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
namespace OnlinePhoneMarket_bot.TelegramBotHandler
{
    public class AdminClass
    {
       List<Phone> phones = new List<Phone>();

        public async Task AdminMethod(ITelegramBotClient botClient,Update update,CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup replyKeyboard = new(new[]
            {
                new KeyboardButton[] {"Phone"},
                new KeyboardButton[] {"Delivered office"},
                new KeyboardButton[] {"OrederStatus"},
                new KeyboardButton[] {"PayType"}
             })
            {
                ResizeKeyboard = true
            };
            if (update.Message.Text == "Phone")
            {

                  await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat,
                    text: "Delete Phone Button",
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
             
            }
           
            
        }
        private static async Task PhoneCRUD(long chatId) 
        {
            ReplyKeyboardMarkup replyKeyboard = new(new[]
              {
                new KeyboardButton[] {"Cread"},
                new KeyboardButton[] {"Read"},
                new KeyboardButton[] {"Update"},
                new KeyboardButton[] {"Delete"}
             })
            {
                ResizeKeyboard = true
            };
        }

       

        private void PhoneDelete()
        {
           

        }

        private void PhoneUpdate()
        {
            throw new NotImplementedException();
        }

        private void PhoneRead()
        {
            throw new NotImplementedException();
        }

        public void PhoneCreat()
        {
            ReplyKeyboardMarkup replyKeyboard = new(new[]
            {
                new KeyboardButton[] {"Brand"},
                new KeyboardButton[] {"Model"},
                new KeyboardButton[] {"Price"}

            }
            )
            {
                ResizeKeyboard = true
            };

        }

    }
    class Phone
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public string Delivered_office { get; set; }
    }
}
