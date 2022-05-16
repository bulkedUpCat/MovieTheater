import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  styleUrls: ['./video.component.css']
})
export class VideoComponent implements OnInit {
  @Input() videoSource: SafeResourceUrl;
  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    this.videoSource = this.sanitizer.bypassSecurityTrustResourceUrl(`${this.videoSource}`);
  }

}
