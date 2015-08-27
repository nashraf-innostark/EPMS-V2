using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.Product;
using EPMS.WebModels.ModelMappers.Website.ShoppingCart;
using EPMS.WebModels.ViewModels.WebsiteClient;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.Website.Controllers
{
    public class ShoppingCartController : Controller
    {
        #region Private
        private readonly IShoppingCartService cartService;
        private readonly IProductService productService;

        #endregion

        #region Constructor

        public ShoppingCartController(IShoppingCartService cartService, IProductService productService)
        {
            this.cartService = cartService;
            this.productService = productService;
        }

        #endregion

        #region Public
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ShoppingCartListViewModel viewModel = new ShoppingCartListViewModel();
            bool isItemsExist = false;
            string cartId = GetCartId();
            var items = Session["ShoppingCartItems"];
            if (items != null)
            {
                IList<ShoppingCart> cartItems = (List<ShoppingCart>)Session["ShoppingCartItems"];
                if (cartItems.Any())
                {
                    isItemsExist = true;
                    viewModel.ShoppingCarts = cartItems;
                }
            }
            if (!isItemsExist)
            {
                viewModel.ShoppingCarts = !string.IsNullOrEmpty(cartId)
                    ? cartService.GetUserCart(cartId).ShoppingCarts.Select(x => x.CreateFromServerToClient()).ToList()
                    : new List<ShoppingCart>();
            }
            ViewBag.ShowSlider = false;
            return View(viewModel);
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
                Guid tempCartId = Guid.NewGuid();
                cartId = tempCartId.ToString();
                Session["ShoppingCartId"] = tempCartId;
            }
            return cartId;
        }
        #endregion

        #region Add Item To Cart
        [HttpPost]
        public JsonResult AddToCart(long productId, long sizeId, int quantity)
        {
            // get Product data from DB
            var product = productService.FindProductById(productId);
            Product userProduct = product.ItemVariationId != null ? product.CreateFromServerToClientFromInventory() : product.CreateFromServerToClient();
            // check if sizr is zero then add default size of Product
            if (sizeId == 0)
            {
                sizeId = userProduct.ItemVariationId != null ? userProduct.SizeId : Convert.ToInt64(userProduct.ProductSize);
            }
            // Product Image path
            string itemImageFolder = "";
            itemImageFolder = userProduct.ItemVariationId != null ? ConfigurationManager.AppSettings["InventoryImage"] : ConfigurationManager.AppSettings["ProductImage"];
            // Get User Cart Id
            string userCartid = GetCartId();
            // Get user Cart from Session
            var items = Session["ShoppingCartItems"];
            if (items != null)
            {
                IList<ShoppingCart> cart = (List<ShoppingCart>)Session["ShoppingCartItems"];
                var item = cart.FirstOrDefault(x => x.ProductId == productId && x.SizeId == sizeId);
                if (item != null)
                {
                    item.Quantity += quantity;
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
                    ShoppingCart itemToAdd = new ShoppingCart
                    {
                        UserCartId = userCartid,
                        ProductId = productId,
                        SizeId = sizeId,
                        Quantity = quantity,
                        ItemNameEn = userProduct.ItemVariationId != null ? userProduct.ItemNameEn : userProduct.ProductNameEn,
                        ItemNameAr = userProduct.ItemVariationId != null ? userProduct.ItemNameAr : userProduct.ProductNameAr,
                        UnitPrice = Convert.ToDouble(userProduct.ProductPrice),
                        SkuCode = userProduct.SKUCode,
                        ImagePath = imagePath,
                        RecCreatedDate = DateTime.Now,
                    };
                    cart.Add(itemToAdd);
                }

                Session["ShoppingCartItems"] = cart;
            }
            else
            {
                IList<ShoppingCart> cart = new List<ShoppingCart>();
                ShoppingCart itemToAdd = new ShoppingCart
                {
                    UserCartId = userCartid,
                    ProductId = productId,
                    SizeId = sizeId,
                    Quantity = quantity,
                    ItemNameEn = userProduct.ItemVariationId != null ? userProduct.ItemNameEn : userProduct.ProductNameEn,
                    ItemNameAr = userProduct.ItemVariationId != null ? userProduct.ItemNameAr : userProduct.ProductNameAr,
                    UnitPrice = Convert.ToDouble(userProduct.ProductPrice),
                    SkuCode = userProduct.SKUCode,
                    ImagePath = userProduct.ItemVariationId != null ? itemImageFolder + userProduct.ItemImage : itemImageFolder + userProduct.ProductImage,
                    RecCreatedDate = DateTime.Now
                };
                cart.Add(itemToAdd);
                Session["ShoppingCartItems"] = cart;
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete Item from Cart
        [HttpPost]
        public JsonResult DeleteFromCart(long productId)
        {
            var items = Session["ShoppingCartItems"];
            if (items != null)
            {
                IList<ShoppingCart> cart = (List<ShoppingCart>)Session["ShoppingCartItems"];
                var item = cart.FirstOrDefault(x => x.ProductId == productId);
                if (item != null)
                {
                    cart.Remove(item);
                    Session["ShoppingCartItems"] = cart;
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}