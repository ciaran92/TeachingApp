import { Routes } from '@angular/router'
import { QuizComponent } from './quiz/quiz.component';
import { RegisterStudentComponent } from './home/register-student/register-student.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './home/login/login.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';

export const appRoutes : Routes = [
    {path: 'home', component:HomeComponent},
    {
        path: 'register', component:HomeComponent,
        children: [{path: '', component:RegisterStudentComponent}]
    },
    {
        path: 'login', component:HomeComponent,
        children: [{path: '', component:LoginComponent}]
    },
    {path: 'quiz', component:QuizComponent},
    { path: 'home/:about', component: AboutUsComponent},
    {path: 'about', component:AboutUsComponent},
    {path: 'contact', component:ContactUsComponent},
    {path: '', redirectTo:'/home',pathMatch:'full'}
];