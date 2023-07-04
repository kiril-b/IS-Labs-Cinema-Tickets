using CinemaTicketsDomain.DomainModels;

namespace CinemaTicketServices.Interface;

public interface IMovieService {
    List<Movie> GetAllMovies();
    Movie GetMovieById(Guid? id);
    void CreateNewMovie(Movie movie);
    void UpdateMovie(Movie movie);
    void DeleteMovie(Guid id);
    bool MovieExists(Guid id);
}