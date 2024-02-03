using OnlinePhoneMarket_bot;


string MyToken = "6604524051:AAGsfKN-6lkfhkze9GSxb8cGdaFaQKRG8ts";
try
{
    TelegramBot bot = new TelegramBot(MyToken);
    await bot.MainFunction();
}
catch (NullReferenceException)
{
    throw new Exception();
}
catch (Exception)
{
    throw new Exception();
}

//MyConvert convert = new MyConvert();
//try
//{
//    convert.ReplayMessage().Wait();
//}
//catch (Exception)
//{
//    throw new Exception("Vay xatolik otdi.");
//}