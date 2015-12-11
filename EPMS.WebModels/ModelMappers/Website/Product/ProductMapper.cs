using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.WebModels.ModelMappers.Website.ProductImage;
using Size = EPMS.WebModels.WebsiteModels.Size;

namespace EPMS.WebModels.ModelMappers.Website.Product
{
    public static class ProductMapper
    {
        public static WebsiteModels.Product CreateFromServerToClientForLv(this Models.DomainModels.Product source)
        {
            return new WebsiteModels.Product
            {
                ProductId = source.ProductId,
                ProductNameEn = !string.IsNullOrEmpty(source.ProductNameEn) ? source.ProductNameEn : source.ItemVariation.SKUDescriptionEn,
                ProductNameAr = !string.IsNullOrEmpty(source.ProductNameAr) ? source.ProductNameAr : source.ItemVariation.SKUDescriptionAr,
                ItemVariationId = source.ItemVariationId,
                SKUCode = source.SKUCode,
                ProductDescEn = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductDescEn) : RemoveCkEditorValues(source.ItemDescriptionEn),
                ProductDescAr = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductDescAr) : RemoveCkEditorValues(source.ItemDescriptionAr),
                ProductPrice = source.ItemVariationId != null ? source.ItemVariation.UnitPrice.ToString() : source.ProductPrice,
                DiscountedPrice = source.DiscountedPrice,
                ProductSpecificationEn = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductSpecificationEn) : RemoveCkEditorValues(source.ItemVariation.AdditionalInfoEn),
                ProductSpecificationAr = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductSpecificationAr) : RemoveCkEditorValues(source.ItemVariation.AdditionalInfoAr),
            };
        }
        public static WebsiteModels.Product CreateFromServerToClient(this Models.DomainModels.Product source)
        {
            WebsiteModels.Product retVal = new WebsiteModels.Product();
            retVal.ProductId = source.ProductId;
            retVal.ProductNameEn = !string.IsNullOrEmpty(source.ProductNameEn) ? source.ProductNameEn : source.ItemVariation.SKUDescriptionEn;
            retVal.ProductNameAr = !string.IsNullOrEmpty(source.ProductNameAr) ? source.ProductNameAr : source.ItemVariation.SKUDescriptionAr;
            retVal.ItemVariationId = source.ItemVariationId;
            retVal.ProductDescEn = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductDescEn) : RemoveCkEditorValues(source.ItemDescriptionEn);
            retVal.ProductDescAr = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductDescAr) : RemoveCkEditorValues(source.ItemDescriptionAr);
            retVal.ProductPrice = source.ItemVariationId != null ? source.ItemVariation.UnitPrice.ToString() : source.ProductPrice;
            retVal.DiscountedPrice = source.DiscountedPrice;
            retVal.ProductSpecificationEn = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductSpecificationEn) : RemoveCkEditorValues(source.ItemVariation.AdditionalInfoEn);
            retVal.ProductSpecificationAr = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductSpecificationAr) : RemoveCkEditorValues(source.ItemVariation.AdditionalInfoAr);
            retVal.ProductSize = source.ProductSize;
            retVal.ProductSectionId = source.ProductSectionId;
            retVal.NewArrival = source.NewArrival;
            retVal.Featured = source.Featured;
            retVal.RandomProduct = source.RandomProduct;
            retVal.BestSeller = source.BestSeller;
            retVal.SKUCode = source.SKUCode;
            retVal.RecCreatedBy = source.RecCreatedBy;
            retVal.RecCreatedDt = source.RecCreatedDt;
            retVal.RecCreatedDate = Convert.ToDateTime(source.RecCreatedDt).ToString("dd/MM/yyyy", new CultureInfo("en"));
            retVal.DeptColor = source.ItemVariation != null ? source.ItemVariation.InventoryItem.InventoryDepartment.DepartmentColor : "";
            retVal.ItemDesc = source.ItemVariation != null ? source.ItemVariation.DescriptionEn : "";
            retVal.RecLastUpdatedBy = source.RecLastUpdatedBy;
            retVal.RecLastUpdatedDt = source.RecLastUpdatedDt;
            retVal.ItemNameEn = source.ItemVariation != null ? source.ItemVariation.SKUDescriptionEn : "";
            retVal.ItemNameAr = source.ItemVariation != null ? source.ItemVariation.SKUDescriptionAr : "";
            retVal.ProductImages = source.ProductImages.Any() ? source.ProductImages.Select(x => x.CreateFromServerToClient()).ToList() : new List<WebsiteModels.ProductImage>();
            retVal.ItemImage = source.ItemVariation != null && source.ItemVariation.ItemImages != null && source.ItemVariation.ItemImages.FirstOrDefault() != null ?
                source.ItemVariation.ItemImages.FirstOrDefault().ItemImagePath : "";
            retVal.ProductImage = source.ProductImages != null && source.ProductImages.Any() && source.ProductImages.FirstOrDefault() != null ?
                source.ProductImages.FirstOrDefault().ProductImagePath : "";
            retVal.DepartmentNameEn = source.ItemVariationId != null ?
                source.ItemVariation.InventoryItem.InventoryDepartment.DepartmentNameEn :
                source.ProductSection != null ? source.ProductSection.SectionNameEn : "";
            retVal.DepartmentNameAr = source.ItemVariationId != null ?
                source.ItemVariation.InventoryItem.InventoryDepartment.DepartmentNameAr :
                source.ProductSection != null ? source.ProductSection.SectionNameAr : "";
            var direction = Resources.Shared.Common.TextDirection;
            if (source.ItemVariationId != null)
            {
                var department = source.ItemVariation.InventoryItem.InventoryDepartment;
                while (department != null)
                {
                    retVal.PathTillParent += direction == "ltr"
                        ? department.DepartmentNameEn + ">"
                        : department.DepartmentNameAr + ">";
                    department = department.ParentDepartment;
                }
                var path = retVal.PathTillParent.Split('>');
                retVal.PathTillParent = "";
                for (int i = path.Length - 2; i >= 0; i--)
                {
                    if (i != path.Length - 2)
                    {
                        retVal.PathTillParent += " > " + path[i];
                    }
                    else
                    {
                        retVal.PathTillParent += path[i];
                    }
                }
            }
            else
            {
                var section = source.ProductSection;
                while (section != null)
                {
                    retVal.PathTillParent += direction == "ltr" ? section.SectionNameEn + ">" : section.SectionNameAr + ">";
                    section = section.ParentSection;
                }
                var path = retVal.PathTillParent.Split('>');
                retVal.PathTillParent = "";
                for (int i = path.Length - 2; i >= 0; i--)
                {
                    if (i != path.Length - 2)
                    {
                        retVal.PathTillParent += " > " + path[i];
                    }
                    else
                    {
                        retVal.PathTillParent += path[i];
                    }
                }
            }
            return retVal;
        }
        public static WebsiteModels.Product CreateFromServerToClientFromInventory(this Models.DomainModels.Product source)
        {
            var retVal = new WebsiteModels.Product
            {
                ProductId = source.ProductId,
                InventoryItemId = source.ItemVariation != null ? source.ItemVariation.InventoryItem.ItemId : 0,
                ProductNameEn = source.ProductNameEn,
                ProductNameAr = source.ProductNameAr,
                ItemVariationId = source.ItemVariationId,
                ProductDescEn = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductDescEn) : RemoveCkEditorValues(source.ItemDescriptionEn),
                ProductDescAr = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductDescAr) : RemoveCkEditorValues(source.ItemDescriptionAr),
                ProductPrice = source.ItemVariationId != null ? source.ItemVariation.UnitPrice.ToString() : source.ProductPrice,
                DiscountedPrice = source.DiscountedPrice,
                ProductSpecificationEn = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductSpecificationEn) : RemoveCkEditorValues(source.ItemVariation.AdditionalInfoEn),
                ProductSpecificationAr = source.ItemVariationId == null ? RemoveCkEditorValues(source.ProductSpecificationAr) : RemoveCkEditorValues(source.ItemVariation.AdditionalInfoAr),
                ProductSize = source.ProductSize,
                ProductSectionId = source.ProductSectionId,
                SKUCode = source.SKUCode,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ItemNameEn = source.ItemVariation != null && source.ItemVariation.InventoryItem != null ? source.ItemVariation.InventoryItem.ItemNameEn : "",
                ItemNameAr = source.ItemVariation != null && source.ItemVariation.InventoryItem != null ? source.ItemVariation.InventoryItem.ItemNameAr : "",
                ProductImages = source.ProductImages != null ? source.ProductImages.Select(x => x.CreateFromServerToClient()).ToList() :
                                    new List<WebsiteModels.ProductImage>(),
                ItemImage = source.ItemVariation != null && source.ItemVariation.ItemImages != null && source.ItemVariation.ItemImages.FirstOrDefault() != null ?
                    source.ItemVariation.ItemImages.FirstOrDefault().ItemImagePath : "",
                ProductImage = source.ProductImages != null && source.ProductImages.Any() && source.ProductImages.FirstOrDefault() != null ?
                    source.ProductImages.FirstOrDefault().ProductImagePath : "",
                SizeId = source.ItemVariation != null && source.ItemVariation.Sizes.FirstOrDefault() != null ? source.ItemVariation.Sizes.FirstOrDefault().SizeId : 0,
                ItemImages = source.ItemVariation != null && source.ItemVariation.ItemImages != null ?
                            source.ItemVariation.ItemImages.Select(x => x.CreateFromServerToClient()) : new List<WebsiteModels.ItemImage>(),
            };
            if (source.ItemVariation != null)
            {
                retVal.Sizes = new List<Size>();
                foreach (var variation in source.ItemVariation.InventoryItem.ItemVariations)
                {
                    if (variation.Sizes != null && variation.Sizes.Any())
                    {
                        var variat = variation.Sizes.FirstOrDefault();
                        if (variat != null)
                        {
                            var size = new Size
                            {
                                SizeId = variation.Products.FirstOrDefault().ProductId,
                                SizeNameEn = variat.SizeNameEn,
                                SizeNameAr = variat.SizeNameAr
                            };
                            retVal.Sizes.Add(size);
                        }
                    }
                }
            }
            
            var direction = Resources.Shared.Common.TextDirection;
            if (source.ItemVariationId != null)
            {
                var department = source.ItemVariation.InventoryItem.InventoryDepartment;
                while (department != null)
                {
                    retVal.PathTillParent += direction == "ltr"
                        ? department.DepartmentNameEn + ">"
                        : department.DepartmentNameAr + ">";
                    department = department.ParentDepartment;
                }
                var path = retVal.PathTillParent.Split('>');
                retVal.PathTillParent = "";
                for (int i = path.Length - 2; i >= 0; i--)
                {
                    if (i != path.Length - 2)
                    {
                        retVal.PathTillParent += " > " + path[i];
                    }
                    else
                    {
                        retVal.PathTillParent += path[i];
                    }
                }
            }
            else
            {
                var section = source.ProductSection;
                while (section != null)
                {
                    retVal.PathTillParent += direction == "ltr" ? section.SectionNameEn + ">" : section.SectionNameAr + ">";
                    section = section.ParentSection;
                }
                var path = retVal.PathTillParent.Split('>');
                retVal.PathTillParent = "";
                for (int i = path.Length - 2; i >= 0; i--)
                {
                    if (i != path.Length - 2)
                    {
                        retVal.PathTillParent += " > " + path[i];
                    }
                    else
                    {
                        retVal.PathTillParent += path[i];
                    }
                }
            }
            retVal.DeptColor = source.ItemVariation != null ? source.ItemVariation.InventoryItem.InventoryDepartment.DepartmentColor : "";
            retVal.DepartmentNameEn = source.ItemVariationId != null ?
                source.ItemVariation.InventoryItem.InventoryDepartment.DepartmentNameEn :
                source.ProductSection != null ? source.ProductSection.SectionNameEn : "";
            retVal.DepartmentNameAr = source.ItemVariationId != null ?
                source.ItemVariation.InventoryItem.InventoryDepartment.DepartmentNameAr :
                source.ProductSection != null ? source.ProductSection.SectionNameAr : "";
            return retVal;
        }
        
        public static ProductRequest CreateFromClientToServer(this WebsiteModels.Product source)
        {
            var product = new Models.DomainModels.Product
            {
                ProductId = source.ProductId,
                ProductNameEn = source.ProductNameEn,
                ProductNameAr = source.ProductNameAr,
                ItemVariationId = source.ItemVariationId,
                ProductDescEn = source.ProductDescEn,
                ProductDescAr = source.ProductDescAr,
                ProductPrice = source.ProductPrice,
                DiscountedPrice = source.DiscountedPrice,
                ProductSpecificationEn = source.ProductSpecificationEn,
                ProductSpecificationAr = source.ProductSpecificationAr,
                ProductSize = source.ProductSize,
                ProductSectionId = source.ProductSectionId,
                NewArrival = source.NewArrival,
                Featured = source.Featured,
                RandomProduct = source.RandomProduct,
                BestSeller = source.BestSeller,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = !string.IsNullOrEmpty(source.RecCreatedDate) ? DateTime.ParseExact(source.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")) : source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
            var request = new ProductRequest
            {
                Product = product,
                ProductImages =
                    new List<Models.DomainModels.ProductImage>(source.ProductImages.Select(x => x.CreateFromClientToServer()))
            };
            return request;
        }

        public static WebsiteModels.Product CreateFromItemVariation(this ItemVariation source)
        {
            return new WebsiteModels.Product
            {
                ProductId = source.Products.FirstOrDefault(x => x.ItemVariationId == source.ItemVariationId).ProductId,
                ItemVariationId = source.ItemVariationId,
                ProductNameEn = source.InventoryItem.ItemNameEn,
                ProductNameAr = source.InventoryItem.ItemNameAr,
                ProductDescEn = RemoveCkEditorValues(source.InventoryItem.ItemDescriptionEn),
                ProductDescAr = RemoveCkEditorValues(source.InventoryItem.ItemDescriptionAr),
                ProductPrice = source.UnitPrice.ToString(),
                SKUCode = source.SKUCode,
                ItemImages = source.ItemImages.Select(x => x.CreateFromServerToClient()),
                Sizes = source.Sizes.Select(x => x.CreateFromServerToClient()).ToList(),
                ItemImage = source.ItemImages != null && source.ItemImages.FirstOrDefault() != null ?
                    source.ItemImages.FirstOrDefault().ItemImagePath : "",
            };
        }

        #region Remove \r \n from CK editor values

        private static string RemoveCkEditorValues(string value)
        {
            string retval = value;
            if (!string.IsNullOrEmpty(retval))
            {
                retval = retval.Replace('\r', ' ');
                retval = retval.Replace('\n', ' ');
            }
            return retval;
        }

        #endregion
    }
}