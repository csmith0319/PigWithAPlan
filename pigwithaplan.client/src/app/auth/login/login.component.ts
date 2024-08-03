import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit, OnDestroy {
  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  private subscription = new Subscription();

  ngOnInit(): void {}

  ngOnDestroy(): void {
    if (!this.subscription.closed) {
      this.subscription.unsubscribe();
    }
  }

  login() {
    this.authService.login(this.username.value, this.password.value).subscribe({
      next: (response) => {
        if (response.token) {
          this.router.navigate(['/budgets']);
        }
      },
      error: (error) => {
        console.error('Login failed', error);
      },
    });
  }

  googleLogin() {
    alert('Not yet implemented');
  }

  submit(): void {
    if (this.formLogin.valid) {
      this.login();
    } else {
      console.log('Invalid form.');
    }
  }

  formLogin = this.formBuilder.group({
    username: this.formBuilder.nonNullable.control<string>('', {
      validators: Validators.required,
    }),
    password: this.formBuilder.nonNullable.control<string>('', {
      validators: Validators.required,
    }),
  });

  get username(): FormControl {
    return this.formLogin.controls.username;
  }

  get password(): FormControl {
    return this.formLogin.controls.password;
  }
}
