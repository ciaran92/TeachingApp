export class QuizInstanceAnswer {
    QuizInstanceId: number;
    QuestionId: number;
    AnswerId: number;
    AppUserAnswer: string; // Not sure about this yet. Probably useful once text answers are required.
    AppUserAnswerDateTime: Date;  // this can/should be set in the api? 

    constructor(
      QuizInstanceId: number, 
    QuestionId: number,
    AnswerId: number,
    AppUserAnswer: string, // Not sure about this yet. Probably useful once text answers are required.
    AppUserAnswerDateTime: Date  // this can/should be set in the api? 
      
    ){
      this.QuizInstanceId = QuizInstanceId;
      this.QuizInstanceId = QuestionId;
      this.AnswerId = AnswerId;
      this.AppUserAnswer = AppUserAnswer;
      this.AppUserAnswerDateTime = AppUserAnswerDateTime;
    }

  }