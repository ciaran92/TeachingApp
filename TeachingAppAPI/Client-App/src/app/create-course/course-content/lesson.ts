export class Lesson{

    lessonId: number;
    lessonName: string;
    lessonOrder: number;
    lessonArticle: string;
    lessonVideoUrl: string;
    videoFileName: string;
    s3VideoFileName: string;

    constructor(lessonId, lessonName, lessonOrder, lessonArticle, lessonVideoUrl, videoFileName, s3VideoFileName){
        this.lessonId = lessonId;
        this.lessonName = lessonName;
        this.lessonOrder = lessonOrder;
        this.lessonArticle = lessonArticle;
        this.lessonVideoUrl = lessonVideoUrl;
        this.videoFileName = videoFileName;
        this.s3VideoFileName = s3VideoFileName;
    }
}