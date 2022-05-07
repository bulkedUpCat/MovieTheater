import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MovieService } from 'src/app/services/movie.service';
import { NotifierService } from 'src/app/services/notifier.service';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css']
})
export class AddMovieComponent implements OnInit {
  public movieForm: FormGroup;

  constructor(private fb: FormBuilder,
    private movieService: MovieService,
    private notifier: NotifierService) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(){
    this.movieForm = this.fb.group({
      title: [null,[Validators.required]],
      genres: [null,[Validators.required]],
      description: [null,[Validators.required]],
      year: [null,[Validators.required]],
      director: [null,[Validators.required]],
      runtime: [null,[Validators.required]]
    })
  }

  get title(){
    return this.movieForm.get('title');
  }

  get genres(){
    return this.movieForm.get('genres');
  }

  get description(){
    return this.movieForm.get('description');
  }

  get year(){
    return this.movieForm.get('year');
  }

  get director(){
    return this.movieForm.get('director');
  }

  onSubmit(){
    if (!this.movieForm.valid){
      this.notifier.showNotification('All input fields are required!','OK','error');
      return;
    }

    const movie = this.movieForm.value;

    let genres = this.genres.value.split(',');

    movie.genres = genres;

    this.movieService.addMovie(movie).subscribe(m => {
      this.notifier.showNotification('Movie added successfully', 'OK', 'success');
    },
    err => {
      this.notifier.showNotification('An error occured', 'OK', 'error');
    })
  }
}
