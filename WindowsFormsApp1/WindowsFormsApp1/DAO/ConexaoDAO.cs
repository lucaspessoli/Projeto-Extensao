using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

public class ConexaoDAO
{
    private NpgsqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;
    public ConexaoDAO()
    {
        Initialize();
    }

    private void Initialize()
    {
        server = "localhost";
        database = "extensao";
        uid = "postgres";
        password = "unipar";
        string connectionString;
        connectionString = $"Server={server};Port=5432;Database={database};User Id={uid};Password={password};";
        connection = new NpgsqlConnection(connectionString);
    }

    private bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    private bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    internal List<Avaliacao> GetAvaliacoes()
    {
        List<Avaliacao> avaliacoes = new List<Avaliacao>();
        string query = "SELECT a.avaliacao_id, a.nome, a.funcionario_id, f.nome as nome_funcionario " +
                       "FROM avaliacoes a " +
                       "JOIN funcionarios f ON a.funcionario_id = f.funcionario_id";

        if (this.OpenConnection() == true)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Avaliacao avaliacao = new Avaliacao();
                avaliacao.AvaliacaoId = Convert.ToInt32(row["avaliacao_id"]);
                avaliacao.Nome = row["nome"].ToString();
                avaliacao.FuncionarioId = Convert.ToInt32(row["funcionario_id"]);
                avaliacao.NomeFuncionario = row["nome_funcionario"].ToString();

                avaliacoes.Add(avaliacao);
            }

            this.CloseConnection();
        }

        return avaliacoes;
    }

    public void UploadAvaliacao(string nomeFuncionario, string avaliacao)
    {
        try
        {
            connection.Open();

            int funcionarioId = GetFuncionarioId(nomeFuncionario);

            if (funcionarioId > 0)
            {
                string query = "INSERT INTO avaliacoes (nome, funcionario_id, nome_funcionario) " +
                               "VALUES (@avaliacao, @funcionarioId, @nomeFuncionario)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@avaliacao", avaliacao);
                    cmd.Parameters.AddWithValue("@funcionarioId", funcionarioId);
                    cmd.Parameters.AddWithValue("@nomeFuncionario", nomeFuncionario);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Avaliação inserida com sucesso!");
            }
            else
            {
                MessageBox.Show("Funcionário não encontrado.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao inserir avaliação: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    private int GetFuncionarioId(string nomeFuncionario)
    {
        string query = "SELECT funcionario_id FROM funcionarios WHERE nome = @nomeFuncionario";
        int funcionarioId = 0;

        using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@nomeFuncionario", nomeFuncionario);
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                funcionarioId = Convert.ToInt32(result);
            }
        }

        return funcionarioId;
    }

    internal List<Funcionario> GetFuncionarios()
    {
        List<Funcionario> funcionarios = new List<Funcionario>();

        try
        {
            connection.Open();

            string query = "SELECT funcionario_id, nome FROM funcionarios";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Funcionario funcionario = new Funcionario();
                        funcionario.FuncionarioId = reader.GetInt32(0);
                        funcionario.Nome = reader.GetString(1);

                        funcionarios.Add(funcionario);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao obter funcionários: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

        return funcionarios;
    }
}