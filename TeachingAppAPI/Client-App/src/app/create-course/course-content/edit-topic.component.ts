import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Lesson } from './lesson';

@Component({
  selector: 'edit-topic',
  templateUrl: './edit-topic.component.html',
  styleUrls: ['../create-course.component.css', './edit-topic.component.css']
})
export class EditTopicComponent implements OnInit {

  private topicId: number;
  private topic: any;
  private topicName: string;
  private topicDescription: string;
  private lessons: Lesson[];
  private filename: string = "No file selected";
  image64: string;

  constructor(private courseService: CreateCourseService, private router: ActivatedRoute, private route: Router) { }

  ngOnInit() {
    this.topicId = this.getTopicId();
    this.displayTopic();
    
  }

  /**
   * Method called from the ngOnInit() when page loads.
   * Gets all the information about the specific topic which was chosen to be edited.
   * sets the private variable topic = response
   */
  displayTopic(){
    this.courseService.getTopicById(this.topicId).subscribe((response) => 
    {
      this.topic = response;
      this.topicName = this.topic.topicName;
      this.topicDescription = this.topic.topicDesc;
      this.lessons = this.topic.lesson as Lesson[];
    }, err => {
      console.log("error: " + err);
    });
  }

  addLesson(){
    let index = this.lessons.length + 1
    let newLesson: Lesson = new Lesson(null, null, index, null, null, null, null);
    this.lessons.push(newLesson)
  }

  createNewLesson(lessonName: string, index: number){
    this.courseService.createNewLesson(this.getTopicId(), lessonName, index + 1).subscribe((response) => 
    {
      console.log(JSON.stringify(response))
      this.lessons[index] = response as Lesson
      //console.log("success: updated lesson name: " + JSON.stringify(this.lessons));
      //this.hideLessonEditView(index);
    }, err => {
      console.log("error: " + err);
    });
  }

  editLesson(index: number){
    
  }

  handleFileInput(event, index){
    let lessonId = this.lessons[index].lessonId;
    let file = event.target.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file);

    // upload new video
    reader.onload = (): void => {
      this.image64 = reader.result as string;
      //console.log(this.image64);
      const res: string = (reader.result as string).match(
        /.+;base64,(.+)/
          )[1];
          sessionStorage.setItem("thumbnail_img", res);
          //console.log(res)
          this.courseService.updateLessonVideo(lessonId, res, file.name).subscribe((response) => 
          {
            console.log("success: updated lesson name");
            //this.hideLessonEditView(index);
          }, err => {
            console.log("error: " + err);
          });
          // upload image here?
    };
    
    reader.onerror = function (error) {
      console.log('Error: ', error);
    };
       
  }

  
  /**
   * Method used to update the Lesson Name
   */
  updateLessonName(index: number, newLessonName: string){
    let id = this.lessons[index].lessonId;
    this.courseService.updateLessonName(id, newLessonName).subscribe((response) => 
    {
      console.log("success: updated lesson name");
      this.hideLessonEditView(index);
    }, err => {
      console.log("error: " + err);
    });
  }


  /**
   * Method used to update the Lesson Video
   */
  /*uploadLessonVideo(id:number, videoFile:string){
    this.courseService.updateLessonVideo(1006, videoFile).subscribe((response) => 
    {
      console.log("success: updated lesson video");
    }, err => {
      console.log("error: " + err);
    });
  }*/

  cancelUpdate(index: number){
    let id = this.lessons[index].lessonId;
    let name = document.getElementById("lesson-name-" + index) as HTMLInputElement;
    name.value = this.lessons[index].lessonName;
    this.hideLessonEditView(index);
  }

  hideLessonEditView(index: number){
    let name = document.getElementById("lesson-name-" + index) as HTMLInputElement;
    let confirmBtn = document.getElementById("confirm-btn-" + index)
    let cancelBtn = document.getElementById("cancel-btn-" + index)
    let editBtn = document.getElementById("edit-btn-" + index)
    let deleteBtn = document.getElementById("delete-btn-" + index)

    // show edit/delete buttons
    editBtn.style.display = "block";
    deleteBtn.style.display = "block";

    // hide tick/cross buttons
    confirmBtn.style.display = "none";
    cancelBtn.style.display = "none";

    name.readOnly = true;
    document.getElementById("lesson-content-" + index).style.display = "none";
  }


  displayLessonEditView(index: number){
    let name = document.getElementById("lesson-name-" + index) as HTMLInputElement;
    let confirmBtn = document.getElementById("confirm-btn-" + index)
    let cancelBtn = document.getElementById("cancel-btn-" + index)
    let editBtn = document.getElementById("edit-btn-" + index)
    let deleteBtn = document.getElementById("delete-btn-" + index)

    // hide edit/delete buttons
    editBtn.style.display = "none";
    deleteBtn.style.display = "none";

    // show tick/cross buttons
    confirmBtn.style.display = "block";
    cancelBtn.style.display = "block";

    name.readOnly = false;
    name.focus();
    document.getElementById("lesson-content-" + index).style.display = "flex";
  }

  getTopicId(){
    return parseInt(this.router.snapshot.paramMap.get('id'));
  }

  /* Method called when cancel button is clicked. Brings user back to content section */
  cancel(){
    let courseId = this.router.parent.snapshot.paramMap.get('id');
    this.route.navigate(['create-course/content/' + courseId]);
  }

  saveChanges(topicName: string, topicDescription: string){
    let courseId = this.router.parent.snapshot.paramMap.get('id');
    console.log("topic name: " + topicName, topicDescription)
    this.courseService.updateTopic(this.getTopicId(), topicName, topicDescription).subscribe((response) => 
    {
      console.log("success: updated topic");
      this.route.navigate(['create-course/content/' + courseId]);
    }, err => {
      console.log("error: " + err);
    });
  }

}
