using Core.Entities;

namespace Core.Interfaces
{
    public interface ICommentaryRepository
    {
        Commentary Add ( Commentary commentary );
        Commentary Update ( Commentary commentary );
        Commentary Delete ( Commentary commentary );
        Commentary GetByTrackingId ( int id );
        IEnumerable<Commentary> GetAll();
    }
}