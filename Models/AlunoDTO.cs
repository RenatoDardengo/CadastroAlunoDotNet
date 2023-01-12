using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cSharp_mvc.Models
{
    public partial class Aluno
    {

        #region Metodos Estaticos| Classe
        public static void Atualizar(Aluno aluno)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand($"update alunos set nome=@nome,matricula=@matricula,notas=@notas where id=@id", sqlConn);
            sqlCommand.Parameters.AddWithValue("@id", aluno.Id);
            sqlCommand.Parameters.AddWithValue("@nome", aluno.Nome);
            sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula);
            sqlCommand.Parameters.AddWithValue("@notas", string.Join(",", aluno.notas.ToArray()));
            var reader = sqlCommand.ExecuteNonQuery();

        }
       
        public static void Incluir(Aluno aluno)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand($"insert into alunos (nome,matricula,notas) values (@nome, @matricula, @notas)", sqlConn);
            sqlCommand.Parameters.AddWithValue("@nome", aluno.Nome);
            sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula);
            sqlCommand.Parameters.AddWithValue("@notas", string.Join(",", aluno.notas.ToArray()));
            var reader = sqlCommand.ExecuteNonQuery();

        }
        public static void DeletarPorId(int id)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand($"delete from alunos where id={id}", sqlConn);
            var reader = sqlCommand.ExecuteNonQuery();


        }
        public static object BuscarPorID( int id){
            var aluno = new Aluno();
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand($"select * from alunos where id={id}", sqlConn);
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read()){
                var notas = new List<double>();
                string strNotas = reader["notas"].ToString();
                foreach (var nota in strNotas.Split(","))
                {
                    if (nota.Trim() != "") notas.Add(Convert.ToDouble(nota));

                }

                    aluno.Id = Convert.ToInt32(reader["id"]);
                    aluno.Nome = reader["nome"].ToString();
                    aluno.Matricula = reader["matricula"].ToString();
                    aluno.Notas = notas;
                

            }
            sqlConn.Close();
            sqlConn.Dispose();
            return aluno;

        }
        public static List<Aluno> Todos()
        {
            var alunos = new List<Aluno>();
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand("select * from alunos", sqlConn);
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var notas = new List<double>();
                string strNotas = reader["notas"].ToString();
                foreach (var nota in strNotas.Split(","))
                {
                    if (nota.Trim() != "") notas.Add(Convert.ToDouble(nota));

                }
                var aluno = new Aluno()
                {

                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                    Matricula = reader["matricula"].ToString(),
                    Notas = notas,
                };
                alunos.Add(aluno);
            }
            sqlConn.Close();
            sqlConn.Dispose();
            return alunos;
        }
        #endregion


    }
}