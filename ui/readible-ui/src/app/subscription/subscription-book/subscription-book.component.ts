import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AppState } from 'src/app/app.state';
import { Book } from 'src/app/models/book';
import { BookService } from 'src/app/services/book.service';
import { SubscriptionService } from 'src/app/services/subscription.service';

@Component({
  selector: 'app-subscription-book',
  templateUrl: './subscription-book.component.html',
  styleUrls: ['./subscription-book.component.scss']
})
export class SubscriptionBookComponent implements OnInit {

  @Input() private rating: number;
  @Input() private starCount = 5;

  @Input() public book: Book;

  public ratingArr = [];
  constructor(
    private snackBar: MatSnackBar,
    private appState: AppState,
    private subscriptionService: SubscriptionService,
    private bookService: BookService) {
  }


  public ngOnInit(): void {
    console.log(this.book)
    this.rating = this.book.rating;

    this.appState.showToolbar = true;

    for (let index = 0; index < this.starCount; index++) {
      this.ratingArr.push(index);
    }
  }


  public showIcon(index: number): string {
    if (this.rating >= index + 1) {
      return 'star';
    } else {
      return 'star_border';
    }
  }
}
