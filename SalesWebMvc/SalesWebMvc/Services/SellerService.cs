using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;

        }

        // Mudando o método de síncrona para assíncrona

        public async Task<List<Seller>> FinAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        // Vamos inserir um novo vendedor no banco de dados
        // E vamos salvar
        // Mudando de síncrona para assíncrona

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Fintrando os vendedores pelo Id.
        /*
         Para que seja possível fazer o join das tabelas,
         temos que usar o namespace Microsoft.EntityFrameworkCore,
         para ter acesso a função include.
        */
        // Mudando de síncrona para assíncrona

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // Médoto remove
        // Mudando de síncrona para assíncrona
        // Implementando o tratamento da exeção.

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        // Mudando de síncrona para assíncrona

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            // Aqui estamos relançando uma exeção, pegando uma mensagem do Db e subindo ela.
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
