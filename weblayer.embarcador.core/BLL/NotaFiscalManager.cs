using System;
using System.Collections.Generic;
using weblayer.embarcador.core.Model;

namespace weblayer.embarcador.core.BLL
{
    public class NotaFiscalManager
    {
        public string mensagem
        {
            get;
            set;
        }

        public List<NotaFiscal> GetNotaFiscal(string filtro)
        {

            try
            {
                //Acessar serviço remoto e validar usuário.
                var service = new WebService.EmbarcadorService
                {
                    Url = UsuarioManager.Instance.PathWebService()
                };

                var retorno = service.BuscarNota(UsuarioManager.Instance.usuario.id_empresa, UsuarioManager.Instance.usuario.id_transportadora, filtro);
                var Notas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NotaFiscal>>(retorno);
                // var Notas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NotaFiscal>>(retorno).Where(x => x.ds_numeronota.Contains(filtro.ToString())).ToList();


                return Notas;

            }
            catch (System.Exception ex)
            {
                mensagem = ex.Message;
                return null;
            }

        }

        public bool InformarEntregaNotaFiscal(int id_nota, DateTime dt_entrega)
        {

            var service = new WebService.EmbarcadorService
            {
                Url = UsuarioManager.Instance.PathWebService()
            };

            var retorno = service.InformarEntrega(UsuarioManager.Instance.usuario.id_empresa, id_nota, dt_entrega, UsuarioManager.Instance.usuario.id_usuario, "Android", "-", "-", "-");

            var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Resultado>>(retorno);

            mensagem = resultado[0].ds_observacao;

            if (resultado[0].ds_resultado == "OK")
                return true;
            else
                return false;




        }


    }

}