using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;

        }

        // Método para retornar todos os departamento.
        // Mudamos a operação síncrona para assíncrona e para isso vamos usar as Tasks

        public async Task<List<Department>> FindallAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
                 
        }



    }
}
