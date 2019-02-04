import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';


@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  private fragment: string;
  

  constructor(private route: ActivatedRoute, private router: Router, private authenticationService: AuthenticationService) { }

  ngOnInit() {
    
  }

  loggedIn(): boolean{
    //console.log(this.authenticationService.IsLoggedIn());
    return this.authenticationService.isUserLoggedIn();
  }

  logOut(){
    console.log("logout called from navbar");
    this.authenticationService.logOut();
  }



}
