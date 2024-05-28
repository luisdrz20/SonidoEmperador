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
    public class ProductoConfiguracion:IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder) 
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Precio).IsRequired();
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.ImagenUrl).IsRequired(false);
            builder.Property(x => x.RequisitosAdicionales).IsRequired(false);
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.CategoriaId).IsRequired();
            //Hagamos las relaciones en Fluent API

            builder.HasOne(x => x.Categoria).WithMany()
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.NoAction); }

    }
}
