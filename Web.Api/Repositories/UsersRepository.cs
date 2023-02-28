using AutoMapper;
using AutoMapper.QueryableExtensions;
using Web.Api.Exceptions;
using Web.Api.Model;
using Web.Api.Model.Classes;
using Web.Api.Repositories.IRepositories;
using Web.Api.ViewModels;
using Web.Api.ViewModels.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Api.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DomainContext _context;
        private readonly IMapper _mapper;
        public UsersRepository(DomainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }   

        public async Task<UserLoginViewModel> GetForLogin(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var ent = await _context.Users
                .Include(p => p.ProfileUsers).ThenInclude(p => p.Profile)
                .FirstOrDefaultAsync(p => p.Email == email && p.Enabled, cancellationToken);
            return _mapper.Map<UserLoginViewModel>(ent);
        }

        public async Task<UserLoginViewModel> GetForLoginExist(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var ent = await _context.Users
                .Include(p => p.ProfileUsers).ThenInclude(p => p.Profile)
                .FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
            return _mapper.Map<UserLoginViewModel>(ent);
        }

        public async Task<UserViewModel> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var ent = await _context.Users
                .Include(p => p.ProfileUsers).ThenInclude(p => p.Profile)
                .FirstOrDefaultAsync(p => p.Email == email && p.Enabled, cancellationToken);
            return _mapper.Map<UserViewModel>(ent);
        }

        public Task<UserRefreshTokenViewModel> GetByRefreshToken(string token, CancellationToken cancellationToken)
        {
           return _context.Users.Where(u => u.RefreshTokens.Any(t => t.Token == token))
                 .ProjectTo<UserRefreshTokenViewModel>(_mapper.ConfigurationProvider)
                 .SingleOrDefaultAsync();
        }

        public Task<int> SaveRefreshToken(int userId, RefreshTokenViewModel newRefreshToken, CancellationToken cancellationToken)
        {
            var refreshTokenToAdd = _mapper.Map<RefreshToken>(newRefreshToken);
            refreshTokenToAdd.UserId = userId;
            _context.RefreshTokens.Add(refreshTokenToAdd);
            return Task.FromResult(_context.SaveChanges());
        }

        public void UpdateRefreshToken(int userId, RefreshTokenViewModel newRefreshToken, RefreshTokenViewModel oldRefreshToken, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(p => p.Id == userId);
            user.RefreshTokens.Add(_mapper.Map<RefreshToken>(newRefreshToken));
            var oldRefreshTokenBd = user.RefreshTokens.FirstOrDefault(p => p.Id == oldRefreshToken.Id);
            _mapper.Map(oldRefreshToken, oldRefreshTokenBd);
            _context.Update(user);
            _context.SaveChanges();
        }

        public Task<IQueryable<UserViewModel>> GetByFilter(UsersFilterViewModel filters, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(_context.Users
               .Where(p=> filters.ShowDeleted || p.Enabled )
               .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider));
        }

        public Task<UserViewModel> GetById(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return  _context.Users.Where(p => p.Id == id)
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<ProfilesBaseViewModel>> GetProfilesById(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entityDb = await _context.Profiles.Where(p => p.ProfileUsers.Any(x => x.UserId == id))
                .ToListAsync();

            return _mapper.Map<IList<ProfilesBaseViewModel>>(entityDb);
        }

        public Task<UserViewModel> Save(UserViewModel entityToAdd, CancellationToken cancellationToken)
        {
            var entityDb = _context.Users.Add(_mapper.Map<User>(entityToAdd)).Entity;
            cancellationToken.ThrowIfCancellationRequested();
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<UserViewModel>(entityDb));
        }
        public Task<UserLoginViewModel> Save(UserCreateViewModel entityToAdd, CancellationToken cancellationToken)
        {
            var entityDb = _context.Users.Add(_mapper.Map<User>(entityToAdd)).Entity;
            cancellationToken.ThrowIfCancellationRequested();
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<UserLoginViewModel>(entityDb));
        }

        public Task<UserPutViewModel> Update(UserPutViewModel entityToEdit, CancellationToken cancellationToken)
        {
            var entityDb = _context.Users.Include(p=> p.ProfileUsers)
                                         .FirstOrDefault(p => p.Id == entityToEdit.Id);
            if (entityDb == null) throw new EntityToEditNotFoundException();
            _mapper.Map(entityToEdit, entityDb);

            var profiles = entityDb.ProfileUsers.ToList();
            foreach (var profile in profiles)
                entityDb.ProfileUsers.Remove(profile);

            if (entityToEdit.ProfileId != null && entityToEdit.ProfileId > 0)
                entityDb.ProfileUsers.Add(new ProfileUser { ProfileId = entityToEdit.ProfileId.Value });

            cancellationToken.ThrowIfCancellationRequested();
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<UserPutViewModel>(entityDb));
        }

        public Task<UserViewModel> Delete(UserViewModel entityToDelete, CancellationToken cancellationToken)
        {
            var entityDb = _context.Users.FirstOrDefault(p => p.Id == entityToDelete.Id);
            if (entityDb == null) throw new EntityToEditNotFoundException();
            _mapper.Map(entityToDelete, entityDb);
            cancellationToken.ThrowIfCancellationRequested();
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<UserViewModel>(entityDb));
        }
    }
}
