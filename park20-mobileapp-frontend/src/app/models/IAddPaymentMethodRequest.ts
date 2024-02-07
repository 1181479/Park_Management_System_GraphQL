export interface IAddPaymentMethodRequest {
  lastFourDigits: number;
  expirationDate: Date;
  fullName: string;
  token: string;
  username: string;
}
