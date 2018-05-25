using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrela
{
    class AEstrelaProc
    {
        public List<Vertice> ProcessarERetornarCaminho(int[,] espacoBusca, int numeroLinhas, int numeroColunas, Vertice origem, Vertice meta, Heuristica heuristica)
        {
            FilaOrdenada abertos = new FilaOrdenada();
            Dictionary<Vertice, Rastreamento> fechados = new Dictionary<Vertice, Rastreamento>();

            if (origem == null || meta ==null || heuristica == null)
                return null;
            Rastreamento atual = new Rastreamento(origem, null);
            heuristica.CalcularCustos(atual, meta, 10);

            abertos.Adicionar(atual);

            while (atual != null)
            {
                if (atual.VerticeAtual.Equals(meta))
                    return RetornarCaminho(atual);
                else
                {
                    fechados.Add(atual.VerticeAtual, atual);
                    abertos.Remover();

                    for (int i = atual.VerticeAtual.Linha - 1; i <= atual.VerticeAtual.Linha + 1; i++)
                    {
                        if (i >= 0 && i < numeroLinhas)
                        {
                            for (int j = atual.VerticeAtual.Coluna - 1 ; j <= atual.VerticeAtual.Coluna + 1; j++)
                            {
                                if (j >= 0 && j < numeroColunas)
                                {
                                    if (i != atual.VerticeAtual.Linha || j != atual.VerticeAtual.Coluna)
                                    {
                                        if (espacoBusca[i,j] != 2)
                                        {
                                            AdicionarVertice(abertos, fechados, atual, meta, heuristica, i, j);

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                atual = abertos.Primeiro;
            }

            return null;
        }

        private void AdicionarVertice(FilaOrdenada abertos, Dictionary<Vertice, Rastreamento> fechados, Rastreamento atual, Vertice meta, Heuristica heuristica, int i, int j)
        {
            var v = new Vertice(i, j);

            if (!fechados.ContainsKey(v))
            {
                var r = new Rastreamento(v, atual);

                if (i != atual.VerticeAtual.Linha && j != atual.VerticeAtual.Coluna)
                    heuristica.CalcularCustos(r, meta, 14);
                else
                    heuristica.CalcularCustos(r, meta, 10);

                if (!abertos.ContemVertice(v))
                    abertos.Adicionar(r);
                else
                {
                    var aux = abertos.Get(v);

                    if(r.CustoTotal < aux.CustoTotal)
                    {
                        abertos.Remover(aux);

                        abertos.Adicionar(r);
                    }
                }
            }
        }

        private List<Vertice> RetornarCaminho(Rastreamento rastreamento)
        {
            var vertices = new List<Vertice>();

            var aux = rastreamento;

            while (aux != null)
            {
                vertices.Add(aux.VerticeAtual);
                aux = aux.Anterior;
            }

            return vertices;
        }
    }
}
