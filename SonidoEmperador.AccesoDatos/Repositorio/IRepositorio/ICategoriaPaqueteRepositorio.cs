using SonidoEmperador.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonidoEmperador.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICategoriaPaqueteRepositorio : IRepositorio<CategoriaPaquete>
    {
        void Actualizar(CategoriaPaquete categoriaPaquete);
    }

}
