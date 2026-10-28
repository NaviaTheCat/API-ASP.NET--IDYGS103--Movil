using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Helpers;
using IDYGS103.Context;
using IDYGS103.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
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

        //OBTENER TODOS LOS USUARIOS
        public async Task<Response<List<Usuario>>> ObtenerUsaurios()
        {
            try
            {
              //  List<Usuario> response = await _context.Usuarios.Include(x => x.Roles).ToListAsync();
              IEnumerable<Usuario> response = await _context.Database.GetDbConnection().
                    QueryAsync<Usuario>("spGetUsers", new {}, commandType:CommandType.StoredProcedure);
                return new Response<List<Usuario>>(response.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }


        //USUARIOS POR ID
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

        //CREAR USUARIOS    
        private readonly PasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();
        public async Task<Response<Usuario>> Create(Usuario request)
        {
            try
            {
                Usuario newUsers = new Usuario();
                {
                    newUsers.Nombre = request.Nombre;
                    newUsers.UserName = request.UserName;
                    newUsers.Password = _passwordHasher.HashPassword(newUsers, request.Password);
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


        //UPDATE DE USUARIOS
        public async Task<Response<Usuario>> Update(int id, Usuario request)
        {
            try
            {
                Usuario result = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == request.PkUsuario);
                if (result == null)
                {
                    return new Response<Usuario>(null, "No se encontro el usuario");
                }
                result.Nombre = request.Nombre;
                result.UserName = request.UserName;
                result.Password = request.Password;
                result.FkRol = request.FkRol;
                _context.Usuarios.Update(result);
                _context.SaveChanges();
                return new Response<Usuario>(result, "Se actualizo correctamente");
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        //DELETE DE USUARIOS
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
