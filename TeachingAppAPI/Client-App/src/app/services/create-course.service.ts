import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { Course } from '../create-course/course';
import { Topic } from '../create-course/course-content/topic';
import { AuthenticationService } from './authentication.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class CreateCourseService {
    
    private step = new BehaviorSubject<number>(1);
    currentStep = this.step.asObservable();
    
    public EnrolledCoursesList: any;
    private courseCreatorURL = "http://localhost:52459/api/creator";
    private topicsURL = "http://localhost:52459/api/topics";
    private lessonsURL = "http://localhost:52459/api/lessons";
    private usersDashboardURL =  "http://localhost:52459/api/UsersDashboard";
    public course: Course = new Course(); // ?? This may be a problem for testing ??

    constructor(private http: HttpClient, private route: Router, private authService: AuthenticationService){}

    getCourses(){
        let token = this.authService.GetJwtToken();
        return this.http.get(this.courseCreatorURL + "/get-courses", {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        });
    }


    // Get all the topics for a course (returns the topicId, topicName & topicOrder)
    getTopics(courseId: number) {
        return this.http.get(this.courseCreatorURL + "/get-topics/" + courseId); 
    }
    

    // Get all required information about the selected topic
    getTopicById(topicId: number){
        return this.http.get(this.courseCreatorURL + "/get-topic-by-id/" + topicId);
    }


    // Gets a list of all courses that a Users teaches (both live courses & courses that are still in creation)
    getTeacherCourseList() {
        let token = this.authService.GetJwtToken();
        return this.http.get(this.usersDashboardURL + "/get-teacher-dashboard", {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        });
    }
    /**
     * Method called from generate-course component when the user creates the course for first time.
     * @param courseName 
     */
    createCourse(courseName: string){
        var body = {
            CourseName: courseName
        }
        let token = this.authService.GetJwtToken();
        console.log(token)
        return this.http.post(this.courseCreatorURL + "/new-course", body, {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        
        }).pipe(catchError(this.handleError));
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


    // Method to create a new topic in course creator
    addTopic(courseId: number, name: string, topicOrder: number){
        var body = {
            CourseId: courseId,
            TopicName: name,
            TopicOrder: topicOrder
        }
        return this.http.post(this.courseCreatorURL + "/new-topic", body)
    }


    // Method used to create new Lesson in course creator
    createNewLesson(topicId: number, lessonName: string, lessonOrder: number) {
        var body = {TopicId: topicId, LessonName: lessonName, LessonOrder: lessonOrder};
        return this.http.post(this.courseCreatorURL + "/new-lesson", body);
    }


    // Update the topic name & description
    updateTopic(topicId: number, topicName: string, topicDescription: string){
        var body = {TopicId: topicId, TopicName: topicName, TopicDesc: topicDescription};
        return this.http.put(this.courseCreatorURL + "/update-topic/" + topicId, body);
    }


    updateLessonName(id: number, newLessonName: string){
        var body = {LessonName: newLessonName}
        return this.http.put(this.courseCreatorURL + '/update-lesson-name/' + id, body);
    }


    updateLessonVideo(id: number, videBase64: string, fileName: string){
        var body = {
            LessonVideoUrl: videBase64,
            VideoFileName: fileName
        };
        return this.http.put(this.courseCreatorURL + '/update-video/' + id, body);
    }

    uploadImage(formData: FormData){
        console.log(formData)
        //formData.append('Name', caption);
        return this.http.post(this.courseCreatorURL + "/upload", formData);
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
        return this.http.post(this.courseCreatorURL + "/upload2", body).subscribe(
            (response) => {
                //this.route.navigate(['/home']);
            },
            err => {
              //this.error = err.error;
              console.log(err.error);
            }
          );
    }

    

    deleteTopic(id: number){
        return this.http.delete(this.courseCreatorURL + '/delete-topic/' + id);
    }

    

    

    
    
}

    
