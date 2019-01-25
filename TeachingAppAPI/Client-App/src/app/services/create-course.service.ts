import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { Course } from '../create-course/course';
import { Topic } from '../create-course/course-content/topic';

@Injectable()
export class CreateCourseService {
    
    private step = new BehaviorSubject<number>(1);
    currentStep = this.step.asObservable();
    //privateId = new BehaviorSubject<number>

    topics: Topic[] = [];
    
    public EnrolledCoursesList: any;
    private rootURL = "http://localhost:52459/api/courses";
    public course: Course = new Course(); // ?? This may be a problem for testing ??

    constructor(private http: HttpClient, private route: Router){}

    uploadImage(formData: FormData){
        console.log(formData)
        //formData.append('Name', caption);
        return this.http.post(this.rootURL + "/upload", formData);
    }

    getTopics(courseId: number) {
        return this.http.get(this.rootURL + "/" + courseId); 
        /*if(sessionStorage.getItem("user_topics") != null){
            console.log("topics have been added");
            this.topics = JSON.parse(sessionStorage.getItem("user_topics"));
        }
        else{
            console.log("no topics added yet");
            this.topics.push(new Topic("Introduction", "custom desc", null));
        }
        return this.topics;*/
    }

    addTopic(courseId: number, name: string, desc: string){
        //this.topics.push(newTopic);
        //sessionStorage.setItem("user_topics", JSON.stringify(this.topics));
        var body = {
            CourseId: courseId,
            TopicName: name,
            TopicDesc: desc
        }
        return this.http.post(this.rootURL + "/add-topic", body)
    }

    changeStep(newStep: number){
        this.step.next(newStep);
    }

    updateCourse(courseName: string, subtitle: string, courseDescription: string, courseThumbnail:string){
        var body = {
            CourseName: courseName,
            Subtitle: subtitle,
            CourseDescription: courseDescription,
            CourseThumbnailUrl: courseThumbnail
        }
        return this.http.post(this.rootURL + "/upload2", body).subscribe(
            (response) => {
                //this.route.navigate(['/home']);
            },
            err => {
              //this.error = err.error;
              console.log(err.error);
            }
          );
    }

    createCourse(courseName: string){
        var body = {
            CourseName: courseName
        }
        return this.http.post(this.rootURL + "/new-course", body);
    }
    
}

    
