using System.Collections.Generic;
using System;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Departament
    {

        public int Id { get; set; }
        public string  Name { get; set; }
        public ICollection <Seller> Sellers { get; set; } = new List<Seller>();


        public Departament() { }

        public Departament(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {

            Sellers.Add(seller);

        }


        public double TotalSales(DateTime initial, DateTime final)
        {

            //Pega a lista de vendedores(Seller) chama o .Sum() dentro do Linq Chama o Objeto da classe Seller
            //Chama o Metodo TotalSales pegando a soma do tempo entre o tempo inicial e final da classe Seller
            return Sellers.Sum(Seller => Seller.TotalSales(initial, final));


        }
        
     }
    }

