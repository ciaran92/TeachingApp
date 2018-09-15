using System;
using System.Collections.Generic;

namespace TeachingAppAPI.Models
{
    public partial class Questions
    {
        //test comment
        public int QuestionId { get; set; }
        public int? TopicId { get; set; }
        public string Question { get; set; }
        public string[] Options
        {
            get
            {
                string[] o = { Option1, Option2, Option3, Option4 };
                o = shuffleOptions(o);
                return o;
            }
        }
        /// <summary>
        /// Test Changes
        /// </summary>
        public string Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public bool? Answered { get; set; }
        public int QuestionPriority { get; set; }
        public Topic Topic { get; set; }

        public string[] shuffleOptions(string[] optionsArray)
        {
            Random r = new Random();
            for (int i = optionsArray.Length; i > 0; i--)
            {
                int j = r.Next(i);
                string k = optionsArray[j];
                optionsArray[j] = optionsArray[i - 1];
                optionsArray[i - 1] = k;
            }
            return optionsArray;
        }
    }
}
