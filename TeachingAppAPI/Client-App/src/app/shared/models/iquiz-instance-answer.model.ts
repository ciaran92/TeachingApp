export interface iQuizInstanceAnswer {
    QuizInstanceId: number;
    QuestionId: number;
    AnswerId: number;
    AppUserAnswer: string; // Not sure about this yet. Probably useful once text answers are required.
    AppUserAnswerDateTime: Date; // this can/should be set in the api? 

  }