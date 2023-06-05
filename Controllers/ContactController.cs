using Microsoft.AspNetCore.Mvc;
using RoastHiveMvc.Models;
using System.Threading.Tasks;
using RoastHiveMvc.Services;

namespace RoastHiveMvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly EmailService _emailService;

        public ContactController(EmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ContactFormVM model)
        {
            if (ModelState.IsValid)
            {
                // Send an email
                string? email = model.Email;
                string subject = "Roast Hive - We'll Reach Out to You Shortly!";
                string message = "";

                if (email != null) // Perform null check
                {
                    await _emailService.SendEmailAsync(email, subject, message);

                    // Show toastr notification
                    TempData["ToastrMessage"] = "Email was sent successfully";

                    // Redirect back to Contact index page
                    return RedirectToAction("Index", "Home");
                }
            }

            // If the model is not valid, return to the contact form with validation errors
            return View("Index", model);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
