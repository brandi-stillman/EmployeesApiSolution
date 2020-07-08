using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeesApi.Domain;
using EmployeesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Controllers
{
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeesDataContext Context;
        private readonly IMapper Mapper;
        private readonly MapperConfiguration MapperConfig;

        public EmployeeController(EmployeesDataContext context, IMapper mapper, MapperConfiguration mapperConfig)
        {
            Context = context;
            Mapper = mapper;
            MapperConfig = mapperConfig;
        }

        [HttpGet("employees")]
        public async Task<ActionResult> GetAllEmployees()
        {
            var employees = await Context.Employees
                .Where(e => e.Active)
                .ProjectTo<EmployeeListItem>(MapperConfig)
                .ToListAsync();
            return Ok(employees);
        }

        [HttpGet("employees{id:int}", Name = "employees-get-by-id")]
        public async Task<ActionResult> GetAnEmployee()
        {
            var employee = await Context.Employees
                .Where(e => e.Active)
                .ProjectTo<GetEmployeeDetailsResponse>(MapperConfig)
                .SingleOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost("employees")]
        public async Task<ActionResult> Hire([FromBody] PostEmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var employee = Mapper.Map<Employee>(request);
                Context.Employees.Add(employee);
                await Context.SaveChangesAsync();
                var employeeResponse = Mapper.Map<GetEmployeeDetailsResponse>(employee);
                return CreatedAtRoute("employee-get-by-id", new { id = employeeResponse.Id }, employeeResponse);
            }
        }

        [HttpDelete("employees/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await Context.Employees.SingleOrDefaultAsync(e => e.Id == id && e.Active);
            if (employee != null)
            {
                employee.Active = false;
                await Context.SaveChangesAsync();
            }

            return NoContent();
        }

        [HttpPut("employees/{id:int}/firstname")]
        public async Task<ActionResult> UpdateFirstName(int id, [FromBody] string firstName)
        {
            var employee = await Context.Employees.SingleOrDefaultAsync(e => e.Id == id && e.Active);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                employee.FirstName = firstName;
                await Context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}
