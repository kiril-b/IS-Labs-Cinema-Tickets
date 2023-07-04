using System.Runtime.InteropServices.JavaScript;
using CinemaTicketsDomain.DomainModels;

namespace CinemaTicketsRepository.Interface;

public interface IMovieProjectionRepository {
    IEnumerable<MovieProjection> GetAvailableProjections();
    IEnumerable<MovieProjection> GetFilteredProjections(DateTime from, DateTime to);
    MovieProjection Get(Guid? id);
    void Insert(MovieProjection entity);
    void Update(MovieProjection entity);
    void Delete(MovieProjection entity);
}