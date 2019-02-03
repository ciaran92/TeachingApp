import { Lesson } from "./lesson";

export class Topic{
    topicId?: number;
    topicName: string;
    topicDesc: string;
    topicOrder: number;
    topicLessons: Lesson[];

    constructor(topicId, topicName, topicDesc, topicOrder, topicLessons){
        this.topicId = topicId;
        this.topicName = topicName;
        this.topicDesc = topicDesc;
        this.topicOrder = topicOrder;
        this.topicLessons = topicLessons;
    }
}