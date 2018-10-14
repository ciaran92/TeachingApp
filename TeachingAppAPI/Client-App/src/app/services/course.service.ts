import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable()
export class CourseService {
    
    private rootURL = "http://localhost:52459/api/courses";
    

    constructor(private http: HttpClient){}

    /**
     * returns http get request for questions.
     * headers require token that is generated on login
     * for authorization.
     */

    getCourses(): any {
        var result = this.http.get(this.rootURL);
        /* .subscribe((response:any) => 
        {
            this.courseDetails = response;
            console.log("this.courseDetails 1: " + this.courseDetails[1].courseId);
        });
        
        console.log("this.courseDetails 2: " + this.courseDetails[0].courseId);
        return this.courseDetails; */
        return result;
    }

    /*getCourses(): any {
        //Observable<Course[]>
        this.http.get(this.rootURL).subscribe((response) => new Course().deserialize(courseDetails));
        console.log("this.courseDetails: ");
    
        return this.courseDetails;
    }
    */
    
}

    
