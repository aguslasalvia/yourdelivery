using System.Diagnostics.Metrics;
using Core.Entities;

namespace DTO;

public class CommentaryCreateDto
{
	public int Id { get; set; }
	public DateTime Date { get; set; }
	public string Text { get; set; }
	public int UserId { get; set; }
	public int ShippingId { get; set; }

	public CommentaryCreateDto() { }

	public CommentaryCreateDto(Commentary commentary)
	{
		Id = commentary.Id;
		Date = commentary.Date;
		Text = commentary.Text;
		UserId = commentary.UserId;
		ShippingId = commentary.ShippingId;
	}

		

	public Commentary toCommentary()
	{
		Commentary commentary = new()
		{
			Id = this.Id,
			Date = this.Date,
			Text = this.Text,
			UserId = this.UserId,
			ShippingId = this.ShippingId
		};

		return commentary;
	}
}