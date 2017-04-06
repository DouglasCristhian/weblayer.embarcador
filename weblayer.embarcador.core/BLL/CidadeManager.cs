using System.Collections.Generic;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.core.BLL
{
    public class CidadeManager
    {
        public List<Cidade> GetCidade(/*string filtro*/)
        {
            var lista = new List<Cidade>();


            lista.Add(new Cidade { ds_cidade = "Diadema", ds_estado = "SP", ds_codmun = "3513801" });
            lista.Add(new Cidade { ds_cidade = "Sorocaba", ds_estado = "SP", ds_codmun = "3552205" });
            lista.Add(new Cidade { ds_cidade = "Osasco", ds_estado = "SP", ds_codmun = "3534401" });
            lista.Add(new Cidade { ds_cidade = "Barueri", ds_estado = "SP", ds_codmun = "3505708" });
            lista.Add(new Cidade { ds_cidade = "São Paulo", ds_estado = "SP", ds_codmun = "3550308" });
            lista.Add(new Cidade { ds_cidade = "Rio de Janeiro", ds_estado = "RJ", ds_codmun = "3304557" });

            return lista;

        }
    }
}