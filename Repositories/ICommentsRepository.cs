using Building_MinimalAPIsMoviesApp.DTOs;
using Building_MinimalAPIsMoviesApp.Entities;

namespace Building_MinimalAPIsMoviesApp.Repositories
{
    public interface ICommentsRepository
    {
        Task<int> Create(Comment comment);
        Task Delete(int id);
        Task<bool> Exists(int id);
        Task<List<Comment>> GetAll(int moviId, PaginationDTO paginationDTO);
        Task<Comment?> GetById(int id);
        Task Update(Comment comment);
    }
}