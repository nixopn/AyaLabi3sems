﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLServerWebApiConsoleClient.Repositories;
using WebAPIModels.Models;

namespace SQLServerWebApiConsoleClient.Controllers
{
    // базовый адрес: api/customers
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //DI конструктор, хранилище задается в Program.cs
        private ICustomerRepository repo;
        public CustomersController(ICustomerRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/customers
        // GET: api/customers/?country=[country]
        // всегда будет возвращать список клиентов, даже если он пуст
        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Employee>))]
        public async Task<IEnumerable<Employee>> GetCustomers(string? country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                return await repo.GetAllAsync();
            }
            else
            {
                return (await repo.GetAllAsync())
                .Where(customer => customer.Country == country);
            }
        }
        // GET: api/customers/[id]
        [HttpGet("{id}", Name = nameof(GetCustomer))] // именованный маршрут
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Employee c = await repo.GetAsync(id);
            if (c == null)
            {
                return NotFound(); // 404 Ресурс не обнаружен
            }
            return Ok(c); // 200 Возвращает ОК с клиентом в теле ответа
        }
        // POST: api/customers
        // BODY: Customer (JSON, XML)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Employee c)
        {
            if (c == null)
            {
                return BadRequest(); // 400 Неверный запрос
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Неверный запрос
            }
            Employee added = await repo.CreateAsync(c);
            return CreatedAtRoute( // 201 Создано
            routeName: nameof(GetCustomer),
            routeValues: new { id = added.EmployeeId },
            value: added);
        }
        // PUT: api/customers/[id]
        // BODY: Customer (JSON, XML)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] Employee c)
        {
            c.EmployeeId = c.EmployeeId;
            if (c == null || c.EmployeeId != id)
            {
                return BadRequest(); // 400 Неверный запрос
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Неверный запрос
            }
            var existing = await repo.GetAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Ресурс не обнаружен
            }
            await repo.UpdateAsync(id, c);
            return new NoContentResult(); // 204 Нет контента
        }
        // DELETE: api/customers/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await repo.GetAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Ресурс не обнаружен
            }
            bool? deleted = await repo.DeleteAsync(id);
            if (deleted.HasValue && deleted.Value) // короткозамкнутая AND
            {
                return new NoContentResult(); // 204 Нет контента
            }
            else
            {
                return BadRequest( // 400 Неверный запрос
                $"Customer {id} was found but failed to delete.");
            }
        }
    }
}