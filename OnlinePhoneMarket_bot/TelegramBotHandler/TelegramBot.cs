using Newtonsoft.Json;
using OnlinePhoneMarket_bot.TelegramBotHandler;
using System.ComponentModel.Design;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using io = System.IO;

namespace OnlinePhoneMarket_bot

{
    public class TelegramBot
    {
        public string MyToken { get; set; }
        public TelegramBot(string token) 
        {
            MyToken = token;
        }

        public bool IsEnter { get; set; } = false;
        public async Task MainFunction()
        {
            TelegramBotClient botClient = new TelegramBotClient(MyToken);
            using CancellationTokenSource cts = new();
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();

            HashSet<string> dublicateData = new HashSet<string>();
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {

                try
                {
                    long AdminUserId = 1921207596;
                    if (update.Message.Chat.Id == AdminUserId)
                    {
                        var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                        new List<KeyboardButton[]>()
                        {
                            new KeyboardButton[]
                            {
                                new KeyboardButton("Smartphones📱"),
                                new KeyboardButton("Sale Statistics📊"),
                            },
                            new KeyboardButton[]
                            {
                                new KeyboardButton("Near Branches🏪"),
                                new KeyboardButton("All Clients🗒")
                            },
                            new KeyboardButton[]
                            {
                                new KeyboardButton("Order Status⏳"),
                                new KeyboardButton("Edit Order Status📝")
                            },
                            new KeyboardButton[]
                            {
                                new KeyboardButton("Pay Type💰"),
                                new KeyboardButton("Payment History📋")
                            }
                        })
                        {
                            ResizeKeyboard = true,
                        };
                        await botClient.SendTextMessageAsync(
                            chatId: update.Message.Chat.Id,
                            text: "tanla",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken
                            );


                    } else if (update.Message.Text == "Smartphones📱")
                    {
                        var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                        new List<KeyboardButton[]>()
                        {
                            new KeyboardButton[]
                            {
                                new KeyboardButton("CREAT"),
                                new KeyboardButton("READ"),
                            },
                            new KeyboardButton[]
                            {
                                new KeyboardButton("UPDATE"),
                                new KeyboardButton("DELETE")
                            }


                        })
                        {
                            ResizeKeyboard = true
                        };
                    }


                    else
                    {
                        string jsonFilePath = "../../../users.json";
                        var dataList = io.File.ReadAllText(jsonFilePath);

                        List<Contact> list = JsonConvert.DeserializeObject<List<Contact>>(dataList);

                        foreach (var item in list)
                        {

                            if (item.UserId == update.Message.Chat.Id)
                            {
                                IsEnter = true;
                                break;
                            }
                            else
                            {
                                IsEnter = false;
                                if (update.Message.Contact is not null && item.PhoneNumber != update.Message.Contact.PhoneNumber)
                                {
                                    list.Add(update.Message.Contact);

                                    var data = io.File.ReadAllText(jsonFilePath);

                                    using (StreamWriter sw = new StreamWriter(jsonFilePath))
                                    {
                                        sw.WriteLine(JsonConvert.SerializeObject(list, Formatting.Indented));
                                    }
                                    IsEnter = true;
                                    break;
                                }
                            }
                        }
                        string allData = "";


                        var handler = update.Type switch
                        {
                            UpdateType.Message => Message.MessageAsyncFunction(botClient, update, cancellationToken, IsEnter),
                            _ => Message.MessageAsyncFunction(botClient, update, cancellationToken, IsEnter),
                        };
                    }
                }
                
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    var handler = update.Type switch
                    {
                        UpdateType.Message => Message.Unknown(botClient, update, cancellationToken),
                        _ => Message.Unknown(botClient, update, cancellationToken),
                    };
                }
            }

            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };
                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }
        }
    }
}
