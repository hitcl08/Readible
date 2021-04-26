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
  @Output() public onDeleteBookEvent = new EventEmitter<Book>();
  constructor(
    public appState: AppState
  ) { }


  public ngOnInit(): void {
    this.rating = this.book.rating;

    this.appState.showToolbar = true;

    for (let index = 0; index < this.starCount; index++) {
      this.ratingArr.push(index);
    }
  }

  public onDeleteBook(book: Book): void {
    this.onDeleteBookEvent.emit(book);
  }

  public onReadNowClick(): void {
    console.log('Once upon a time in a galaxy far far away...');
  }

  public showIcon(index: number): string {
    if (this.rating >= index + 1) {
      return 'star';
    } else {
      return 'star_border';
    }
  }

}
