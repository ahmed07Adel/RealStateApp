using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Model;
using WebApplication2.viewmodels;


namespace WebApplication2.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly UserManager<AppUsers> usermanager;

        public AdministrationController(RoleManager<IdentityRole> rolemanager, UserManager<AppUsers> usermanager )
        {
            this.rolemanager = rolemanager;
            this.usermanager = usermanager;

        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await usermanager.FindByIdAsync(id);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"this userID={id} can not be found";
                return View("NotFound");
            }

            var UserClaims = await usermanager.GetClaimsAsync(user);
            var UserRoles = await usermanager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = UserClaims.Select(e => e.Value).ToList()

                
            };
            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await usermanager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"this userID={model.Id} can not be found";
                return View("NotFound");
            }

            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                var result = await usermanager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach(var Error in result.Errors)
                {
                    ModelState.AddModelError("", Error.Description);
                }
                return View(model);
            }



        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(EditUserViewModel model)
        {
            var user = await usermanager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"this userID={model.Id} can not be found";
                return View("NotFound");
            }

            else
            {
                var result = await usermanager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }

            }


        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await rolemanager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With userID= {id} can not be found";
                return View("NotFound");
            }

            else
            {

                try
                {
                    var result = await rolemanager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);
                    }
                    return View("ListRoles");
                }
                catch (Exception)
                {
                    ViewBag.ErrorTitle = $"{role.Name} is in use";
                    ViewBag.ErrorMessage = $"{role.Name} can not be deleted there a user use it, if you want to delete this role please remove thr user in this role first";
                    return View("Error");
                }
            }



        }




        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await rolemanager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");

                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = rolemanager.Roles;
            return View(roles);
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await rolemanager.FindByIdAsync(id);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id={id} can not found";
                return View("NotFound");
            }
          var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name    
            };
            foreach (var user in usermanager.Users)
            {
                if (await usermanager.IsInRoleAsync(user, role.Name))
                {
                        model.Users.Add(user.UserName);

                }

            }
            return View(model);

        }
      
        

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await rolemanager.FindByIdAsync(model.Id);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id = {model.Id} is not found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var Result = await rolemanager.UpdateAsync(role);
                if (Result.Succeeded)
                {
                    return RedirectToAction("ListRoles");

                }
                foreach(var error in Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);    
                }


                return View(model);

            }


            

        }
        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await rolemanager.FindByIdAsync(roleId);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id= {roleId} Can not be Found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach(var user in usermanager.Users)
            {
                var userRoleviewmodel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if(await usermanager.IsInRoleAsync(user , role.Name))
                {
                    userRoleviewmodel.IsSelected = true;
                }
                else
                {
                    userRoleviewmodel.IsSelected = false;

                }
                model.Add(userRoleviewmodel);
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await rolemanager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With ID= {roleId} Can Not BE Found";
                return View("NotFound");
            }
           
            for (int i = 0; i < model.Count; i++)
            {
                var user = await usermanager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await usermanager.IsInRoleAsync(user, role.Name)))
                {
                    result = await usermanager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await usermanager.IsInRoleAsync(user, role.Name)))
                {
                    result = await usermanager.RemoveFromRoleAsync(user, role.Name);

                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }

            }
            return RedirectToAction("EditRole", new { Id = roleId });

        }

        public IActionResult ListUsers()
        {
            var users = usermanager.Users;
            return View(users);
        }





    }
}
