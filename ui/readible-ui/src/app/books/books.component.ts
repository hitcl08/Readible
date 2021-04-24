import { Component, OnInit } from '@angular/core';
import { BookService } from '../services/book.service';
import { Book} from '../models/book';
import { AppState } from '../app.state';
@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {

  constructor(private appState: AppState) { }
  public books: Book[] = [];
  ngOnInit(): void {
    this.appState.showToolbar = true;

  }


}
