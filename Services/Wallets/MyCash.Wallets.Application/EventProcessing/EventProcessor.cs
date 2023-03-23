﻿using AutoMapper;
using Micro.Messaging.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using MyCash.Wallets.Application.DTO;
using MyCash.Wallets.Core.Entities;
using MyCash.Wallets.Core.Repositories;
using System.Text.Json;

namespace MyCash.Wallets.Application.EventProcessing;

internal sealed class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
    }

    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.SignUpUser:
                AddUser(message);
                break;
            case EventType.StocksUpdated:
                UpdateStocks(message);
                break;
            default:
                break;
        }
    }

    private async void UpdateStocks(string message)
    {
        throw new NotImplementedException();
    }

    private async void AddUser(string userMessage)
{
    var userDto = JsonSerializer.Deserialize<UserBusDto>(userMessage);

    using (var scope = _serviceScopeFactory.CreateScope())
    {
        var repo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        try
        {
            var user = _mapper.Map<User>(userDto);
            if (!await repo.ExternalUserExists(user.ExternalId))
            {
                await repo.AddAsync(user, CancellationToken.None);
            }
            else
            {
                Console.WriteLine($"--> User with ID: {user.ExternalId} already exists...");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Couldn't add User to db: {ex.Message}");
        }
    }
}

private EventType DetermineEvent(string message)
{
    var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);

    if (eventType is null)
        return EventType.Undetermined;

    switch (eventType.Event)
    {
        case "SignUpUser":
            return EventType.SignUpUser;
        default:
            return EventType.Undetermined;
    }
}
}

enum EventType
{
    Undetermined,
    SignUpUser,
    StocksUpdated
}

class GenericEventDto
{
    public string Event { get; set; }
}
