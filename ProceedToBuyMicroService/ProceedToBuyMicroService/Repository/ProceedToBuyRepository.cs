using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProceedToBuyMicroService.Models;

namespace ProceedToBuyMicroService.Repository
{
    public class ProceedToBuyRepository : IProceedToBuyRepository
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyRepository));

        private List<Wishlist> Wlist;
        private List<Cart> Clist;
        private Vendor vendor = new Vendor { VendorId = 1, VendorName = "Apple", DeliveryCharge = 150, Rating = 4 };
        public ProceedToBuyRepository()
        {
            Wlist = new List<Wishlist>() {
            new Wishlist { ProductId = 1, Quantity = 2, DateAddedToWishlist = DateTime.Parse("2021-09-03") } ,
            new Wishlist { ProductId = 2, Quantity = 1, DateAddedToWishlist = DateTime.Parse("2021-01-01") } ,
            new Wishlist { ProductId = 3, Quantity = 4, DateAddedToWishlist = DateTime.Parse("2020-12-12") }
            };

            Clist = new List<Cart>() {
            new Cart { CartId=1 ,CustomerId=1,ProductId=1 ,Zipcode=533048 ,DeliveryDate = DateTime.Parse("2021-09-03") ,VendorObj=vendor} ,
            new Cart {CartId=2  ,CustomerId=2,ProductId=1 ,Zipcode=533296 ,DeliveryDate = DateTime.Parse("2020-12-28") ,VendorObj=vendor} ,
            new Cart {CartId=3  ,CustomerId=3,ProductId=2 ,Zipcode=533047 ,DeliveryDate = DateTime.Parse("2021-01-05") ,VendorObj=vendor} ,
            new Cart {CartId=2  ,CustomerId=4,ProductId=2 ,Zipcode=533207 ,DeliveryDate = DateTime.Parse("2021-02-15") ,VendorObj=vendor}

            };
        }
        public CartDto addToCart(CartDto entity)
        {
            try
            {
                Cart newcart = new Cart()
                {
                    CartId = entity.CartId,
                    CustomerId = entity.CustomerId,
                    ProductId = entity.ProductId,
                    DeliveryDate = entity.DeliveryDate,
                    VendorObj = entity.VendorObj,

                };
                Clist.Add(newcart);
                _log4net.Info("Item Added to Cart");
                return entity;
            }
            catch
            {
                _log4net.Info("Item not Added to Cart");
                return null;
            }

        }

        public WishlistDto addToWishlist(WishlistDto entity)
        {
            try
            {
                if (entity.ProductId != 0 && entity.CustomerId != 0)
                {
                    Wishlist nwl = new Wishlist()
                    {
                        CustomerId = entity.CustomerId,
                        ProductId = entity.ProductId,
                        Quantity = entity.Quantity,
                        DateAddedToWishlist = entity.DateAddedToWishlist,
                    };
                    Wlist.Add(nwl);
                    _log4net.Info("Item Added to Wishlist");
                    return entity;
                }
                return null;
            }
            catch
            {
                _log4net.Info("Item not Added to Wishlist");
                return null;
            }
        }
    }
}
