import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { VideoTestService } from '../services/video-test.service';

@Component({
  selector: 'app-video-test',
  templateUrl: './video-test.component.html',
  styleUrls: ['./video-test.component.css']
})
export class VideoTestComponent implements OnInit {
  title: 'video-list';
  videolist = [
    {
      name: "item 1",
      slug: "item-1",
      embed: 'HBsZbL-Akms',
    },
    {
      name: "item 2",
      slug: "item-2",
      embed: 'Lm38Ojh61lY',
    },
    {
      name: "item 3",
      slug: "item-3",
      embed: 'vz-wzsCmuCs',
    }
  ];


  courseDescVid: any;
  courseDescVidResponse: any[];


 
  constructor(private sanitizer: DomSanitizer, private videoTestService: VideoTestService){}

  ngOnInit() {
    this.courseDescVid = 1;
    this.videoTestService.getCourseVideoDesc(this.courseDescVid).subscribe((response) => {
      this.courseDescVidResponse = response;
    });
  }

  getEmbedURL(item: any){
    return this.sanitizer.bypassSecurityTrustResourceUrl('https://www.youtube.com/embed/' + item.embed)
  }

}
