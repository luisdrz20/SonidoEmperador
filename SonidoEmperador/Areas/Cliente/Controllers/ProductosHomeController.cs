using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SonidoEmperador.AccesoDatos.Repositorio.IRepositorio;
using SonidoEmperador.Modelos;
using SonidoEmperador.Modelos.Espesificaciones;
using SonidoEmperador.Modelos.ViewModels;
using SonidoEmperador.Utilidades;
using System.Diagnostics;

namespace SonidoEmperador.Areas.Cliente.Controllers
{
    [Area("Cliente")]

    public class ProductosHomeController : Controller
    {
        private readonly ILogger<ProductosHomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ProductosHomeController(ILogger<ProductosHomeController> logger, IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult ProductosHome(int pageNumber = 1, string busqueda = "",
                                    string busquedaActual = "")
        {
            if (!String.IsNullOrEmpty(busqueda))
            {
                pageNumber = 1;
            }
            else
            {
                busqueda = busquedaActual;
            }
            ViewData["BusquedaActual"] = busqueda;

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            Parametros parametros = new Parametros()
            {
                PageNumber = pageNumber,
                PageSize = 4//Controla la cantidad de articulos por pagina
            };
            var resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros);

            if (!String.IsNullOrEmpty(busqueda))
            {
                resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros,
                    p => p.Nombre.Contains(busqueda));
            }


            ViewData["TotalPaginas"] = resultado.MetaData.TotalPages;
            ViewData["TotalRegistros"] = resultado.MetaData.TotalCount;
            ViewData["PageSize"] = resultado.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disabled";
            ViewData["Siguiente"] = "";

            if (pageNumber > 1)
            {
                ViewData["Previo"] = "";
            }
            if (resultado.MetaData.TotalPages <= pageNumber)
            {
                ViewData["Siguiente"] = "disabled";
            }

            return View(resultado);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
