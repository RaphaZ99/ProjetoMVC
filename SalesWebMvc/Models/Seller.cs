using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Seller
    {

        public int Id { get; set; }
        public String  Name { get; set; }
        public String  Email { get; set; }
        public DateTime BirthDate  { get; set; }
        public double  BaseSalary { get; set; }
        public Departament Departament { get; set; }
        public int DepartamentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Departament departament)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Departament = departament;
        }

        //Adiciona Vendas
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }


        //Remeve Vendas
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);

        }

            public double TotalSales(DateTime initial, DateTime final)
        {
             //Where + expressão lambda para filtrar as datas do  vendedor da lista
             //.Sum para somar o valor total as vendas de todos os vendedores
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);

        }


    }
}
