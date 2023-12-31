﻿using CinemaTicketsDomain.DomainModels;
using CinemaTicketsRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsRepository.Implementation;

public class MovieProjectionRepository : IMovieProjectionRepository {
    private readonly ApplicationDbContext context;
    private DbSet<MovieProjection> entities;
    string errorMessage = string.Empty;

    public MovieProjectionRepository(ApplicationDbContext context) {
        this.context = context;
        this.entities = context.Set<MovieProjection>();
    }

    public IEnumerable<MovieProjection> GetProjections() {
        return this.entities
            .Include(m => m.Movie)
            .Where(m => m.DateTime >= DateTime.Now)
            .AsEnumerable();
    }

    public IEnumerable<MovieProjection> GetFilteredProjections(DateTime from, DateTime to) {
        return this.entities
            .Include(m => m.Movie)
            .Where(m => m.AvailableTickets > 0 && m.DateTime >= from &&  m.DateTime <= to)
            .AsEnumerable();
    }

    public MovieProjection Get(Guid? id) {
        return entities
            .Include(m => m.Movie)
            .SingleOrDefault(s => s.Id == id);
    }

    public void Insert(MovieProjection entity) {
        if (entity == null) {
            throw new ArgumentNullException("entity");
        }

        entities.Add(entity);
        context.SaveChanges();
    }

    public void Update(MovieProjection entity) {
        if (entity == null) {
            throw new ArgumentNullException("entity");
        }

        entities.Update(entity);
        context.SaveChanges();
    }

    public void Delete(MovieProjection entity) {
        if (entity == null) {
            throw new ArgumentNullException("entity");
        }

        entities.Remove(entity);
        context.SaveChanges();
    }
}