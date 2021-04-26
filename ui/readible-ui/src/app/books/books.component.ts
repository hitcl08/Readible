import { Component, OnInit } from '@angular/core';
import { BookService } from '../services/book.service';
import { Book } from '../models/book';
import { AppState } from '../app.state';
import { BookCard } from '../models/book-card';
import { NavEnum } from '../enums/nav.enum';
@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {

  constructor(
    private appState: AppState,
    private bookService: BookService
  ) { }
  public books: Book[] = [];
  public subscriptionBooks: Book[] = [];
  public bookCards: BookCard[] = [];


  ngOnInit(): void {
    this.appState.isLoading = true;
    this.appState.showToolbar = true;

    this.bookService.getBooks().subscribe(res => {
      this.books = res;

      this.bookService.getBooksBySubscriptionId(this.appState.subscriptionId).subscribe(subBooks => {
        this.subscriptionBooks = subBooks;

        this.books.forEach(book => {
          const bookCard = new BookCard(book, this.isBookAdded(book.id));
          this.bookCards.push(bookCard);
        });

        this.appState.isLoading = false;
      });
    });
  }

  private isBookAdded(bookId: number): boolean {
    const bookInSub = this.subscriptionBooks.find(sb => sb.id === bookId);
    return bookInSub ? true : false;
  }

}
