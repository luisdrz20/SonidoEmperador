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
    public class UsuarioAplicacionRepositorio : Repositorio<UsuarioAplicacion>, IUsuarioAplicacionRepositorio
    {
        private readonly ApplicationDbContext _db;

        public UsuarioAplicacionRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(UsuarioAplicacion usuarioAplicacion)
        {
            throw new NotImplementedException();
        }
    }


}
