import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppState } from 'src/app/app.state';
import { BookCard } from 'src/app/models/book-card';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss']
})
export class BookCardComponent implements OnInit {
  @Input() public books: BookCard[];

  public showPopup = false;

  constructor(
    private bookService: BookService,
    private appState: AppState,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void { }

  public onAddToSubscription(bookCard: BookCard): void {
    this.appState.isLoading = true;
    this.bookService.addBookToSubscription(this.appState.subscriptionId, bookCard.book.id).subscribe(res => {
      bookCard.isAdded = res;
      if (bookCard.isAdded) {
        this.appState.isLoading = false;
        this.openPopup(`'${bookCard.book.name}' has been added to your subscription`);
      }
    });
  }

  private openPopup(message: string): void {
    this.snackBar.open(message, 'X', {
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: ['popup']
    }).afterDismissed().subscribe(() => this.showPopup = false);
  }
}
