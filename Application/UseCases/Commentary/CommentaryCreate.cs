using Application.Interfaces;
using Core.Interfaces;
using DTO;

namespace Application.UseCases.Commentary;

public class CommentaryCreate : ICommentaryCreate
{

	private readonly ICommentaryRepository _commentaryRepository;

	public CommentaryCreate(ICommentaryRepository commentaryRepository)
	{
		_commentaryRepository = commentaryRepository;
	}

	public void Execute(CommentaryCreateDto commentaryDto)
	{
		_commentaryRepository.Add(commentaryDto.toCommentary());
	}
}
