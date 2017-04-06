using System;
using System.Collections.Generic;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.core.BLL
{
    public class PerformanceManager
    {


        public List<Performance> GetPerformance(int ano, int mes)
        {
            //TODO -> LIDAR COM OPÇÃO 0 -> TODOS

            //return lista ;
            var datainicial = GetStartOfMonth(ano, mes);
            var datafinal = GetEndOfMonth(ano, mes);

            //TODO DATEHELPER


            //Acessar serviço remoto e validar usuário.
            var service = new WebService.EmbarcadorService
            {
                Url = UsuarioManager.Instance.PathWebService()
            };

            var retorno = service.RetornaPerformance(UsuarioManager.Instance.usuario.id_empresa, datainicial, datafinal, "",
                UsuarioManager.Instance.usuario.id_transportadora);

            var Performance = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Performance>>(retorno);

            return Performance;
        }

        public static DateTime GetStartOfMonth(int year, int month)
        {
            return new DateTime(year, month, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfMonth(int year, int month)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999);
        }
    }
}