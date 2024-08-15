import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { debounceTime, Subject } from 'rxjs';

@Component({
  selector: 'app-search-text',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    FormsModule,
    MatButtonModule,
  ],
  templateUrl: './search-text.component.html',
  styleUrl: './search-text.component.scss',
})
export class SearchTextComponent implements OnInit {
  searchText: string = '';

  @Output('search')
  onSearch: EventEmitter<string> = new EventEmitter<string>();

  searcher: Subject<string> = new Subject();

  ngOnInit(): void {
    this.searcher.pipe(debounceTime(100)).subscribe((text) => {
      this.onSearch.emit(text);
    });
  }

  search(): void {
    this.searcher.next(this.searchText);
  }

  clear(): void {
    this.searchText = '';
    this.searcher.next(this.searchText);
  }
}
