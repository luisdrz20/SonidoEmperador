using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonidoEmperador.Modelos
{
    public class CategoriaPaquete
    {
        [Key]
        public int Id_Paquete { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [MaxLength(60, ErrorMessage = "El nombre solo se compone de 60 caracteres")]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "El campo Descripcion es requerido")]
        [MaxLength(300, ErrorMessage = "La descripción solo se compone de 300 caracteres")]
        public String Descripcion { get; set; }
        [Required(ErrorMessage = "El campo Estado de la categoria del paquete es requerido")]
        public bool Estado { get; set; }
    }
}
