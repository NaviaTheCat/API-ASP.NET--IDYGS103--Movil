using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Helpers;
using IDYGS103.Context;
using IDYGS103.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IDYGS103.Services.Services
{
    public class RolesServices: IRolesServices
    {
        private readonly ApplicationDbContext _context;
        public RolesServices(ApplicationDbContext context)
        {
            _context = context;

        }

        //OBTENER TODOS LOS ROLES
        public async Task<Response<List<Rol>>> ObtenerRoles()
        {
            try
            {
                IEnumerable<Rol> response = await _context.Database.GetDbConnection().
                    QueryAsync<Rol>("spGetRoles", new { }, commandType: CommandType.StoredProcedure);
                return new Response<List<Rol>>(response.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }


        //ROLES POR ID
        public async Task<Response<Rol>> ById(int id)
        {
            try
            {
                IEnumerable<Rol> result = await _context.Database.GetDbConnection().
                    QueryAsync<Rol>("spGetRolById", new { PkRol = id }, commandType: CommandType.StoredProcedure);
                return new Response<Rol>(result.ToList().FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CREAR ROLES USUARIOS    
        public async Task<Response<Rol>> Create(Rol request)
        {
            try
            {
                Rol newRoles = new Rol();
                {
                    newRoles.Nombre = request.Nombre;
                    newRoles.Estatus = request.Estatus;

                };
                
                IEnumerable<Rol> result = await _context.Database.GetDbConnection().
                   QueryAsync<Rol>("spCreateRol", new { Nombre = request.Nombre, Estatus = request.Estatus }, commandType: CommandType.StoredProcedure);

                return new Response<Rol>(newRoles, "Se creo correctamente ");
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }


        //UPDATE DE ROLES
        public async Task<Response<Rol>> Update(int id, Rol request)
        {
            try
            {
               IEnumerable<Rol> result = await _context.Database.GetDbConnection().
                   QueryAsync<Rol>("spUpdateRol", new { PkRol = id, Nombre = request.Nombre, Estatus = request.Estatus }, commandType: CommandType.StoredProcedure);
                return new Response<Rol>(result.ToList().FirstOrDefault(), "Se actualizo correctamente");
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        //DELETE DE ROLES
        public async Task<Response<Rol>> Delete(int id)
        {
            try
            {
                IEnumerable<Rol> result = await _context.Database.GetDbConnection().
                    QueryAsync<Rol>("spDeleteRol", new { PkRol = id }, commandType: CommandType.StoredProcedure);
                return new Response<Rol>(result.ToList().FirstOrDefault(), "Se elimino correctamente");
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

    }
}
