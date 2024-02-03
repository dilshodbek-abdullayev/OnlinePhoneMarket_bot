﻿using OnlinePhoneMarket_bot.TelegramBotHandler;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace OnlinePhoneMarket_bot
{
    public class Message
    {
        AdminClass admin = new AdminClass();
        public static async Task MessageAsyncFunction(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken, bool isEnter)
        {
            var message = update.Message;
            Console.WriteLine($"User Name: {message.Chat.Username}\nYou said: {message.Text}\nData: {DateTime.Now}\n");
            if (isEnter == true)
            {
                var handler = message.Type switch
                {
                    MessageType.Text => TextAsyncFunction(botClient, update, cancellationToken),
                    MessageType.Contact => ContactAsyncFunction(botClient, update, cancellationToken),
                    _ => OtherAsyncFunctiob(botClient, update, cancellationToken)
                };
            }
            else
            {
                Contact(botClient, update, isEnter).Wait();
            }
        }

        static async Task ContactAsyncFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            await botClient.SendTextMessageAsync
            (
                chatId: message.Chat.Id,
                text: $"Xush kelibsiz {message.Chat.FirstName}!",
                replyToMessageId: message.MessageId,
                replyMarkup: new ReplyKeyboardRemove()
            );
        }

        static async Task Contact(ITelegramBotClient botClient, Update update, bool isEnter)
        {
            Console.WriteLine(isEnter);
            ReplyKeyboardMarkup markup = new ReplyKeyboardMarkup
            (
                KeyboardButton.WithRequestContact("Contakt yuborish")
            );

            markup.ResizeKeyboard = true;
            await botClient.SendTextMessageAsync
            (
                    chatId: update.Message.Chat.Id,
                    text: "Oldin Contakt yuboring!",
                    replyMarkup: markup
            );

        }
        static async Task TextAsyncFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            await botClient.SendTextMessageAsync
            (
                chatId: message.Chat.Id,
                replyToMessageId: message.MessageId,
                text: "Iltimos biror bo'lim tanlang",
                cancellationToken: cancellationToken
            ) ;
        }
        static async Task OtherAsyncFunctiob(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                replyToMessageId: message.MessageId,
                text: "Nimadir xato ketdi.",
                cancellationToken: cancellationToken);
        }

        public static async Task Unknown(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                replyToMessageId: message.MessageId,
                text: "",
                cancellationToken: cancellationToken);
        }
    }
}
