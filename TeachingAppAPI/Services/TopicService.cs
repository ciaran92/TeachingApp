using System;
using System.Collections.Generic;
using System.Linq;
using TeachingAppAPI.Helpers;
using TeachingAppAPI.Entities;
using TeachingAppAPI.Data;

namespace TeachingAppAPI.Services
{
    public class TopicService : ITopicService
    {
        private TestDB_Phase2Context _context;

        public TopicService(TestDB_Phase2Context context)
        {
            _context = context;
        }


        public Topic CreateTopic(Topic topic)
        {
            if (topic.CourseId == null)
            {
                throw new CustomException("Topic not associated with a course.");
            }
            if (string.IsNullOrWhiteSpace(topic.TopicName))
            {
                throw new CustomException("Topic name is required");
            }

            if (DoesTopicAlreadyExist(topic.TopicName))
            {
                throw new CustomException("Topic name already exists");
            }
            else
            {
                _context.Topic.Add(topic);
                _context.SaveChanges();
                Console.WriteLine("created topic");
                return topic;
            }

        }

        private Boolean DoesTopicAlreadyExist(string topicname)
        {
            if (_context.Topic.Any(x => x.TopicName == topicname))
            {
                return true;
            }
            return false;
        }

    }
}
