import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

interface myData{
    obj: Object;
}

@Injectable()
export class CourseService {
    
    private step = new BehaviorSubject<number>(1);
    currentStep = this.step.asObservable();
    private rootURL = "http://localhost:52459/api/courses";
    public EnrolledCoursesList: any;
    private courseDetails: any;
    constructor(private http: HttpClient, private cookie: CookieService, private route: Router){}

    /**
     * returns http get request for questions.
     * headers require token that is generated on login
     * for authorization.
     */

    getCourses(): any {
        let token = this.cookie.get("jwt");
        var result = this.http.get(this.rootURL, {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        });
        return result;
    }

    GetCoursesEnrolledIn(): any{
        let token = this.cookie.get("jwt");
        var result = this.http.get(this.rootURL + "/my-courses", {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        });
        return result;
    } 

    getCourse(courseId: number): any{
        return this.http.get(this.rootURL + "/" + courseId);        
    }

    createCourse(courseName: string, courseSubtitle: string, courseDescription: string){
        var body = {
            CourseName: courseName,
            CourseDescription: courseDescription,
            Subtitle: courseSubtitle
        }
        return this.http.post(this.rootURL, body).subscribe(
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
        return this.http.post(this.rootURL + "/upload", formData);
    }

    changeStep(newStep: number){
        this.step.next(newStep);
    }
    
}

    
