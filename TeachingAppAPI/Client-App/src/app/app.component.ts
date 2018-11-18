import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'app';
  private showPopup: boolean = false;

  constructor(private authService: AuthenticationService){}

  ngOnInit(){
    //this.showPopup = this.authService.GetVerificationPopup();
  }

  DisplayPopup(): boolean{
    this.showPopup = this.authService.GetVerificationPopup();
    return this.showPopup;
  }

  Close(){
    this.showPopup = false;
  }

}
