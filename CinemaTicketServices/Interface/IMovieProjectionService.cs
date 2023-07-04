using CinemaTicketsDomain.DomainModels;

namespace CinemaTicketServices.Interface;

public interface IMovieProjectionService {
    List<MovieProjection> GetAllMovieProjections();
    MovieProjection GetMovieProjectionById(Guid? id);
    void CreateNewMovieProjection(MovieProjection movie);
    void UpdateMovieProjection(MovieProjection movie);
    void DeleteMovieProjection(Guid id);
    bool MovieProjectionExists(Guid id);
}