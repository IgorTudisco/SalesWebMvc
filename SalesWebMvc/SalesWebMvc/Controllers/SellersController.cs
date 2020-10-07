using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

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
        // Mudando de síncrona para assíncrona

        public async Task<IActionResult> Index()
        {
            var list = await _sellersService.FinAllAsync();
            return View(list);
        }

        // Método para abrir o formulário para cadastrar um vendedor
        // Mudando de síncrona para assíncrona

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindallAsync();
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
        // Mudando de síncrona para assíncrona

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            // Condição saber se a ação foi feita corretamente

            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindallAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

           await _sellersService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // Método para que o usuário confirme a deleção
        // O argumento é opsional
        // Mudando de síncrona para assíncrona

        public async Task<IActionResult> Delete(int? id)
        {
            // teste se nulo

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { massege = "Id not privided" });
            }

            var obj = await _sellersService.FindByIdAsync(id.Value);

            // teste se o Id achado existe

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { massege = "Id not found" });
            }

            return View(obj);

        }

        // Método que fará a ação de deletar o vendedor
        // Mudando de síncrona para assíncrona
        // Implementando o tratamento do erro

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellersService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Método que tem a ação de monstrar os detalhes
        // Mudando de síncrona para assíncrona

        public async Task<IActionResult> Details(int? id)
        {
            // teste se nulo

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not privided" });
            }

            var obj = await _sellersService.FindByIdAsync(id.Value);

            // teste se o Id achado existe

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // Esse método serve para abrir a caixa de edição.
        // Os testes são feito para saber se o id é nulo e se existe.
        // Depois passamos uma listinha para povoar a minha listinha de seleção.
        // Depois passamos os dados que buscamos do bando de dados, paque se possa fazer a edição.
        // Mudando de síncrona para assíncrona

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellersService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindallAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);

        }

        // Método para salvar as alterações no vendedor
        // Médoto post
        // Mudando de síncrona para assíncrona

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            // Condição saber se a ação foi feita corretamente

            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindallAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellersService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                // No lugar do NotFound foi passado a ação/Método Error e mais uma chmada anonima
                // para que se possa passar o parámetro com a mensagem adequada.

                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Método para retornar a view de error.
        // O atributo Message vai receber a mensagem que veio como argumento
        // O atributo RequestId vai receder o Id interno pelo massete abaixo do FrameWork.
        // Mudando de síncrona para assíncrona

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

    }
}
