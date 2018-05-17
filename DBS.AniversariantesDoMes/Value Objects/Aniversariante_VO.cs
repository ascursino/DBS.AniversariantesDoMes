using System;

namespace DBS.AniversariantesDoMes.VO
{
    public class Aniversariante_VO
    {
        private String nomeResumido;

        public String NomeResumido
        {
            get { return nomeResumido; }
            set { nomeResumido = value; }
        }
        private DateTime dataAniversario;

        public DateTime DataAniversario
        {
            get { return dataAniversario; }
            set { dataAniversario = value; }
        }

        private int idAniversariante;

        public int IdAniversariante
        {
            get { return idAniversariante; }
            set { idAniversariante = value; }
        }


    }
}
