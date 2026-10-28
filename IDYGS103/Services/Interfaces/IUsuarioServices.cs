using Domain.Entities;
using Domain.Helpers;
using IDYGS103.Context;
namespace IDYGS103.Services.Interfaces
{
    public interface IUsuarioServices
    {
        public Task<Response<List<Usuario>>> ObtenerUsaurios();
        public Task<Response<Usuario>> ById(int id);
        public Task<Response<Usuario>> Create(Usuario request);
        public Task<Response<Usuario>> Update(int id ,Usuario request);
        public Task<Response<Usuario>> Delete(int id);

    }
}
