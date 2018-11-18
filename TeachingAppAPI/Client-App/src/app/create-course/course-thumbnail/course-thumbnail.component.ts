import { Component, OnInit } from '@angular/core';
import { CreateCourseService } from 'src/app/services/create-course.service';
import { ReplaySubject } from 'rxjs';

@Component({
  selector: 'app-course-thumbnail',
  templateUrl: './course-thumbnail.component.html',
  styleUrls: ['../create-course.component.css']
})
export class CourseThumbnailComponent implements OnInit {

  currentStep: number;
  filesToUpload: File;
  constructor(private courseService: CreateCourseService) { }

  ngOnInit() {
    this.courseService.currentStep.subscribe(step => this.currentStep = step);
    this.courseService.changeStep(2);
  }

  handleFileInput(file: FileList){
    this.filesToUpload = file.item(0);

    this.courseService.course.CourseThumbnail = this.filesToUpload;
    //let base64Observable = new ReplaySubject<MSBaseReader>(1);
    var reader = new FileReader();
    reader.onload = (event:any) => {
      console.log("something happened");
    }
    var data = reader.readAsDataURL(this.filesToUpload);
    //sessionStorage.setItem("balls", data);
    //console.log("file: " + filesToUpload);
  }

  getBase64Image(img){
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;

    var dataURL = canvas.toDataURL('image/jpg');
    return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
  }

  dataURItoBlob(dataURI) {
    const byteString = atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([arrayBuffer], { type: 'image/jpeg' });    
    return blob;
 }

}
