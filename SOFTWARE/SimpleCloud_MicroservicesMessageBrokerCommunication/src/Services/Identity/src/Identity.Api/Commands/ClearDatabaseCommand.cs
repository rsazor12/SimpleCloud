using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Common.Exceptions;
using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Models.HandlerResponse;
using Identity_SimpleCloud_MicroservicesMessageBroker.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity_SimpleCloud_MicroservicesMessageBroker.Application.Clients.Commands.CreateClient
{
    public class ClearDatabaseCommand
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}