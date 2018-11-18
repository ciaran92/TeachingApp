import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable()
export class CourseService {
    
    private rootURL = "http://localhost:52459/api/courses";
    

    constructor(private http: HttpClient){}

    /**
     * returns http get request for topics.
     * headers require token that is generated on login
     * for authorization.
     */

    getCourses(): any {
        var result = this.http.get(this.rootURL);
       
        return result;
    }


}

    
