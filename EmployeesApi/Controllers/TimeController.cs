using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Controllers
{
    public class TimeController : ControllerBase
    {
        public ActionResult GetGame()
        {
            return Ok();
        }
        
        public ActionResult AddGame()
        {
            return Ok();
        }

        [HttpGet("/time")]
        public ActionResult Get()
        {
            return Ok();
        }
    }

    public class PostGameModel
    {
        [Required]
        public string Title { get; set; }
        public string Platform { get; set; }
        public decimal Price { get; set; }
    }
}
