using Domain.DTO;
using Domain.Entities;
using IDYGS103.Services.Interfaces;
using IDYGS103.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDYGS103.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController: ControllerBase
    {
        private readonly IRolesServices _rolesServices;
        public RolesController(IRolesServices rolesServices)
        {
            _rolesServices = rolesServices;
        }
        // GET: Roles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var response = await _rolesServices.ObtenerRoles();
            return Ok(response);
        }

        // GET: Roles/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ById(int id)
        {
            var response = await _rolesServices.ById(id);
            return Ok(response);
        }

        // POST: Roles
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] RolDto i)
        {
            Rol rol1 = new Rol()
            {
                Nombre = i.Nombre,
                Estatus = i.Estatus

            };

            var result = await _rolesServices.Create(rol1);
            return Ok(result);
        }


        // PUT: Roles/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolDto i)
        {
            Rol rol1 = new Rol()
            {
                PkRol = id,
                Nombre = i.Nombre,
                Estatus = i.Estatus,
            };

            var result = await _rolesServices.Update(id, rol1);
            return Ok(result);
        }

        // DELETE: Roles/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rolesServices.Delete(id);
            return Ok(result);
        }



    }
}
