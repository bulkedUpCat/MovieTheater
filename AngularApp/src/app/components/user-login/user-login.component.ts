import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NotifierService } from 'src/app/services/notifier.service';
import { SharedParamsService } from 'src/app/services/shared-params.service';
import { ForgotPasswordComponent } from '../dialogs/forgot-password/forgot-password.component';


@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {
  submitted: boolean;
  returnUrl: string;
  loaded: boolean = true;
  public loginForm: FormGroup;
  constructor(private authService: AuthService,
              private sharedParams: SharedParamsService,
              private notifier: NotifierService,
              private fb: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private dialog: MatDialog) { }

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
    this.loaded = false;
    this.authService.logIn(user).subscribe(x => {
      this.router.navigateByUrl('/movies');
      this.loaded = true;
      this.sharedParams.userEmail = user.email;
      this.notifier.showNotification("You've successfully logged in!", 'Ok', 'success');
    },err => {
      this.loaded = true;
      this.notifier.showNotification(err.error, 'OK','error');
    });

    this.loginForm.reset();
  }

  onForgotPassword(){
    this.dialog.open(ForgotPasswordComponent,{
      height: '210px',
      width: '400px'
    });
  }
}
