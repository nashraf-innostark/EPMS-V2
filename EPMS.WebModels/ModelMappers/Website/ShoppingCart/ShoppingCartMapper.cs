using System;
using System.Configuration;
using System.Linq;
using DomainModels=EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers.Website.ShoppingCart
{
    public static class ShoppingCartMapper
    {

        public static DomainModels.ShoppingCart CreateFromClientToServer(this WebsiteModels.ShoppingCart source)
        {
            return new DomainModels.ShoppingCart
            {
                CartId = source.CartId,
                UserCartId = source.UserCartId,
                ProductId = source.ProductId,
                Quantity = source.Quantity,
                SizeId = source.SizeId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }

        public static WebsiteModels.ShoppingCart CreateFromServerToClient(this DomainModels.ShoppingCart source)
        {
            var itemImage = source.Product.ItemVariation.ItemImages.FirstOrDefault();
            string itemImageName = itemImage != null ? itemImage.ItemImagePath : "";
            var productImage = source.Product.ProductImages.FirstOrDefault();
            string productImageName = productImage != null ? productImage.ProductImagePath : "noimage_department.png";

            string itemImageFolder = source.Product.ItemVariationId != null
                ? ConfigurationManager.AppSettings["InventoryImage"] + itemImageName
                : ConfigurationManager.AppSettings["ProductImage"] + productImageName;
            return new WebsiteModels.ShoppingCart
            {
                CartId = source.CartId,
                UserCartId = source.UserCartId,
                ProductId = source.ProductId,
                Quantity = source.Quantity,
                SizeId = source.SizeId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                ItemNameEn = source.Product.ItemVariationId != null ? source.Product.ItemVariation.InventoryItem.ItemNameEn : source.Product.ProductNameEn,
                ItemNameAr = source.Product.ItemVariationId != null ? source.Product.ItemVariation.InventoryItem.ItemNameAr : source.Product.ProductNameAr,
                SkuCode = source.Product.ItemVariationId != null ? source.Product.ItemVariation.SKUCode : source.Product.SKUCode,
                UnitPrice = source.Product.ItemVariationId != null ? (double)source.Product.ItemVariation.UnitPrice : Convert.ToDouble(source.Product.ProductPrice),
                ImagePath = itemImageFolder
            };
        }
    }
}
