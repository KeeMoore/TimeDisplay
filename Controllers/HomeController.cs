using Microsoft.AspNetCore.Mvc;
namespace TimeDisplay.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            DateTime currentTime = DateTime.Now;
            DateTime endTime = new DateTime(2022, 12, 25, 7, 0, 0);

            string formattedStartTime = currentTime.ToString("MMM dd, yyyy hh:mm tt");
            string formattedEndTime = endTime.ToString("MMM dd, yyyy hh:mm tt");

            TimeSpan timeRemaining = endTime - currentTime;
            int days = timeRemaining.Days;
            int hours = timeRemaining.Hours;
            int minutes = timeRemaining.Minutes;

            string userFriendlyCountdown = $"{days} Day(s), {hours} Hour(s), {minutes} Minute(s) Remaining";

            System.Timers.Timer timer = new System.Timers.Timer(1000); // Update every second
            timer.Elapsed += (sender, e) =>
            {
                currentTime = DateTime.Now;
                timeRemaining = endTime - currentTime;

                if (timeRemaining.TotalSeconds <= 0)
                {
                    timer.Stop();
                    userFriendlyCountdown = "Countdown finished";
                }
                else
                {
                    days = timeRemaining.Days;
                    hours = timeRemaining.Hours;
                    minutes = timeRemaining.Minutes;
                    userFriendlyCountdown = $"{days} Day(s), {hours} Hour(s), {minutes} Minute(s) Remaining";
                }
            };
            timer.Start();

            // Pass userFriendlyCountdown to the view
            return View((object)userFriendlyCountdown);
        }
    }
}
