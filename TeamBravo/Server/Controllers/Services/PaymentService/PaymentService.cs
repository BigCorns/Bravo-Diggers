using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamBravo.Shared;

namespace TeamBravo.Server.Controllers.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {

        public PaymentService()
        {
            StripeConfiguration.ApiKey = "sk_test_51KnlQcIgdmJqWtggzjVTp4RUa2Eq4vLLRdO4Xu6ImdDDLQlPTmWtrBUWFDeLEbtioKFW6qm6Q54jy0ZGIRmwfv3B00DX0WMH9D";
        }
        public Session CreateCheckoutSession(List<CartItem> cartItems)
        {
            var lineItems = new List<SessionLineItemOptions>();
            cartItems.ForEach(ci => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = ci.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = ci.ProductTitle,
                        Images = new List<string> { ci.imgUrl }
                    }
                },

                Quantity = 1
            }));
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:44374/order-success",
                CancelUrl = "https://localhost:44374/cart",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }
    }
}
