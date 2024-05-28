using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SonidoEmperador.Modelos
{
    public class Paquete
    {
        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(500, ErrorMessage = "Maximo 500 caracteres")]
        public string Nombre { get; set; }



        [Required(ErrorMessage = "La descripcion es requerida")]
        [MaxLength(2000, ErrorMessage = "Maximo 2000 caracteres")]
        public string Descripcion { get; set; }




        [Required(ErrorMessage = "El precio es requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "La duracion del servicio es requerida")]
        [MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string Duracion { get; set; }


        [Required(ErrorMessage = "Los componentes son requerida")]
        public string Componentes { get; set; }


        public string RequisitosAdicionales { get; set; }

        public string ImagenUrl { get; set; }





        [Required(ErrorMessage = "El estado es requerido")]
        public bool Estado { get; set; }

    }
}
