namespace CinemaTicketServices.Interface; 

public interface IMailService {
    void SendEmail(string to, string subject, string content);
}