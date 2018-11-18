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
import { MyCoursesComponent } from './my-courses/my-courses.component';
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
        path: 'dashboard', component:DashboardComponent//,
        //canActivate: [AuthGuard]
    },
    {
        path: 'my-courses', component:MyCoursesComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'courses', component:CoursesComponent
    },
    {
        path: 'courses/:id', component:CourseDetailsComponent
    },
    {
        path: 'courses', component:CoursesComponent
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
    {   path: 'create-course', component:CreateCourseComponent,
        children:[
            { path: '', component: CourseInfoComponent },
            { path: 'thumbnail', component: CourseThumbnailComponent },
            { path: 'submit', component: SubmitCourseComponent },
            { path: 'content', component: CourseContentComponent}
        ]    
    },
    {path: '', redirectTo:'/home',pathMatch:'full'},
    

    
];