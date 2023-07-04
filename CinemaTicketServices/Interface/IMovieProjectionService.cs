using System.Runtime.InteropServices.JavaScript;
using CinemaTicketsDomain.DomainModels;

namespace CinemaTicketServices.Interface;

public interface IMovieProjectionService {
    List<MovieProjection> GetAllMovieProjections();
    Dictionary<Movie, List<MovieProjection>> GetMovieProjectionsByMovie();
    List<MovieProjection> GetFilteredProjections(DateTime from, DateTime to);
    Dictionary<Movie, List<MovieProjection>> GetGroupedFilteredProjections(DateTime from, DateTime to);
    MovieProjection GetMovieProjectionById(Guid? id);
    void CreateNewMovieProjection(MovieProjection movie);
    void UpdateMovieProjection(MovieProjection movie);
    void DeleteMovieProjection(Guid id);
    bool MovieProjectionExists(Guid id);
}