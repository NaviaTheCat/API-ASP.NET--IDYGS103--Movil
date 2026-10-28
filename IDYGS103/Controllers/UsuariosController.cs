using Domain.DTO;
using Domain.Entities;
using IDYGS103.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IDYGS103.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;

        public UsuariosController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }
        // GET: api/Usuarios
        [HttpGet]
        public async Task<IActionResult> GetUsuers()
        {
            var response = await _usuarioServices.ObtenerUsaurios();
            return Ok(response);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ById(int id)
        {
            var response = await _usuarioServices.ById(id);
            return Ok(response);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuarioDto i)
        {
            Usuario usuario1 = new Usuario()
            {
                Nombre = i.Nombre,
                UserName = i.UserName,
                Password = i.Password,
                FkRol = i.FkRol

            };

            var result = await _usuarioServices.Create(usuario1);
            return Ok(result);
        }


        // PUT: api/Usuarios/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDto i)
        {
            Usuario usuario1 = new Usuario()
            {
                PkUsuario = id,
                Nombre = i.Nombre,
                UserName = i.UserName,
                Password = i.Password,
                FkRol = i.FkRol
            };

            var result = await _usuarioServices.Update(id, usuario1);
            return Ok(result);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuarioServices.Delete(id);
            return Ok(result);
        }
    }
}
