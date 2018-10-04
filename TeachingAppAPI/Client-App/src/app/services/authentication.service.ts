import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable()
export class AuthenticationService {
    private questionsUrl = "http://localhost:52459/api/questions";
    private rootURL = "http://localhost:52459/api/users";
    private rootURL2 = "http://localhost:52459/api/users/confirm-account";
    userDetails: any;
    loggedIn: boolean = false;
    constructor(private http: HttpClient, private route: Router){}

    /**
     * returns http get request for questions.
     * headers require token that is generated on login
     * for authorization.
     */
    getQuestions(): any {
        let token = localStorage.getItem("jwt");
        console.log("i got this far");
        var result = this.http.get(this.questionsUrl, {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        });
        return result;
    }

    createUser(firstName: string, lastName: string, username: string, email: string, userPassword: string){
        var body = {
            FirstName: firstName,
            LastName: lastName,
            UserName: username,
            Email: email,
            UserPassword: userPassword
        }
        return this.http.post(this.rootURL, body).subscribe(
            (response) => {
                this.userDetails = (response);
                //console.log("usrname: " + this.username.appUser);
                //console.log("usrnamew: " + <any>response.Username);
                this.route.navigate(['/confirm-account']);
            },
            err => {
              //this.error = err.error;
              console.log(err.error);
            }
          );
    }

    confirmAccount(userId: number, confirmationCode: string){
        var code = {
            AppUserId: userId,
            VerificationCode: confirmationCode
        };
        return this.http.post(this.rootURL2, code);
    }

    /**
     * called from login.component.ts login()
     * @param credentials JSON stringified creds
     */
    login(credentials){
        var requiredHeader = new HttpHeaders({'Content-Type':'application/json'});
        //console.log("reqHeader: " + reqHeader);
        this.http.post("http://localhost:52459/api/users/token", credentials, { headers: requiredHeader }).subscribe(
            (response: any) => {
              if(response.verified == false){
                this.loggedIn = true;
                this.userDetails = response;
                this.route.navigate(['/dashboard']);
              } else {
                let token = (<any>response).token;
                localStorage.setItem("jwt", token);
                this.loggedIn = true;
                this.userDetails = response;
                this.route.navigate(['/dashboard']);
              }  
            },
            err => {
              console.log("failed to login");
              this.loggedIn = false;
            }
        );
    }

    IsLoggedIn(){
        return this.loggedIn;
        //return !!localStorage.getItem('jwt');
    }

    logOut(){
        this.loggedIn = false;
    }
}