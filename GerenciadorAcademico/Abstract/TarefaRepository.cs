using GerenciadorAcademico.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace GerenciadorAcademico.Controllers
{
	public class TarefaRepository : AbstractRepository<TarefaModel, int>
	{
		public override void Delete(TarefaModel entity)
		{
			using (var conexao = new OleDbConnection(StringConnection))
			{
				string oledb = "DELETE FROM [tbl_tarefas] WHERE [id] = ?";
				OleDbCommand comando = new OleDbCommand(oledb, conexao);
				comando.Parameters.AddWithValue("?", entity.Id);
				try
				{
					conexao.Open();
					comando.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					throw e;
				}
			}
		}

		public override void DeleteById(int id)
		{
			using (var conexao = new OleDbConnection(StringConnection))
			{
				string oledb = "DELETE FROM [tbl_tarefas] WHERE [id] = ?";
				OleDbCommand comando = new OleDbCommand(oledb, conexao);
				comando.Parameters.AddWithValue("?", id);
				try
				{
					conexao.Open();
					comando.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					throw e;
				}
			}
		}

		public override List<TarefaModel> GetAll()
		{
			string oledb = "Select [id], [tarefa], [descricao], [categoria], [data] FROM [tbl_tarefas] ORDER BY [id]";
			using (var conexao = new OleDbConnection(StringConnection))
			{
				var comando = new OleDbCommand(oledb, conexao);
				List<TarefaModel> list = new List<TarefaModel>();
				TarefaModel tarefa = null;
				try
				{
					conexao.Open();
					using (var reader = comando.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (reader.Read())
						{
							tarefa = new TarefaModel();
							tarefa.Id = (int)reader["id"];
							tarefa.Tarefa = reader["tarefa"].ToString();
							tarefa.Descricao = reader["descricao"].ToString();
							tarefa.Categoria = reader["categoria"].ToString();
							tarefa.Data = (DateTime)reader["data"];
							list.Add(tarefa);
						}
					}
				}
				catch (Exception e)
				{
					throw e;
				}
				return list;
			}
		}

		public override TarefaModel GetById(int id)
		{
			using (var conexao = new OleDbConnection(StringConnection))
			{
				string oledb = "SELECT [id], [tarefa], [descricao], [categoria], [data] from [tbl_tarefas] where [id]=@id";
				OleDbCommand comando = new OleDbCommand(oledb, conexao);
				comando.Parameters.AddWithValue("@id", id);
				TarefaModel tarefa = null;
				try
				{
					conexao.Open();
					using (var reader = comando.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (reader.HasRows)
						{
							if (reader.Read())
							{
								tarefa = new TarefaModel();
								tarefa.Id = (int)reader["id"];
								tarefa.Tarefa = reader["tarefa"].ToString();
								tarefa.Descricao = reader["descricao"].ToString();
								tarefa.Categoria = reader["categoria"].ToString();
								tarefa.Data = (DateTime)reader["data"];
							}
						}
					}
				}
				catch (Exception e)
				{
					throw e; 
				}
				return tarefa;
			}
		}

		public override void Save(TarefaModel entity)
		{
			using (var conexao = new OleDbConnection(StringConnection))
			{
				string oledb = "INSERT INTO [tbl_tarefas]([tarefa], [descricao], [categoria], [data]) VALUES (@tarefa, @descricao, @categoria, @data)";
				OleDbCommand comando = new OleDbCommand(oledb, conexao);
				comando.Parameters.AddWithValue("@tarefa", entity.Tarefa);
				comando.Parameters.AddWithValue("@descricao", entity.Descricao);
				comando.Parameters.AddWithValue("@categoria", entity.Categoria);
				comando.Parameters.AddWithValue("@data", entity.Data);
				try
				{
					conexao.Open();
					comando.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					throw e;
				}
			}
		}

		public override void Update(TarefaModel entity)
		{
			using (var conexao = new OleDbConnection(StringConnection))
			{
				string oledb = "UPDATE [tbl_tarefas] SET [tarefa] = ?, [descricao] = ?, [categoria] = ?, [data] = ? WHERE [id] = @id";
				OleDbCommand comando = new OleDbCommand(oledb, conexao);
				comando.Parameters.AddWithValue("?", entity.Tarefa);
				comando.Parameters.AddWithValue("?", entity.Descricao);
				comando.Parameters.AddWithValue("?", entity.Categoria);
				comando.Parameters.AddWithValue("?", entity.Data);
				comando.Parameters.AddWithValue("?", entity.Id);
				try
				{
					conexao.Open();
					comando.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					throw e;
				}
			}
		}

		public int GetByDate()
		{
			int vencidas = 0;
			using (var conexao = new OleDbConnection(StringConnection))
			{
				string oledb = "SELECT * FROM [tbl_tarefas] WHERE [data]";
				OleDbCommand comando = new OleDbCommand(oledb, conexao);
				conexao.Open();
				OleDbDataReader reader = comando.ExecuteReader(CommandBehavior.CloseConnection);
				while (reader.Read())
				{
					TarefaModel tarefa = new TarefaModel()
					{
						Data = Convert.ToDateTime(reader["data"])
					};
					if (tarefa.Data.Date <= DateTime.Today)
					{
						vencidas++;
					}
				}
				conexao.Close();
				return vencidas;
			}
		}
	}
}