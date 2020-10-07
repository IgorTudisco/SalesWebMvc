using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // Customizando o Display

        [Display(Name = "Bith Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BithDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Department Department { get; set; }

        public int DepartmentId { get; set; }

        // No metodo Mvc é necessário que tenha um construtor vazio

        public Seller()
        {
        }

        // Construtor sem a lista (relação de dependencia)

        public Seller(int id, string name, string email, DateTime bithDate, double baseSalary, Department departments)
        {
            Id = id;
            Name = name;
            Email = email;
            BithDate = bithDate;
            BaseSalary = baseSalary;
            Department = departments;
        }

        // Metodo para add a venda na nossa lista de venda

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        // Metodo para remover a venda na nossa lista de venda

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        /* Com o Linq vamos fazer um filtro na nossa lista e vamos
         * somar os valores desse intervalo para ter o total.
         */

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }

    }
}
