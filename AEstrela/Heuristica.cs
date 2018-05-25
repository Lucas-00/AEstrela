using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrela
{
    class Heuristica
    {
        public void CalcularCustos(Rastreamento atual, Vertice meta, double valor)
        {
            if (atual.Anterior != null)
            {
                atual.CustoG = valor + atual.Anterior.CustoG;
            }
            else
            {
                atual.CustoG = valor;
            }

            double c1 = 10 * Math.Abs(atual.VerticeAtual.Linha - meta.Linha);
            double c2 = 10 * Math.Abs(atual.VerticeAtual.Coluna - meta.Coluna);

            atual.CustoH = Math.Sqrt(Math.Pow(c1, 2) + Math.Pow(c2, 2));

        }
    }
}
