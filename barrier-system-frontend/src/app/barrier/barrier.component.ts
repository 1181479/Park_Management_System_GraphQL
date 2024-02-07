import { Component, OnInit } from '@angular/core';
import { BarrierHub } from '../services/barrier-hub.service';

@Component({
  selector: 'app-barrier',
  templateUrl: './barrier.component.html',
  styleUrl: './barrier.component.css',
})
export class BarrierComponent implements OnInit {
  barrierRaised: boolean = false;

  constructor(private barrierHub: BarrierHub) {}

  ngOnInit() {
    this.barrierHub.barrierRaised$.subscribe((barrierRaised) => {
      this.barrierRaised = barrierRaised;
    });
  }

  toggleBarrier(type: string) {
    this.barrierRaised = !this.barrierRaised;
  }
}
