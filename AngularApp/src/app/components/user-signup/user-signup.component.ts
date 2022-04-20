import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-signup',
  templateUrl: './user-signup.component.html',
  styleUrls: ['./user-signup.component.css']
})
export class UserSignupComponent implements OnInit {
  submitted: boolean = false;
  signupForm: FormGroup;
  constructor(private fb: FormBuilder,
              private authService: AuthService,
              private router: Router) { }

  ngOnInit(): void {
    this.createForm();
  }

  onSignup(){
    this.submitted = true;

    if(!this.signupForm.valid){
      return;
    }

    const user = this.signupForm.value;
    console.log(user);
    this.authService.signUp(user).subscribe(x => this.router.navigate(['/login']),
                            err => console.log(err));
  }

  createForm(){
    this.signupForm = this.fb.group({
      login: [null,[Validators.required,Validators.minLength(4)]],
      password: [null,[Validators.required,Validators.minLength(6)]],
      email: [null,[Validators.required,Validators.email]],
      name: [null,[Validators.required,Validators.minLength(4)]],
      confirmPassword: [null,[Validators.required]],
      age: [0,[Validators.required,Validators.min(1),Validators.max(113)]]
    });
  }

  get f(){
    return this.signupForm.controls;
  }
}
