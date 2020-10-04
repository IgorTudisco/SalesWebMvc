using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // Criando a nossa dependencia atraves do construtor com a classe SellerService e DepartmentService

        private readonly SellerService _sellersService;

        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellersService = sellerService;
            _departmentService = departmentService;
        }

        // inserindo a nossa chamada da lista de vendedores
        // E estamos passando a lista como parânmetro da View.

        public IActionResult Index()
        {
            var list = _sellersService.FinAll();
            return View(list);
        }

        // Método para abrir o formulário para cadastrar um vendedor
        
        public IActionResult Create()
        {
            var departments = _departmentService.Findall();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
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

        // Método para que o usuário confirme a deleção
        // O argumento é opsional

        public IActionResult Delete(int? id)
        {
            // teste se nulo

            if(id == null)
            {
                return NotFound();
            }

            var obj = _sellersService.FindById(id.Value);

            // teste se o Id achado existe

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }

        // Método que fará a ação de deletar o vendedor

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellersService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
