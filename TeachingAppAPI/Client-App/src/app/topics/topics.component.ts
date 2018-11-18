import { Component, OnInit } from '@angular/core';
import { TopicService } from '../services/topic.service';

@Component({
  selector: 'app-topics',
  templateUrl: './topics.component.html',
  styleUrls: ['./topics.component.css']
})
export class TopicsComponent implements OnInit {

  private courseTopics : any;
  constructor(private topicService: TopicService) { }

  ngOnInit() {
    this.topicService.getTopics().subscribe((response) => {this.courseTopics = response})
  }

}
