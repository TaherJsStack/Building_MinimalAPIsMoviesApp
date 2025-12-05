using Building_MinimalAPIsMoviesApp.Entities;

namespace Building_MinimalAPIsMoviesApp.Repositories
{
    public interface IErrorsRepository
    {
        Task Create(Error error);
    }
}