using System;
using System.Collections.Generic;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.core.BLL
{
    public class CenarioEntregaManager : IComparable
    {
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public List<CenarioEntrega> GetCenario2(int ano, int mes)
        {
            var lista = new List<CenarioEntrega>();

            lista.Add(new CenarioEntrega { nr_dias = -14, nr_notas = 1 });
            lista.Add(new CenarioEntrega { nr_dias = -13, nr_notas = 1 });
            lista.Add(new CenarioEntrega { nr_dias = -12, nr_notas = 1 });
            lista.Add(new CenarioEntrega { nr_dias = -10, nr_notas = 3 });
            lista.Add(new CenarioEntrega { nr_dias = -9, nr_notas = 2 });
            lista.Add(new CenarioEntrega { nr_dias = -8, nr_notas = 1 });
            lista.Add(new CenarioEntrega { nr_dias = -7, nr_notas = 1 });
            lista.Add(new CenarioEntrega { nr_dias = -6, nr_notas = 2 });
            lista.Add(new CenarioEntrega { nr_dias = -5, nr_notas = 1 });
            lista.Add(new CenarioEntrega { nr_dias = -4, nr_notas = 2 });
            lista.Add(new CenarioEntrega { nr_dias = -3, nr_notas = 7 });
            lista.Add(new CenarioEntrega { nr_dias = -2, nr_notas = 2 });
            lista.Add(new CenarioEntrega { nr_dias = -1, nr_notas = 11 });
            lista.Add(new CenarioEntrega { nr_dias = -0, nr_notas = 225 });
            lista.Add(new CenarioEntrega { nr_dias = 1, nr_notas = 43 });
            lista.Add(new CenarioEntrega { nr_dias = 2, nr_notas = 35 });
            lista.Add(new CenarioEntrega { nr_dias = 3, nr_notas = 31 });
            lista.Add(new CenarioEntrega { nr_dias = 4, nr_notas = 16 });
            lista.Add(new CenarioEntrega { nr_dias = 5, nr_notas = 23 });
            lista.Add(new CenarioEntrega { nr_dias = 6, nr_notas = 8 });
            lista.Add(new CenarioEntrega { nr_dias = 7, nr_notas = 2 });
            lista.Add(new CenarioEntrega { nr_dias = 8, nr_notas = 2 });
            lista.Add(new CenarioEntrega { nr_dias = 11, nr_notas = 1 });

            return lista;

        }
    }
}