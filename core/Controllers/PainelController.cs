using core.Domain.Entities;
using core.Domain.Interfaces;
using core.Models;
using core.Service;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.Controllers
{

    public class PainelController : Controller
    {
        public IActionResult Proibido()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AreaGestor()
        {
            return View();
        }

        public IActionResult Mensagens()
        {
            return View();
        }

        public IActionResult Roteiros()
        {
            return View();
        }

        public IActionResult Usuarios()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Renda()
        {
            return View();
        }
        public IActionResult Gastos()
        {
            return View();
        }
        public IActionResult Investimentos()
        {
            return View();
        }
        public IActionResult OrcamentoRenda()
        {
            return View();
        }
        public IActionResult OrcamentoGastos()
        {
            return View();
        }
        public IActionResult OrcamentoInvestimentos()
        {
            return View();
        }
        public IActionResult Lancamentos()
        {
            return View();
        }
        public IActionResult Parametros()
        {
            return View();
        }
    }
}
