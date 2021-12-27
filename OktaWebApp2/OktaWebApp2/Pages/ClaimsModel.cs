﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace OktaWebApp2.Pages
{
    [Authorize]
    public class ClaimsModel
    {
        private readonly ILogger<ClaimsModel> _logger;

        public ClaimsModel(ILogger<ClaimsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}