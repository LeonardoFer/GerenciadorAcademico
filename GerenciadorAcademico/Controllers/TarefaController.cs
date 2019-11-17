using GerenciadorAcademico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorAcademico.Controllers
{
	public class TarefaController : Controller
	{
		private TarefaRepository repository = new TarefaRepository();

		// GET: Tarefa

		public ActionResult Home()
		{ 
			int vencidas = repository.GetByDate();

			if (vencidas > 0)
			{
				ViewBag.mensagem = "Você tem " + vencidas + " tarefa(s) acadêmica(s) atrasada(s)! Por favor verifique no sistema.";
			}
			else
			{
				ViewBag.mensagem = "Você não tem nenhuma tarefa acadêmica atrasada!";
			}
			
			return View();
		}

		public ActionResult Index()
		{
			return View(repository.GetAll());
		}

		// GET: Tarefa/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Tarefa/Create
		[HttpPost]
		public ActionResult Create(TarefaModel tarefaModel)
		{
			if (ModelState.IsValid)
			{
				repository.Save(tarefaModel);
				return RedirectToAction("Index");
			}
			else
			{
				return View(tarefaModel);
			}
		}

		// GET: Tarefa/Edit/5
		public ActionResult Edit(int id)
		{
			var tarefaModel = repository.GetById(id);
			if (tarefaModel == null)
			{
				return HttpNotFound();
			}

			return View(tarefaModel);
		}

		// POST: Tarefa/Edit/5
		[HttpPost]
		public ActionResult Edit(TarefaModel tarefaModel)
		{
			if (ModelState.IsValid)
			{
				repository.Update(tarefaModel);
				return RedirectToAction("Index");
			}
			else
			{
				return View(tarefaModel);
			}
		}

		// Get: Tarefa/Delete/5
		public ActionResult Delete(int id)
		{
			var tarefaModel = repository.GetById(id);
			if (tarefaModel == null)
			{
				return HttpNotFound();
			}

			return View(tarefaModel);
		}

		[HttpPost]
		public ActionResult Delete(TarefaModel tarefaModel) 
		{
			if (tarefaModel == null)
			{
				return HttpNotFound();
			}
			else
			{
				repository.Delete(tarefaModel);
			}
			return RedirectToAction("Index");
		}
	}
}
