using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IOrganizationRepository
{
	Task<Organization> CreateOrganization(string name, User creator);
}
}

