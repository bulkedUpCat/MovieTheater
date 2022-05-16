import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MovieService } from 'src/app/services/movie.service';
import { NotifierService } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-create-report',
  templateUrl: './create-report.component.html',
  styleUrls: ['./create-report.component.css']
})
export class CreateReportComponent implements OnInit {
  loaded: boolean = true;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  public dialogRef: MatDialogRef<CreateReportComponent>,
  private movieService: MovieService,
  private notifier: NotifierService) { }

  ngOnInit(): void {
  }

  createReport(){
    this.loaded = false;
    this.movieService.createReport(this.data.params).subscribe( m => {
      this.notifier.showNotification('The report has been successfully sent to your email', 'Ok', 'success');
    }, err => {
      this.notifier.showNotification('Something went wrong','Ok','error');
    }, () => {
      this.loaded = true;
    })
  }

}
