import { Component } from '@angular/core';
import { IRegisterUserRequest } from '../../models/IRegisterUserRequest';
import { UserService } from '../../services/user.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.css',
})
export class CreateAccountComponent {
  username: string = '';
  password: string = '';
  email: string = '';
  name: string = '';

  constructor(
    public userService: UserService,
    private messageService: MessageService,
    private router: Router
  ) {}

  clearFields() {
    this.username = '';
    this.password = '';
    this.email = '';
    this.name = '';
  }

  signUp() {
    var userRequest: IRegisterUserRequest = {
      username: this.username,
      password: this.password,
      email: this.email,
      name: this.name,
    };
    this.userService.registerUser(userRequest).subscribe(
      (data: any) => {
        if (data) {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            sticky: true,
            detail: 'User ' + data.username + ' registered successfully',
          });
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        } 
      },
      (error: HttpErrorResponse) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          sticky: true,
          detail: error.error.title,
        });
        this.clearFields();
      }
    );
  }
}
