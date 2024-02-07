import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PaymentService } from '../../services/payment.service';
import { IPaymentMethodTokenRequest } from '../../models/IPaymentMethodTokenRequest';
import { IAddPaymentMethodRequest } from '../../models/IAddPaymentMethodRequest';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-add-payment',
  templateUrl: './add-payment.component.html',
  styleUrl: './add-payment.component.css',
  providers: [MessageService],
})
export class AddPaymentComponent implements OnInit {
  expirationDate: string = '';
  cardNumber: string = '';
  cvv: number | undefined;
  fullName: string = '';
  username: string = '';

  constructor(
    private paymentService: PaymentService,
    private messageService: MessageService,
    public ref: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig
  ) {}

  ngOnInit(): void {
    this.username = this.dialogConfig.data;
  }

  clearFields() {
    this.cardNumber = '';
    this.expirationDate = '';
    this.cvv = undefined;
    this.fullName = '';
  }

  addPaymentMethodToUser() {
    if (this.cardNumber && this.cvv) {
      this.cardNumber = this.cardNumber.replace(/\s/g, '');

      console.log(this.cardNumber);
      var dateFormated = new Date(this.expirationDate);
      dateFormated.setHours(1);
      const numberAsString: string = this.cardNumber.toString();
      const last4Digits: number = parseInt(numberAsString.slice(-4));
      var newTokenRequest: IPaymentMethodTokenRequest = {
        cardNumber: parseInt(this.cardNumber),
        cvv: this.cvv,
        fullName: this.fullName,
        expirationDate: dateFormated,
      };
      this.paymentService.generateToken(newTokenRequest).subscribe(
        (token: any) => {
          var newPaymentRequest: IAddPaymentMethodRequest = {
            lastFourDigits: last4Digits,
            expirationDate: dateFormated,
            fullName: this.fullName,
            token: token.token,
            username: this.username,
          };
          this.paymentService.addPaymentMethod(newPaymentRequest).subscribe(
            (data: any) => {
              this.ref.close(data);
            },
            (error: HttpErrorResponse) => {
              this.messageService.add({
                severity: 'error',
                summary: 'Something went wrong',
                sticky: true,
                detail: error.error.title,
              });
              this.clearFields();
            }
          );
        },
        (error: HttpErrorResponse) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Something went wrong',
            sticky: true,
            detail: error.error.title,
          });
        }
      );
    }
  }
}
