import { Component, OnInit } from '@angular/core';
import { IAddVehicleRequest } from '../../models/IAddVehicleRequest';
import { VehicleService } from '../../services/vehicle.service';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-add-vehicle',
  templateUrl: './add-vehicle.component.html',
  styleUrl: './add-vehicle.component.css',
  providers: [MessageService],
})
export class AddVehicleComponent implements OnInit {
  model: string = '';
  brand: string = '';
  type: string = '';
  licensePlate: string = '';
  username: string = '';
  vehicleTypeList: string[] = ['GPL', 'Electric', 'Automobile', 'Motocycle'];
  selectedType: any = null;

  constructor(
    private vehicleService: VehicleService,
    private messageService: MessageService,
    public ref: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig
  ) {}

  ngOnInit(): void {
    this.username = this.dialogConfig.data;
  }

  clearFields() {
    this.selectedType = null;
    this.model = '';
    this.brand = '';
    this.type = '';
    this.licensePlate = '';
  }

  isLicensePlateValid(inputString: string): boolean {
    const regexPattern = /^([A-Z]{2}-\d{2}-\d{2}|\d{2}-\d{2}-[A-Z]{2})$/;
    return regexPattern.test(inputString);
  }

  addVehicleToUser() {
    var newVehicle: IAddVehicleRequest = {
      licensePlate: this.licensePlate,
      brand: this.brand,
      model: this.model,
      type: this.selectedType,
      username: this.username,
    };
    this.vehicleService.addVehicle(newVehicle).subscribe(
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
  }
}
