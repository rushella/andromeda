using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using ShikimoriSharp;

namespace Andromeda.Bot.Modules;

public class ShikimoriModule : ApplicationCommandModule
{
    private readonly ShikimoriClient _shikimoriClient;
    public ShikimoriModule(IServiceProvider services)
    {
        _shikimoriClient = services.GetService<ShikimoriClient>()!;
    }
    
    [SlashCommand("su", "shikimori info")]
    public async Task ShikimoriInfo(InteractionContext ctx, 
        [Option("user", "shikimori username")]string username)
    {
        await ctx.Interaction.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        
        var interaction = new DiscordInteractionResponseBuilder();
        var embed = new DiscordEmbedBuilder();
        
        var user = await _shikimoriClient.Users.GetUser(username);
        
        if (user == null)
        {
            
        }
        else
        {
            var animeCompleted = user.Stats.Statuses.Anime.FirstOrDefault(x=>x.Name=="completed")!.Size;
            var mangaCompleted = user.Stats.Statuses.Manga.FirstOrDefault(x=>x.Name=="completed")!.Size;

            embed.WithTitle(user.Name);
            embed.AddField("Anime Completed", animeCompleted.ToString());
            embed.AddField("Manga Completed", mangaCompleted.ToString());

            interaction.AddEmbed(embed);
        }

        var response = new DiscordWebhookBuilder();

        response.AddEmbed(embed);
        await ctx.EditResponseAsync(response);

    }
    
    [SlashCommand("sl", "link")]
    public async Task ShikimoriLinkAsync(InteractionContext ctx)
    {
        throw new NotImplementedException();
    }
}