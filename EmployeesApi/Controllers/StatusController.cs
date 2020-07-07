using EmployeesApi.Models;
using EmployeesApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace EmployeesApi.Controllers
{
    public class StatusController : ControllerBase
    {
        ISystemTime Time;

        public StatusController(ISystemTime time)
        {
            Time = time;
        }

        // GET /status 
        [HttpGet("status")]
        public ActionResult GetStatus()
        {
            var response = new StatusResponse()
            {
                Status = "Best status ever",
                CheckedBy = "BStillman",
                LastChecked = Time.GetCurrent().AddDays(-10)
            };
            return Ok(response);
        }

        [SwaggerResponse(200)]
        [SwaggerResponse(404, "Opps.... a problem", typeof(ErrorResponse))]
        [HttpGet("books/{bookId:int}")]
        public ActionResult GetABook(int bookId)
        {
            return Ok($"Getting you info for book {bookId}");
        }

        [HttpGet("blogs/{year:int}/{month:int}/{day:int}")]
        public ActionResult GetBlogPostsFor(int year, int month, int day)
        {
            return Ok($"Getting blog posts for {month}/{day}/{year}");
        }

        [HttpGet("books")]
        public ActionResult GetBooks([FromQuery] string genre)
        {
            return Ok($"Getting you books in the {genre} genre");
        }

        [HttpGet("whoami")]
        public ActionResult WhoAmI([FromHeader(Name="User-Agent")] string userAgent)
        {
            return Ok($"I see you are running {userAgent}");
        }
    }

    public class StatusResponse
    {
        public string Status { get; set; }
        public string CheckedBy { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
