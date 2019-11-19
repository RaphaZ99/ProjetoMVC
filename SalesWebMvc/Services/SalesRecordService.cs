using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {

        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {

            _context = context;
        }

        public async  Task <List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //pega o sales record tipo dbset e constroi um objeto do tipo ICreable
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                //validacao para que a data minima n seja meno que ela mesma
                result = result.Where(x => x.Date >= minDate.Value);

            }
            if (maxDate.HasValue)
            {
                //validacao para que a data minima n seja meno que ela mesma
                result = result.Where(x => x.Date >= maxDate.Value);

            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Departament)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
                
              
        }

        public async Task<List<IGrouping<Departament,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            //pega o sales record tipo dbset e constroi um objeto do tipo ICreable
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                //validacao para que a data minima n seja meno que ela mesma
                result = result.Where(x => x.Date >= minDate.Value);

            }
            if (maxDate.HasValue)
            {
                //validacao para que a data minima n seja meno que ela mesma
                result = result.Where(x => x.Date >= maxDate.Value);

            }

            //Join das tabelas
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Departament)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Departament)
                .ToListAsync();


        }
    }
}
