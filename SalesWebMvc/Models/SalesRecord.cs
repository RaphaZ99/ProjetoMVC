﻿using SalesWebMvc.Models.Enums;
using System;


namespace SalesWebMvc.Models
{
    public class SalesRecord
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Double  Amount { get; set; }
        public SaleStatus MyProperty { get; set; }
        public Seller Seller { get; set; }


        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus myProperty, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            MyProperty = myProperty;
            Seller = seller;
        }
    }
}
