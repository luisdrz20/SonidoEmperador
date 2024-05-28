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
        public ICategoriaPaqueteRepositorio CategoriaPaquete { get; set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            CategoriaPaquete = new CategoriaPaqueteRepositorio(_db);
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
