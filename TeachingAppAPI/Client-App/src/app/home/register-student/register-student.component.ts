import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';


@Component({
  selector: 'app-register-student',
  templateUrl: './register-student.component.html',
  styleUrls: ['./register-student.component.css']
})
export class RegisterStudentComponent implements OnInit {

  error: any;
  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit() {
  }

  OnSubmit(FirstName:string, LastName:string, Username: string, Email:string, UserPassword: string){
    this.authenticationService.createUser(FirstName, LastName, Username, Email, UserPassword);
  }

}
