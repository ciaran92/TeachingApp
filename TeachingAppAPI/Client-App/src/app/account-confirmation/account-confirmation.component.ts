import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account-confirmation',
  templateUrl: './account-confirmation.component.html',
  styleUrls: ['./account-confirmation.component.css']
})
export class AccountConfirmationComponent implements OnInit {

  error: any;
  private userDetails: any;
  constructor(private authenticationService: AuthenticationService, private route: Router) { }

  ngOnInit() {
    this.userDetails = this.authenticationService.userDetails;
  }

  OnSubmit(code: string){
    this.authenticationService.confirmAccount(this.userDetails.id, code).subscribe(
      (response) => {  
        //let token = (<any>response).token;
        //localStorage.setItem("jwt", token);
        //this.failedLogin = false;
        this.route.navigate(['/home']);
      },
      err => {
        console.log("err.error");
        //this.failedLogin = true;
      }
    );
  }

}
