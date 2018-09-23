import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { QuizService } from './quiz/quiz.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

   constructor(private quizService: QuizService, private router: Router){}

   canActivate(): boolean{
    if(this.quizService.loggedIn()){
      return true;
    }else{
      this.router.navigate(['login']);
      return false;
    }
  }
}
