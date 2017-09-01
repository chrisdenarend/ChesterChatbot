using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ChesterChatbot.Dialogs
{
    /*
     * TODO
     * Show video
     * Validate Question
     */
    [Serializable]
    public class Step1Dialog : IDialog<string>
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
        private IDialogContext CurrentContext;

        /* Working Code */
        public async Task StartAsync (IDialogContext context)
        {
            PromptDialog.Confirm(context, ShowFirstContent, $"Are you ready to start your introduction?", promptStyle: PromptStyle.Keyboard);
        }
        public async Task ShowFirstContent(IDialogContext context, IAwaitable<bool> result)
        {
            IEnumerable<string> options = new List<string>() { "Next" };
            await context.PostAsync(Message_Introduction);
            await context.PostAsync(Message_IntroToMedia1);
            await context.PostAsync("Here The Video Will Be Placed");
            PromptDialog.Confirm(context, ShowSecondContent, $"Are you ready for the next content", promptStyle: PromptStyle.Keyboard);
        }
        public async Task ShowSecondContent(IDialogContext context, IAwaitable<bool> result)
        {
            IEnumerable<string> options = new List<string>() { "Next" };
            await context.PostAsync(Message_IntroToMedia2);
            await context.PostAsync("Here The Video Will Be Placed");
            PromptDialog.Confirm(context, ShowThirdContent, $"Are you ready for the next content", promptStyle: PromptStyle.Keyboard);
        }

        public async Task ShowThirdContent(IDialogContext context, IAwaitable<bool> result)
        {
            IEnumerable<string> options = new List<string>() { "Next" };
            await context.PostAsync(Message_IntroToMedia3);
            await context.PostAsync("Here The Video Will Be Placed");
            PromptDialog.Confirm(context, ShowFourthContent, $"Are you ready for the next content", promptStyle: PromptStyle.Keyboard);
        }

        public async Task ShowFourthContent(IDialogContext context, IAwaitable<bool> result)
        {
            IEnumerable<string> options = new List<string>() { "Next" };
            await context.PostAsync(Message_IntroToMedia4);
            await context.PostAsync("Here The Video Will Be Placed");
            PromptDialog.Confirm(context, ShowFinalQuestion, $"Are you ready for the next content", promptStyle: PromptStyle.Keyboard);
        }

        public async Task ShowFinalQuestion(IDialogContext context, IAwaitable<bool> result)
        {
            IEnumerable<string> options = new List<string>() { "Next" };
            await context.PostAsync(Message_IntroToQuestion);
            PromptDialog.Text(context, FinalQuestionAnswered, Message_Question);
       }
        public async Task FinalQuestionAnswered(IDialogContext context, IAwaitable<string> result)
        {
            string answer = await result;
            if (answer.Contains(Question_Answer))
            {
                await context.PostAsync(Question_Correct);
                context.Done(Question_Correct);
            }
            else
            {
                await context.PostAsync(Question_Wrong);
                context.Done(Question_Wrong);
            }
        }
        
        /* Old practice code */
        //public async Task StartAsync(IDialogContext context)
        //{
        //    //CurrentContext = context;
        //    //GetCurrentUserData();
        //    //if (QuestionAnsweredCorrectly)
        //    //{
        //    //    CurrentContext.PostAsync(Message_End);
        //    //}
        //    //else
        //    //{
        //    //    DetermineStepMessage();
        //    //}

        //    //PromptDialog.Confirm(context, MessageReceivedAsync, $"Are you ready to progress?", promptStyle: PromptStyle.Keyboard);
        //    PromptDialog.Confirm(context, StartMessageReceivedAsync, $"Are you ready to progress?", promptStyle: PromptStyle.Keyboard);
        //}



        //public async Task FirstContentShown(IDialogContext context, IAwaitable<bool> result)
        //{
        //    IEnumerable<string> options = new List<string>() { "Next" };
        //    await context.PostAsync(Message_IntroToMedia2);
        //    await context.PostAsync("Here The Video Will Be Placed");
        //    PromptDialog.Confirm(context, StartMessageReceivedAsync, $"Are you ready for the next step", promptStyle: PromptStyle.Keyboard);
        //}
        //private Task MessageReceivedAsync(IDialogContext context, IAwaitable<bool> result)
        //{
        //    //var message = await result;
        //    CurrentContext = context;
        //    GetCurrentUserData();

        //    if (QuestionAnsweredCorrectly)
        //    {
        //        CurrentContext.PostAsync(Message_End);
        //    }
        //    else
        //    {
        //        DetermineStepMessage();
        //    }
        //    return Task.CompletedTask;
        //    //await MessageReceivedAsync;
        //}
        //private  void DetermineStepMessage()
        //{
        //    ShowStepMessage(new List<string>() { Message_Introduction, Message_IntroToMedia1 },
        //                URL_Media1);
        //    //return Task.CompletedTask;
        //    /*
        //    switch (LastmessageShown)
        //    {
        //        case -1:
        //            await ShowStepMessage(new List<string>() { Message_Introduction, Message_IntroToMedia1 },
        //                URL_Media1);
        //            break;
        //        case 0:
        //            ShowStepMessage(new List<string>() { Message_IntroToMedia2 },
        //                URL_Media2);
        //            break;
        //        case 1:
        //            ShowStepMessage(new List<string>() { Message_IntroToMedia3 },
        //                URL_Media3);
        //            break;
        //        case 2:
        //            ShowStepMessage(new List<string>() { Message_IntroToMedia4 },
        //                URL_Media4);
        //            break;
        //        case 3:
        //            ShowStepQuestion(new List<string>() { Message_IntroToQuestion, Message_Question });
        //            break;
        //    }
        //    //return Task.CompletedTask;
            
        //}
        //private void ShowStepMessage(List<string> message_Text, string url_Media)
        //{
        //    IEnumerable<string> options = new List<string>() { "Next" };
        //    /* Show Messages */
        //    foreach (string message in message_Text)
        //    {
        //       CurrentContext.PostAsync(message);
        //    }

        //    /* Show Media */
        //    CurrentContext.PostAsync("This is where the video will be shown");
        //    //CurrentContext.Wait(MessageReceivedAsync);
        //    //await MessageReceivedAsync(CurrentContext);
        //    //await MessageReceivedAsync(CurrentContext);
        //    PromptDialog.Confirm(CurrentContext, MessageReceivedAsync, $"Are you ready for the next step", promptStyle: PromptStyle.Keyboard);
        //    //PromptDialog.Choice(CurrentContext, OnClickedNext, options, $"Press next when you watched the video", promptStyle: PromptStyle.Keyboard);
            
        //    //return Task.CompletedTask;
        //    //await this.MessageReceivedAsync();
        //    //CurrentContext.Wait(MessageReceivedAsync);
        //}
        //private Task ShowStepQuestion(List<string> message_Text)
        //{
        //    /* Show Question */
        //    foreach (string message in message_Text)
        //    {
        //        CurrentContext.PostAsync(message);
        //    }
            
        //    return Task.CompletedTask;
        //}
        //private async Task OnClickedNext(IDialogContext context, IAwaitable<string> result)
        //{
        //    //SaveCurrentUserData();
        //    //await Task.Run(DetermineStepMessage);
        //    //return Task.CompletedTask
        //    context.Done(await result);
        //}
        //private Task OnOptionSelected_Temp()
        //{
        //    SaveCurrentUserData(true);
        //    return Task.CompletedTask;
        //}
        //private void GetCurrentUserData()
        //{
        //    /* To Be Replaced When implementing State Management*/
        //    int? _lastmessageShown;
        //    Boolean? _questionAnsweredCorrectly;

        //    CurrentContext.UserData.TryGetValue(UserData_LastMessageShown, out _lastmessageShown);
        //    CurrentContext.UserData.TryGetValue(UserData_QuestionAnsweredCorrectly, out _questionAnsweredCorrectly);

        //    QuestionAnsweredCorrectly = _questionAnsweredCorrectly.HasValue ? _questionAnsweredCorrectly.Value : false;
        //    LastmessageShown = _lastmessageShown.HasValue ? _lastmessageShown.Value : -1;
        //}

        //private void SaveCurrentUserData(Boolean questionAnsweredCorrectly =false)
        //{
        //    /* To Be Replaced When implementing State Management*/
        //    int _lastmessageShown = LastmessageShown >= 3 ? -1 : LastmessageShown + 1;
        //    Boolean _questionAnsweredCorrectly = false;

        //    CurrentContext.UserData.SetValue<int>(UserData_LastMessageShown, _lastmessageShown);
        //    CurrentContext.UserData.SetValue<Boolean>(UserData_QuestionAnsweredCorrectly, _questionAnsweredCorrectly);

        //    QuestionAnsweredCorrectly = _questionAnsweredCorrectly;
        //    LastmessageShown = _lastmessageShown;
        //}
    }
}