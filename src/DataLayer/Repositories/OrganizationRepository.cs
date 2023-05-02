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
		var admin_group = new Group { Name="Admin", Organization = organization };
		var teachers_group = new Group { Name="Teachers", Organization = organization };


		_context.Organizations.Add(organization);
		_context.Groups.Add(admin_group);
		_context.Groups.Add(teachers_group);

		creator.Group = admin_group;
		await _context.SaveChangesAsync();
		return organization;
	}
}
}

