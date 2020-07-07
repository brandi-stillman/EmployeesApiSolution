using EmployeesApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderProcessorController : ControllerBase
    {
        private readonly ISystemTime _clock;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clock"></param>
        public OrderProcessorController(ISystemTime clock)
        {
            _clock = clock;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost("orders")]
        public ActionResult<OrderResponse> PlaceOrder([FromBody] OrderRequest order)
        {
            var response = new OrderResponse
            {
                EstimatedShipDate = _clock.GetCurrent().Hour < 12 ? _clock.GetCurrent() : _clock.GetCurrent().AddDays(1)
            };
            return Ok(response);
        }
    }

    public class OrderRequest
    {

    }

    public class OrderResponse
    {
        public DateTime EstimatedShipDate { get; set; }
    }
}
