using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Application.Mappers
{
    public static class PaymentMethodMapper
    {
        public static PaymentMethodResultDto ToPaymentMethodDto(PaymentMethod PaymentMethod)
        {
            return new PaymentMethodResultDto(PaymentMethod.CardLastFourDigits, PaymentMethod.FullName);
        }

        public static PaymentMethod ToPaymentMethodDomain(CreatePaymentMethodRequestDto PaymentMethodDto)
        {
            return new PaymentMethod
            {
                FullName = PaymentMethodDto.FullName,
                CardLastFourDigits = PaymentMethodDto.LastFourDigits,
                ExpirationDate = PaymentMethodDto.ExpirationDate,
                PaymentToken = PaymentMethodDto.Token
            };

        }
    }
}
