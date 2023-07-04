using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Interface;
using CinemaTicketsRepository.Interface;

namespace CinemaTicketServices.Implementation;

public class MovieService : IMovieService {
    
    private readonly IRepository<Movie> _movieRepository;

    public MovieService(IRepository<Movie> movieRepository) {
        _movieRepository = movieRepository;
    }

    public List<Movie> GetAllMovies() {
        return _movieRepository.GetAll().ToList();
    }

    public Movie GetMovieById(Guid? id) {
        return _movieRepository.Get(id);
    }

    public void CreateNewMovie(Movie movie) {
        movie.Id = Guid.NewGuid();
        _movieRepository.Insert(movie);
    }

    public void UpdateMovie(Movie movie) {
        _movieRepository.Update(movie);
    }

    public void DeleteMovie(Guid id) {
        Movie movie = _movieRepository.Get(id);
        _movieRepository.Delete(movie);
    }

    public bool MovieExists(Guid id) {
        return _movieRepository.Get(id) != null;
    }
}