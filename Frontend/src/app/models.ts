export interface currencyModel {
  date: Date;
  source: string;
  target: string;
  value: number;
  result: number;
}


export interface ResponseDto<T> {
  messageToClient: string;
  responseData: T;
}
