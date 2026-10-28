using Domain.Entities;
using Domain.Helpers;

namespace IDYGS103.Services.Interfaces
{
    public interface IRolesServices
    {
        public Task<Response<List<Rol>>> ObtenerRoles();
        public Task<Response<Rol>> ById(int id);
        public Task<Response<Rol>> Create(Rol request);
        public Task<Response<Rol>> Update(int id, Rol request);
        public Task<Response<Rol>> Delete(int id);

    }
}
