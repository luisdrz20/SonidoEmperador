using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class PaqueteRepositorio : Repositorio<Paquete>, IPaqueteRepositorio
    {
        private readonly ApplicationDbContext _db;

        public PaqueteRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Paquete paquete)
        {
            var paqueteBD = _db.Paquetes.FirstOrDefault(b => b.Id == paquete.Id);
            if (paqueteBD != null)
            {
                if (paquete.ImagenUrl != null)
                {
                    paqueteBD.ImagenUrl = paquete.ImagenUrl;
                }


                paqueteBD.Nombre = paquete.Nombre;
                paqueteBD.Descripcion = paquete.Descripcion;
                paqueteBD.Precio = paquete.Precio;
                paqueteBD.Duracion = paquete.Duracion;
                paqueteBD.Componentes = paquete.Componentes;

                paqueteBD.RequisitosAdicionales = paquete.RequisitosAdicionales;

                paqueteBD.Estado = paquete.Estado;

                _db.SaveChanges();
            }
        }

        //public IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj)
        //{
        //    if (obj == "Categoria")
        //    {
        //        return _db.Categorias.Where(c => c.Estado == true).Select(c => new SelectListItem 
        //        {
        //            Text = c.Nombre,
        //            Value = c.Id.ToString(),
        //        });

        //    }

        //    return null;
        //}

    }


}
