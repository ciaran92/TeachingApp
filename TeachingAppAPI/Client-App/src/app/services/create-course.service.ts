import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { Course } from '../create-course/course';

@Injectable()
export class CreateCourseService {
    
    private step = new BehaviorSubject<number>(1);
    currentStep = this.step.asObservable();
    public EnrolledCoursesList: any;
    private rootURL = "http://localhost:52459/api/courses";
    public course: Course = new Course();

    constructor(private http: HttpClient, private route: Router){}

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

    uploadImage(formData: FormData){
        console.log(formData)
        //formData.append('Name', caption);
        return this.http.post(this.rootURL + "/upload", formData);
    }

    changeStep(newStep: number){
        this.step.next(newStep);
    }
    
}

    
