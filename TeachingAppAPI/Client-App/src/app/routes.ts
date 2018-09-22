import { Routes } from '@angular/router'
import { QuizComponent } from './quiz/quiz.component';
import { RegisterStudentComponent } from './home/register-student/register-student.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './home/login/login.component';

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
    {path: '', redirectTo:'/home',pathMatch:'full'}
];