using System;
using System.Collections.Generic;
using System.Text;

namespace DBS.AniversariantesDoMes.Value_Objects
{
    public class Mensagem_VO
    {
        private int id;
        private String mensagem;
        private String autor;
        private DateTime data;
        private String titulo;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public String Mensagem
        {
            get { return mensagem; }
            set { mensagem = value; }
        }
        
        public String Autor
        {
            get { return autor; }
            set { autor = value; }
        }
        
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public String Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        //public Mensagem_VO(int id, String mensagem, String Autor, DateTime data)
        //{
        //    this.id = id;
        //    this.mensagem = mensagem;
        //    this.autor = Autor;
        //    this.data = data;
        //}
    }
}
