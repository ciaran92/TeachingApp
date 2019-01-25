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

import { QuizComponent } from './quiz/quiz.component';
import { CookieService } from 'ngx-cookie-service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxEditorModule } from 'ngx-editor';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { EditorModule } from '@tinymce/tinymce-angular';


import { CreateCourseService } from './services/create-course.service';

import { RegisterStudentComponent } from './register-student/register-student.component';
import { appRoutes } from './routes';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { FooterComponent } from './footer/footer.component';

import { MyCoursesComponent } from './my-courses/my-courses.component';
import { CoursesComponent } from './courses/courses.component';
import { CourseDetailsComponent } from './courses/course-details.component';
import { CreateCourseComponent } from './create-course/create-course.component';
import { GenerateCourseComponent } from './create-course/generate-course.component';


import { CourseInfoComponent } from './create-course/course-info/course-info.component';
import { CourseThumbnailComponent } from './create-course/course-thumbnail/course-thumbnail.component';
import { CourseRequirementsComponent } from './create-course/course-requirements/course-requirements.component';

import { CourseContentComponent } from './create-course/course-content/course-content.component';
import { AddTopicComponent } from './create-course/course-content/add-topic.component';
import { EditTopicComponent } from './create-course/course-content/edit-topic.component';
import { AddLessonComponent } from './create-course/course-content/add-lesson.component';
import { EditLessonComponent } from './create-course/course-content/edit-lesson.component';

import { SubmitCourseComponent } from './create-course/submit-course/submit-course.component';
import { AuthGuard } from './auth.guard';
import { AccountConfirmationComponent } from './account-confirmation/account-confirmation.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TopicsComponent } from './topics/topics.component';
import { Quiz2Component } from './quiz2/quiz2.component';
import { QuizInstanceComponent } from './quiz-instance/quiz-instance.component';
import { VideoTestComponent } from './video-test/video-test.component';
import { CreateQuizComponent } from './create-quiz/create-quiz.component';


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
    CourseDetailsComponent,
    CreateCourseComponent,
    CourseInfoComponent,
    CourseThumbnailComponent,
    CourseRequirementsComponent,
    CourseContentComponent,
    SubmitCourseComponent,
    TopicsComponent,
    QuizComponent,
    TopicsComponent,
    Quiz2Component,
    QuizInstanceComponent,
    VideoTestComponent,
    CreateQuizComponent,
    QuizInstanceComponent,
    VideoTestComponent,
    AddTopicComponent,
    EditTopicComponent,
    AddLessonComponent,
    EditLessonComponent,
    GenerateCourseComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    HttpClientModule,
    CommonModule,
    ScrollToModule.forRoot(),
    NgxEditorModule,
    AngularFontAwesomeModule,
    EditorModule
  ],
  providers: [
    AuthenticationService,
    CourseService,
    AuthGuard,
    CookieService,
    JwtHelperService,
    CreateCourseService,
    TopicService,
    Quiz2Service,
    QuizInstanceService,
    QuestionService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
