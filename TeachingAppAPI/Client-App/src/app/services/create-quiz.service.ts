import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { Course } from '../create-course/course';

@Injectable()
export class CreateQuizService {

    private step = new BehaviorSubject<number>(1);
    currentStep = this.step.asObservable();
    //public EnrolledCoursesList: any;
    private rootURL = "http://localhost:52459/api/courses";
    //public course: Course = new Course(); // ?? This may be a problem for testing ??

    constructor(private http: HttpClient, private route: Router){}

    uploadImage(formData: FormData){
        console.log(formData)
        //formData.append('Name', caption);
        return this.http.post(this.rootURL + "/upload", formData);
    }

    changeStep(newStep: number){
        this.step.next(newStep);
    }

}