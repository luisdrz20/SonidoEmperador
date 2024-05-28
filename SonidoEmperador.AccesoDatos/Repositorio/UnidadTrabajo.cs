using SonidoEmperador.AccesoDatos.Data;
using SonidoEmperador.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonidoEmperador.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public ICategoriaRepositorio Categoria { get; set; }
        

        public IProductoRepositorio Producto { get; set; }

        public IPaqueteRepositorio Paquete { get; set; }

        public IUsuarioAplicacionRepositorio UsuarioAplicacion { get; set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
         
            Categoria = new CategoriaRepositorio(_db);
      
            Producto = new ProductoRepositorio(_db);
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
            Paquete = new PaqueteRepositorio(_db);
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }

}
