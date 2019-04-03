import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject, throwError, of  } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';

interface myData{
    obj: Object;
}

@Injectable()
export class CourseService {
    
    private step = new BehaviorSubject<number>(1);
    currentStep = this.step.asObservable();
    private courseURL = "http://localhost:52459/api/courses";
    private enrolmentURL = "http://localhost:52459/api/enrolment";
    public EnrolledCoursesList: any;
    private courseDetails: any;
    constructor(private http: HttpClient, private cookie: CookieService, private route: Router){}

    /**
     * returns http get request for topics.
     * headers require token that is generated on login
     * for authorization.
     */

    getCourses(): any {
        let token = this.cookie.get("jwt");
        var result = this.http.get(this.courseURL, {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        }).pipe(catchError(this.handleError));
        return result;
    }

    private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error.message);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong,
          console.error(
            `Backend returned code ${error.status}, ` +
            `body was: ${error.error}`);
        }
        // return an observable with a user-facing error status
        return throwError(error.status);
      };

    GetCoursesEnrolledIn(): any{
        let token = this.cookie.get("jwt");
        var result = this.http.get(this.courseURL + "/my-courses", {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        });
        return result;
    }

    getCourse(courseId: number): any{
        return this.http.get(this.courseURL + "/" + courseId);        
    }

    createCourse(courseName: string, courseSubtitle: string, courseDescription: string){
        var body = {
            CourseName: courseName,
            CourseDescription: courseDescription,
            Subtitle: courseSubtitle
        }
        return this.http.post(this.courseURL, body).subscribe(
            (response) => {
                //this.route.navigate(['/home']);
            },
            err => {
              //this.error = err.error;
              console.log(err.error);
            }
          );
    }

    uploadImage(caption: string, image: File){
        const formData: FormData = new FormData();
        formData.append(image.name, image);
        //formData.append('Name', caption);
        return this.http.post(this.courseURL + "/upload", formData);
    }

    changeStep(newStep: number){
        this.step.next(newStep);
    }

    enrolInCourse(courseId: number){
        let token = this.cookie.get("jwt");
        let body = {
            CourseId: courseId
        }
        var result = this.http.post(this.enrolmentURL, body, {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        }).pipe(catchError(this.handleError));
        return result; 
    }
    
}

    
