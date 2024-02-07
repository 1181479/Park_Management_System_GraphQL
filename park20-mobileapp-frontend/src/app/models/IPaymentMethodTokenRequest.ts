export interface IPaymentMethodTokenRequest {
  cardNumber: number;
  cvv: number;
  fullName: string;
  expirationDate: Date;
}
