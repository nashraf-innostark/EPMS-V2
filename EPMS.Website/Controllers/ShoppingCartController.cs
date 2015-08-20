﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Models.RequestModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Website.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index(long? id, string from)
        {
            string cartId = GetCartId();
            if (id != null)
            {
                ShoppingCartSearchRequest request = new ShoppingCartSearchRequest
                {
                    ProductId = (long)id,
                    From = from
                };

            }
            return View();
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
                Session["ShoppingCartId"] = tempCartId;
            }
            return cartId;
        }
    }
}