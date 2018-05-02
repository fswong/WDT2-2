using Assignment2.Data;
using Assignment2.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Repository
{
    public class StoreRepository
    {
        #region properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region ctor
        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region methods
        /// <summary>
        /// list all stores
        /// </summary>
        /// <returns></returns>
        public List<Store> GetStoreList() {
            try {
                var response = _context.Stores.ToList();
                return response;
            }
            catch (Exception e) {
                throw e;
            }
        }

        /// <summary>
        /// get single store
        /// </summary>
        /// <param name="id"></param>
        /// <param name="allowedNotFound"></param>
        /// <returns></returns>
        public Store GetStore(int id, bool allowedNotFound = false) {
            try
            {
                Store response = null;
                if (allowedNotFound) {
                    response = _context.Stores.SingleOrDefault(s => s.StoreID == id);
                }
                else {
                    response = _context.Stores.Single(s => s.StoreID == id);
                }
                
                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
