using EPMS.Implementation.Identity;
using EPMS.Models.DomainModels;
using EPMS.Models.IdentityModels.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using EPMS.Models.MenuModels;
using EPMS.Interfaces.IServices;
using EPMS.Web.ViewModels.RightsManagement;
using EPMS.Web.ViewModels.Common;
using EPMS.WebBase.Mvc;

namespace IdentitySample.Controllers
{
    [Authorize]
    public class RolesAdminController : Controller
    {
        private IMenuRightsService menuRightsService;
        private IDashboardWidgetPreferencesService PreferencesService;
        public RolesAdminController()
        {
        }

        //public RolesAdminController(ApplicationUserManager userManager,
        //    ApplicationRoleManager roleManager)
        //{
        //    UserManager = userManager;
        //    RoleManager = roleManager;
        //}

        public RolesAdminController(IMenuRightsService menuRightsService, IDashboardWidgetPreferencesService preferencesService)
        {
            this.menuRightsService = menuRightsService;
            this.PreferencesService = preferencesService;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Roles/
        public ActionResult Index()
        {
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Roles = RoleManager.Roles.ToList();
            return View(roleViewModel);
        }

        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            // Get the list of Users in this Role
            var users = new List<AspNetUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(role);
        }

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new AspNetRole();
                role.Name = roleViewModel.Name;
                int rolesCount = RoleManager.Roles.Count() + 1;
                role.Id = rolesCount.ToString();
                if (!RoleManager.RoleExists(role.Name))
                {
                    var roleresult = await RoleManager.CreateAsync(role);
                    if (!roleresult.Succeeded)
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Error in creating role",
                            IsError = true
                        };
                        return View();
                    }
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Role has been created successfully",
                        IsSaved = true
                    };
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
            return View(roleModel);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id")] RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                IdentityResult result;
                if (deleteUser != null)
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                else
                {
                    result = await RoleManager.DeleteAsync(role);
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        [SiteAuthorize(PermissionKey = "RightsManagement")]
        public ActionResult RightsManagement()
        {
            UserMenuResponse userMenuRights = menuRightsService.GetRoleMenuRights(string.Empty);
            RightsManagementViewModel viewModel = new RightsManagementViewModel();

            viewModel.Roles = userMenuRights.Roles.ToList();
            viewModel.Rights =
                userMenuRights.Menus.Select(
                    m =>
                        new Rights
                        {
                            MenuId = m.MenuId,
                            MenuTitle = m.MenuTitle,
                            IsParent = m.IsRootItem,
                            IsSelected = userMenuRights.MenuRights.Any(menu => menu.Menu.MenuId == m.MenuId),
                            ParentId = m.ParentItem != null ? m.ParentItem.MenuId : (int?)null
                        }).ToList();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        public ActionResult PostRightsManagement(string roleValue, string selectedList)
        {

            UserMenuResponse userMenuRights = menuRightsService.SaveRoleMenuRight(roleValue, selectedList, RoleManager.FindById(roleValue));
            //string[] roles = selectedList.Split(',');
            //IList<string> userWidgets = new List<string>();
            //string widget = "";
            //for (int i = 0; i < 12; i++)
            //{
            //    if (i >= 0 && i < 9)
            //    {
            //        widget = "310" + ( i + 1 );
            //        if (roles.Contains(widget))
            //        {
            //            // Entry in WidgetPreference Service
            //            userWidgets.Add(widget);
            //        }
            //    }
            //    else
            //    {
            //        widget = "31" + (i + 1);
            //        if (roles.Contains(widget))
            //        {
            //            // Entry in WidgetPreference Service
            //            userWidgets.Add(widget);
            //        }
            //    }
            //}
            RightsManagementViewModel viewModel = new RightsManagementViewModel();

            viewModel.Roles = userMenuRights.Roles.ToList();
            viewModel.Rights =
                userMenuRights.Menus.Select(
                    m =>
                        new Rights
                        {
                            MenuId = m.MenuId,
                            MenuTitle = m.MenuTitle,
                            IsParent = m.IsRootItem,
                            IsSelected = userMenuRights.MenuRights.Any(menu => menu.Menu.MenuId == m.MenuId),
                            ParentId = m.ParentItem != null ? m.ParentItem.MenuId : (int?)null
                        }).ToList();
            viewModel.SelectedRoleId = roleValue;
            // User Widgets Preferences
            //IList<string> widgetName = new List<string>();
            //foreach (var userWidget in userWidgets)
            //{
            //    int menuId = System.Convert.ToInt32(userWidget);
            //    var menu = viewModel.Rights.FirstOrDefault(x => x.MenuId == menuId);
            //    string widgetToAdd = menu.MenuTitle.Replace(" ", "");
            //    widgetToAdd = widgetToAdd + "Widget";
            //    widgetName.Add(widgetToAdd);
            //}
            //string userId = User.Identity.GetUserId();
            //var userpreferences = PreferencesService.LoadAllPreferencesByUserId(userId).ToList();
            //foreach (var userpreference in userpreferences)
            //{
            //    PreferencesService.Deletepreferences(userpreference);
            //}
            //int sortNo = 1;
            //foreach (var pref in widgetName)
            //{
            //    DashboardWidgetPreference preference = new DashboardWidgetPreference { UserId = userId, WidgetId = pref, SortNumber = sortNo };
            //    //var preferenceToUpdate = preference.CreateFromClientToServerWidgetPreferences();
            //    if (PreferencesService.AddPreferences(preference))
            //    {
            //        sortNo++;
            //    }
            //}
            TempData["message"] = new MessageViewModel
            {
                Message = "Record has been updated.",
                IsUpdated = true
           };
            return RedirectToAction("RightsManagement");
        }
        [HttpPost]
        public ActionResult RightsManagement(FormCollection collection)
        {
            string RoleId = collection.Get("SelectedRoleId");
            UserMenuResponse userMenuRights = menuRightsService.GetRoleMenuRights(RoleId);
            RightsManagementViewModel viewModel = new RightsManagementViewModel();

            viewModel.Roles = userMenuRights.Roles.ToList();
            viewModel.Rights =
                userMenuRights.Menus.Select(
                    m =>
                        new Rights
                        {
                            MenuId = m.MenuId,
                            MenuTitle = m.MenuTitle,
                            IsParent = m.IsRootItem,
                            IsSelected = userMenuRights.MenuRights.Any(menu => menu.Menu.MenuId == m.MenuId),
                            ParentId = m.ParentItem != null ? m.ParentItem.MenuId : (int?)null,

                        }).ToList();
            viewModel.SelectedRoleId = RoleId;
            return View(viewModel);
        }
    }
}
