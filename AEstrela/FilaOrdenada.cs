using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrela
{
    class FilaOrdenada
    {
        private LinkedList<Rastreamento> lista = new LinkedList<Rastreamento>();

        public Rastreamento Primeiro
        {
            get { return lista.First != null ? lista.First.Value : null; }
        }

        public void Adicionar(Rastreamento atual)
        {
            var noPosterior = lista.First;

            while (noPosterior != null)
            {
                if (noPosterior.Value.CustoTotal > atual.CustoTotal)
                    break;

                noPosterior = noPosterior.Next;
            }

            if (noPosterior == null)
                lista.AddLast(atual);
            else
                lista.AddBefore(noPosterior, atual);
        }
        public bool ContemVertice(Vertice v)
        {
            var no = lista.First;

            while (no != null)
                if (no.Value.VerticeAtual.Equals(v))
                    return true;
                else
                    no = no.Next;
            return false;
        }

        public Rastreamento Get(Vertice v)
        {
            var no = lista.First;

            while (no != null)
                if (no.Value.VerticeAtual.Equals(v))
                    return no.Value;
                else
                    no = no.Next;
            return null;
        }

        public void Remover()
        {
            lista.RemoveFirst();
        }
        public void Remover(Rastreamento atual)
        {
            var no = lista.First;

            while (no != null)
                if (no.Value.Equals(atual))
                {
                    lista.Remove(no.Value);
                    break;
                }
                else
                    no = no.Next;
        }
    }
}
