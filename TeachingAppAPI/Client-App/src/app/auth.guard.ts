import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from './services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthenticationService, private router: Router){}

  /**
   * Very basic can activate method checks to see if there is a jwt cookie present
   * If there is you can go to the selected route
   * if not redirected to login page
   */
  canActivate(): boolean{
    if(!this.authService.isUserLoggedIn())
    {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }

}
