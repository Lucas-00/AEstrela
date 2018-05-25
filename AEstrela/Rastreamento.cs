using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrela
{
    class Rastreamento
    {
        public Rastreamento(Vertice atual, Rastreamento anterior)
        {
            this.VerticeAtual = atual;
            this.anterior = anterior;
        }

        public Vertice VerticeAtual { get; set; }
        public Rastreamento Anterior { get => anterior; set => anterior = value; }

        public double CustoG { get; set; }
        public double CustoH { get; set; }

        public double CustoTotal { get => CustoG + CustoH ; }

        private Rastreamento anterior = null;
    }
}
