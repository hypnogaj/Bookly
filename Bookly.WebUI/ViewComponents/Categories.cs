using Bookly.Service.Abstract;
using Bookly.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.WebUI.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly IService<Category> _service;

        public Categories(IService<Category> service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _service.GetAllAsync(c => c.IsTopMenu && c.IsActive));
        }
    }
}
