import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { IPark } from '../../models/IPark';
import { ParkService } from '../../services/park.service';
import * as L from 'leaflet';
import { IParkingSpotsCount } from '../../models/IParkingSpotsCount';
import { Subscription, interval } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { IVehicle } from '../../models/IVehicle';
import { VehicleService } from '../../services/vehicle.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit, AfterViewInit, OnDestroy {
  vehicleList: IVehicle[] = [];
  nearbyParksList: IPark[] = [];
  selectedVehicle: any;
  username: string | null = '';
  lat: number = 0;
  lng: number = 0;
  private map: any;
  private userMarker: any;
  private updateSubscription: Subscription | undefined;
  iconDefault: any = L.icon({
    iconRetinaUrl: 'assets/marker-icon-2x.png',
    iconUrl: 'assets/marker-icon.png',
    shadowUrl: 'assets/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    tooltipAnchor: [16, -28],
    shadowSize: [41, 41],
  });
  redicon: any = L.icon({
    iconUrl: 'https://geobgu.xyz/web-mapping-2018/examples/images/redIcon.png',
    shadowUrl: 'assets/marker-shadow.png',
    iconSize: [25, 41],
    iconAnchor: [12, 41],
    popupAnchor: [1, -34],
    tooltipAnchor: [16, -28],
    shadowSize: [41, 41],
  });

  private initMap(): void {
    L.Marker.prototype.options.icon = this.iconDefault;
    L.Icon.Default.imagePath = 'assets';
    this.map = L.map('map');
    this.map
      .locate({ setView: true, maxZoom: 9 })
      .on('locationfound', (e: any) => {
        L.marker(e.latlng, { icon: this.redicon })
          .addTo(this.map)
          .bindPopup('You are here!')
          .openPopup();
      });
    const tiles = L.tileLayer(
      'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
      {
        maxZoom: 18,
        minZoom: 3,
        attribution:
          '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
      }
    );
    tiles.addTo(this.map);
  }

  constructor(
    public parkService: ParkService,
    public vehicleService: VehicleService
  ) {}
  ngAfterViewInit(): void {
    this.initMap();
  }

  ngOnInit() {
    this.getLocation();
    this.startUpdateTimer();
    this.username = localStorage.getItem('username');
    this.vehicleService
      .getListVehicleFromUser(this.username!)
      .subscribe((data: any) => (this.vehicleList = data));
  }

  ngOnDestroy() {
    // Unsubscribe to avoid memory leaks when the component is destroyed
    if (this.updateSubscription) {
      this.updateSubscription.unsubscribe();
    }
  }

  checkAvailableParks() {
    if (this.selectedVehicle) {
      this.nearbyParksList.forEach((park) => {
        this.parkService
          .checkAvailableSpaceOnPark(
            park.parkName,
            this.selectedVehicle.licensePlate
          )
          .subscribe((result: boolean) => {
            if (result === false) {
              this.map.eachLayer((layer: any) => {
                if (
                  layer instanceof L.Marker &&
                  layer.getLatLng().equals([park.latitude, park.longitude])
                ) {
                  this.map.removeLayer(layer);
                }
                this.nearbyParksList = this.nearbyParksList.filter(
                  (p: IPark) => p !== park
                );
              });
            }
          });
      });
    } else {
      this.getNearbyParksToUserLocation();
    }
  }

  getLocation() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position: any) => {
          if (position) {
            this.lat = position.coords.latitude;
            this.lng = position.coords.longitude;
            this.userMarker = L.marker([this.lat, this.lng], {
              icon: this.redicon,
            })
              .addTo(this.map)
              .bindPopup('You are here!')
              .openPopup();
            this.getNearbyParksToUserLocation();
          }
        },
        (error: any) => console.log(error)
      );
    } else {
      alert('Geolocation is not supported by this browser.');
    }
  }

  getNearbyParksToUserLocation() {
    this.parkService
      .getNearbyParksByLocation(this.lat, this.lng)
      .subscribe((data: any) => {
        this.nearbyParksList = data;
        this.map.whenReady(() => {
          this.addMarkersForNearbyParks(data);
        });
      });
  }

  addMarkersForNearbyParks(parksList: IPark[]) {
    parksList.forEach((park: IPark) => {
      this.parkService
        .getParkingSpotsCount(park.parkName)
        .subscribe((parkingSpotsCount: IParkingSpotsCount) => {
          L.marker([park.latitude, park.longitude])
            .addTo(this.map)
            .bindPopup(
              `<div><b>${park.parkName}</b></div>
              <div><b>Distance:</b> ${park.distanceToTarget.toFixed(2)} km<div>
              <div><b>Free Parking Spots</b></div>
              <div>GPL: ${parkingSpotsCount.gplCount}<div>
              <div>Electric: ${parkingSpotsCount.electricCount}<div>
              <div>Motocycle: ${parkingSpotsCount.motocycleCount}<div>
              <div>Automobile: ${parkingSpotsCount.automobileCount}<div>`
            );
        });
    });
  }

  startUpdateTimer() {
    // Use RxJS interval to periodically update the marker data
    this.updateSubscription = interval(10000) // Update every 10 seconds
      .pipe(
        switchMap(() =>
          this.parkService.getNearbyParksByLocation(this.lat, this.lng)
        )
      )
      .subscribe((data: any) => {
        if (this.nearbyParksList.length !== data.length) {
          data = data.filter((park: IPark) =>
            this.nearbyParksList.some(
              (nearbyPark: IPark) => nearbyPark.parkName === park.parkName
            )
          );
        }
        this.updateMarkers(data);
      });
  }

  updateMarkers(newParksList: IPark[]) {
    // Clear existing markers
    this.map.eachLayer((layer: any) => {
      if (layer instanceof L.Marker) {
        this.map.removeLayer(layer);
      }
    });
    this.userMarker = L.marker([this.lat, this.lng], {
      icon: this.redicon,
    })
      .addTo(this.map)
      .bindPopup('You are here!')
      .openPopup();
    this.addMarkersForNearbyParks(newParksList);
  }
}
