﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonidoEmperador.AccesoDatos.Data;
using SonidoEmperador.AccesoDatos.Repositorio.IRepositorio;
using SonidoEmperador.Utilidades;

namespace SonidoEmperador.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]
    public class UsuarioController : Controller
    {

        //Crear los accesos a la unidad de trabajo y a la base de datos
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ApplicationDbContext _db;

        //inyeccion de dependencias 
        public UsuarioController(IUnidadTrabajo unidadTrabajo,ApplicationDbContext db)
        {
            _unidadTrabajo = unidadTrabajo;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarioLista = await _unidadTrabajo.UsuarioAplicacion.ObtenerTodos();
            var userRole =  await _db.UserRoles.ToListAsync();
            var roles = await _db.Roles.ToListAsync();

            foreach (var usuario in usuarioLista)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == usuario.Id).RoleId;
                usuario.Role = roles.FirstOrDefault(u => u.Id == roleId).Name; 

            }
            return Json(new { data = usuarioLista }) ;
        }

        [HttpPost]
        public async Task<IActionResult> BloquearDesbloquear([FromBody] string id)
        {
            var usuario = await _unidadTrabajo.UsuarioAplicacion
                .ObtenerPrimero(u => u.Id == id);
            if(usuario == null)
            {
                return Json(new {success = false, message = "Error de usuario" });
            }
            if(usuario.LockoutEnd != null && usuario.LockoutEnd > DateTime.Now)
            {
                usuario.LockoutEnd = DateTime.Now;
            }else
            {
                //Usuaio desbloqueado y hay que bloquearlo
                usuario.LockoutEnd= DateTime.Now.AddYears(1000);
            }
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Operacion exitosa" });
        }

        #endregion
    }
}
