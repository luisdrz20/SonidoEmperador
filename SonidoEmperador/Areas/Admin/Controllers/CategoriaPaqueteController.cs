using Microsoft.AspNetCore.Mvc;
using SonidoEmperador.AccesoDatos.Repositorio.IRepositorio;
using SonidoEmperador.Modelos;

namespace SonidoEmperador.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaPaqueteController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public CategoriaPaqueteController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Metodo Upsert GET
        public async Task<IActionResult> Upsert(int? id)
        {
            CategoriaPaquete categoriaPaquete = new CategoriaPaquete();
            if (id == null)
            {
                //Creamos un nuevo registro
                categoriaPaquete.Estado = true;
                return View(categoriaPaquete);
            }
            categoriaPaquete = await _unidadTrabajo.CategoriaPaquete.Obtener(id.GetValueOrDefault());
            if (categoriaPaquete == null)
            {
                return NotFound();
            }
            return View(categoriaPaquete);
        }


        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.CategoriaPaquete.ObtenerTodos();
            return Json(new { data = todos });
        }
        #endregion
    }
}
