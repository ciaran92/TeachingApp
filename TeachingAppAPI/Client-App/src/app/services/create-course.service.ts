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
    
    public EnrolledCoursesList: any;
    private coursesURL = "http://localhost:52459/api/courses";
    private topicsURL = "http://localhost:52459/api/topics"
    private lessonsURL = "http://localhost:52459/api/lessons"
    public course: Course = new Course(); // ?? This may be a problem for testing ??

    constructor(private http: HttpClient, private route: Router){}

    uploadImage(formData: FormData){
        console.log(formData)
        //formData.append('Name', caption);
        return this.http.post(this.coursesURL + "/upload", formData);
    }

    // Get all the topics for a course (returns the topicId, topicName & topicOrder)
    getTopics(courseId: number) {
        return this.http.get(this.topicsURL + "/get-topics/" + courseId); 
    }

    // Get all required information about the selected topic
    getTopicById(topicId: number){
        return this.http.get(this.topicsURL + "/get-topic-by-id/" + topicId);
    }

    addTopic(courseId: number, name: string, topicOrder: number){
        //this.topics.push(newTopic);
        //sessionStorage.setItem("user_topics", JSON.stringify(this.topics));
        var body = {
            CourseId: courseId,
            TopicName: name,
            TopicOrder: topicOrder
        }
        return this.http.post(this.topicsURL, body)
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
        return this.http.post(this.coursesURL + "/upload2", body).subscribe(
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
        return this.http.post(this.coursesURL + "/new-course", body);
    }

    deleteTopic(id: number){
        return this.http.delete(this.topicsURL + '/' + id);
    }

    updateLessonVideo(id: number, videBase64: string, fileName: string){
        var body = {
            LessonVideoUrl: videBase64,
            VideoFileName: fileName
        };
        return this.http.put(this.lessonsURL + '/update-video/' + id, body);
    }

    updateLessonName(id: number, newLessonName: string){
        var body = {LessonName: newLessonName}
        return this.http.put(this.lessonsURL + '/update-name/' + id, body);
    }

    createNewLesson(topicId: number, lessonName: string, lessonOrder: number) {
        var body = {TopicId: topicId, LessonName: lessonName, LessonOrder: lessonOrder};
        return this.http.post(this.lessonsURL, body);
    }

    updateTopic(topicId: number, topicName: string, topicDescription: string){
        var body = {TopicId: topicId, TopicName: topicName, TopicDesc: topicDescription};
        return this.http.put(this.topicsURL + "/" + topicId, body);
    }
    
}

    
