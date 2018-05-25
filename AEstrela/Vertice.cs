using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEstrela
{
    class Vertice
    {
        public Vertice(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public int Linha { get; set; }
        public int Coluna { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Vertice)
            {
                Vertice v = (Vertice)obj;

                return (v.Linha == Linha && v.Coluna == Coluna);
            }
            else
                    return false;
        }

        public override int GetHashCode()
        {
            return Linha.GetHashCode() ^ Coluna.GetHashCode();
        }
    }
}
