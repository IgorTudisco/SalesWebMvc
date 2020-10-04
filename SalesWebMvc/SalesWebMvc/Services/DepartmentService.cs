using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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

        public List<Department> Findall()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
                 
        }



    }
}
