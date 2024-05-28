using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonidoEmperador.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        //Aqui se van  gregar todos los nuevos modelos
  
        ICategoriaRepositorio Categoria { get; }
  
        IProductoRepositorio Producto { get; }

        IUsuarioAplicacionRepositorio UsuarioAplicacion { get; }

        IPaqueteRepositorio Paquete { get; }
        Task Guardar();

    }

}
