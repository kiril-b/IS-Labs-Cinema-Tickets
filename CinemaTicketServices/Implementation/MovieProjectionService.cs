using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Interface;
using CinemaTicketsRepository.Implementation;
using CinemaTicketsRepository.Interface;

namespace CinemaTicketServices.Implementation;

public class MovieProjectionService : IMovieProjectionService {
    
    private readonly IMovieProjectionRepository _movieProjectionRepository;

    public MovieProjectionService(IMovieProjectionRepository movieProjectionRepository) {
        _movieProjectionRepository = movieProjectionRepository;
    }

    public List<MovieProjection> GetAllMovieProjections() {
        return _movieProjectionRepository.GetAll().ToList();
    }

    public MovieProjection GetMovieProjectionById(Guid? id) {
        return _movieProjectionRepository.Get(id);
    }

    public void CreateNewMovieProjection(MovieProjection movieProjection) {
        movieProjection.Id = Guid.NewGuid();
        _movieProjectionRepository.Insert(movieProjection);
    }

    public void UpdateMovieProjection(MovieProjection movieProjection) {
        _movieProjectionRepository.Update(movieProjection);
    }

    public void DeleteMovieProjection(Guid id) {
        MovieProjection movieProjection = _movieProjectionRepository.Get(id);
        _movieProjectionRepository.Delete(movieProjection);
    }

    public bool MovieProjectionExists(Guid id) {
        return _movieProjectionRepository.Get(id) != null;
    }
}