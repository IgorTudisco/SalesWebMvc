using System.Collections.Generic;
using System;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        // No metodo Mvc é necessário tem um construtor vazio

        public Department()
        {
        }

        // Construtor sem a lista (relação de dependencia)

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Metodo para addicionar um vendedor 

        public void AddSelller(Seller seller)
        {
            Sellers.Add(seller);
        }

        // Metodo para calcular o total de vendas do departamento
        // Chamamos o metodo da class Seller para somar o total de cada vendedor
        // E obter o total de cada departamento.

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }



    }
}
