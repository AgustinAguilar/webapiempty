using AutoMapper;
using AutoMapper.QueryableExtensions;
using Web.Api.Model;
using Web.Api.Model.Classes;
using Web.Api.Repositories.IRepositories;
using Web.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Api.Repositories
{
    public class AccessesRepository : IAccessesRepository
    {
        private readonly DomainContext _context;
        private readonly IMapper _mapper;

        public AccessesRepository(DomainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> FirstAccess(int userId)
        {
            var count = await _context.Accesses
                .Where(x => x.UserId == userId)
                .CountAsync();

            return count == 0;
        }

        public async Task<IEnumerable<AccessViewModel>> GetByFilter(AccessFilterViewModel filters, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_context.Accesses
                .Include(p => p.User)
                .Include(p => p.AccessType)
                .Where(p => filters.FromDate == null || filters.FromDate <= p.Date)
                .Where(p => filters.ToDate == null || filters.ToDate >= p.Date)
                .OrderByDescending(p => p.Date)
                .ProjectTo<AccessViewModel>(_mapper.ConfigurationProvider)
                .AsEnumerable());
        }

        public Task<AccessViewModel> Save(AccessViewModel entityToAdd, CancellationToken cancellationToken)
        {
            var entityDb = _context.Accesses.Add(_mapper.Map<Access>(entityToAdd)).Entity;
            cancellationToken.ThrowIfCancellationRequested();
            _context.SaveChanges();
            return Task.FromResult(_mapper.Map<AccessViewModel>(entityDb));
        }
    }
}
