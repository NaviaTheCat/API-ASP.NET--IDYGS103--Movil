using Domain.Entities;
using Domain.Helpers;
using IDYGS103.Context;
using IDYGS103.Services.Interfaces;
using Microsoft.EntityFrameworkCore; 

namespace IDYGS103.Services.Services

{
    // Todas las funcionalidades para la BD
    public class UsuarioService: IUsuarioServices
    {
        private readonly ApplicationDbContext _context;
        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Response<List<Usuario>>> ObtenerUsaurios()
        {
            try
            {
                List<Usuario> response = await _context.Usuarios.ToListAsync();
                return new Response<List<Usuario>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        public async Task<Response<Usuario>> ById(int id)
        {
            try
            {
                Usuario result = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);
                return new Response<Usuario>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Response<Usuario>> Create(Usuario request)
        {
            try
            {
                Usuario newUsers = new Usuario();
                {
                    newUsers.Nombre = request.Nombre;
                    newUsers.UserName = request.UserName;
                    newUsers.Password = request.Password;
                    newUsers.FkRol = request.FkRol;
                    
                };

                var result = await _context.Usuarios.AddAsync(newUsers);
                _context.SaveChanges();
                
                return new Response<Usuario>(newUsers, "Se creo correctamente ");
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

       
        public async Task<Response<Usuario>> Delete(int id)
        {
            try
            {
                Usuario result = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);
                if (result == null)
                {
                    return new Response<Usuario>(null, "No se encontro el usuario");
                }
                _context.Usuarios.Remove(result);
                _context.SaveChanges();
                return new Response<Usuario>(result, "Se elimino correctamente");
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

    }
}
