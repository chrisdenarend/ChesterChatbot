using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using ChesterChatbot.BussinesLayer;

namespace ChesterChatbot.Dialogs
{
    /* NOT WORKING */ 
    /* 
     * Refactored version of Step 1
     * Less Code
     * Less Complex
     */
     [Serializable]
    public class Step1DialogRefactored : IDialog<string>
    {
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

        public async Task StartAsync(IDialogContext context)
        {
            PromptDialog.Confirm(context, DetermineStepMessage, $"Are you ready to start your introduction?", promptStyle: PromptStyle.Keyboard);
            CurrentContext = context;
            GetCurrentUserData();

            if(QuestionAnsweredCorrectly)
            {
                CurrentContext.PostAsync(Step1BusinessLayer.Message_End);
            }
            else
            {
                if (LastmessageShown > -1)
                {
                    PromptDialog.Confirm(CurrentContext, DetermineStepMessage, $"Are you ready to progress?", promptStyle: PromptStyle.Keyboard);
                }
                else
                {
                    PromptDialog.Confirm(CurrentContext, DetermineStepMessage, $"Are you ready to start your introduction?", promptStyle: PromptStyle.Keyboard);
                }
            }
        }

        public async Task DetermineStepMessage(IDialogContext context, IAwaitable<bool> result)
        {
            CurrentContext = context;
            GetCurrentUserData();

            switch (LastmessageShown)
            {
                case -1:
                    await ShowStepMessage(Step1BusinessLayer.StringContent1, Step1BusinessLayer.URL_Media1);
                    break;
                case 0:
                    await ShowStepMessage(Step1BusinessLayer.StringContent2, Step1BusinessLayer.URL_Media2);
                    break;
                case 1:
                    await ShowStepMessage(Step1BusinessLayer.StringContent3, Step1BusinessLayer.URL_Media3);
                    break;
                case 2:
                    await ShowStepMessage(Step1BusinessLayer.StringContent4, Step1BusinessLayer.URL_Media4);
                    break;
                case 3:
                    await ShowStepQuestion(Step1BusinessLayer.StringContentQuestion);
                    break;
            }
        }
        public async Task ShowStepMessage(List<string> message_Text, string url_Media)
        {
            IEnumerable<string> options = new List<string>() { "Next" };
            /* Show Messages */
            foreach (string message in message_Text)
            {
                await CurrentContext.PostAsync(message);
            }

            /* Show Media */
            var videoPost = CurrentContext.MakeMessage();
            videoPost.Attachments.Add(new VideoCard(media: new[] { new MediaUrl(url_Media) }).ToAttachment());
            await CurrentContext.PostAsync(videoPost);

            /* Progress Confirmation */
            PromptDialog.Confirm(CurrentContext, DetermineStepMessage, $"Are you ready for the next content", promptStyle: PromptStyle.Keyboard);
        }

        public async Task ShowStepQuestion(List<string> message_Text)
        {
            IEnumerable<string> options = new List<string>() { "Next" };
            /* Show Messages */
            foreach (string message in message_Text)
            {
                await CurrentContext.PostAsync(message);
            }

            /* Progress Confirmation */
            PromptDialog.Text(CurrentContext, FinalQuestionAnswered,"");
        }

        public async Task FinalQuestionAnswered(IDialogContext context, IAwaitable<string> result)
        {
            string answer = await result;
            if (answer.Contains(Step1BusinessLayer.Question_Answer))
            {
                await context.PostAsync(Step1BusinessLayer.Question_Correct);
                context.Done(Step1BusinessLayer.Question_Correct);
            }
            else
            {
                await context.PostAsync(Step1BusinessLayer.Question_Wrong);
                context.Done(Step1BusinessLayer.Question_Wrong);
            }
        }
        private void GetCurrentUserData()
        {
            /* To Be Replaced When implementing State Management*/
            int? _lastmessageShown;
            Boolean? _questionAnsweredCorrectly;

            CurrentContext.UserData.TryGetValue(UserData_LastMessageShown, out _lastmessageShown);
            CurrentContext.UserData.TryGetValue(UserData_QuestionAnsweredCorrectly, out _questionAnsweredCorrectly);

            QuestionAnsweredCorrectly = _questionAnsweredCorrectly.HasValue ? _questionAnsweredCorrectly.Value : false;
            LastmessageShown = _lastmessageShown.HasValue ? _lastmessageShown.Value : -1;
        }

        private void SaveCurrentUserData(Boolean questionAnsweredCorrectly = false)
        {
            /* To Be Replaced When implementing State Management*/
            int _lastmessageShown = LastmessageShown >= 3 ? -1 : LastmessageShown + 1;
            Boolean _questionAnsweredCorrectly = false;

            CurrentContext.UserData.SetValue<int>(UserData_LastMessageShown, _lastmessageShown);
            CurrentContext.UserData.SetValue<Boolean>(UserData_QuestionAnsweredCorrectly, _questionAnsweredCorrectly);

            QuestionAnsweredCorrectly = _questionAnsweredCorrectly;
            LastmessageShown = _lastmessageShown;
        }

    }
}