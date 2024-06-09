using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SonidoEmperador.AccesoDatos.Repositorio.IRepositorio;
using SonidoEmperador.Modelos;
using SonidoEmperador.Modelos.ViewModels;
using SonidoEmperador.Utilidades;

namespace SonidoEmperador.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Inventario)]
    public class PaqueteController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PaqueteController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Metodo Upsert GET
        public async Task<IActionResult> Upsert(int? id)
        {
            PaqueteVM paqueteVM = new PaqueteVM()
            {
                Paquete = new Paquete()

            };
            if (id == null)
            {
                //Crear un producto Nuevo
                return View(paqueteVM);

            }else
            {
                //Actualizar un producto existente
                paqueteVM.Paquete = await _unidadTrabajo.Paquete
                    .Obtener(id.GetValueOrDefault());
                if(paqueteVM.Paquete == null)
                {
                    return NotFound();
                }
                return View(paqueteVM);
            }
        }




        #region API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upsert(PaqueteVM paqueteVM)
        {       
            if (ModelState.IsValid)
            {               
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(paqueteVM.Paquete.Id == 0)
                {
                    //crear un nuevo paquete
                    string upload = webRootPath + DS.ImagenRutaPaquetes;
                    //Crear un id unico en mi sistema
                    string fileName = Guid.NewGuid().ToString();
                    //creamos una variable para conocer la extencion del archivo
                    string extension = Path.GetExtension(files[0].FileName);

                    //habilitar el filestream para crear el archivo de imagen en tiempo real 
                    using(var filestream = new FileStream(Path.Combine(upload, fileName + extension)
                                                           , FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }

                    paqueteVM.Paquete.ImagenUrl = fileName + extension;
                    await _unidadTrabajo.Paquete.Agregar(paqueteVM.Paquete);
                }
                else
                {
                    //Actualizar al paquete
                    var objPaquete = await _unidadTrabajo.Paquete
                                                .ObtenerPrimero(p => p.Id == paqueteVM.Paquete.Id
                                                , isTracking: false);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath+DS.ImagenRutaPaquetes;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //borrar la imagen anterior 
                        var anteriorFile = Path.Combine(upload, objPaquete.ImagenUrl);

                        //verificamos que la imagen exista

                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }

                        //creamos la nueva imagen
                        using (var filestream = new FileStream(
                                Path.Combine(upload, fileName + extension)
                                , FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }

                        paqueteVM.Paquete.ImagenUrl= fileName + extension;

                    }// si no elige imagen 
                    else
                    {
                        paqueteVM.Paquete.ImagenUrl = objPaquete.ImagenUrl;
                    }
                    _unidadTrabajo.Paquete.Actualizar(paqueteVM.Paquete);
                }
                TempData[DS.Exitosa] = "Paquete Registrado";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }
            TempData[DS.Error] = "Error al grabar el paquete";
            //paqueteVM.CategoriaLista = _unidadTrabajo.Paquete.ObtenerTodosDropDownList("Catgoria");
            return View(paqueteVM.Paquete);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var paqueteDB = await _unidadTrabajo.Paquete.Obtener(id);
            if (paqueteDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el rgistro en la Base de datos" });
            }
            //borrar la imagen del paquete eliminado
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRutaPaquetes;
            var anteriorFile = Path.Combine(upload, paqueteDB.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {

                System.IO.File.Delete(anteriorFile);

            }

            _unidadTrabajo.Paquete.Remover(paqueteDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Paquete eliminado con exito" });

            

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Paquete.ObtenerTodos();
            return Json(new { data = todos });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre,int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Paquete.ObtenerTodos();
            
            if( id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() 
                                    == nombre.ToLower().Trim()
                                    && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }



        #endregion
    }

}
