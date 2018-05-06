using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Memberships.Areas.Admin.Models;
using Memberships.Entities;
using Memberships.Models;

namespace Memberships.Areas.Admin.Extensions
{
    public static class ConversionExtension
    {
        public static async Task<IEnumerable<ProductModel>> Convert(this IEnumerable<Product> products, ApplicationDbContext db)
        {
            if(products.Count().Equals(0)) return new List<ProductModel>();

            var texts = await db.ProductLinkTexts.ToListAsync();
            var types = await db.ProductTypes.ToListAsync();

            return from p in products
                select new ProductModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    ProductLinkTextId = p.ProductLinkTextId,
                    ProductTypeIs = p.ProductTypeIs,
                    ProductLinkTexts = texts,
                    ProductTypes = types
                };
        }

        public static async Task<ProductModel> Convert(this Product product, ApplicationDbContext db)
        {
           

            var texts = await db.ProductLinkTexts.FirstOrDefaultAsync(p => p.Id.Equals(product.ProductLinkTextId));
            var types = await db.ProductTypes.FirstOrDefaultAsync(p => p.Id.Equals(product.ProductTypeIs));

            var model = new ProductModel
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    ProductLinkTextId = product.ProductLinkTextId,
                    ProductTypeIs = product.ProductTypeIs,
                    ProductLinkTexts = new List<ProductLinkText>(),
                    ProductTypes = new List<ProductType>()
                };

            model.ProductLinkTexts.Add(texts);
            model.ProductTypes.Add(types);

            return model;
        }

        public static async Task<IEnumerable<ProductItemModel>> Convert(this IQueryable<ProductItem> productsItems, ApplicationDbContext db)
        {
            if (productsItems.Count().Equals(0)) return new List<ProductItemModel>();

            return await( from pi in productsItems
                select new ProductItemModel()
                {
                    ItemId = pi.ItemId,
                    ProductId = pi.ProductId,
                    ItemTitle = db.Items.FirstOrDefault(i => i.Id.Equals(pi.ItemId)).Title,
                    ProductTitle = db.Products.FirstOrDefault(p => p.Id.Equals(pi.ProductId)).Title
                }).ToListAsync();
        }

        public static async Task<ProductItemModel> Convert(this ProductItem productItem, ApplicationDbContext db)
        {

            var model = new ProductItemModel
            {
                ItemId = productItem.ItemId,
                ProductId = productItem.ProductId,
                Items = await db.Items.ToListAsync(),
                Products = await db.Products.ToListAsync(),
            };

            return model;
        }
    }
}