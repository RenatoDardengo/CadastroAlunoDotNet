using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cSharp_mvc.Models
{
    public partial class Aluno
    {
        #region Propriedades
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        private List<double> notas;
        #endregion

        #region Metodos de Intância

        private static string connectionString(){
            return @"Server=localhost\SQLEXPRESS; database =desafioAPI; Integrated Security=SSPI; user ID =workPC\renato; PassWord =''";
        }
        public List<double> Notas
        {
            /* Aqui utilizamos o metodo complexo pois ao inicarmos essa lista ela retornará nula e por ser uma lista 
            do tipo double isso quebrará nossa aplicação. Logo,
            utilizamos uma lógica para que se ela estiver nula criamos uma nova lista*/
            get
            {
                if (this.notas == null) this.notas = new List<double>();
                return this.notas;


            }
            set
            {
                this.notas = value;
            }
        }

        public double CalcularMedia()
        {
            double somaNotas = 0.0;
            foreach (var nota in this.Notas)
            {
                somaNotas += nota;
            }
            return somaNotas / this.Notas.Count;
        }

        public string StrNotas(){
            return string.Join(",", this.Notas.ToArray());
        }
        public string Situacao()

        {
            return this.CalcularMedia() > 7 ? "Aprovado" : "Reprovado";

        }
        public void ApagarporId(){
            Aluno.DeletarPorId(this.Id);
        }

        public void Salvar(){
            if (this.Id>0)
            {
                Aluno.Atualizar(this);
            }else
            {
                Aluno.Incluir(this);
            }
        }
        #endregion

        


    }
}