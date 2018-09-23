import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { QuizService } from '../../quiz/quiz.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  failedLogin: boolean = false;

  constructor(private quizService: QuizService, private route: Router) { }

  ngOnInit() {
  }

  login(form: NgForm) {
    let credentials = JSON.stringify(form.value);
    this.quizService.login(credentials).subscribe(
      (response: any) => {  
        let token = (<any>response).token;
        localStorage.setItem("jwt", token);
        this.failedLogin = false;
        this.route.navigate(['/landing-page']);
      },
      err => {
        console.log("failed to login");
        this.failedLogin = true;
      }
    );
  }
  
}
