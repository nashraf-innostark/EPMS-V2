using System;
using System.Configuration;
using System.Linq;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers.Website.ShoppingCart
{
    public static class ShoppingCartItemMapper
    {
        public static DomainModels.ShoppingCartItem CreateFromClientToServer(this WebsiteModels.ShoppingCartItem source)
        {
            return new DomainModels.ShoppingCartItem
            {
                CartItemId = source.CartItemId,
                ProductId = source.ProductId,
                UnitPrice = source.UnitPrice,
                Quantity = source.Quantity,
                SizeId = source.SizeId,
            };
        }

        public static WebsiteModels.ShoppingCartItem CreateFromServerToClient(this DomainModels.ShoppingCartItem source)
        {
            var itemImage = source.Product.ItemVariation.ItemImages.FirstOrDefault();
            string itemImageName = itemImage != null ? itemImage.ItemImagePath : "";
            var productImage = source.Product.ProductImages.FirstOrDefault();
            string productImageName = productImage != null ? productImage.ProductImagePath : "noimage_department.png";

            string itemImageFolder = source.Product.ItemVariationId != null
                ? ConfigurationManager.AppSettings["InventoryImage"] + itemImageName
                : ConfigurationManager.AppSettings["ProductImage"] + productImageName;
            return new WebsiteModels.ShoppingCartItem
            {
                CartItemId = source.CartItemId,
                ProductId = source.ProductId,
                UnitPrice = source.Product.ItemVariationId != null ? Convert.ToDecimal(source.Product.ItemVariation.UnitPrice) : Convert.ToDecimal(source.Product.ProductPrice),
                Quantity = source.Quantity,
                SizeId = source.SizeId,
                ItemNameEn = source.Product.ItemVariationId != null ? source.Product.ItemVariation.InventoryItem.ItemNameEn : source.Product.ProductNameEn,
                ItemNameAr = source.Product.ItemVariationId != null ? source.Product.ItemVariation.InventoryItem.ItemNameAr : source.Product.ProductNameAr,
                SkuCode = source.Product.ItemVariationId != null ? source.Product.ItemVariation.SKUCode : source.Product.SKUCode,
                ImagePath = itemImageFolder
            };
        }
    }
}
