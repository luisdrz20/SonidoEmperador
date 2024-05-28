using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SonidoEmperador.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonidoEmperador.AccesoDatos.Configuracion
{
    public class PaqueteConfiguracion:IEntityTypeConfiguration<Paquete>
    {
        public void Configure(EntityTypeBuilder<Paquete> builder) 
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Precio).IsRequired();
            builder.Property(x => x.Duracion).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Componentes).IsRequired();   
            builder.Property(x => x.RequisitosAdicionales).IsRequired(false);
            builder.Property(x => x.ImagenUrl).IsRequired(false);
            builder.Property(x => x.Estado).IsRequired();
        }

    }
}
