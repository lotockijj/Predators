using BattleBands.Data;
using BattleBands.Models.ApplicationModels;
using BattleBands.Models.ViewModels.MessageViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleBands
{
    public class temp
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public temp(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async void GetFromDb()
        {
            ApplicationMessage[] messages = await _context.Messages.Include(m => m.User).ToArrayAsync();
            List<MessageViewModel> model = new List<MessageViewModel>();
            foreach (ApplicationMessage msg in messages)
            {
                model.Add(new MessageViewModel(msg));
            }
        }

        public async Task<MessageViewModel> SendToDbAsync(NewMessageViewModel message)
        {
            // Create a new message to save to the database
                ApplicationMessage newMessage = new ApplicationMessage()
                {
                    Content = message.Content,
                    UserId = user.Id,
                    User = user
                };

                // Save the new message
                await _context.AddAsync(newMessage);
                await _context.SaveChangesAsync();

                MessageViewModel model = new MessageViewModel(newMessage);

            return model;
        }
    }
}
