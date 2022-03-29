﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApi.MODEL;

namespace WebApi.DAO
{
    public class Pessoa
    {

        private string conexao = @"Data Source=DESKTOP-GPICHSB\SQLEXPRESS;Initial Catalog=Web_Api;Integrated Security=True";

        public List<PessoaModel> GetPessoas()
        {
            List<PessoaModel> Pessoas = new List<PessoaModel>();
           
            using (SqlConnection con = new SqlConnection(conexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pessoa",con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                using (SqlDataReader reader = cmd.ExecuteReader()) 
                {
                    if (reader != null) 
                    {
                        while (reader.Read()) 
                        {
                            var Pessoa = new PessoaModel();
                            Pessoa.ID = Convert.ToInt32(reader["ID"].ToString());
                            Pessoa.nome = reader["nome"].ToString();
                            Pessoa.sobrenome = reader["sobrenome"].ToString();
                            Pessoa.endereco = reader["endereco"].ToString();
                            Pessoas.Add(Pessoa);

                        }
                    }
                }
            }
            return Pessoas;
        }
        public List<PessoaModel> GetPessoaId(int Id) 
        {
            List<PessoaModel> Pessoaid = new List<PessoaModel>();

            using (SqlConnection con = new SqlConnection(conexao)) 
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"SELECT id,nome,sobrenome,endereco FROM Pessoa WHERE ID={Id} ", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            var Pessoa = new PessoaModel();
                            Pessoa.ID = int.Parse(reader["ID"].ToString());
                            Pessoa.nome = reader["nome"].ToString();
                            Pessoa.sobrenome = reader["sobrenome"].ToString();
                            Pessoa.endereco = reader["endereco"].ToString();
                            Pessoaid.Add(Pessoa);

                        }
                    }
                }
            }
            return Pessoaid;
        }
        public void InserPessoa(PessoaModel pessoa) 
        {        
            using (SqlConnection con = new SqlConnection(conexao))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"INSERT INTO Pessoa(nome,sobrenome,endereco)VALUES(@nome,@sobrenome,@endereco)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", pessoa.nome);
                cmd.Parameters.AddWithValue("@sobrenome", pessoa.sobrenome);
                cmd.Parameters.AddWithValue("@endereco", pessoa.endereco);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdatePessoa(PessoaModel pessoa)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexao))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"UPDATE Pessoa SET nome='{pessoa.nome}',sobrenome='{pessoa.sobrenome}',endereco='{pessoa.endereco}' WHERE ID={pessoa.ID}", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void DeletarPessoa(int id) 
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conexao)) 
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE Pessoa WHERE ID={id}",con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

