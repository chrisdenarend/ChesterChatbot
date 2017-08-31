using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ChesterChatbot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"Hi, I'm Chester and I'll support your ambition to learn how to build a chatbot!");

            context.Call(new NameDialog(), NameDialogResumeAfter);
        }

        private async Task NameDialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            var name = result.GetAwaiter().GetResult();
            context.UserData.SetValue("name", name);

            context.Call(new IntroDialog(), IntroDialogResumeAfter);
        }

        private async Task IntroDialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                switch (result.GetAwaiter().GetResult())
                {
                    case "My first time":
                        context.Call(new Step1Dialog(), StepDialogResumeAfter);
                        break;
                    case "Already made some progress":
                        context.Call(new IntroDialog(), IntroDialogResumeAfter);
                        break;
                    default:
                        context.Call(new IntroDialog(), IntroDialogResumeAfter);
                        break;
                }
            }
            catch (TooManyAttemptsException)
            {
                //await context.PostAsync("Ik begrijp je helaas niet. Laten we het opnieuw proberen.");
                await context.PostAsync("I don't understand, let's try again.");
                await StartAsync(context);
            }
        }

        private async Task StepDialogResumeAfter(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new IntroDialog(), IntroDialogResumeAfter);
        }
    }
}