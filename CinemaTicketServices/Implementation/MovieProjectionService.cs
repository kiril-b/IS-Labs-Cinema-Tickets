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
        return _movieProjectionRepository
            .GetProjections()
            .ToList();
    }

    public Dictionary<Movie, List<MovieProjection>> GetMovieProjectionsByMovie() {
        List<MovieProjection> movieProjections = this.GetAllMovieProjections();
        return MovieProjectionsByMovie(movieProjections);
    }

    private static Dictionary<Movie, List<MovieProjection>> MovieProjectionsByMovie(List<MovieProjection> movieProjections) {
        Dictionary<Movie, List<MovieProjection>> movieProjectionMap = new Dictionary<Movie, List<MovieProjection>>();

        foreach (MovieProjection projection in movieProjections)
            if (movieProjectionMap.ContainsKey(projection.Movie)) {
                movieProjectionMap[projection.Movie].Add(projection);
            }
            else {
                List<MovieProjection> projections = new List<MovieProjection> { projection };
                movieProjectionMap.Add(projection.Movie, projections);
            }

        return movieProjectionMap;
    }


    public List<MovieProjection> GetFilteredProjections(DateTime from, DateTime to) {
        return _movieProjectionRepository
            .GetFilteredProjections(from, to)
            .ToList();
    }

    public Dictionary<Movie, List<MovieProjection>> GetGroupedFilteredProjections(DateTime from, DateTime to) {
        List<MovieProjection> movieProjections = this.GetFilteredProjections(from, to);
        return MovieProjectionsByMovie(movieProjections);
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