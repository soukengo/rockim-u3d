using System;
using RockIM.Sdk.Api.V1;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Api.V1.Enums;
using RockIM.Sdk.Internal.V1.Context;
using RockIM.Sdk.Internal.V1.Domain.Events;
using RockIM.Sdk.Internal.V1.Infra;
using RockIM.Sdk.Internal.V1.Infra.Http;
using RockIM.Sdk.Internal.V1.Infra.Socket;
using RockIM.Sdk.Internal.V1.Service;

namespace RockIM.Sdk.Internal.V1
{
    public sealed class AuthorizedApisV1 : IAuthorizedApis, IDisposable
    {
        private SdkContext _context;

        private IEventBus _eventBus;

        private readonly ISocketManager _socketManager;

        private readonly MessageService _messageService;

        private readonly ChatRoomService _chatRoomService;


        public IMessageApi Message => _messageService;
        public IChatRoomApi ChatRoom => _chatRoomService;
        public User Current => _context.Authorization.User;
        public ConnectionStatus ConnectionStatus => _context.ConnectionStatus;

        public AuthorizedApisV1(SdkContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
            var httpManager = new HttpManager(context, eventBus);
            _socketManager = new SocketManager(context, eventBus);
            _socketManager.Connect(context.Config.ServerConfig.Socket);
            _messageService = new MessageService(context, new MessageRepository(httpManager));
            _chatRoomService = new ChatRoomService(new ChatRoomRepository(httpManager));
        }

        public void Dispose()
        {
            _socketManager.Dispose();
        }
    }
}