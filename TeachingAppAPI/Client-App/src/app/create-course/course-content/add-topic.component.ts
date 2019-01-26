import { Component, OnInit, Input } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { Lesson } from './lesson';
import { Topic } from './topic';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'add-topic',
  templateUrl: './add-topic.component.html',
  styleUrls: ['../create-course.component.css', './course-content.component.css']
})
export class AddTopicComponent implements OnInit {

    lessons: Lesson[] = [];
    @Input() courseId: any;

    constructor(private courseService: CreateCourseService, private router: ActivatedRoute, private route: Router) { }

    ngOnInit() {
        console.log("parentId" + this.router.parent.snapshot.paramMap.get('id'))
        console.log("myId" + this.router.snapshot.paramMap.get('id'))
        if(sessionStorage.getItem("user_lessons") != null){
            console.log("lessons have been added");
            this.lessons = JSON.parse(sessionStorage.getItem("user_lessons"));
        }
        else {
            console.log("no topics added yet");
            this.lessons.push(new Lesson("Sample", null, null));
        }
    }

    addTopics(name: string, desc: string){
        if(name.length > 0) {
          this.courseService.addTopic(this.getCourseId(), name, desc).subscribe(
            (response) => {
                //location.reload();
                this.route.navigate(['create-course/content', this.getCourseId()]);
                console.log("topic added");
            },
            err => {
              //this.error = err.error;
              console.log(err.error);
            }
          );
        } 
        else {
          alert("Please enter a name for the topic");
        }        
    }

    // Cancel creating the topic and go back to course-content page
    cancel(){
        this.route.navigate(['create-course/content', this.getCourseId()]);
    }

    // get the current course Id from the route params of the parent component (course-content)
    getCourseId(){
        return parseInt(this.router.parent.snapshot.paramMap.get('id'));
    }
}
