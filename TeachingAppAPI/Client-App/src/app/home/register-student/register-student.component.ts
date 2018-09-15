import { Component, OnInit } from '@angular/core';
import { QuizService } from '../../quiz/quiz.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-student',
  templateUrl: './register-student.component.html',
  styleUrls: ['./register-student.component.css']
})
export class RegisterStudentComponent implements OnInit {

  constructor(private quizService: QuizService, private route: Router) { }

  ngOnInit() {
  }

  OnSubmit(FirstName:string, LastName:string, Username: string, UserPassword: string){
    this.quizService.createUser(FirstName, LastName, Username, UserPassword).subscribe(
      (data: any) => {
        this.route.navigate(['/quiz'])
      }
    );
  }

}
