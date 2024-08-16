
using Domin.Entity.SignalR;
using Infarstuructre.BL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Infarstuructre.ViewModel
{
    public class ChatHub : Hub
    {
        IIUserInformation iUserInformation;
        IIMessageChat iMessageChat;
        IIConnectAndDisconnect iConnectAndDisconnect;
        UserManager<ApplicationUser> _userManager;
        MasterDbcontext dbcontext;
        TBConnectAndDisConnect rec;
        public ChatHub(UserManager<ApplicationUser> _userManager1, IIUserInformation iUserInformation1, MasterDbcontext dbcontext1, IIConnectAndDisconnect iConnectAndDisconnect1, IIMessageChat iMessageChat1)
        {
            _userManager = _userManager1;
            iUserInformation = iUserInformation1;
            dbcontext = dbcontext1;
            iMessageChat = iMessageChat1;
            iConnectAndDisconnect = iConnectAndDisconnect1;

        }

        // connection & Disconnection Logic
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
                }
                if (await _userManager.IsInRoleAsync(user, "Support"))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, "Supports");
                }

                var profileImageUrl = GetProfileImageFromDatabase(user.UserName) ?? "No img";
                var connect = new TBConnectAndDisConnect
                {
                    ConnectId = Context.ConnectionId,
                    UserImg = profileImageUrl,
                    UserName = user.UserName
                };

                iConnectAndDisconnect.addConnection(connect);
                await Clients.All.SendAsync("UserConnected", user.UserName, profileImageUrl);
                await CheckUnreadMessages(user.UserName);
            }

            //if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
            //{
            //    await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
            //}
            //if (user != null && await _userManager.IsInRoleAsync(user, "Support"))
            //{
            //    await Groups.AddToGroupAsync(Context.ConnectionId, "Supports");
            //}

            //var profileImageUrl = GetProfileImageFromDatabase(user.UserName) ?? "No img";
            //if (!string.IsNullOrEmpty(user.UserName))
            //{
            //    var connect = new TBConnectAndDisConnect
            //    {
            //        ConnectId = Context.ConnectionId,
            //        UserImg = profileImageUrl,
            //        UserName = user.UserName
            //    };

            //    iConnectAndDisconnect.addConnection(connect);

            //    await Clients.All.SendAsync("UserConnected", user.UserName, profileImageUrl);
            //}
            //CheckUnreadMessages(user.UserName);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectId = Context.ConnectionId;
            var user1 = iConnectAndDisconnect.GetById(connectId);
            iConnectAndDisconnect.RemoveConnection(connectId);

            var user = Context.User.Identity.Name;
            await base.OnDisconnectedAsync(exception);
        }


        // Send and recive messages from and to clients with admin
        public async Task SendMessageToAdmin(string message, string to, string? filePath)
        {
            // Get the receiver's details
            var receiverUser = iUserInformation.GetByName(to);
            var receiverId = receiverUser?.Id;
            var senderId = Context.UserIdentifier;

            if (receiverId == null)
            {
                // Handle case where receiver is not found
                return;
            }

            // Prepare the message details
            var img = filePath ?? "Null";
            var currentUserName = Context.User.Identity.Name;
            var currentUserProfileImage = GetProfileImageFromDatabase(currentUserName);

            // Send the message to the specific receiver
            await Clients.User(receiverId).SendAsync("ReceiveMessage", currentUserName, message, filePath, currentUserProfileImage, DateTime.UtcNow.ToString("HH:mm"));

            // Store the message in the database
            var chatMsg = new TBMessageChat
            {
                Message = message,
                ReciverId = receiverId,
                SenderId = senderId,
                ImgMsg = img,
                IsRead = false,
                MessageeTime = DateTime.Now,
                CurrentState = true,
            };

            iMessageChat.saveData(chatMsg);

            // Notify the admin group only if the current user is not an admin (to prevent double notification)
            if (!(await CheckIfUserIsAdmin(senderId)))
            {
                await Clients.Group("Admins").SendAsync("ReceiveMessage", currentUserName, message, filePath, currentUserProfileImage, DateTime.UtcNow.ToString("HH:mm"));
            }

            // Update unread message count
            var unreadCount = await dbcontext.TBMessageChats.CountAsync(m => m.ReciverId == receiverId && !m.IsRead);
            await Clients.User(receiverId).SendAsync("UnreadMessagesNotification", unreadCount);
        }

        //public async Task SendMessageToAdmin(string message, string to, string? filePath)
        //{
        //    ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

        //    var userd = vmodel.sUser = iUserInformation.GetByName(to);
        //    var reciverId = userd.Id;
        //    var senderId = Context.UserIdentifier;
        //    var img = filePath ?? "Null";
        //    var currentUserName = Context.User.Identity.Name;
        //    var currentUserProfileImage = GetProfileImageFromDatabase(currentUserName);

        //    // Sending message to the specific client user
        //    await Clients.User(reciverId).SendAsync("ReceiveMessage", currentUserName, message, filePath, currentUserProfileImage, DateTime.UtcNow.ToString("HH:mm"));

        //    // Update the unread message count
        //    var unreadCount = await dbcontext.TBMessageChats.CountAsync(m => m.ReciverId == reciverId && !m.IsRead);
        //    await Clients.User(reciverId).SendAsync("UnreadMessagesNotification", unreadCount);

        //    var chatMsg = new TBMessageChat
        //    {
        //        Message = message,
        //        ReciverId = reciverId,
        //        SenderId = senderId,
        //        ImgMsg = img,
        //        IsRead = false,
        //        MessageeTime = DateTime.Now,
        //        CurrentState = true,
        //    };

        //    iMessageChat.saveData(chatMsg);
        //    // Additionally notify the admin group if needed
        //    await Clients.Group("Admins").SendAsync("ReceiveMessage", currentUserName, message, filePath, currentUserProfileImage, DateTime.UtcNow.ToString("HH:mm"));
        //}

        public async Task SendMessageToClients(string message, string to, string? filePath)
        {
            ViewmMODeElMASTER vmodel = new ViewmMODeElMASTER();

            var userd = vmodel.sUser = iUserInformation.GetByName(to);
            var reciverId = userd.Id;
            var senderId = Context.UserIdentifier;
            var img = filePath ?? "Null";
            var currentUserName = Context.User.Identity.Name;
            var currentUserProfileImage = GetProfileImageFromDatabase(currentUserName);

            // Sending message to the specific client user
            await Clients.User(reciverId).SendAsync("ReceiveMessage", currentUserName, message, filePath, currentUserProfileImage, DateTime.UtcNow.ToString("HH:mm"));

            // Update the unread message count
            var unreadCount = await dbcontext.TBMessageChats.CountAsync(m => m.ReciverId == reciverId && !m.IsRead);
            await Clients.User(reciverId).SendAsync("UnreadMessagesNotification", unreadCount);

            var chatMsg = new TBMessageChat
            {
                Message = message,
                ReciverId = reciverId,
                SenderId = senderId,
                ImgMsg = img,
                IsRead = false,
                MessageeTime = DateTime.Now,
                CurrentState = true,
            };

            iMessageChat.saveData(chatMsg);
            // Additionally notify the admin group if needed
            await Clients.Group("Admins").SendAsync("ReceiveMessage", currentUserName, message, filePath, currentUserProfileImage, DateTime.UtcNow.ToString("HH:mm"));
            //if (rec != null) 
            //{
            //    await Clients.User(rec.ConnectId).SendAsync("ReceiveMessage", currentUserName, message, filePath, DateTime.UtcNow.ToString("HH:mm"));
            //    var unreadCount = await dbcontext.TBMessageChats.CountAsync(m => m.ReciverId == to && !m.IsRead);
            //    await Clients.User(rec.ConnectId).SendAsync("UnreadMessagesNotification", unreadCount);

            //    var chatMsg = new TBMessageChat
            //    {
            //        Message = message,
            //        ReciverId = reciverId,
            //        SenderId = senderId,
            //        ImgMsg = img,
            //        IsRead = false,
            //        MessageeTime = DateTime.Now,
            //        CurrentState = true,
            //    };
            //    iMessageChat.saveData(chatMsg);
            //}
        }


        //      // Send and recive messages from and to AirFreight with admin
        //      public async Task SendMessageToAdminFromAirFreightArea(string user, string message)
        //{
        //	await Clients.Group("Admin").SendAsync("ReceiveMessageFromAirFreight", user, message);
        //}

        //      public async Task SendMessageToAirFreight(string user, string message)
        //      {
        //          await Clients.Group("DeliveryCompanies").SendAsync("ReceiveMessage", user, message);
        //      }

        //      // Send and recive messages from and to AirFreight with admin
        //      public async Task SendMessageToAdminFromMerchantsArea(string user, string message)
        //{
        //	await Clients.Group("Admin").SendAsync("ReceiveMessageFromMerchant", user, message);
        //}

        //      public async Task SendMessageToMerchants(string user, string message)
        //      {
        //          await Clients.Group("ShippingCompanies").SendAsync("ReceiveMessage", user, message);
        //      }

        private string GetProfileImageFromDatabase(string userName)
        {
            var user = iUserInformation.GetAllByName(userName).FirstOrDefault();
            if (user != null)
            {
                var photo = $"{Helper.PathImageuser}{user.ImageUser}";


                return photo;
            }

            return null;
        }

        private async Task<bool> CheckIfUserIsAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task MarkMessagesAsRead(string user)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            var user1 = viewmMODeElMASTER.sUser = iUserInformation.GetByName(user);

            var unreadMessages = dbcontext.TBMessageChats
                .Where(m => m.ReciverId == user1.Id && m.IsRead == false)
                .ToList();

            foreach (var unreadMessage in unreadMessages)
            {
                unreadMessage.IsRead = true;
                dbcontext.Update(unreadMessage);
                dbcontext.SaveChanges();
            }

            var rec = iConnectAndDisconnect.GetByName(user1.Name);
            await Clients.User(rec.ConnectId).SendAsync("UnreadMessagesNotification", 0);
        }

        public async Task CheckUnreadMessages(string user)
        {
            ViewmMODeElMASTER viewmMODeElMASTER = new ViewmMODeElMASTER();
            var reciver = viewmMODeElMASTER.sUser = iUserInformation.GetByName(user);

            var unreadCount = dbcontext.TBMessageChats.Where(m => m.ReciverId == reciver.Id)
                .Count(m => m.IsRead == false);

            var x = unreadCount;

            if (unreadCount > 0)
            {
                await Clients.All.SendAsync("UnreadMessagesNotification", unreadCount);
            }
        }

    }
}
