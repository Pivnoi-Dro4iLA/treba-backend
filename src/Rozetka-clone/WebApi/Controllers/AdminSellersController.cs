using Application.Sellers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/admin/sellers")]
    public sealed class AdminSellersController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public AdminSellersController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SellerDto>>> GetAll(
            CancellationToken cancellationToken)
        {
            var sellers = await _sellerService.GetAllAsync(
                cancellationToken);

            return Ok(sellers);
        }

        [HttpPost("{id:guid}/approve")]
        public async Task<ActionResult<SellerDto>> Approve(
            Guid id,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerService.ApproveAsync(
                id,
                cancellationToken);

            if (seller is null)
                return NotFound();

            return Ok(seller);
        }

        [HttpPost("{id:guid}/suspend")]
        public async Task<ActionResult<SellerDto>> Suspend(
            Guid id,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerService.SuspendAsync(
                id,
                cancellationToken);

            if (seller is null)
                return NotFound();

            return Ok(seller);
        }
    }
}
