using CoronavirusBrasil.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace CoronavirusBrasil
{
    class Program
    {
        private static IConfiguration config;
        private static TelegramBotClient botClient;
        private static string BOT_KEY;
        private static string COVID_API_KEY;

        static void Main(string[] args)
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            BOT_KEY = config["BotTelegramKey"];
            COVID_API_KEY = config["CovidApiKey"];

            botClient = new TelegramBotClient(BOT_KEY);
            botClient.OnMessage += CovidBrasilBotMessage;
            botClient.StartReceiving();
            Console.ReadLine();
            botClient.StopReceiving();
        }

        private static void CovidBrasilBotMessage(object sender, MessageEventArgs eventArgs)
        {
            SendMessage(eventArgs);
        }

        public static async void SendMessage(MessageEventArgs eventArgs)
        {
            var service = new CoronavirusService(COVID_API_KEY);
            Response brazilStats = await service.GetBrazilStats();

            _ = botClient.SendTextMessageAsync(eventArgs.Message.Chat.Id, $"Olá, {eventArgs.Message.Chat.FirstName}!"
                + Environment.NewLine
                + Environment.NewLine
                + "Abaixo encontra-se as estatísticas atualizadas do Coronavírus no Brasil:"
                + Environment.NewLine
                + Environment.NewLine
                + brazilStats.ToString());
            Thread.Sleep(1500);
            _ = botClient.SendTextMessageAsync(eventArgs.Message.Chat.Id, "Para mais informações acesse: https://covid.saude.gov.br/");
        }
    }
}
