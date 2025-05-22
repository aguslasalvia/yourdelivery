using Core.Entities;
using DTO;

namespace Application.Interfaces;

public interface ICommentaryCreate
{
	void Execute(CommentaryCreateDto commentaryDto);
}