using System.Collections.Generic;

namespace gestao_produtos.Domain.Models
{
    public class PagedResult<T>
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
