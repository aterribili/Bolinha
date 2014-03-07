using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreBolinha;

namespace BolinhaWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(String url)
        {
            ViewBag.Url = url;
            ViewBag.Arquivos = new List<Arquivo>();

            if(url != null)
                ViewBag.Arquivos = new GeradorArquivo(url, new Paths().PathAleatorio()).Arquivos();

            return View();
        }
    }
}