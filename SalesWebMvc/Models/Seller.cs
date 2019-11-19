using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        [Display(Name = "ID do Usuario")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} requirido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do nome deve ser entre {2} e {1}")]
        public String Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} requirido")]
        public String Email { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyy}")]  
        public DateTime BirthDate  { get; set; }
        [Display (Name = "Salario Base")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} requirido")]
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
