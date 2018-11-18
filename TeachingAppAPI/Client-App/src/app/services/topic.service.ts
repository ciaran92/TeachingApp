import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TopicService {

  private rootURL = "http://localhost:52459/api/topics";
    

  constructor(private http: HttpClient) { }
  
  getTopics(): any {
    var result = this.http.get(this.rootURL);
    
    return result;
  }

}
