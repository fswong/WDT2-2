using Assignment2.Data;
using Assignment2.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Repository
{
    public class OwnerInventoryRepository
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public OwnerInventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region methods
        public List<OwnerInventory> GetOwnerInventoryList() {
            try
            {
                var response = _context.OwnerInventories.ToList();
                return response;
            }
            catch (Exception e) {
                throw e;
            }
        }
        #endregion
    }
}
