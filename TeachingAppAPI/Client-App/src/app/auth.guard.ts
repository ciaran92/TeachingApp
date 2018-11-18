import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from './services/authentication.service';
import { CookieService } from 'ngx-cookie-service';
import { Status } from './services/status';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  loggedIn: boolean = false;

  constructor(private authService: AuthenticationService, private router: Router, private cookie: CookieService){}

  /*canActivate(): boolean{
    if(this.authService.IsLoggedIn()){
      return true;
    }else{
      this.router.navigate(['login']);
      return false;
    }
  }*/

  canActivate(): boolean{
    this.authService.RefreshToken(this.cookie.get("refresh"));
    return this.authService.IsLoggedIn();
  }

}
