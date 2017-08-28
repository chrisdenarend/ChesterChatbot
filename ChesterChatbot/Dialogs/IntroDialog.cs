using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChesterChatbot.Dialogs
{
    [Serializable]
    public class IntroDialog : IDialog<string>
    {
        public async Task StartAsync(IDialogContext context)
        {
            IEnumerable<string> options = new List<string>() { "My first time", "Already made some progress" };
            PromptDialog.Choice(context, OnOptionSelected, options, $"Hi {context.UserData.GetValue<string>("name")}, did you already made some progress or is this your first time? ", promptStyle: PromptStyle.Keyboard);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            context.Done(await result);
        }
    }
}