using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;


public class Discipline
{
	[Key, MaxLength(12)]
	public long Id
	{
		get;
		set;
	}
	public string Name
	{
		get;
		set;
	} = null!;
	public string? Description
	{
		get;
		set;
	}

	public ICollection<User> Teachers
	{
		get;
		set;
	} = new List<User>();
	public ICollection<Group> Groups
	{
		get;
		set;
	} = new List<Group>();
}
