﻿using UserTestingApplication.Data;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Filters;
using UserTestingApplication.Repositories.Inerfaces;

namespace UserTestingApplication.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationUserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ApplicationUser applicationUser)
        {
            await _dbContext.Set<ApplicationUser>().AddAsync(applicationUser);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ApplicationUser applicationUser)
        {
            _dbContext.Set<ApplicationUser>().Remove(applicationUser);
        }

        public async Task<IQueryable<ApplicationUser>> GetAsync(ApplicationUserFilter applicationUserFilter)
        {
            var query = _dbContext.ApplicationUsers.AsQueryable();

            if (applicationUserFilter.Id != null)
                query = query.Where(applicationUser => applicationUser.Id == applicationUserFilter.Id);
            else if (applicationUserFilter.Email != null)
                query = query.Where(applicationUser => applicationUser.Email == applicationUserFilter.Email);
            else if (applicationUserFilter.UserName != null)
                query = query.Where(applicationUser => applicationUser.UserName == applicationUserFilter.UserName);

            return query;
        }
    }
}