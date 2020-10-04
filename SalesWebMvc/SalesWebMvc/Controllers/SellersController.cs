using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // Criando a nossa dependencia atraves do construtor com a classe SellerService

        private readonly SellerService _sellersService;

        public SellersController(SellerService sellerService)
        {
            _sellersService = sellerService;
        }

        // inserindo a nossa chamada da lista de vendedores
        // E estamos passando a lista como parânmetro da View.

        public IActionResult Index()
        {
            var list = _sellersService.FinAll();
            return View(list);
        }



    }
}
