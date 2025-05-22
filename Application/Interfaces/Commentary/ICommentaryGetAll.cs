using Core.Entities;
using DTO;

namespace Application.Interfaces;

public interface ICommentaryGetAll
{
	IEnumerable<CommentaryDto> Execute();
}