import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {ScrollToModule} from 'ng2-scroll-to';


import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { QuizComponent } from './quiz/quiz.component';

import { QuizService } from './quiz/quiz.service';
import { RegisterStudentComponent } from './home/register-student/register-student.component';
import { appRoutes } from './routes';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './home/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { FooterComponent } from './footer/footer.component';

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
    FooterComponent
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
    QuizService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
