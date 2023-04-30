using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
public class OrganizationRepository : IOrganizationRepository
{
    private readonly ModelsContext _context;

    public OrganizationRepository(ModelsContext context)
    {
        _context = context;
    }

	public async Task<Organization> CreateOrganization(string name, User creator)
	{
		var organization = new Organization { Name = name };
		var group = new Group { Name="Admin", Organization = organization };


		_context.Organizations.Add(organization);
		_context.Groups.Add(group);

		creator.Group = group;
		await _context.SaveChangesAsync();
		return organization;
	}
}
}

