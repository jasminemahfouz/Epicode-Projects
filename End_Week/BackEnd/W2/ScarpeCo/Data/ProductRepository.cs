
using System.Collections.Generic;
using System.Linq;
using ScarpeCo.Models;

namespace ScarpeCo.Data
{
    public static class ProductRepository
    {
        private static List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Nike Air Zoom Vapor Pro",
                Price = 140.00m,
                Description = "Scarpa da tennis leggera e reattiva, ideale per movimenti rapidi e dinamici.",
                CoverImage = "https://static.nike.com/a/images/t_PDP_1728_v1/f_auto,q_auto:eco/69689570-b718-4bbd-b6bd-565716260bbd/scarpa-da-tennis-per-campi-in-cemento-nikecourt-air-zoom-vapor-pro-2-q4BR1z.png",
                AdditionalImage1 = "https://static.nike.com/a/images/t_PDP_1728_v1/f_auto,q_auto:eco/ba51e36d-9685-490c-80de-9decfa10bc8d/scarpa-da-tennis-per-campi-in-cemento-nikecourt-air-zoom-vapor-pro-2-q4BR1z.png",
                AdditionalImage2 = "https://static.nike.com/a/images/t_PDP_1728_v1/f_auto,q_auto:eco/2206b1d6-acb3-49a3-bfba-ac8ad03a40e0/scarpa-da-tennis-per-campi-in-cemento-nikecourt-air-zoom-vapor-pro-2-q4BR1z.png"
            },
            new Product
            {
                Id = 2,
                Name = "Adidas Adizero Ubersonic 4",
                Price = 130.00m,
                Description = "Design moderno e prestazioni elevate per i giocatori di tennis più esigenti.",
                CoverImage = "https://m.media-amazon.com/images/I/711nuj3yMHL._AC_SX695_.jpg",
                AdditionalImage1 = "https://m.media-amazon.com/images/I/71gNCCsJP+L._AC_SX695_.jpg",
                AdditionalImage2 = "https://m.media-amazon.com/images/I/71mJMAhx+fL._AC_SX695_.jpg"
            },
            new Product
            {
                Id = 3,
                Name = "Asics Gel-Resolution 8",
                Price = 150.00m,
                Description = "Ottima stabilità e comfort per lunghe sessioni di allenamento e gioco.",
                CoverImage = "https://images.asics.com/is/image/asics/1041A079_001_SB_FR_GLB?$zoom$",
                AdditionalImage1 = "https://images.asics.com/is/image/asics/1041A079_001_SB_FR_GLB?$zoom$",
                AdditionalImage2 = "https://images.asics.com/is/image/asics/1041A079_001_SB_TP_GLB?$zoom$"
            },
            new Product
            {
                Id = 4,
                Name = "Wilson Rush Pro 3.5",
                Price = 120.00m,
                Description = "Scarpa versatile con ammortizzazione e supporto per ogni tipo di superficie.",
                CoverImage = "https://m.media-amazon.com/images/I/612Mb0sDmJL._AC_SY695_.jpg",
                AdditionalImage1 = "https://m.media-amazon.com/images/I/6139emKc2bL._AC_SY695_.jpg",
                AdditionalImage2 = "https://m.media-amazon.com/images/I/61Ic3mikjhL._AC_SY695_.jpg"
            },
            new Product
            {
                Id = 5,
                Name = "New Balance Fresh Foam Lav",
                Price = 160.00m,
                Description = "Tecnologia Fresh Foam per un comfort e una reattività eccezionali.",
                CoverImage = "https://m.media-amazon.com/images/I/7102NlZIVvL._AC_SY695_.jpg",
                AdditionalImage1 = "https://m.media-amazon.com/images/I/71LbOnpXgEL._AC_SY695_.jpg",
                AdditionalImage2 = "https://m.media-amazon.com/images/I/817SH4Uhw2L._AC_SY695_.jpg"
            },
            new Product
            {
                Id = 6,
                Name = "Babolat Jet Mach II",
                Price = 135.00m,
                Description = "Scarpa leggera e performante per velocità e agilità in campo.",
                CoverImage = "https://m.media-amazon.com/images/I/61LqwLtmAXL._AC_SY695_.jpg",
                AdditionalImage1 = "https://m.media-amazon.com/images/I/71rzO5XMuzL._AC_SY695_.jpg",
                AdditionalImage2 = "https://m.media-amazon.com/images/I/61OWqaVIdoL._AC_SY695_.jpg"
            },
            new Product
            {
                Id = 7,
                Name = "Fila Axilus 2 Energized",
                Price = 110.00m,
                Description = "Massimo supporto e durata con un design moderno.",
                CoverImage = "https://m.media-amazon.com/images/I/71yyc-1W07L._AC_SX695_.jpg",
                AdditionalImage1 = "https://m.media-amazon.com/images/I/6124AtI2hPL._AC_SX695_.jpg",
                AdditionalImage2 = "https://m.media-amazon.com/images/I/61qWN3AfluL._AC_SX695_.jpg"
            },
            new Product
            {
                Id = 8,
                Name = "Yonex Power Cushion Eclipsion 3",
                Price = 145.00m,
                Description = "Assorbimento degli shock e ottimo supporto per movimenti veloci.",
                CoverImage = "https://m.media-amazon.com/images/I/81Oaqw3JRdL._AC_SY695_.jpg",
                AdditionalImage1 = "https://m.media-amazon.com/images/I/91fpetFXuDL._AC_SY695_.jpg",
                AdditionalImage2 = "https://m.media-amazon.com/images/I/91pjfeP1qQL._AC_SY695_.jpg"
            },
            new Product
            {
                Id = 9,
                Name = "K-Swiss Hypercourt Express 2",
                Price = 125.00m,
                Description = "Comodità e prestazioni in una scarpa leggera e traspirante.",
                CoverImage = "https://m.media-amazon.com/images/I/51yTZdUC9rL._AC_SY695_.jpg",
                AdditionalImage1 = "https://m.media-amazon.com/images/I/51KnVEknIAL._AC_SY695_.jpg",
                AdditionalImage2 = "https://m.media-amazon.com/images/I/510onHvp-aL._AC_SY695_.jpg"
            }
        };

        public static List<Product> GetProducts()
        {
            return products;
        }

        public static Product GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public static void AddProduct(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
        }

        public static void RemoveProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                products.Remove(product);
            }
        }
        public static void UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.CoverImage = product.CoverImage;
                existingProduct.AdditionalImage1 = product.AdditionalImage1;
                existingProduct.AdditionalImage2 = product.AdditionalImage2;
            }
        }
        public static void PurchaseProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                product.IsPurchased = true;
            }
        }

        public static void UnpurchaseProduct(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                product.IsPurchased = false;
            }
        }

        public static List<Product> GetPurchasedProducts()
        {
            return products.Where(p => p.IsPurchased).ToList();
        }
    
}
}
