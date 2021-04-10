using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorMicroService.Models;

namespace VendorMicroService.Repository
{
    public class VendorRepository : IVendorRepository
    {
        private List<Vendor> vendors;
        private List<VendorStock> vendorstocks;


        public VendorRepository()
        {
            vendors = new List<Vendor>()
            {
                new Vendor { VendorId = 1, VendorName = "Apple",  DeliveryCharge= 150,VendorRating = 4 },
                new Vendor { VendorId = 2, VendorName = "Samsung", DeliveryCharge = 200, VendorRating = 3 },
                new Vendor { VendorId = 3, VendorName = "Microsoft", DeliveryCharge = 150, VendorRating = 3 },
                new Vendor { VendorId = 4, VendorName = "Boult", DeliveryCharge= 250, VendorRating = 5 },
                new Vendor { VendorId = 5, VendorName = "Sony", DeliveryCharge = 300, VendorRating = 4 },
                new Vendor { VendorId = 6, VendorName = "Redmi", DeliveryCharge = 300, VendorRating = 4 },
                new Vendor { VendorId = 7, VendorName = "Amazon", DeliveryCharge = 300, VendorRating = 4 },
                new Vendor { VendorId = 8, VendorName = "GoPro", DeliveryCharge = 300, VendorRating = 4 },
            };
            vendorstocks = new List<VendorStock>()
            {
                new VendorStock {ProductId=1, VendorId = 1,StockInHand=1,  ExpectedStockReplenishmentDate=new DateTime(2020,7,6) },
                new VendorStock {ProductId=1, VendorId = 2,StockInHand=1,  ExpectedStockReplenishmentDate=new DateTime(2020,7,6) },
                new VendorStock { ProductId = 2, VendorId = 4,StockInHand=1,  ExpectedStockReplenishmentDate=new DateTime(2020,6,15) },
                new VendorStock { ProductId = 3,VendorId = 2,StockInHand=0,  ExpectedStockReplenishmentDate=new DateTime(2020,6,15) },
                new VendorStock {ProductId = 4,VendorId = 6,StockInHand=0,  ExpectedStockReplenishmentDate=new DateTime(2020,6,15) },
                new VendorStock {ProductId = 5, VendorId = 5,StockInHand=1,  ExpectedStockReplenishmentDate=new DateTime(2020,6,15) },
                new VendorStock {ProductId = 6, VendorId = 5,StockInHand=1,  ExpectedStockReplenishmentDate=new DateTime(2020,6,15) },
                new VendorStock {ProductId = 7, VendorId = 3,StockInHand=1,  ExpectedStockReplenishmentDate=new DateTime(2020,6,15) },
                new VendorStock {ProductId = 8, VendorId = 7,StockInHand=1,  ExpectedStockReplenishmentDate=new DateTime(2020,6,15) },
                new VendorStock {ProductId = 9, VendorId = 8,StockInHand=1,  ExpectedStockReplenishmentDate=new DateTime(2020,6,15) },
                new VendorStock {ProductId = 10, VendorId = 2,StockInHand=1,  ExpectedStockReplenishmentDate=new DateTime(2020,6,15) },
            };
        }
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(VendorRepository));

        public List<VendorDto> GetVendorDetails()
        {
            List<VendorDto> vendorsdto = new List<VendorDto>();

            foreach (Vendor ven in vendors)
            {
                VendorDto vendornewdto = new VendorDto()
                {
                    VendorId = ven.VendorId,
                    VendorName = ven.VendorName,
                    DeliveryCharge = ven.DeliveryCharge,
                    VendorRating = ven.VendorRating,
                };
                vendorsdto.Add(vendornewdto);
            }

            return vendorsdto;
        }
        public List<VendorStockDto> GetVendorStocks()
        {
            List<VendorStockDto> vendorstockdto = new List<VendorStockDto>();

            foreach (VendorStock venst in vendorstocks)
            {
                VendorStockDto vendorstocknewdto = new VendorStockDto()
                {
                    ProductId = venst.ProductId,
                    VendorId = venst.VendorId,
                    StockInHand = venst.StockInHand,
                    ExpectedStockReplenishmentDate = venst.ExpectedStockReplenishmentDate,
                };
                vendorstockdto.Add(vendorstocknewdto);
            }

            return vendorstockdto;
        }


    }
}
