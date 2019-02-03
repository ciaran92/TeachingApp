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

  showAddVideo: boolean = false;
  showAddArticle: boolean = false;

  videoToUpload_base64: string;
  topics: Topic[];
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
      this.topics = response as Topic[];
      console.log("bruh: " + JSON.stringify(this.topics));
    }, err => {
      console.log("error: " + err.error);
    });
  }

  /**
   * Method to create a new topic locally in the topics array
   */
  addTopic(){
    let index = this.topics.length + 1
    let newTopic: Topic = new Topic(null, null, null, index, null)
    this.topics.push(newTopic)
  }

  /**
   * Called when the add button is clicked when creating a new topic
   * @param topicName takes in the name of the topic to be created
   * @param index takes in the position (index) of the topic in the topics array. adds 1 to this value to get the topics order value
   */
  createTopic(topicName: string, index: number) {
    console.log("index: " + index)
    this.courseService.addTopic(this.getCourseId(), topicName, index + 1).subscribe(
      (response) => {
          console.log("topic added: " + JSON.stringify(response));
          this.topics[index] = response as Topic;
      },
      err => {
        //this.error = err.error;
        console.log(err.error);
      }
    );
  }

  deleteTopic(index: number){
    let topicId = this.topics[index].topicId;
    this.courseService.deleteTopic(topicId).subscribe(
      (response) => {
        console.log(response);
      },
      err => {
        //this.error = err.error;
        console.log(err.error);
      }
    );
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

  editTopic(index: number) {
    let topicId = this.topics[index].topicId;
    this.route.navigate(['create-course/content/' + this.getCourseId() + '/edit-topic/' + topicId]);
  }

  cancel(){
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
