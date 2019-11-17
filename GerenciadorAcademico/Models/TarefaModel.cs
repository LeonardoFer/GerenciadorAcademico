using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorAcademico.Models
{
	public class TarefaModel
	{
		[Display(Name = "Identificação")]
		public int Id { get; set; }

		[Display(Name = "Tarefa: ")]
		[StringLength(50, ErrorMessage = "Este campo precisa ser preenchido!")]
		[Required]
		public string Tarefa { get; set; }

		[Display(Name = "Descrição: ")]
		[StringLength(500, MinimumLength = 10, ErrorMessage = "Este campo precisa ser preenchido com no mínimo 20 caracteres!")]
		[Required]
		public string Descricao { get; set; }

		[Display(Name = "Categoria: ")]
		[StringLength(50, ErrorMessage = "Este campo precisa ser preenchido!")]
		[Required]
		public string Categoria { get; set; }

		[Display(Name = "Data de Entrega: ")]
		[DataType(DataType.Date)]
		[Required(ErrorMessage = "Este campo precisa ser preenchido!")]
		public DateTime Data { get; set; }
	}
}