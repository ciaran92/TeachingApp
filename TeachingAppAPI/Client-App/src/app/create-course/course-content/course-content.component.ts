import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { Topic } from './topic';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-course-content',
  templateUrl: './course-content.component.html',
  styleUrls: ['../create-course.component.css', './course-content.component.css']
})
export class CourseContentComponent implements OnInit {

  htmlContent: string;
  filesToUpload: File;
  currentStep: number;
  topics: Object;

  addTopic: boolean = false;

  showAddVideo: boolean = false;
  showAddArticle: boolean = false;

  videoToUpload_base64: string;
  topicsToAdd: Topic[];
  courseId: any;

  constructor(private courseService: CreateCourseService, private router: ActivatedRoute, private route: Router) { }

  ngOnInit() {
    console.log("content")
    this.courseService.currentStep.subscribe(step => this.currentStep = step);
    this.courseService.changeStep(4);
    //this.topics = this.courseService.getTopics(this.getCourseId());
    this.displayTopicsList();

    this.courseId = this.getCourseId;
  }

  displayTopicsList(){
    this.courseService.getTopics(this.getCourseId()).subscribe((response) => 
    {
      this.topicsToAdd = response[0].topics;
      console.log("topics list: " + JSON.stringify(response[0].topics));
    }, err => {
      console.log("error: " + err.error);
    });
  }

  createTopic(topicName: string) {
    this.addTopic = true;
  }

  editTopic(i: number) {
    
  }

  doSomething(topicType: string){
    if(topicType === 'video'){
      console.log("clicked | video");
      this.showAddVideo = true;
    }
    if(topicType === 'article'){
      console.log("clicked | article");
      this.showAddArticle = true;
    }
    
  }

  handleFileInput(event){
    let file = event.target.files[0];
    console.log(file);
    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (): void => {
      this.videoToUpload_base64 = (reader.result as string).match(
        /.+;base64,(.+)/
          )[1];
    };
    reader.onerror = function (error) {
      console.log('Error: ', error);
    };
  }

  cancel(){
    this.addTopic = false;
    this.showAddVideo = false;
    this.showAddArticle = false;
  }

  goBack(){
    this.route.navigate(['create-course/thumbnail', this.getCourseId()]);
  }

  getCourseId(){
    return parseInt(this.router.snapshot.paramMap.get('id'));
  }
}
