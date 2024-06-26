﻿using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Reflection;
using TicTacToe.Cache;
using T3BL = TicTacToe.BusinessLogic;
using T3Ent = TicTacToe.Entity;
using T3ML = TicTacToe.ML;
using T3Mod = TicTacToe.Models;

namespace WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDistributedCache _distCache;
        private string _OutcomePredictionModelPath = string.Empty;
        private string _ComputerPlayerV3ModelPath = string.Empty;
        private log4net.ILog _logger;
        
        public HomeController(IConfiguration config, IDistributedCache distCache, log4net.ILog logger)
        {
            _config = config;
            _distCache = distCache;
            _logger = logger;
        }

        [Route("")]
        [Route("Default")]
        [Route("Default/Index")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            WebApiInstanceInfo instanceInfo = new WebApiInstanceInfo();
            instanceInfo.AppInstanceId = _distCache.AppInstanceId();
            instanceInfo.AppStartTimeUTC = _distCache.GetCache("AppStartTimeUTC") ?? string.Empty;

            return Ok(instanceInfo);
        }
    }
}
