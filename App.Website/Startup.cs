﻿using System.Configuration;
using System.Data.Entity;
using App.Infrastructure.IdentityManagement;
using App.Website;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace App.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
