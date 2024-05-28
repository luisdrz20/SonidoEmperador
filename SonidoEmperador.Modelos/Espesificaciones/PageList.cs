using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonidoEmperador.Modelos.Espesificaciones
{
    public class PageList <T>:List<T>
    {
        public MetaData MetaData { get; set; }
        public PageList(List<T> items,int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count/(double)pageSize)
            };
            AddRange(items);
        }

        public static PageList<T> ToPagesList(
                                    IEnumerable<T> entidad,
                                    int pageNumbre,
                                    int pageSize
                                    )
        
        {
            var count = entidad.Count();
            var items = entidad.Skip((pageNumbre-1)*pageSize).Take(pageSize).ToList();
            return new PageList<T>(items, count, pageNumbre, pageSize);
        }

    }
}

