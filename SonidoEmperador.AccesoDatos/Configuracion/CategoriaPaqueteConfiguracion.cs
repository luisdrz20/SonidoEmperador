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
    public class CategoriaPaqueteConfiguracion : IEntityTypeConfiguration<CategoriaPaquete>

    {

        public void Configure(EntityTypeBuilder<CategoriaPaquete> builder)
        {
            builder.Property(x => x.Id_Paquete).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Estado).IsRequired();
        }
    }
}
