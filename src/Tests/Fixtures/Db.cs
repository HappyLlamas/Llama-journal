using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Tests.Fixtures;

class TestDbContext
{
	public static ModelsContext Get()
	{
		var optionBuilder = new DbContextOptionsBuilder<ModelsContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
		return new ModelsContext(optionBuilder.Options);
	}
}
