import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Status } from './status';

@Injectable()
export class AuthenticationService {
    private questionsUrl = "http://localhost:52459/api/questions";
    private rootURL = "http://localhost:52459/api/users";
    private rootURL2 = "http://localhost:52459/api/users/confirm-account";
    private failedLogin: boolean = false;
    private showVerificationPopup: boolean;
    public status: Status = new Status();

    constructor(private http: HttpClient, private route: Router, private cookie: CookieService){}

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
                let token = (<any>response).token;
                localStorage.setItem("jwt", token);
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
        this.http.post(this.rootURL2, code).subscribe(
            (response) => {  
              this.route.navigate(['/home']);
            },
            err => {
              console.log("err.error");
            }
          );
    }

    /**
     * called from login.component.ts login()
     * @param credentials JSON stringified creds
     */
    login(credentials){
        var requiredHeader = new HttpHeaders({'Content-Type':'application/json'});
        this.http.post("http://localhost:52459/api/users/token", credentials, { headers: requiredHeader }).subscribe(
            (response: any) => {
                let token = (<any>response).token;
                let refreshToken = response.refreshToken;
                const helper = new JwtHelperService();
                const decodedToken = helper.decodeToken(token);
                console.log("decoded token: " + JSON.stringify(decodedToken));
                this.cookie.set("jwt", token);
                this.cookie.set("refresh", refreshToken);
                //this.cookie.set("loggedIn", "true");
                this.status.loggedIn = true;
                console.log("refreshToken: " + refreshToken);
                this.cookie.set("data", JSON.stringify(response));
                this.route.navigate(['/home']);    
            },
            err => {
              console.log("login error: " + err.error);
              this.failedLogin = true;
            }
        );
    }

    IsLoggedIn(): boolean{
        return this.status.loggedIn;
    }
    
    LoginAttemptFailed(){
        return this.failedLogin;
    }

    logOut(){
        this.cookie.delete('jwt');
        //this.cookie.delete('loggedIn');
        this.status.loggedIn = false;
        this.route.navigate(['/home']);
    }

    ShowVerificationPopup(){
        this.showVerificationPopup = true;
    }

    HideVerificationPopup(){
        this.showVerificationPopup = false;
    }

    GetVerificationPopup(): boolean{
        return this.showVerificationPopup;
    }

    RefreshToken(token: string): any{
        var body = {
            Token: token
        }
        return this.http.post("http://localhost:52459/api/users/token/refresh", body).subscribe(
            (response: any) => {
                console.log("myId: " + response.id);
                let token = (<any>response).token;
                let refreshToken = response.refreshToken;
                this.cookie.set("jwt", token);
                this.cookie.set("refresh", refreshToken);
                console.log("refresh token still valid, keep browsing");
            },
            err => {
                    this.logOut();
                    this.route.navigate(['home']);
                    console.log(err.status);      
            }      
        );
    }
}

