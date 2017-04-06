using System;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.core.BLL
{
    public class SimulacaoFreteManager
    {

        public string mensagem
        {
            get;
            set;
        }

        public List<SimulacaoFrete> GetSimulacaoFrete(string origemcodmun, string destinocodmun, decimal valornf, decimal pesonf, decimal volumenf)
        {

            mensagem = "";

            var service = new WebService.EmbarcadorService
            {
                Url = UsuarioManager.Instance.PathWebService()
            };


            try
            {
                var retorno = service.SimularFrete(UsuarioManager.Instance.usuario.id_empresa, origemcodmun, destinocodmun, valornf, pesonf, volumenf);

                var ResultadoSimulacao = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimulacaoFrete>>(retorno);

                return ResultadoSimulacao;
            }
            catch (SoapException ex)
            {
                mensagem = ex.Actor;
                return new List<SimulacaoFrete>();
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
                return new List<SimulacaoFrete>();
            }


        }

    }
}