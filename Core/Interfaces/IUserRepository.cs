using Core.Entities;

namespace Core.Interfaces
{
	public interface IUserRepository
	{
		User Add(User user);
		User Update(User user);
		void Delete(User user);
		User? GetByEmail(string email);

		User? GetByEmailAndPassword(string email, string password);

		IEnumerable<User> GetAll();


		IEnumerable<User> GetAllClient();


	}
}
