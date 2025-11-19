using Building_MinimalAPIsMoviesApp.DTOs;
using Building_MinimalAPIsMoviesApp.Entities;
using Building_MinimalAPIsMoviesApp.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace Building_MinimalAPIsMoviesApp.Endpoints
{
    public static class GenresEndpoints
    {
        public static RouteGroupBuilder MapGenres(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetGenres).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("genres-get"));

            group.MapGet("/{id:int}", GetById);

            group.MapPost("/", Create);

            group.MapPut("/{id:int}", Update);

            group.MapDelete("/{id:int}", Delete);

            return group;
        }

        static async Task<Ok<List<GenreDTO>>> GetGenres(IGenresRepositories repository)
        {
            var genres = await repository.GetAll();
            var genreDTO = genres.Select(genre => new GenreDTO { Id = genre.Id, Name = genre.Name }).ToList();
            return TypedResults.Ok(genreDTO);
        }

        static async Task<Results<Ok<GenreDTO>, NotFound>> GetById(int id, IGenresRepositories repository)
        {
            var genre = await repository.GetById(id);

            if (genre == null)
            {
                return TypedResults.NotFound();
            }

            var genreDTO = new GenreDTO 
            {
                Id = genre.Id,
                Name = genre.Name,
            };

            return TypedResults.Ok(genreDTO);
        }

        static async Task<Created<GenreDTO>> Create(CreateGenreDTO createGenreDTO, IGenresRepositories repository, IOutputCacheStore outputCacheStore)
        {
            var genre = new Genre
            {
                Name = createGenreDTO.Name,
            };
            var id = await repository.Create(genre);
            await outputCacheStore.EvictByTagAsync("genres-get", default);
            var genreDTO = new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name,
            };
            return TypedResults.Created($"/geners/{id}", genreDTO);
        }

        static async Task<Results<NotFound, NoContent>> Update(int id, CreateGenreDTO createGenreDTO, IGenresRepositories repository, IOutputCacheStore outputCacheStore)
        {
            var exists = await repository.Exists(id);
            if (!exists)
            {
                return TypedResults.NotFound();
            }
            var genre = new Genre
            {
                Id = id,
                Name = createGenreDTO.Name,
            };
            await repository.Update(genre);
            await outputCacheStore.EvictByTagAsync("genres-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NotFound, NoContent>> Delete(int id, IGenresRepositories repository, IOutputCacheStore outputCacheStore)
        {
            var exists = await repository.Exists(id);
            if (!exists)
            {
                return TypedResults.NotFound();
            }
            await repository.Delete(id);
            await outputCacheStore.EvictByTagAsync("genres-get", default);
            return TypedResults.NoContent();
        }

    }
}
