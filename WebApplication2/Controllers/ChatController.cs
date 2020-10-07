using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly Appdbcontext context;

        public ChatController(UserManager<AppUsers> userManager, Appdbcontext context)
        {
            _userManager = userManager;
            this.context = context;
        }
        public async Task<IActionResult> Chat()
        {
            var CurrentUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = CurrentUser.UserName;


            }
            var messages = await context.Messages.ToListAsync();
            return View("Chat");
        }
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid) {
                message.UserName = User.Identity.Name;
                var Sender = await _userManager.GetUserAsync(User);
                message.UserID = Sender.Id;
                await context.Messages.AddAsync(message);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();

        }
    }
}
