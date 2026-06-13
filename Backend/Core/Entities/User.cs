using Core.Enums;
namespace Core.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public DateOnly Birth { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
		public UserStates State { get; set; }
		public Gender Gender { get; set; }
		public int CreatedByID { get; set; }
		public int UpdatedByID { get; set; }
		public DateTime LastUpdated { get; set; }

		public User() { }

		public User(string name,
								string lastname,
								string phone,
								DateOnly birth,
								string email,
								string password,
								Role role,
								UserStates state,
								Gender gender,
								int createdById,
								int updatedById,
								DateTime lastUpdated)
		{
			Name = name;
			Lastname = lastname;
			Phone = phone;
			Email = email;
			Birth = birth;
			Password = password;
			Role = role;
			State = state;
			Gender = gender;
			CreatedByID = createdById;
			UpdatedByID = updatedById;
			LastUpdated = lastUpdated;
		}
	}
}