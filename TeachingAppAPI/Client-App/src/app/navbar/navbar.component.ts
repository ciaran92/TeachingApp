import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuizService } from '../quiz/quiz.service';
import { longStackSupport } from 'q';


@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  
  constructor(private route: ActivatedRoute, private router: Router, private quizService: QuizService) { }

  ngOnInit() {
  }

  onAnchorClick ( ) {
    let x = document.querySelector("#about");
    if (x){
        x.scrollIntoView();
    }
  }

  loggedIn(): boolean{
    if(this.quizService.loggedIn()){
      return true;
    }else{
      return false;
    }
  }

  logOut(){
    localStorage.removeItem("jwt");
  }




}
