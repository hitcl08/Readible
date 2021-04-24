import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppState } from 'src/app/app.state';
import { Book } from 'src/app/models/book';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss']
})
export class BookCardComponent implements OnInit {

  public books: Book[];
  public isAdded: true;
  public showPopup = false;

  constructor(private bookService: BookService,
    private appState: AppState,
    private snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {
    this.bookService.getBooks().subscribe(res => {
      this.books = res;
    })
  }

  public onAddToSubscription (book: Book): void {
      this.bookService.addBookToSubscription(this.appState.subscriptionId, book.id).subscribe(res => {
        this.isAdded = res
        if (this.isAdded) {
          this.openPopup(`${book.name} has been added to your subscription`)
        }
      })
  }

  private openPopup(message: string): void {
    this.snackBar.open(message, 'X', {
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: ['popup']
    }).afterDismissed().subscribe(() => this.showPopup = false);
  }
}
