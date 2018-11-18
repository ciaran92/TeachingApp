using System;
using Microsoft.EntityFrameworkCore;
using TeachingAppAPI.Models;

namespace TeachingAppAPI.Data
{
    public partial class TestDB_Phase2Context : DbContext
    {
        public TestDB_Phase2Context()
        {
        }

        public TestDB_Phase2Context(DbContextOptions<TestDB_Phase2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<AnswerType> AnswerType { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<AppUserStatus> AppUserStatus { get; set; }
        public virtual DbSet<AppUserType> AppUserType { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseStatus> CourseStatus { get; set; }
        public virtual DbSet<Enrolment> Enrolment { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }
        public virtual DbSet<QuizInstance> QuizInstance { get; set; }
        public virtual DbSet<QuizInstanceAnswer> QuizInstanceAnswer { get; set; }
        public virtual DbSet<QuizType> QuizType { get; set; }
        public virtual DbSet<QuizUserStatus> QuizUserStatus { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        //public virtual DbSet<test1> Test1 { get; set; }

        public virtual DbQuery<test1> Test1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-EI3KQ5R;Database=TestDB_Phase2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.AnswerText).HasMaxLength(200);

                entity.Property(e => e.AnswerTypeId).HasColumnName("AnswerTypeID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.AnswerType)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.AnswerTypeId)
                    .HasConstraintName("FK__Answer__AnswerTy__52593CB8");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Answer__Question__5165187F");
            });

            modelBuilder.Entity<AnswerType>(entity =>
            {
                entity.Property(e => e.AnswerTypeId)
                    .HasColumnName("AnswerTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AnswerTypeDesc)
                    .HasColumnName("AnswerType_desc")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.Property(e => e.AppUserStatusId).HasColumnName("AppUserStatusID");

                entity.Property(e => e.AppUserTypeId).HasColumnName("AppUserTypeID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VerificationCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.AppUserStatus)
                    .WithMany(p => p.AppUser)
                    .HasForeignKey(d => d.AppUserStatusId)
                    .HasConstraintName("FK__AppUser__AppUser__3C69FB99");

                entity.HasOne(d => d.AppUserType)
                    .WithMany(p => p.AppUser)
                    .HasForeignKey(d => d.AppUserTypeId)
                    .HasConstraintName("FK__AppUser__AppUser__3B75D760");
            });

            modelBuilder.Entity<AppUserStatus>(entity =>
            {
                entity.Property(e => e.AppUserStatusId)
                    .HasColumnName("AppUserStatusID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AppUserStatusDesc)
                    .IsRequired()
                    .HasColumnName("AppUserUserStatus_desc")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppUserType>(entity =>
            {
                entity.Property(e => e.AppUserTypeId)
                    .HasColumnName("AppUserTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AppUserTypeDesc)
                    .HasColumnName("AppUserType_Desc")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseDateTimeStart)
                    .HasColumnName("Course_DateTime_start")
                    .HasColumnType("datetime");

                entity.Property(e => e.CourseDesc)
                    .HasColumnName("Course_desc")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CourseDuration).HasColumnName("Course_Duration");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CourseStatusId).HasColumnName("CourseStatusID");

                entity.HasOne(d => d.CourseStatus)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.CourseStatusId)
                    .HasConstraintName("FK__Course__CourseSt__412EB0B6");
            });

            modelBuilder.Entity<CourseStatus>(entity =>
            {
                entity.Property(e => e.CourseStatusId)
                    .HasColumnName("CourseStatusID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CourseStatusDesc)
                    .HasColumnName("CourseStatus_desc")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Enrolment>(entity =>
            {
                entity.Property(e => e.EnrolmentId).HasColumnName("EnrolmentID");

                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.Property(e => e.EnrolmentDateTimeStart)
                    .HasColumnName("Enrolment_DateTime_start")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Enrolment)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK__Enrolment__AppUs__5535A963");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrolment)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Enrolment__Cours__5629CD9C");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionText).HasMaxLength(200);

                entity.Property(e => e.QuizId).HasColumnName("QuizID");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK__Question__QuizID__4CA06362");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.QuizId).HasColumnName("QuizID");

                entity.Property(e => e.QuizDesc)
                    .HasColumnName("Quiz_desc")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.QuizTypeId).HasColumnName("QuizTypeID");

                entity.Property(e => e.TopicId).HasColumnName("TopicID");

                entity.HasOne(d => d.QuizType)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.QuizTypeId)
                    .HasConstraintName("FK__Quiz__QuizTypeID__49C3F6B7");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__Quiz__TopicID__48CFD27E");
            });

            modelBuilder.Entity<QuizInstance>(entity =>
            {
                entity.Property(e => e.QuizInstanceId).HasColumnName("QuizInstanceID");

                entity.Property(e => e.EnrolmentId).HasColumnName("EnrolmentID");

                entity.Property(e => e.QuizDateTimeStart)
                    .HasColumnName("Quiz_DateTime_start")
                    .HasColumnType("datetime");

                entity.Property(e => e.QuizId).HasColumnName("QuizID");

                entity.Property(e => e.QuizUserStatusId).HasColumnName("QuizUserStatusID");

                entity.HasOne(d => d.Enrolment)
                    .WithMany(p => p.QuizInstance)
                    .HasForeignKey(d => d.EnrolmentId)
                    .HasConstraintName("FK__QuizInsta__Enrol__5CD6CB2B");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizInstance)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK__QuizInsta__QuizI__5AEE82B9");

                entity.HasOne(d => d.QuizUserStatus)
                    .WithMany(p => p.QuizInstance)
                    .HasForeignKey(d => d.QuizUserStatusId)
                    .HasConstraintName("FK__QuizInsta__QuizU__5BE2A6F2");
            });

            modelBuilder.Entity<QuizInstanceAnswer>(entity =>
            {
                entity.Property(e => e.QuizInstanceAnswerId).HasColumnName("QuizInstanceAnswerID");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.AppUserAnswer).HasMaxLength(200);

                entity.Property(e => e.AppUserAnswerDateTime)
                    .HasColumnName("AppUserAnswer_DateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuizInstanceId).HasColumnName("QuizInstanceID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.QuizInstanceAnswer)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK__QuizInsta__Answe__619B8048");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuizInstanceAnswer)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__QuizInsta__Quest__60A75C0F");

                entity.HasOne(d => d.QuizInstance)
                    .WithMany(p => p.QuizInstanceAnswer)
                    .HasForeignKey(d => d.QuizInstanceId)
                    .HasConstraintName("FK__QuizInsta__QuizI__5FB337D6");
            });

            modelBuilder.Entity<QuizType>(entity =>
            {
                entity.Property(e => e.QuizTypeId)
                    .HasColumnName("QuizTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.QuizTypeDesc)
                    .HasColumnName("QuizType_desc")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuizUserStatus>(entity =>
            {
                entity.Property(e => e.QuizUserStatusId)
                    .HasColumnName("QuizUserStatusID")
                    .ValueGeneratedNever();

                entity.Property(e => e.QuizUserStatusDesc)
                    .HasColumnName("QuizUserStatus_desc")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TopicDesc)
                    .HasColumnName("Topic_desc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TopicName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Topic)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Topic__CourseId__440B1D61");
            });



            // MyAttempt:
            //modelBuilder.Entity<test1>(entity =>
            //{

            //    entity.Property(e => e.EnrolmentId).HasColumnName("EnrolmentID");

            //    entity.Property(e => e.QuizInstanceId).HasColumnName("QuizInstanceID");

            //    entity.Property(e => e.QuizId).HasColumnName("QuizID");

            //    entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            //    entity.Property(e => e.QuestionText).HasColumnName("QuestionText");

            //    entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

            //    entity.Property(e => e.AnswerText).HasColumnName("AnswerText");

            //    entity.Property(e => e.AnswerTypeId).HasColumnName("AnswerTypeID");

            //    entity.Property(e => e.AnswerType_Desc).HasColumnName("AnswerType_desc");


            //    modelBuilder.Entity<test1>()
            //        .HasKey(c => new { c.EnrolmentId, c.QuizInstanceId, c.QuizId });

                //entity.Property(e => e.QuizDateTimeStart)
                //    .HasColumnName("Quiz_DateTime_start")
                //    .HasColumnType("datetime");



                //entity.Property(e => e.QuizUserStatusId).HasColumnName("QuizUserStatusID");

                //entity.HasOne(d => d.Enrolment)
                //    .WithMany(p => p.QuizInstance)
                //    .HasForeignKey(d => d.EnrolmentId)
                //    .HasConstraintName("FK__QuizInsta__Enrol__5CD6CB2B");

                //entity.HasOne(d => d.Quiz)
                //    .WithMany(p => p.QuizInstance)
                //    .HasForeignKey(d => d.QuizId)
                //    .HasConstraintName("FK__QuizInsta__QuizI__5AEE82B9");

                //entity.HasOne(d => d.QuizUserStatus)
                //    .WithMany(p => p.QuizInstance)
                //    .HasForeignKey(d => d.QuizUserStatusId)
                //    .HasConstraintName("FK__QuizInsta__QuizU__5BE2A6F2");
            //});







        }
    }
}
