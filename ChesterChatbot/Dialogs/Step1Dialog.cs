using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;

namespace ChesterChatbot.Dialogs
{
    /*
     * TODO
     * Show video
     * Validate Question
     */
    [Serializable]
    public class Step1Dialog : IDialog<object>
    {
        /*
         * Conversation constants
         * In a later iteration this can be moved to the BusinessLayer
         */
        private const string Message_Introduction = "Lets start off with an introduction to chatbots";
        private const string Message_IntroToMedia1 = "Watch this clip to understand the concept of chatbots";
        private const string URL_Media1 = "https://www.youtube.com/watch?v=5yr1r1YlGVo";
        private const string Message_IntroToMedia2 = "The following clip is an introduction to Microsoft Bot Framework";
        private const string URL_Media2 = "https://mva.microsoft.com/en-US/training-courses/creating-bots-in-the-microsoft-bot-framework-using-c-17590?l=ALwJe9kqD_4000115881";
        private const string Message_IntroToMedia3 = "Next is how to create a simple bot";
        private const string URL_Media3 = "https://mva.microsoft.com/en-US/training-courses/creating-bots-with-the-microsoft-bot-framework-part-1-17756";
        private const string Message_IntroToMedia4 = "And now part 2";
        private const string URL_Media4 = "https://mva.microsoft.com/en-US/training-courses/creating-bots-with-the-microsoft-bot-framework-part-2-17757";
        private const string Message_IntroToQuestion = "Now let's see what you learned";
        private const string Message_Question = "How does a bot communicate with the messaging platform?";
        private const string Question_Answer = "Connector";
        private const string Question_Wrong = "Pity, your answer was wrong. Try again";
        private const string Question_Correct = "Yeah, your answer was pretty outstanding correct.";
        private const string Message_End = "You have successfully ended this part of the course";

        /* 
         * Step Constants
         * Reusable in other steps
         */
        private const string UserData_LastMessageShown = "lastMessageShownStep1";
        private const string UserData_QuestionAnsweredCorrectly = "QuestionAnsweredCorrectlyStep1";

        /* 
         * Private variables
         */
        private Boolean QuestionAnsweredCorrectly = false;
        private int LastmessageShown = -1;

        public Task StartAsync(IDialogContext context)
        {
            GetCurrentUserData(context);

            if (QuestionAnsweredCorrectly)
            {
                context.PostAsync(Message_End);
            }
            else
            {
                ShowStepMessage(context);
            }
            return Task.CompletedTask;
        }

        private Task ShowStepMessage(IDialogContext context)
        {
            IEnumerable<string> options = new List<string>() { "Next" };
            
            switch (LastmessageShown)
            {
                case -1:
                    context.PostAsync(Message_Introduction);
                    context.PostAsync(Message_IntroToMedia1);
                    context.PostAsync("This is where the video will be shown");
                    PromptDialog.Choice(context, OnOptionSelected, options, $"Press next when you are done watching the video", promptStyle: PromptStyle.Keyboard);
                    break;
                case 0:
                    context.PostAsync(Message_IntroToMedia2);
                    context.PostAsync("This is where the video will be shown");
                    PromptDialog.Choice(context, OnOptionSelected, options, $"Press next when you are done watching the video", promptStyle: PromptStyle.Keyboard);
                    break;
                case 1:
                    context.PostAsync(Message_IntroToMedia3);
                    context.PostAsync("This is where the video will be shown");
                    PromptDialog.Choice(context, OnOptionSelected, options, $"Press next when you are done watching the video", promptStyle: PromptStyle.Keyboard);
                    break;
                case 2:
                    context.PostAsync(Message_IntroToMedia4);
                    context.PostAsync("This is where the video will be shown");
                    PromptDialog.Choice(context, OnOptionSelected, options, $"Press next when you are done watching the video", promptStyle: PromptStyle.Keyboard);
                    break;
                case 3:
                    context.PostAsync(Message_IntroToQuestion);
                    context.PostAsync(Message_Question);
                    PromptDialog.Choice(context, OnOptionSelected_Temp, options, $"Press next when you gave the right answer", promptStyle: PromptStyle.Keyboard);
                    break;
            }
            return Task.CompletedTask;
        }
        private Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            SaveCurrentUserData(context);
            ShowStepMessage(context);
            return Task.CompletedTask;
        }
        private async Task OnOptionSelected_Temp(IDialogContext context, IAwaitable<string> result)
        {
            SaveCurrentUserData(context, true);
            context.Done(await result);
        }
        private void GetCurrentUserData(IDialogContext context)
        {
            /* To Be Replaced When implementing State Management*/
            int? _lastmessageShown;
            Boolean? _questionAnsweredCorrectly;

            context.UserData.TryGetValue(UserData_LastMessageShown, out _lastmessageShown);
            context.UserData.TryGetValue(UserData_QuestionAnsweredCorrectly, out _questionAnsweredCorrectly);

            QuestionAnsweredCorrectly = _questionAnsweredCorrectly.HasValue ? _questionAnsweredCorrectly.Value : false;
            LastmessageShown = _lastmessageShown.HasValue ? _lastmessageShown.Value : -1;
        }

        private void SaveCurrentUserData(IDialogContext context, Boolean questionAnsweredCorrectly =false)
        {
            /* To Be Replaced When implementing State Management*/
            int _lastmessageShown = LastmessageShown >= 3 ? 0 : LastmessageShown + 1;
            Boolean _questionAnsweredCorrectly = false;

            context.UserData.SetValue<int>(UserData_LastMessageShown, _lastmessageShown);
            context.UserData.SetValue<Boolean>(UserData_QuestionAnsweredCorrectly, _questionAnsweredCorrectly);

            QuestionAnsweredCorrectly = _questionAnsweredCorrectly;
            LastmessageShown = _lastmessageShown;
        }
    }
}