using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDYGS103.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        //MODELOS A MAPEAR
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //insertar en la tabla de usuarios

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    PkUsuario = 1,
                    Nombre = "Ricardo",
                    UserName = "NaviVani",
                    Password = "Universal.25",
                    FkRol = 1
                }
                );

            //inserta en la tabla de roles
            modelBuilder.Entity<Rol>().HasData(
                 new Rol
                 {
                     PkRol = 1,
                     Nombre = "sa",
                     Estatus = true

                 },
                new Rol
                {
                    PkRol = 2,
                    Nombre = "admin",
                    Estatus = false
                }
                );
               

            base.OnModelCreating(modelBuilder);
        }
    }
}