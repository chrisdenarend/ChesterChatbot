using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChesterChatbot.BussinesLayer
{
    internal static class Step1BusinessLayer
    {
        internal const string Message_Introduction = "Lets start off with an introduction to chatbots";
        internal const string Message_IntroToMedia1 = "Watch this clip to understand the concept of chatbots";
        internal const string URL_Media1 = "https://www.youtube.com/watch?v=5yr1r1YlGVo";
        internal const string Message_IntroToMedia2 = "The following clip is an introduction to Microsoft Bot Framework";
        internal const string URL_Media2 = "https://progressivemlx-a.akamaihd.net/asset-100f555f-b6b6-429e-a984-f442bf8840ee/24273-368618-pskjlvtu.oia_H264_3400kbps_AAC_und_ch2_96kbps.mp4?sv=2012-02-12&sr=c&si=d35e8091-d0ee-4eb7-8b88-0cf60ade674d&sig=UtvNDw8R7cCD6sNtncX%2B%2B5H%2FFZkG58gnCx%2FjjsotHSw%3D&st=2017-04-19T18%3A06%3A30Z&se=2020-01-14T18%3A06%3A30Z";
        internal const string Message_IntroToMedia3 = "Next is how to create a simple bot";
        internal const string URL_Media3 = "https://progressivemlx-a.akamaihd.net:443/asset-2882f0ca-2418-4b89-907e-57e747fd6e0f/24638-374727-awptf0tw.pnv_H264_3400kbps_AAC_und_ch2_96kbps.mp4?sv=2012-02-12&sr=c&si=7997e392-5a07-4180-962e-9fbe52135965&sig=0V%2FwpfKGYSlijDV5fuM4ivUDgDUjRcgi1FDMeg6TxD8%3D&st=2017-06-09T20%3A25%3A33Z&se=2020-03-05T20%3A25%3A33Z";
        internal const string Message_IntroToMedia4 = "And now part 2";
        internal const string URL_Media4 = "https://progressivemlx-a.akamaihd.net:443/asset-2637d8f6-681e-4d70-b6ad-cbade8bb4343/24639-380228-wtizbzpr.u5w_H264_3400kbps_AAC_und_ch2_96kbps.mp4?sv=2012-02-12&sr=c&si=c688d080-a451-4040-b4ca-0e25b2953797&sig=gHLjpIF5ZEwHwD7iNnpO%2BCLQfHt7ZJCwg5i9gIl3Y8s%3D&st=2017-08-07T21%3A54%3A28Z&se=2020-05-03T21%3A54%3A28Z";
        internal const string Message_IntroToQuestion = "Now let's see what you learned";
        internal const string Message_Question = "How does a bot communicate with the messaging platform?";
        internal const string Question_Answer = "Connector";
        internal const string Question_Wrong = "Pity, your answer was wrong. Try again";
        internal const string Question_Correct = "Yeah, your answer was pretty outstanding correct.";
        internal const string Message_End = "You have successfully ended this part of the course";

        internal static List<string> StringContent1
        {
            get
            {
                List<String> returnValue = new List<string>();
                returnValue.Add(Message_Introduction);
                returnValue.Add(Message_IntroToMedia1);
                return returnValue;
            }
        }
        internal static List<string> StringContent2
        {
            get
            {
                List<String> returnValue = new List<string>();
                returnValue.Add(Message_IntroToMedia2);
                return returnValue;
            }
        }
        internal static List<string> StringContent3
        {
            get
            {
                List<String> returnValue = new List<string>();
                returnValue.Add(Message_IntroToMedia3);
                return returnValue;
            }
        }
        internal static List<string> StringContent4
        {
            get
            {
                List<String> returnValue = new List<string>();
                returnValue.Add(Message_IntroToMedia4);
                return returnValue;
            }
        }
        internal static List<string> StringContentQuestion
        {
            get
            {
                List<String> returnValue = new List<string>();
                returnValue.Add(Message_IntroToQuestion);
                returnValue.Add(Message_Question);
                return returnValue;
            }
        }
    }
}