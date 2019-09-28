using AutoMapper;
using CoffeeMug.Data;
using CoffeeMug.Data.Entities;
using CoffeeMug.Data.Repository;
using CoffeeMug.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMug.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository, IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                var result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption has been thrown durring product Get() request. \n{ex.Message}");
                return StatusCode(500, "Something goes wrong... :(");
            }
        }

        [HttpGet("{id}", Name = nameof(Get))]
        public IActionResult Get(Guid id)
        {
            try
            {
                var product = _productRepository.FindByInclude(p => p.Id == id, p => p.Addons).FirstOrDefault();
                if (product == null)
                {
                    _logger.LogInformation($"Product with id: {id} not found");
                    return NotFound();
                }

                var result = _mapper.Map<Product, ProductDto>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption has been thrown durring product Get({id}) request. \n{ex.Message}");
                return StatusCode(500, "Something goes wrong... :(");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] ProductForCreationDto product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newProduct = _mapper.Map<ProductForCreationDto, Product>(product);
            _productRepository.Add(newProduct);

            if (!await _productRepository.SaveAsync())
            {
                return StatusCode(500, "Something goes wrong... in Save method :(");
            }

            var productToReturn = _mapper.Map<ProductDto>(newProduct);

            return CreatedAtRoute(nameof(ProductController.Get), new { id = productToReturn.Id }, productToReturn);
        }

        /// <summary>
        /// Update all fields
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productForUpdate"></param>
        /// <returns></returns>
        [HttpPut("{productId}")]
        public async Task<IActionResult> Put(Guid productId, [FromBody] ProductForUpdateDto productForUpdate)
        {
            //TODO: FluentValidation
            if (productForUpdate == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = _productRepository.FindByInclude(p => p.Id == productId, p => p.Addons).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(productForUpdate, product);

            if (!await _productRepository.SaveAsync())
            {
                return StatusCode(500, "Something goes wrong... in Save method :(");
            }

            return NoContent();
        }

        /// <summary>
        /// Update selected fields
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{productId}")]
        public async Task<IActionResult> Patch(Guid productId, [FromBody] JsonPatchDocument<ProductForUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var productFromStore = _productRepository.FindByInclude(p => p.Id == productId, p => p.Addons).FirstOrDefault();
            if (productFromStore == null)
            {
                return NotFound();
            }

            var productToPatch = _mapper.Map<ProductForUpdateDto>(productFromStore);

            patchDocument.ApplyTo(productToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(productToPatch);

            _mapper.Map(productToPatch, productFromStore);

            if (!await _productRepository.SaveAsync())
            {
                return StatusCode(500, "Something goes wrong... in Save method :(");
            }

            return NoContent();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(Guid productId)
        {
            var product = _productRepository.FindByInclude(p => p.Id == productId, p => p.Addons).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Remove(product);

            if (!await _productRepository.SaveAsync())
            {
                return StatusCode(500, "Something goes wrong... in Save method :(");
            }

            return NoContent();
        }
    }
}
