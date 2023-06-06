using EmployeeClientAppNetworkForPhotographers.API.ApiRequests;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models;
using EmployeeClientAppNetworkForPhotographers.Models.Data;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Categories;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;
using EmployeeClientAppNetworkForPhotographers.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeClientAppNetworkForPhotographers.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CategoriesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<GetCategoryDirDto> categories;

            try
            {
                categories = await ApiCategoryDirs.GetAllWithDirs(AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(categories);
        }



        public ActionResult CreateDir()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateDir(CreateCategoryDirDto categoryDirDto)
        {
            if (!ModelState.IsValid) return View(categoryDirDto);

            CategoryDir categoryDir;
            try
            {
                categoryDir = await ApiCategoryDirs.Create(categoryDirDto, AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return View(categoryDirDto);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View("DetailsDir", categoryDir);
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.CategoryDir = new SelectList(await GetSelectListCategories(), nameof(ViewSelectList.ValueInt), nameof(ViewSelectList.Name));
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryDir = new SelectList(await GetSelectListCategories(), nameof(ViewSelectList.ValueInt), nameof(ViewSelectList.Name));
                return View(categoryDto);
            }

            GetCategoryDto category;
            try
            {
                category = await ApiCategories.Create(categoryDto, AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                ViewBag.CategoryDir = new SelectList(await GetSelectListCategories(), nameof(ViewSelectList.ValueInt), nameof(ViewSelectList.Name));
                return View(categoryDto);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View("Details", category);
        }



        public async Task<ActionResult> UpdateDir(int id)
        {
            CategoryDir categoryDir;

            try
            {
                categoryDir = await ApiCategoryDirs.GetById(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View(categoryDir);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDir(CategoryDir categoryDir)
        {
            if (!ModelState.IsValid) return View(categoryDir);

            try
            {
                categoryDir = await ApiCategoryDirs.Update(categoryDir, AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                return View(categoryDir);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View("DetailsDir", categoryDir);
        }

        public async Task<ActionResult> Update(int id)
        {
            GetCategoryDto category;

            try
            {
                category = await ApiCategories.GetById(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            ViewBag.CategoryDir = new SelectList(await GetSelectListCategories(), nameof(ViewSelectList.ValueInt), nameof(ViewSelectList.Name));
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> Update(GetCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryDir = new SelectList(await GetSelectListCategories(), nameof(ViewSelectList.ValueInt), nameof(ViewSelectList.Name));
                return View(categoryDto);
            }

            try
            {
                categoryDto = await ApiCategories.Update(new Category(categoryDto), AppUser.GetToken(HttpContext));
            }
            catch (FieldException ex)
            {
                ModelState.AddModelError(ex.Field, ex.Message);
                ViewBag.CategoryDir = new SelectList(await GetSelectListCategories(), nameof(ViewSelectList.ValueInt), nameof(ViewSelectList.Name));
                return View(categoryDto);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return View("Details", categoryDto);
        }



        public async Task<ActionResult> DeleteDir(int id)
        {
            CategoryDir categoryDir;
            bool checkCategories;

            var token = AppUser.GetToken(HttpContext);

            try
            {
                categoryDir = await ApiCategoryDirs.GetById(id, token);
                checkCategories = await ApiCategoryDirs.CheckCategories(id, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            ViewBag.CheckCategories = checkCategories;
            return View(categoryDir);
        }

        [HttpPost, ActionName("DeleteDir")]
        public async Task<ActionResult> DeleteDirPost(int id)
        {
            try
            {
                await ApiCategoryDirs.Delete(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            GetCategoryDto category;
            bool checkContents;

            var token = AppUser.GetToken(HttpContext);

            try
            {
                category = await ApiCategories.GetById(id, token);
                checkContents = await ApiCategories.CheckContents(id, token);
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            ViewBag.CheckContents = checkContents;
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                await ApiCategories.Delete(id, AppUser.GetToken(HttpContext));
            }
            catch (ApiException ex)
            {
                return RedirectToAction(nameof(CommonController.ApiError), "Common", ex.ToObj());
            }

            return RedirectToAction("Index");
        }



        private async Task<List<ViewSelectList>> GetSelectListCategories()
        {
            List<CategoryDir> categories = await ApiCategoryDirs.GetAll(AppUser.GetToken(HttpContext));

            var categoriesDir = new List<ViewSelectList>();
            foreach(var category in categories)
            {
                categoriesDir.Add(new ViewSelectList(category.Id, category.Name));
            }

            return categoriesDir;
        }
    }
}
