using Application.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Threading;
using AzureFunctionExample.Models;
using NSwag.Annotations;
using NSwag.Collections;
using Domain;
using Domain.States;

namespace AzureFunctionExample
{
    public class CreateUserFunction
    {
        public ICreateUserCommandHandler _commandHandler { get; set; }

        public CreateUserFunction(ICreateUserCommandHandler commandHandler, IServiceProvider serviceProvider)         
        {
            _commandHandler = commandHandler;
        }

        [FunctionName(nameof(CreateUserFunction))]
        [Description("")]
        //[SwaggerQueryParameter("")]
        [SwaggerResponse(200, typeof(CreateUserRequest), Description = "User Added")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/{username}/createUser")] 
            HttpRequest req,
            [Description("The Customer UserName")] string userName,
            CancellationToken ct = default)
        {

            //var body = await req.GetBodyAsync<CreateUserRequest>();
            //if (!body.IsValid)
            //{
            //    return new BadRequestResult();
            //}
             
            var email = req.Query["email"];
            var firstName = req.Query["firstname"];
            var lastname = req.Query["lastname"];
            DateTime.TryParse(req.Query["dob"], out DateTime dob);

            //using var logScope = log.BeginScopeWith(new
            //{
            //    UserName = userName,
            //    Email = email
            //}) ;
            var command = new CreateUserCommand(userName, firstName, lastname, email, dob);
            var responseMessage = await _commandHandler.HandleAsync(command);
            var val = 1;

            return responseMessage.Match<IActionResult>(
                 userCreated => new OkObjectResult(userCreated),
                 userAlreadyExit => new ConflictResult()
                );
        }
    }
}
