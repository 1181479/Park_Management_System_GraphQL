import { Component, OnInit } from '@angular/core';
import { IVehicle } from '../../models/IVehicle';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AddVehicleComponent } from '../add-vehicle/add-vehicle.component';
import { VehicleService } from '../../services/vehicle.service';
import { AddPaymentComponent } from '../add-payment/add-payment.component';
import { PaymentService } from '../../services/payment.service';
import { IPaymentMethod } from '../../models/IPaymentMethod';
import { MessageService } from 'primeng/api';
import { UserService } from '../../services/user.service';
import { IMovement } from '../../models/IMovement';
import { ParkyWalletService } from '../../services/parky-wallet.service';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrl: './my-account.component.css',
  providers: [DialogService, MessageService],
})
export class MyAccountComponent implements OnInit {
  ref: DynamicDialogRef | undefined;

  vehicleList: IVehicle[] = [];
  fullName: string = '';
  username: string | null = '';
  email: string = '';
  currentBalance: number = 0;
  paymentMethodList: IPaymentMethod[] = [];
  movementsList: IMovement[] = [];

  constructor(
    public dialogService: DialogService,
    private vehicleService: VehicleService,
    private paymentService: PaymentService,
    private messageService: MessageService,
    private userService: UserService,
    private parkyWalletService: ParkyWalletService
  ) {}

  ngOnInit() {
    this.username = localStorage.getItem('username');
    this.userService
      .getUserByUsername(this.username!)
      .subscribe((result: any) => {
        this.fullName = result.name;
        this.email = result.email;
      });
    this.getListVehicles();
    this.getListPaymentMethod();
    this.getTransactionList();
  }

  getListVehicles() {
    this.vehicleService
      .getListVehicleFromUser(this.username!)
      .subscribe((data: any) => (this.vehicleList = data));
  }

  getListPaymentMethod() {
    this.paymentService
      .getListPaymentMethodFromUser(this.username!)
      .subscribe((data: any) => {
        this.paymentMethodList = data;
      });
  }

  getTransactionList() {
    this.parkyWalletService
      .getListTransactionsFromUser(this.username!)
      .subscribe((data: any) => {
        if (data) {
          this.movementsList = data.movements;
          this.movementsList.sort(
            (a, b) => new Date(b.date).getTime() - new Date(a.date).getTime()
          );
          this.currentBalance = data.currentBalance;
        }
      });
  }

  submitPersonalInfoChanges() {}

  showVehicleForm() {
    this.ref = this.dialogService.open(AddVehicleComponent, {
      header: 'Add a New Vehicle',
      data: this.username,
    });
    this.ref.onClose.subscribe((vehicleCreated: IVehicle) => {
      if (vehicleCreated) {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          sticky: true,
          detail:
            'Vehicle ' + vehicleCreated.licensePlate + ' created successfully',
        });
        this.getListVehicles();
      }
    });
  }

  showPaymentMethodForm() {
    this.ref = this.dialogService.open(AddPaymentComponent, {
      header: 'Add New Payment Method',
      data: this.username,
    });
    this.ref.onClose.subscribe((paymentMethodCreated: IPaymentMethod) => {
      if (paymentMethodCreated) {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          sticky: true,
          detail:
            'Payment Method added successfully. Card Number: ' +
            paymentMethodCreated.lastFourDigits,
        });
        this.getListPaymentMethod();
      }
    });
  }
}
