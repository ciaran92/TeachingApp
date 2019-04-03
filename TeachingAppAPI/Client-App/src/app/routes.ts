import { Routes } from '@angular/router'
import { QuizComponent } from './quiz/quiz.component';
import { RegisterStudentComponent } from './register-student/register-student.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './auth.guard';
import { AccountConfirmationComponent } from './account-confirmation/account-confirmation.component';
import { MyCoursesComponent } from './dashboard/my-courses/my-courses.component';
import { CoursesComponent } from './courses/courses.component';
import { TopicsComponent } from './topics/topics.component';
import { Quiz2Component } from './quiz2/quiz2.component';
import { QuizInstanceComponent } from './quiz-instance/quiz-instance.component';
import { VideoTestComponent } from './video-test/video-test.component';

import { CourseDetailsComponent } from './courses/course-details.component';
import { CreateCourseComponent } from './create-course/create-course.component';
import { CourseInfoComponent } from './create-course/course-info/course-info.component';
import { CourseThumbnailComponent } from './create-course/course-thumbnail/course-thumbnail.component';
import { SubmitCourseComponent } from './create-course/submit-course/submit-course.component';
import { CourseContentComponent } from './create-course/course-content/course-content.component';
import { EditTopicComponent } from './create-course/course-content/edit-topic.component';
import { GenerateCourseComponent } from './create-course/generate-course.component';
import { CreateQuizComponent } from './create-course/course-content/create-quiz/create-quiz.component';
import { TeacherDashboardComponent } from './dashboard/teacher-dashboard/teacher-dashboard.component';

export const appRoutes : Routes = [
    {path: 'home', component:HomeComponent},
    {path: 'courses', component: CoursesComponent},
    {path: 'contact', component:ContactUsComponent},
    {
        path: 'register', component:RegisterStudentComponent
    },
    {
        path: 'sign-in', component:LoginComponent
    },
    {
        path: 'quiz', component:QuizComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'dashboard', component:DashboardComponent,
        children:[
            { path: '', redirectTo: 'teacher', pathMatch: 'full' },
            { path: 'teacher', component: TeacherDashboardComponent }
        ]    
    },
    {
        path: 'my-courses', component:MyCoursesComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'courses', component:CoursesComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'courses/:id', component:CourseDetailsComponent
    },
    {
        path: 'topics', component:TopicsComponent
    },
    {
        path: 'quizzes', component:Quiz2Component
    },
    {
        path: 'quiz-instances', component:QuizInstanceComponent
    },
    {
        path: 'video-test', component:VideoTestComponent
    },
    {
        path: 'confirm-account', component:AccountConfirmationComponent
    },
    {
        path: '', redirectTo:'/home',pathMatch:'full'
    },
    {
        path: 'add-course', component:GenerateCourseComponent
    },
    {   path: 'create-course', component:CreateCourseComponent,
        children:[
            { path: 'course-info/:id', component: CourseInfoComponent },
            { path: 'thumbnail/:id', component: CourseThumbnailComponent },
            { path: 'submit/:id', component: SubmitCourseComponent },
            { 
                path: 'content/:id', component: CourseContentComponent,
                children: [
                    { path: 'edit-topic/:id', component: EditTopicComponent,
                        children: [
                            { path: 'create-quiz', component: CreateQuizComponent }
                        ] 
                    }
                ]
            }
        ]    
    },
    {path: '', redirectTo:'/home',pathMatch:'full'},
    

    
];