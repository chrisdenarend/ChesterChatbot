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
        private const string URL_Media2 = "https://progressivemlx-a.akamaihd.net/asset-100f555f-b6b6-429e-a984-f442bf8840ee/24273-368618-pskjlvtu.oia_H264_3400kbps_AAC_und_ch2_96kbps.mp4?sv=2012-02-12&sr=c&si=d35e8091-d0ee-4eb7-8b88-0cf60ade674d&sig=UtvNDw8R7cCD6sNtncX%2B%2B5H%2FFZkG58gnCx%2FjjsotHSw%3D&st=2017-04-19T18%3A06%3A30Z&se=2020-01-14T18%3A06%3A30Z";
        private const string Message_IntroToMedia3 = "Next is how to create a simple bot";
        private const string URL_Media3 = "https://progressivemlx-a.akamaihd.net:443/asset-2882f0ca-2418-4b89-907e-57e747fd6e0f/24638-374727-awptf0tw.pnv_H264_3400kbps_AAC_und_ch2_96kbps.mp4?sv=2012-02-12&sr=c&si=7997e392-5a07-4180-962e-9fbe52135965&sig=0V%2FwpfKGYSlijDV5fuM4ivUDgDUjRcgi1FDMeg6TxD8%3D&st=2017-06-09T20%3A25%3A33Z&se=2020-03-05T20%3A25%3A33Z";
        private const string Message_IntroToMedia4 = "And now part 2";
        private const string URL_Media4 = "https://progressivemlx-a.akamaihd.net:443/asset-2637d8f6-681e-4d70-b6ad-cbade8bb4343/24639-380228-wtizbzpr.u5w_H264_3400kbps_AAC_und_ch2_96kbps.mp4?sv=2012-02-12&sr=c&si=c688d080-a451-4040-b4ca-0e25b2953797&sig=gHLjpIF5ZEwHwD7iNnpO%2BCLQfHt7ZJCwg5i9gIl3Y8s%3D&st=2017-08-07T21%3A54%3A28Z&se=2020-05-03T21%3A54%3A28Z";
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

            var videoPost = context.MakeMessage();
            videoPost.Attachments.Add(new VideoCard(media: new[] { new MediaUrl(URL_Media1) }).ToAttachment());

            await context.PostAsync(Message_Introduction);
            await context.PostAsync(Message_IntroToMedia1);
            await context.PostAsync(videoPost);
            PromptDialog.Confirm(context, ShowSecondContent, $"Are you ready for the next content", promptStyle: PromptStyle.Keyboard);
        }
        public async Task ShowSecondContent(IDialogContext context, IAwaitable<bool> result)
        {
            IEnumerable<string> options = new List<string>() { "Next" };

            var videoPost = context.MakeMessage();
            videoPost.Attachments.Add(new VideoCard(media: new[] { new MediaUrl(URL_Media2) }).ToAttachment());

            await context.PostAsync(Message_IntroToMedia2);
            await context.PostAsync(videoPost);
            PromptDialog.Confirm(context, ShowThirdContent, $"Are you ready for the next content", promptStyle: PromptStyle.Keyboard);
        }

        public async Task ShowThirdContent(IDialogContext context, IAwaitable<bool> result)
        {
            IEnumerable<string> options = new List<string>() { "Next" };

            var videoPost = context.MakeMessage();
            videoPost.Attachments.Add(new VideoCard(media: new[] { new MediaUrl(URL_Media3) }).ToAttachment());

            await context.PostAsync(Message_IntroToMedia3);
            await context.PostAsync(videoPost);
            PromptDialog.Confirm(context, ShowFourthContent, $"Are you ready for the next content", promptStyle: PromptStyle.Keyboard);
        }

        public async Task ShowFourthContent(IDialogContext context, IAwaitable<bool> result)
        {
            IEnumerable<string> options = new List<string>() { "Next" };


            var videoPost = context.MakeMessage();
            videoPost.Attachments.Add(new VideoCard(media: new[] { new MediaUrl(URL_Media4) }).ToAttachment());

            await context.PostAsync(Message_IntroToMedia4);
            await context.PostAsync(videoPost);
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
                PromptDialog.Text(context, FinalQuestionAnswered, Message_Question);
            }
        }
    }
}