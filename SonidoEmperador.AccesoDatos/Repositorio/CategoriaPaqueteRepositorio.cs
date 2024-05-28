using SonidoEmperador.AccesoDatos.Data;
using SonidoEmperador.AccesoDatos.Repositorio.IRepositorio;
using SonidoEmperador.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonidoEmperador.AccesoDatos.Repositorio
{
    public class CategoriaPaqueteRepositorio : Repositorio<CategoriaPaquete>, ICategoriaPaqueteRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CategoriaPaqueteRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(CategoriaPaquete categoriaPaquete)
        {
            var categoriaPaqueteBD = _db.CategoriaPaquetes.FirstOrDefault(b => b.Id_Paquete == categoriaPaquete.Id_Paquete);
            if (categoriaPaqueteBD != null)
            {
                categoriaPaqueteBD.Nombre = categoriaPaquete.Nombre;
                categoriaPaqueteBD.Descripcion = categoriaPaquete.Descripcion;
                categoriaPaqueteBD.Estado = categoriaPaquete.Estado;
                _db.SaveChanges();
            }
        }
    }

}
