using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe.Checkout;

namespace HiddenVilla_Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class StripePaymentController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public StripePaymentController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpPost]
    public async Task<IActionResult> Create(StripePaymentDTO payment)
    {
        try
        {
            var domain = _configuration.GetValue<string>("HidenVilla_Client_URL");

            var options = new SessionCreateOptions() {
                PaymentMethodTypes = new List<string>() { "card" },
                LineItems = new List<SessionLineItemOptions>() {
                    new SessionLineItemOptions() {
                        PriceData = new SessionLineItemPriceDataOptions() {
                            UnitAmount = payment.Amount, //* 100, //convert to cents from client
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions() {
                                Name = payment.ProductName
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = domain + "/success-payment?session_id={CHECKOUT_SESSION_ID}}",
                CancelUrl = domain + payment.ReturnUrl
            };
            var service = new Stripe.Checkout.SessionService();
            var session = await service.CreateAsync(options);
            return Ok(new SuccessModel() {
                Data = session.Id,

            });
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorModel() { ErrorMessage = e.Message });
        }
    }
}
