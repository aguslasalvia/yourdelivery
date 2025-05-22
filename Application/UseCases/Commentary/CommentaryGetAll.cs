using Application.Interfaces;
using Core.Interfaces;
using DTO;

namespace Application.UseCases.Commentary;

public class CommentaryGetAll : ICommentaryGetAll
{

	private readonly ICommentaryRepository _commentaryRepository;

	public CommentaryGetAll(ICommentaryRepository commentaryRepository)
	{
		_commentaryRepository = commentaryRepository;
	}

	public IEnumerable<CommentaryDto> Execute()
	{
        var commentaries = _commentaryRepository.GetAll();
		return commentaries.Select(c => new CommentaryDto(c));
	}
}
