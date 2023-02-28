using Web.Api.ViewModels;
using Web.Api.ViewModels.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Api.Repositories.IRepositories
{
    public interface IUsersRepository
    {
        Task<UserLoginViewModel> GetForLogin(string email, CancellationToken cancellationToken);
        Task<UserLoginViewModel> GetForLoginExist(string email, CancellationToken cancellationToken);
        Task<UserViewModel> GetUserByEmail(string email, CancellationToken cancellationToken);
        Task<UserRefreshTokenViewModel> GetByRefreshToken(string token, CancellationToken cancellationToken);
        void UpdateRefreshToken(int userId, RefreshTokenViewModel newRefreshToken, RefreshTokenViewModel oldRefreshToken, CancellationToken cancellationToken);
        Task<int> SaveRefreshToken(int userId, RefreshTokenViewModel newRefreshToken, CancellationToken cancellationToken);
        Task<IQueryable<UserViewModel>> GetByFilter(UsersFilterViewModel filters, CancellationToken cancellationToken);
        Task<UserViewModel> GetById(int id, CancellationToken cancellationToken);
        Task<IList<ProfilesBaseViewModel>> GetProfilesById(int id, CancellationToken cancellationToken);
        Task<UserViewModel> Save(UserViewModel entityToAdd, CancellationToken cancellationToken);
        Task<UserLoginViewModel> Save(UserCreateViewModel entityToAdd, CancellationToken cancellationToken);
        Task<UserPutViewModel> Update(UserPutViewModel entityToEdit, CancellationToken cancellationToken);
        Task<UserViewModel> Delete(UserViewModel entityToDelete, CancellationToken cancellationToken);
    }
}
