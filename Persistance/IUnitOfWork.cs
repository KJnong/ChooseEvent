using ChooseEvent2.Repositories;

namespace ChooseEvent2.Persistance
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository applicationUserRepository { get; }
        IGenreRepository genreRepository { get; }
        IGigRepository gigRepository { get; }

        void Complete();
    }
}