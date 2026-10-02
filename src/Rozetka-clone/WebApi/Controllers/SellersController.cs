using Application.Sellers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/sellers")]
    public sealed class SellersController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellersController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpPost]
        public async Task<ActionResult<SellerDto>> Create(
            [FromBody] CreateSellerRequest request,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerService.CreateAsync(
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = seller.Id },
                seller);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SellerDto>> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerService.GetByIdAsync(
                id,
                cancellationToken);

            if (seller is null)
                return NotFound();

            return Ok(seller);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<SellerDto>> Update(
            Guid id,
            [FromBody] UpdateSellerRequest request,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerService.UpdateAsync(
                id,
                request,
                cancellationToken);

            if (seller is null)
                return NotFound();

            return Ok(seller);
        }
    }
}
