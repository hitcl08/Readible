import { Book } from './book';

export class Subscription {
  constructor(
    public id: number,
    public userId: number,
    public books: Book[]) { }
}
