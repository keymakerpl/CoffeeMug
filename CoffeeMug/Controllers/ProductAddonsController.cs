using AutoMapper;
using CoffeeMug.Data.Entities;
using CoffeeMug.Data.Repository;
using CoffeeMug.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMug.Controllers
{
    [Route("api/products")]
    public class ProductAddonsController : Controller
    {
        private readonly ILogger<ProductAddonsController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductAddonsController(ILogger<ProductAddonsController> logger, IProductRepository productRepository, IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("{productId}/addons/{id}", Name = "GetProductAddon")]
        public async Task<IActionResult> GetAddon(Guid productId, Guid addonId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var addon = product.Addons.FirstOrDefault(a => a.Id == addonId);
            if (addon == null)
            {
                return NotFound();
            }

            var addonDTO = _mapper.Map<ProductAddon, ProductAddonDto>(addon);
            return Ok(addonDTO);
        }
    }
}
