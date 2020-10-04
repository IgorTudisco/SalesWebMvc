using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
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

        public IActionResult Create()
        {
            return View();
        }

        // Inserindo o vendedor no banco de dados
        // Após a operação, estamos retornando para a pagina index
        // Estamos usando a função nameof, para facilitar a manutenção. Assim
        // se amanha o nome da class mudar isso não irá interferir no nosso metodo.
        // É recomendado que se use o nameof.
        // Colocando um anotation
        // A primeira anotação é para diser que é um método/ação de post
        // A segunda é para evitar ataques de fora. Ataques CSRF.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellersService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

    }
}
