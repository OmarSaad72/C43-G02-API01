using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Authorize] // If Login
      //baseUrl/api/Basket
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        [HttpGet("{id}")]  //Get: baseUrl/api/Basket/id
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var basket = await _serviceManager.BasketService.getBasketAsync(id);
            return Ok(basket);
        }
        [HttpPost]  //Post: baseUrl/api/Basket/id
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await _serviceManager.BasketService.updateBasketAsync(basketDto);
            return Ok(basket);
        }
        [HttpDelete("{id}")]  // Delete: baseUrl/api/Basket/id
        public async Task<ActionResult> Delete(string id)
        {
            await _serviceManager.BasketService.deleteBasketAsync(id);
            return NoContent();  // StatusCode ==> 204
        }
    }
}
