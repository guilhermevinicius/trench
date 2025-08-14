export interface IApiResponse<T> {
    success: boolean;
    status: number;
    dateUtc: any;
    data: T;
    messages: Array<string>;
  }