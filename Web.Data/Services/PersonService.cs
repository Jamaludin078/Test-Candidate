using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models.Database;

namespace Web.Data.Services
{
	public interface IPersonService
	{
		Task<List<Person>> GetAsync();
		Task<Person> GetAsync(int id);
		Task<int> AddAsync(Person TEntity);
		Task<int> EditAsync(Person TEntity);
		Task<int> DeleteAsync(int id);
	}
	public class PersonService : IPersonService
	{
		readonly DataContext context;

		public PersonService(DataContext context) => this.context = context;

		public async Task<List<Person>> GetAsync()
			=> await context.Person.AsNoTracking().ToListAsync();

		public async Task<Person> GetAsync(int id)
		{
			return await context.Person.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public async Task<int> AddAsync(Person TEntity)
		{
			if (TEntity.Age == null)
				TEntity.Age = 0;

			context.Add(TEntity);
			return await context.SaveChangesAsync();
		}

		public async Task<int> EditAsync(Person TEntity)
		{
			context.Update(TEntity);

			return await context.SaveChangesAsync();
		}

		public async Task<int> DeleteAsync(int id)
		{
			var model = new Person { Id = id };
			return await DeleteAsync(model);
		}

		public async Task<int> DeleteAsync(Person TEntity)
		{
			context.Remove(TEntity);
			return await context.SaveChangesAsync();
		 }
	}
}
