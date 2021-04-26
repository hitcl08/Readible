import { Book} from './book';

export class BookCard {
  constructor(
    public book: Book,
    public isAdded: boolean
  ){}
}
