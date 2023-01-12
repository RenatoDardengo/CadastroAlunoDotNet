using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cSharp_mvc.Models;

namespace cSharp_mvc.Controllers;

public class StudantsController : Controller
{
    private readonly ILogger<StudantsController> _logger;

    public StudantsController(ILogger<StudantsController> logger)
    {
        _logger = logger;
    }
    [Route("/studants")]

    public IActionResult Index()
    {
        ViewData["Titulo"] = "Cadastro de Alunos";
        ViewBag.Aluno = Aluno.Todos();

        return View();
    }
    [Route("/studants/create")]
    [HttpGet]
    public IActionResult Create()
    {

        ViewData["Titulo"] = "Cadastrar novo Aluno";
        return View();

    }
    [Route ("/studants/edit")]
    [HttpPut]
    public IActionResult Edit (Aluno aluno){
        aluno.Salvar();
        return Redirect("/studants");

    }


    [Route("/studants/edit/{id}")]
    [HttpGet]
    public IActionResult Edit(int id)
    {

        ViewBag.Aluno = Aluno.BuscarPorID(id);
        var aluno = @ViewBag.Aluno;
        ViewData["Titulo"] = "Editar Aluno";
        ViewBag.Nome=aluno.Nome;
        ViewBag.Matricula = aluno.Matricula;
        ViewBag.notas = aluno.Notas;

        return View();
    }


    [Route("/studants/{id}/delete")]
    public IActionResult DeletarPorId(Aluno aluno)
    {
        aluno.ApagarporId();
        return Redirect("/studants");

    }

    [Route("/create")]
    [HttpPost]
    public IActionResult Incluir(Aluno aluno)
    {

        aluno.Salvar();
        return Redirect("/studants");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
