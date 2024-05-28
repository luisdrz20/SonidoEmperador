using Microsoft.AspNetCore.Mvc.Rendering;
using SonidoEmperador.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonidoEmperador.AccesoDatos.Repositorio.IRepositorio
{
    public interface IPaqueteRepositorio : IRepositorio<Paquete>
    {
        void Actualizar(Paquete paquete);
       // IEnumerable<SelectListItem> ObtenerTodosDropDownList (string obj);
    }

}
