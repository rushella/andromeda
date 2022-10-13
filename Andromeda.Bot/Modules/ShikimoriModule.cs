using DSharpPlus.SlashCommands;

namespace Andromeda.Bot.Modules;

public class ShikimoriModule : ApplicationCommandModule
{
    [SlashCommand("sl", "link")]
    public async Task ShikimoriLinkAsync(InteractionContext ctx)
    {
        throw new NotImplementedException();
    }
}