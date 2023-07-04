using CinemaTicketsDomain.DomainModels;

namespace CinemaTicketsRepository.Interface;

public interface IMovieProjectionRepository {
    IEnumerable<MovieProjection> GetAll();
    MovieProjection Get(Guid? id);
    void Insert(MovieProjection entity);
    void Update(MovieProjection entity);
    void Delete(MovieProjection entity);
}