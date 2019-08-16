﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartRestaurant.Application.Interfaces;
using SmartRestaurant.Application.Restaurants.Restaurants.Queries.GetBySlugUrl;

namespace SmartRestaurant.Client.Web.Areas.Clients.Controllers
{
    [Area("Clients")]
    [Route("clients/tables")]
    public class TablesController : ClientBaseController
    {
        private readonly ILoggerService<TablesController> log;

        public TablesController(IConfiguration configuration,
                                         IMailingService mailing,
                                         INotifyService notify,
                                         ILoggerService<ClientBaseController> baselog,
                                         IGetRetaurantBySlugUrlQuery getRetaurantBySlugUrlQuery,
                                         ILoggerService<TablesController> log) : base(configuration, mailing, notify, baselog, getRetaurantBySlugUrlQuery)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}