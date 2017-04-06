using System.Collections.Generic;

namespace weblayer.embarcador.core.Model
{
    public class SimulacaoFrete
    {

        public SimulacaoFrete() { }

        List<SimulacaoFreteDetalhe> _detalhes = new List<SimulacaoFreteDetalhe>();

        public virtual int id_transportadora
        { get; set; }

        public virtual string ds_transportadora
        { get; set; }

        public virtual decimal vl_frete
        { get; set; }

        public virtual decimal vl_frete_imposto
        { get; set; }

        public virtual string ds_memoriacalculo
        { get; set; }

        public virtual string ds_memoriacalculo_clear
        {
            get; set;
            /* get
             {
                 return Regex.Replace(this.ds_memoriacalculo, @"<[^>]+>|&nbsp;", "").Trim();
             }*/
        }

        public List<SimulacaoFreteDetalhe> Detalhes
        {
            get { return _detalhes; }
            set { _detalhes = value; }
        }

    }

    public class SimulacaoFreteDetalhe
    {
        public SimulacaoFreteDetalhe() { }

        public virtual int id_elementocusto
        { get; set; }

        public virtual string ds_elementocusto
        { get; set; }

        public virtual decimal vl_frete
        { get; set; }

        public virtual decimal vl_frete_imposto
        { get; set; }


    }

}