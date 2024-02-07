import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(
    public userService: UserService,
    private router: Router,
    private messageService: MessageService
  ) {}

  login() {
    this.userService
      .checkIfUserIsRegistered(this.username, this.password)
      .subscribe((response: boolean) => {
        if (response) {
          localStorage.setItem('username', this.username);
          this.router.navigate(['/home']);
        } else {
          this.messageService.add({
            severity: 'error',
            summary: 'User not found',
            sticky: true,
            detail: 'Please register first',
          });
        }
      });
  }
}
