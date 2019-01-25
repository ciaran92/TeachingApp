import { Lesson } from "./lesson";

export class Topic{
    topicName: string;
    topicDescription: string;
    lessons: Lesson[];

    constructor(topicName, topicDescription, lessons){
        this.topicName = topicName;
        this.topicDescription = topicDescription;
        this.lessons = lessons;
    }
}