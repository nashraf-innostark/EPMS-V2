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
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static WebsiteModels.ShoppingCart CreateFromServerToClient(this DomainModels.ShoppingCart source)
        {
            return new WebsiteModels.ShoppingCart
            {
                CartId = source.CartId,
                UserCartId = source.UserCartId,
                ProductId = source.ProductId,
                Quantity = source.Quantity,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
    }
}
