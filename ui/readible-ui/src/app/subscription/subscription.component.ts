import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppState } from '../app.state';
import { Book } from '../models/book';
import { BookService } from '../services/book.service';
import { SubscriptionService } from '../services/subscription.service';

@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.scss']
})
export class SubscriptionComponent implements OnInit, OnChanges {
  @Input() public books: Book[] = []
  public showPopup = false;
  public subscriptionHasBooks = true;
  constructor(
    private snackBar: MatSnackBar,
    private appState: AppState,
    private subscriptionService: SubscriptionService,
    private bookService: BookService) {
  }

  public ngOnInit(): void {
    this.loadSubscription();
  }

  public ngOnChanges(): void{
    this.subscriptionHasBooks = this.books.length > 0;
  }

  public deleteBookFromSubscription(book: Book): void {
    this.bookService.deleteBookFromSubscription(book.id, this.appState.subscriptionId).subscribe(isDeleted => {
      if (isDeleted) {
        this.books = this.books.filter(b => b.id !== book.id);
        this.openPopup(`'${book.name}' has been removed from your subscription`);
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

  private loadSubscription(): void {
    this.subscriptionService.getUserSubscription(this.appState.userId).subscribe(res => {
      this.bookService.getBooksBySubscriptionId(res.id).subscribe((res) => {
        res.forEach(el => {
          const book = new Book(el.id, el.name, el.author, el.description, el.rating, el.imageUrl);
          this.books.push(book);
        });
        this.subscriptionHasBooks = this.books.length > 0;
      });
    });
  }
}
