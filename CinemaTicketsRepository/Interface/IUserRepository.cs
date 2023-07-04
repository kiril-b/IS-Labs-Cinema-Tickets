using CinemaTicketsDomain.Identity;

namespace CinemaTicketsRepository.Interface;

public interface IUserRepository {
    IEnumerable<CustomUser> GetAll();
    CustomUser Get(string id);
    void Insert(CustomUser entity);
    void Update(CustomUser entity);
    void Delete(CustomUser entity);
}