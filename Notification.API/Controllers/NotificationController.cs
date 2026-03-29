using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.API.Services;
using Notification.Domain;

namespace Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpPost]
        public IActionResult SendNotification([FromBody] NotificationRequest request)
        {
            if(request == null)
                return BadRequest("Notifactions cannot be null");
            return Ok(_notificationService.CreateNotificationAsync(request));
        }
    }
}
