import { Component, OnInit } from '@angular/core';
import { MessageService } from '../services/message-service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrl: './message.component.css',
})
export class MessageComponent implements OnInit {
  constructor(private messageService: MessageService) {
    this.messageService
      .getParkingSpotsCount(this.parkName)
      .subscribe((response) => {
        this.availableAutomobileSpots = response.automobileCount;
        this.availableElectricSpots = response.electricCount;
        this.availableGPLSpots = response.gplCount;
        this.availableMotorcycleSpots = response.motocycleCount;
      });
  }

  availableMotorcycleSpots: number = 0;
  availableAutomobileSpots: number = 0;
  availableGPLSpots: number = 0;
  availableElectricSpots: number = 0;
  otherAmount: number = 0;
  parkyCoinSpent: number = 0;
  totalCost: number = 0;
  parkName: string = 'Parque de Estacionamento Trindade';
  messageType: string = 'General Message';

  ngOnInit() {
    this.messageService.getParkingSpotNotifications().subscribe((data) => {
      this.handleNotification(data);
    });
  }
  private handleNotification(data: any) {
    if (data.message === 'General Message') {
      this.messageService
        .getParkingSpotsCount(this.parkName)
        .subscribe((response) => {
          this.availableAutomobileSpots = response.automobileCount;
          this.availableElectricSpots = response.electricCount;
          this.availableGPLSpots = response.gplCount;
          this.availableMotorcycleSpots = response.motocycleCount;
        });
      this.messageType = 'General Message';
    } else if (data.message === 'Welcome Message') {
      this.messageType = 'Welcome Message';
    } else if (data.message === 'Goodbye Message') {
      this.messageType = 'Goodbye Message';
      this.parkyCoinSpent = data.parkyCoinSpent;
      this.otherAmount = data.otherAmount;
      this.totalCost = data.totalCost;
    }
  }
}
