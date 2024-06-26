﻿using Microsoft.AspNetCore.Mvc;
using SonidoEmperador.AccesoDatos.Repositorio.IRepositorio;
using SonidoEmperador.Modelos;
using SonidoEmperador.Modelos.Espesificaciones;
using SonidoEmperador.Modelos.ViewModels;
using System.Diagnostics;

namespace SonidoEmperador.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public HomeController(ILogger<HomeController> logger,IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
        }

        public  IActionResult Index(int pageNumber=1, string busqueda="",
                                    string busquedaActual="")
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
            var resultado = _unidadTrabajo.Paquete.ObtenerTodosPaginado(parametros);

            if(!String.IsNullOrEmpty(busqueda))
            {
                resultado = _unidadTrabajo.Paquete.ObtenerTodosPaginado(parametros,
                    p => p.Descripcion.Contains(busqueda));
            }


            ViewData["TotalPaginas"]=resultado.MetaData.TotalPages;
            ViewData["TotalRegistros"]= resultado.MetaData.TotalCount;
            ViewData["PageSize"] = resultado.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disabled";
            ViewData["Siguiente"] = "";

            if (pageNumber > 1)
            {
                ViewData["Previo"] = "";
            }
            if(resultado.MetaData.TotalPages<= pageNumber)
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
