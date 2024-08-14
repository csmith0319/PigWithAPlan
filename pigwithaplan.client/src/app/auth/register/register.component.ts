import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService
  ) {}

  private subscription = new Subscription();

  ngOnInit(): void {}

  ngOnDestroy(): void {
    if (!this.subscription.closed) {
      this.subscription.unsubscribe();
    }
  }

  getDetails(_newUser: any) {
    _newUser.Username = this.username.value;
    _newUser.Password = this.getPassword(false).value;
    _newUser.Email = this.email.value;
  }

  register(): void {
    let _newUser: any = {};
    this.getDetails(_newUser);

    this.subscription.add(
      this.authService.register(_newUser).subscribe({
        next: (response) => {},
        error: (error) => {},
      })
    );
  }

  submit(): void {
    if (this.formRegister.valid) {
      this.register();
    } else {
      console.log('Invalid form.');
    }
  }

  formRegister = this.formBuilder.group({
    username: this.formBuilder.nonNullable.control<string>('', {
      validators: Validators.required,
    }),
    email: this.formBuilder.nonNullable.control<string>('', {
      validators: Validators.required,
    }),
    password: this.formBuilder.nonNullable.control<string>('', {
      validators: Validators.required,
    }),
    passwordConfirm: this.formBuilder.nonNullable.control<string>('', {
      validators: Validators.required,
    }),
  });

  get email(): FormControl {
    return this.formRegister.controls.email;
  }

  get username(): FormControl {
    return this.formRegister.controls.username;
  }

  getPassword(confirm: boolean): FormControl {
    if (confirm) return this.formRegister.controls.passwordConfirm;
    else return this.formRegister.controls.password;
  }
}
