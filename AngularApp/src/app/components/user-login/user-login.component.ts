import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {
  submitted: boolean;
  returnUrl: string;
  public loginForm: FormGroup;
  constructor(private authService: AuthService,
              private http: HttpClient,
              private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.createForm();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  createForm(){
    this.loginForm = this.fb.group({
      email: [null,Validators.required],
      password: [null,Validators.required]
    })
  }

  get email(){
    return this.loginForm.get('email');
  }

  get password(){
    return this.loginForm.get('password');
  }

  onLogin(){
    this.submitted = true;

    if (!this.loginForm.valid){
      return;
    }

    var user = this.loginForm.value;

    if (user.login == undefined){
      user.login = user.email;
    }

    this.authService.logIn(user).subscribe(x => {
      this.router.navigateByUrl('/movies');
    },err => alert("Wrong email or password!"));

    this.loginForm.reset();
  }
}
