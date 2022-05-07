import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { SharedParamsService } from 'src/app/services/shared-params.service';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit, OnChanges {

  currentPage: number;
  @Input() totalPages: number = 0;

  @Output() goTo: EventEmitter<number> = new EventEmitter<number>();
  @Output() next: EventEmitter<number> = new EventEmitter<number>();
  @Output() previous: EventEmitter<number> = new EventEmitter<number>();

  constructor(private sharedParamsService: SharedParamsService) { }

  ngOnChanges(changes: SimpleChanges): void {
  }

  ngOnInit(): void {
    this.currentPage = this.sharedParamsService.pageNumber;
  }

  onGoTo(page: number){
    this.goTo.emit(page);
  }

  onNext(){
    this.currentPage++;
    this.next.emit(this.currentPage);
  }

  onPrevious(){
    this.currentPage--;
    this.previous.emit(this.currentPage);
  }
}
