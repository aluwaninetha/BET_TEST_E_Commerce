﻿using E_Commerce.Application.DTOs.Account;
using E_Commerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Authentication / login User.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Authenticated User</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Account/Authenticate
        ///     {
        ///         "email": "user@example.com",
        ///         "password": "string",
        ///          
        ///      }
        ///
        /// </remarks>
        /// <response code="200">Returns Authenticated User</response>
        /// <response code="400">If the user login detail is null</response>
        /// <response code="500">If error occurs</response>
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));
        }

        /// <summary>
        /// Register a new User.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A newly created new User Account</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Account/register
        ///     {
        ///          "email": "user@example.com",
        ///          "password": "string",
        ///          "confirmPassword": "string"
        ///      }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly Register new User</response>
        /// <response code="400">If the user login detail is null</response>
        /// <response code="500">If error occurs</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
        }
        [HttpGet("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(userId, code));
        }
        
        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}