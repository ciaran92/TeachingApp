import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {ScrollToModule} from 'ng2-scroll-to';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';


import { AuthenticationService } from './services/authentication.service';

import { CourseService } from './services/course.service';
import { TopicService } from './services/topic.service';
import { Quiz2Service } from './services/quiz2.service';
import { QuizInstanceService } from './services/quiz-instance.service';
import { QuestionService } from './services/question.service';

import { appRoutes } from './routes';

import { HomeComponent } from './home/home.component';
import { RegisterStudentComponent } from './home/register-student/register-student.component';
import { LoginComponent } from './home/login/login.component';
import { AuthGuard } from './auth.guard';
import { AccountConfirmationComponent } from './account-confirmation/account-confirmation.component';
import { DashboardComponent } from './dashboard/dashboard.component';

import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { FooterComponent } from './footer/footer.component';

import { MyCoursesComponent } from './my-courses/my-courses.component';
import { CoursesComponent } from './courses/courses.component';
import { TopicsComponent } from './topics/topics.component';
import { QuizComponent } from './quiz/quiz.component';
import { Quiz2Component } from './quiz2/quiz2.component';
import { QuizInstanceComponent } from './quiz-instance/quiz-instance.component';
import { VideoTestComponent } from './video-test/video-test.component';
import { VideoTestService } from './services/video-test.service';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    QuizComponent,
    RegisterStudentComponent,
    HomeComponent,
    LoginComponent,
    AboutUsComponent,
    ContactUsComponent,
    FooterComponent,
    DashboardComponent,
    AccountConfirmationComponent,
    MyCoursesComponent,
    CoursesComponent,
    TopicsComponent,
    Quiz2Component,
    QuizInstanceComponent,
    VideoTestComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    HttpClientModule,
    CommonModule,
    ScrollToModule.forRoot()
  ],
  providers: [
    AuthenticationService,
    CourseService,
    TopicService,
    Quiz2Service,
    QuizInstanceService,
    QuestionService,
    VideoTestService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
