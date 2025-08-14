export class PagerResponse<T> {
    items: Array<T>;
    totalRows!: number;
    pageNumber!: number;
  
    constructor() {
      this.items = new Array<T>();
    }
  }