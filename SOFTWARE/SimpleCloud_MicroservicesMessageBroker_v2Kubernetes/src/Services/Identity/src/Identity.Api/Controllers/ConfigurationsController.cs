﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Identity_SimpleCloud_MicroservicesHttp.Application.Clients.Commands.CreateClient;
using Identity_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Identity_SimpleCloud_MicroservicesHttp.WebUI.Controllers;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using Microsoft.Extensions.Options;

namespace Identity_SimpleCloud_MicroservicesHttp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ApiController
    {
        private readonly IIdentityDbContext _identityDbContext;

        private readonly AppSettings _appSettings;


        public ConfigurationsController(
            IOptions<AppSettings> settings,
            IIdentityDbContext dbContext)
        {
            _appSettings = settings.Value;
            _identityDbContext = dbContext;
        }

        //public ConfigurationsController(IIdentityDbContext dbContext, IMapper mapper)
        //{
        //    _identityDbContext = dbContext;
        //    _mapper = mapper;
        //}

        [HttpPost("clearDatabase")]
        public async Task<ActionResult> ClearDatabase()
        {
            var clients = _identityDbContext.Clients.ToList();
            _identityDbContext.Clients.RemoveRange(clients);

            await _identityDbContext.SaveChangesAsync(default);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetConfiguration()
        {
            return _appSettings;
        }
    }
}