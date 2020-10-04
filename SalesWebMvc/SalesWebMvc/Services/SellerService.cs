using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;

        }

        public List<Seller> FinAll()
        {
            return _context.Seller.ToList();
        }

        // Vamos inserir um novo vendedor no banco de dados
        // E vamos salvar

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        // Fintrando os vendedores pelo Id.
        /*
         Para que seja possível fazer o join das tabelas,
         temos que usar o namespace Microsoft.EntityFrameworkCore,
         para ter acesso a função include.
        */

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        // Médoto remove

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

    }
}
