using DSharpPlus;
using ShikimoriSharp;
using Microsoft.Extensions.DependencyInjection;
using Andromeda.Bot.Modules;
using Andromeda.Bot.Settings;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShikimoriSharp.Bases;

var environment = Environment.GetEnvironmentVariable("ANDROMEDA_ENVIRONMENT")!;
var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true)
    .Build();
var discordOptions = configuration.GetSection("Discord").Get<DiscordOptions>()!;
var shikimoriOptions = configuration.GetSection("Shikimori").Get<ShikimoriOptions>()!;

var serviceProvider = new ServiceCollection()
    .AddSingleton(new DiscordConfiguration
    {
        TokenType = TokenType.Bot,
        Token = discordOptions.BotToken,
        AutoReconnect = true,
        MinimumLogLevel = LogLevel.Debug,
        
    })
    .AddSingleton<DiscordClient>()
    .AddSingleton<ShikimoriClient>(x => new ShikimoriClient(null, 
        new ClientSettings(shikimoriOptions.ClientName, shikimoriOptions.ClientId, shikimoriOptions.ClientSecret)))
    .BuildServiceProvider();

var discordClient = serviceProvider.GetRequiredService<DiscordClient>();

var slash = discordClient.UseSlashCommands(new SlashCommandsConfiguration {Services = serviceProvider});

slash.SlashCommandErrored += (sender, eventArgs) =>
{
    Console.WriteLine(eventArgs.Exception);
    
    return Task.CompletedTask;
};

slash.RegisterCommands<ShikimoriModule>();

await discordClient.ConnectAsync();

await Task.Delay(Timeout.Infinite);