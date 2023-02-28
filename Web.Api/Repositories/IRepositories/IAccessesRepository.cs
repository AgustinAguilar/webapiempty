using Web.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Api.Repositories.IRepositories
{
    public interface IAccessesRepository
    {
        Task<bool> FirstAccess(int userId);
        Task<AccessViewModel> Save(AccessViewModel entityToAdd, CancellationToken cancellationToken);
        Task<IEnumerable<AccessViewModel>> GetByFilter(AccessFilterViewModel filters, CancellationToken cancellationToken);
    }
}
