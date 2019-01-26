import { Lesson } from "./lesson";

export class Topic{
    topicId: number;
    topicName: string;
    topicOrder: number;
    newTopic: boolean;

    constructor(topicId, topicName, topicOrder, newTopic){
        this.topicId = topicId;
        this.topicName = topicName;
        this.topicOrder = topicOrder;
        this.newTopic = newTopic;
    }
}