using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ModelMappers.Website.ShoppingCart;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.WebsiteClient;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Website.Controllers
{
    public class ShoppingCartController : BaseController
    {
        #region Private
        private readonly IShoppingCartService cartService;
        private readonly IShoppingCartItemService cartItemService;
        private readonly IProductService productService;
        private readonly IWebsiteHomePageService homePageService;

        #endregion

        #region Constructor

        public ShoppingCartController(IShoppingCartService cartService, IProductService productService, IShoppingCartItemService cartItemService, IWebsiteHomePageService homePageService)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.cartItemService = cartItemService;
            this.homePageService = homePageService;
        }

        #endregion

        #region Public
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ShoppingCartListViewModel viewModel = new ShoppingCartListViewModel
            {
                ShowProductPrice = homePageService.GetHomePageResponse().HomePage.ShowProductPrice
            };
            string cartId = GetCartId();
            // check DB
            var shoppingCart = cartService.FindByUserCartId(cartId);
            
            viewModel.ShoppingCart = shoppingCart != null
                    ? shoppingCart.CreateFromServerToClient()
                    : new ShoppingCart();
            if (shoppingCart != null)
            {
                Session["ShoppingCartItems"] = viewModel.ShoppingCart;
            }
            // check local
            var cart = Session["ShoppingCartItems"];
            if (cart != null)
            {
                ShoppingCart cartItem = (ShoppingCart)Session["ShoppingCartItems"];
                if (cartItem != null)
                {
                    viewModel.ShoppingCart = cartItem;
                }
            }
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        public ActionResult CheckOut(ShoppingCartListViewModel model)
        {
            return View(model);
        }

        private string GetCartId()
        {
            string cartId = "";
            if (Session["ShoppingCartId"] != null)
            {
                cartId = Session["ShoppingCartId"].ToString();
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    cartId = User.Identity.GetUserId();
                    Session["ShoppingCartId"] = cartId;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    cartId = tempCartId.ToString();
                    Session["ShoppingCartId"] = tempCartId;
                }
            }
            return cartId;
        }
        #endregion

        #region Add Item To Cart
        [HttpPost]
        public JsonResult AddToCart(long productId, long sizeId, int quantity)
        {
            long? nullSize = null;
            string itemName = "";
            var direction = Resources.Shared.Common.TextDirection;
            // get Product data from DB
            var product = productService.FindProductById(productId);
            Product userProduct = product.ItemVariationId != null ? product.CreateFromServerToClientFromInventory() : product.CreateFromServerToClient();
            // check if size is zero then add default size of Product
            if (sizeId == 0)
            {
                sizeId = userProduct.ItemVariationId != null ? userProduct.SizeId : sizeId;
            }
            // Product Image path
            string itemImageFolder = "";
            itemImageFolder = userProduct.ItemVariationId != null ? ConfigurationManager.AppSettings["InventoryImage"] : ConfigurationManager.AppSettings["ProductImage"];
            // Get User Cart Id
            string userCartid = GetCartId();
            // Get user Cart from Session
            var items = Session["ShoppingCartItems"];
            ShoppingCart cart;
            if (items != null)
            {
                cart = (ShoppingCart)Session["ShoppingCartItems"];
                var item = cart.ShoppingCartItems.FirstOrDefault(y => y.ProductId == productId);
                if (item != null)
                {
                    item.Quantity += quantity;
                    itemName = direction == "ltr" ? item.ItemNameEn : item.ItemNameAr;
                    // Update DB Cart
                    if (User.Identity.IsAuthenticated)
                    {
                        var dbCartItem = cartItemService.FindById(item.CartItemId);
                        if (dbCartItem != null)
                        {
                            dbCartItem.Quantity += quantity;
                            cartItemService.UpdateShoppingCartItem(dbCartItem);
                        }
                    }
                }
                else
                {
                    string imagePath = "";
                    if (userProduct.ItemVariationId != null)
                    {
                        if (string.IsNullOrEmpty(userProduct.ItemImage))
                        {
                            imagePath = ConfigurationManager.AppSettings["SiteURL"] + "/images/noimage_department.png";
                        }
                        else
                        {
                            imagePath = itemImageFolder + userProduct.ItemImage;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(userProduct.ProductImage))
                        {
                            imagePath = ConfigurationManager.AppSettings["SiteURL"] + "/images/noimage_department.png";
                        }
                        else
                        {
                            imagePath = itemImageFolder + userProduct.ProductImage;
                        }
                    }
                    ShoppingCartItem itemToAdd = new ShoppingCartItem
                    {
                        CartId = cart.CartId,
                        ProductId = productId,
                        SizeId = sizeId != 0 ? sizeId : nullSize,
                        Quantity = quantity,
                        ItemNameEn = userProduct.ItemVariationId != null ? userProduct.ItemNameEn : userProduct.ProductNameEn,
                        ItemNameAr = userProduct.ItemVariationId != null ? userProduct.ItemNameAr : userProduct.ProductNameAr,
                        UnitPrice = !string.IsNullOrEmpty(userProduct.ProductPrice) ? Convert.ToDecimal(userProduct.ProductPrice) : 0,
                        SkuCode = userProduct.SKUCode,
                        ImagePath = imagePath,
                    };
                    cart.ShoppingCartItems.Add(itemToAdd);
                    itemName = direction == "ltr" ? itemToAdd.ItemNameEn : itemToAdd.ItemNameAr;
                    if (User.Identity.IsAuthenticated)
                    {
                        var cartToAdd = itemToAdd.CreateFromClientToServer();
                        cartToAdd.CartId = cart.CartId;
                        var status = cartItemService.AddShoppingCartItem(cartToAdd);
                    }
                }
                Session["ShoppingCartItems"] = cart;
            }
            else
            {
                cart = new ShoppingCart();
                ShoppingCartItem itemToAdd = new ShoppingCartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    SizeId = sizeId != 0 ? sizeId : nullSize,
                    Quantity = quantity,
                    ItemNameEn = userProduct.ItemVariationId != null ? userProduct.ItemNameEn : userProduct.ProductNameEn,
                    ItemNameAr = userProduct.ItemVariationId != null ? userProduct.ItemNameAr : userProduct.ProductNameAr,
                    UnitPrice = !string.IsNullOrEmpty(userProduct.ProductPrice) ? Convert.ToDecimal(userProduct.ProductPrice) : 0,
                    SkuCode = userProduct.SKUCode,
                    ImagePath = userProduct.ItemVariationId != null ? itemImageFolder + userProduct.ItemImage : itemImageFolder + userProduct.ProductImage,
                };
                cart.ShoppingCartItems.Add(itemToAdd);
                itemName = direction == "ltr" ? itemToAdd.ItemNameEn : itemToAdd.ItemNameAr;
                if (User.Identity.IsAuthenticated)
                {
                    var cartToAdd = cart.CreateFromClientToServer();
                    cartToAdd.UserCartId = GetCartId();
                    cartToAdd.Status = (int)PurchaseStatus.New;
                    cartToAdd.RecCreatedBy = User.Identity.GetUserId();
                    cartToAdd.RecCreatedDate = DateTime.Now;
                    cartToAdd.RecLastUpdatedBy = User.Identity.GetUserId();
                    cartToAdd.RecLastUpdatedDate = DateTime.Now;
                    var status = cartService.AddItemToCart(cartToAdd);
                }
                Session["ShoppingCartItems"] = cart;
            }
            return Json(new { itemName = itemName, count = cart.ShoppingCartItems.Count, response = "OK" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete Item from Cart
        [HttpPost]
        public JsonResult DeleteFromCart(long productId)
        {
            var items = Session["ShoppingCartItems"];
            ShoppingCart cart = new ShoppingCart();
            string itemName = "";
            var direction = Resources.Shared.Common.TextDirection;
            if (items != null)
            {
                cart = (ShoppingCart)Session["ShoppingCartItems"];
                var item = cart.ShoppingCartItems.FirstOrDefault(x => x.ProductId == productId);
                if (item != null)
                {
                    itemName = direction == "ltr" ? item.ItemNameEn : item.ItemNameAr;
                    if (User.Identity.IsAuthenticated)
                    {
                        cartItemService.DeleteShoppingCartItem(item.CartItemId);
                        cart.ShoppingCartItems.Remove(item);
                    }
                    else
                    {
                        cart.ShoppingCartItems.Remove(item);
                    }
                    Session["ShoppingCartItems"] = cart;
                    return Json(new { itemName = itemName, count = cart.ShoppingCartItems.Count, response = "OK" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { itemName = "", count = 0, response = "Error" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region UpdateQuantity

        [HttpPost]
        public JsonResult UpdateQuantity(long productId, int quantity)
        {
            var items = Session["ShoppingCartItems"];
            ShoppingCart cart = new ShoppingCart();
            string itemName = "";
            var direction = Resources.Shared.Common.TextDirection;
            if (items != null)
            {
                cart = (ShoppingCart) Session["ShoppingCartItems"];
                var item = cart.ShoppingCartItems.FirstOrDefault(y => y.ProductId == productId);
                if (item != null)
                {
                    itemName = direction == "ltr" ? item.ItemNameEn : item.ItemNameAr;
                    item.Quantity = quantity;
                    // Update DB Cart
                    if (User.Identity.IsAuthenticated)
                    {
                        var dbCartItem = cartItemService.FindById(item.CartItemId);
                        if (dbCartItem != null)
                        {
                            dbCartItem.Quantity = quantity;
                            cartItemService.UpdateShoppingCartItem(dbCartItem);
                        }
                    }
                    Session["ShoppingCartItems"] = cart;
                    return Json(new { itemName = itemName, count = cart.ShoppingCartItems.Count, response = "OK" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { itemName = "", count = 0, response = "Error" }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}