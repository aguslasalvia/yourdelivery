namespace Core.Entities
{

	public class Commentary
	{

		public int Id { get; set; }
		public string Text { get; set; }
		public DateTime Date { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }

		public int ShippingId { get; set; }
		public Shipping Shipping { get; set; }


		public Commentary() { }

		public Commentary(string text, DateTime date, User user, Shipping shipping)
		{
			Text = text;
			Date = date;
			User = user;
			UserId = user.Id;
			Shipping = shipping;
			ShippingId = shipping.Id;
		}
	}
}