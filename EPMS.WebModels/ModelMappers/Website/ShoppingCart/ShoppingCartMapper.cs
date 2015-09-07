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
                TransactionId = source.TransactionId,
                Status = source.Status,
                AmountPaid = source.AmountPaid,
                CurrencyCode = source.CurrencyCode,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                ShoppingCartItems = source.ShoppingCartItems.Select(x=>x.CreateFromClientToServer()).ToList()
            };
        }

        public static WebsiteModels.ShoppingCart CreateFromServerToClient(this DomainModels.ShoppingCart source)
        {
            return new WebsiteModels.ShoppingCart
            {
                CartId = source.CartId,
                UserCartId = source.UserCartId,
                TransactionId = source.TransactionId,
                Status = source.Status,
                AmountPaid = source.AmountPaid,
                CurrencyCode = source.CurrencyCode,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                ShoppingCartItems = source.ShoppingCartItems.Select(x => x.CreateFromServerToClient()).ToList()
            };
        }
    }
}
