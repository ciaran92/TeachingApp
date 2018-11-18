using Microsoft.Extensions.DependencyInjection;
using TeachingAppAPI.Data;
using TeachingAppAPI.Services;

namespace TeachingAppAPI
{
    public class DependencyInjectionConfig
    {

        public static void AddScope(IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrolmentService, EnrolmentService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IQuizInstanceService, QuizInstanceService>();
            services.AddScoped<IQuizInstanceAnswerService, QuizInstanceAnswerService>();
        }
    }
}
