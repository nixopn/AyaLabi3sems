using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Concurrent;
using WebAPIModels.Models;

namespace SQLServerWebApiConsoleClient.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        public static ConcurrentDictionary<int, Employee> customersCache;
        private NorthwindContext db;

        public CustomerRepository(NorthwindContext db)
        {
            this.db = db;

            if(customersCache == null)
            {
                //ключи - CustomerId
                customersCache = new ConcurrentDictionary<int, Employee>(db.Employees.ToDictionary(c=>c.EmployeeId));
            }
        }
        private Employee UpdateCache(int id, Employee customer)
        {
            //TryGetValue - приставка "Try", concurrentDictionary многопоточная коллекция
            Employee old;
            if (customersCache.TryGetValue(id, out old))
            {
                if (customersCache.TryUpdate(id, customer, old))
                {
                    return customer;
                }
            }
            return null;
        }

        public async Task<Employee> CreateAsync(Employee customer)
        {

            EntityEntry<Employee> added = await db.Employees.AddAsync(customer);
            int affectedRows = await db.SaveChangesAsync();
            if(affectedRows > 0)
            {
                return customersCache.AddOrUpdate(customer.EmployeeId, customer, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool?> DeleteAsync(int customerId)
        {
            // удаление из базы данных
            Employee? c = db.Employees.Find(customerId);

            db.Employees.Remove(c);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // удаление из кэша
                return customersCache.TryRemove(customerId, out c);
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            // извлечение из кэша для производительности
            return Task.Run<IEnumerable<Employee>>(() => customersCache.Values);
        }

        public Task<Employee?> GetAsync(int customerId)
        {
            return Task.Run(() =>
            {
                // извлечение из кэша для производительности
                customersCache.TryGetValue(customerId, out Employee? c);
                return c;
            });
        }

        public async Task<Employee> UpdateAsync(int customerId, Employee customer)
        {

            // обновление в базе данных
            db.Employees.Update(customer);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                // обновление в кэше
                return UpdateCache(customerId, customer);
            }
            return null;
        }
    }
}
