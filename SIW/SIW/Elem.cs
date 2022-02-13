using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIW
{
    public class Elem<T>
    {
        private T data;
        public T Data { get; set; } = default!;
        public Elem(T Data)
        {
            data = Data;
        }
    }
}
