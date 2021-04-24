import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AppState } from '../app.state';
import { Book } from '../models/book';
import { BookService } from '../services/book.service';
import { SubscriptionService } from '../services/subscription.service';

@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.scss']
})
export class SubscriptionComponent implements OnInit {
  @Input() public books: Book[] = []

  constructor(
    private snackBar: MatSnackBar,
    private appState: AppState,
    private subscriptionService: SubscriptionService,
    private bookService: BookService) {
  }

  public ngOnInit(): void {
    this.loadSubscription();
  }

  private loadSubscription(): void {
    this.subscriptionService.getUserSubscription(this.appState.userId).subscribe(res => {
      this.bookService.getBooksBySubscriptionId(res.id).subscribe((res) => {
        res.forEach(el => {
          const book = new Book(el.id, el.name, el.author, el.description, el.rating, el.imageUrl);
          this.books.push(book);
        });
        this.appState.isLoading = false;
      });
    });

  }
}
