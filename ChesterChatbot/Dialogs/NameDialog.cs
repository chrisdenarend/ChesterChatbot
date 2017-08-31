using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace ChesterChatbot.Dialogs
{
    [Serializable]
    public class NameDialog : IDialog<string>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("First things first, can you please tell me your name?");

            context.Wait(this.MessageReceivedAsync);
        }


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var name = await result;
            context.Done(name.Text);
        }
    }
}