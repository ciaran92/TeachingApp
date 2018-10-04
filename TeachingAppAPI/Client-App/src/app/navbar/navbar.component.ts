import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { longStackSupport } from 'q';


@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  private fragment: string;

  constructor(private route: ActivatedRoute, private router: Router, private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.route.fragment.subscribe(fragment => { this.fragment = fragment; });
  }

  ngAfterViewInit(): void {
    try { 
      document.querySelector('#' + this.fragment).scrollIntoView();
    } catch (e) { }
  }

  onAnchorClick ( ) {
    let x = document.querySelector("#about");
    if (x){
        x.scrollIntoView();
    }
  }

  go () {
    this.route.fragment.subscribe ( f => {
      const element = document.querySelector ( "#" + f )
      if ( element ){
        element.scrollIntoView();
      } 
    });
  }

  loggedIn(): boolean{
    return this.authenticationService.IsLoggedIn();
  }

  logOut(){
    //localStorage.removeItem("jwt");
    this.authenticationService.logOut();
  }

  goTo(id){
    this.router.navigate( ['/home', id ]);
  }

  scrollUp(){
    let x = document.querySelector("#home");
    if (x){
        x.scrollIntoView({ behavior: "smooth"});
    }
    //document.body.scrollTop = document.documentElement.scrollTop = 0;
  }

  about(){
    this.router.navigate( ['/home']);
    let x = document.querySelector("#about");
    if (x){
      x.scrollIntoView();
    }

    
  }



}
