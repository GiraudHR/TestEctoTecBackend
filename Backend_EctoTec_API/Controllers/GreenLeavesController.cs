using Microsoft.AspNetCore.Mvc;
using Backend_EctoTec.Core.Interfaces.Services;
using System;
using Microsoft.AspNetCore.Http;
using Backend_EctoTec.Core.Entities;
using System.Collections.Generic;
using Backend_EctoTec_API.Models;
using Backend_EctoTec_API.Email;

namespace Backend_EctoTec_API.Controllers
{
    [ApiController]
    [Route("api/GreenLeaves")]
    public class GreenLeavesController : ControllerBase
    {

        private readonly IServiceGreenLeaves _servGreenLeaves;
        private readonly SendEmailService _emailService;
        public GreenLeavesController(IServiceGreenLeaves servGreenLeaves, SendEmailService emailService)
        {
            _servGreenLeaves = servGreenLeaves;
            _emailService = emailService;
        }

        [HttpGet("GetAddress")]
        public ActionResult<List<Address>> GetAddress(string address)
        {
            try
            {
                return _servGreenLeaves.GetAddress(address);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("SendEmail")]
        public ActionResult<bool> SendEmail([FromBody] GreenLeaves greenLeaves)
        {
            GreenLeavesDto greenLeavesDto = new GreenLeavesDto();
            try
            {
                DateTime date = new DateTime(greenLeaves.Date.Year, greenLeaves.Date.Month, greenLeaves.Date.Day);
                string fechaFormateada = date.ToString("d 'de' MMMM 'del' yyyy");

                greenLeavesDto.Name = greenLeaves.Name;
                greenLeavesDto.Email = greenLeaves.Email;
                greenLeavesDto.Phone = greenLeaves.Phone;
                greenLeavesDto.Date = fechaFormateada;
                greenLeavesDto.Address = greenLeaves.Address;
                _emailService.SendEmail(greenLeavesDto);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
