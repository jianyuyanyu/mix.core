﻿using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using Mix.Constant.Constants;
using Mix.Identity.Constants;
using Mix.Lib.Services;
using Mix.Shared.Models;
using Mix.SignalR.Constants;
using Mix.SignalR.Enums;
using Mix.SignalR.Models;

namespace Mix.SignalR.Hubs
{
    public abstract class BaseSignalRHub : Hub
    {
        protected AuditLogService _auditLogService;

        protected BaseSignalRHub(AuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        public static Dictionary<string, List<HubUserModel>> Rooms = new Dictionary<string, List<HubUserModel>>();
        public virtual async Task JoinRoom(string roomName)
        {
            await AddUserToRoom(roomName);
        }

        public virtual async Task SendMessage(SignalRMessageModel message)
        {
            LogMessage(message);
            await Clients.All.SendAsync(HubMethods.ReceiveMethod, message);
        }

        private void LogMessage(SignalRMessageModel message)
        {
            _auditLogService.LogRequest(Context.User?.Identity?.Name, new ParsedRequestModel()
            {
                RequestIp = GetIPAddress(),
                Endpoint = GetType().Name,
                Method = "SignalR",
                Body = message.ToString()
            });
        }

        private string? GetIPAddress()
        {
            var feature = Context.Features.Get<IHttpConnectionFeature>();
            // here you could get your client remote address
            return feature?.RemoteIpAddress?.ToString();
        }

        public virtual async Task SendPrivateMessage(SignalRMessageModel message, string connectionId, bool selfReceive)
        {
            LogMessage(message);
            await Clients.Client(connectionId).SendAsync(HubMethods.ReceiveMethod, message);
            if (selfReceive)
            {
                await SendMessageToCaller(message);
            }
        }

        public virtual Task SendMessageToCaller(SignalRMessageModel message)
        {
            LogMessage(message);
            return Clients.Caller.SendAsync(HubMethods.ReceiveMethod, message);
        }

        public virtual Task SendGroupMessage(SignalRMessageModel message, string groupName, bool exceptCaller = true)
        {
            LogMessage(message);
            message.From ??= GetCurrentUser();
            return exceptCaller
                ? Clients.GroupExcept(groupName, Context.ConnectionId).SendAsync(HubMethods.ReceiveMethod, message)
                : Clients.Group(groupName).SendAsync(HubMethods.ReceiveMethod, message);
        }

        #region Private
        private async Task AddUserToRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            if (!Rooms.ContainsKey(roomName))
            {
                Rooms.Add(roomName, new List<HubUserModel>());
            }

            var users = Rooms[roomName];
            if (!users.Any(u => u.ConnectionId == Context.ConnectionId))
            {
                var user = GetCurrentUser();
                users.Add(user);
                Rooms[roomName] = users;
                await SendMessageToCaller(new(user) { Action = MessageAction.MyConnection });
                await SendMessageToCaller(new(users) { Action = MessageAction.MemberList });
                await SendGroupMessage(new(user) { Action = MessageAction.NewMember }, roomName);
            }
        }


        private HubUserModel GetCurrentUser()
        {
            return new()
            {
                ConnectionId = Context.ConnectionId,
                Username = Context.User?.Identity?.Name,
                Avatar = Context.User?.Claims.FirstOrDefault(m => m.Type == MixClaims.Avatar)?.Value ?? MixConstants.CONST_DEFAULT_EXTENSIONS_FILE_PATH,
            };
        }
        #endregion

        #region Override

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync().ConfigureAwait(false);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            foreach (var room in Rooms)
            {
                var user = room.Value.FirstOrDefault(m => m.ConnectionId == Context.ConnectionId);
                if (user != null)
                {
                    room.Value.Remove(user);
                    await SendGroupMessage(new(user) { Action = MessageAction.MemberOffline }, room.Key);
                }
            }
            await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
        }
        #endregion
    }
}