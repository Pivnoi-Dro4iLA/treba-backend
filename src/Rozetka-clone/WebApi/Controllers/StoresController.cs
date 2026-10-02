using Application.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/sellers/{sellerId:guid}/stores")]
    public sealed class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<StoreDto>>> GetAll(
            Guid sellerId,
            CancellationToken cancellationToken)
        {
            var stores = await _storeService.GetBySellerIdAsync(
                sellerId,
                cancellationToken);

            return Ok(stores);
        }

        [HttpGet("{storeId:guid}")]
        public async Task<ActionResult<StoreDto>> GetById(
            Guid sellerId,
            Guid storeId,
            CancellationToken cancellationToken)
        {
            var store = await _storeService.GetByIdAsync(
                sellerId,
                storeId,
                cancellationToken);

            if (store is null)
                return NotFound();

            return Ok(store);
        }

        [HttpPost]
        public async Task<ActionResult<StoreDto>> Create(
            Guid sellerId,
            [FromBody] CreateStoreRequest request,
            CancellationToken cancellationToken)
        {
            var store = await _storeService.CreateAsync(
                sellerId,
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    sellerId,
                    storeId = store.Id
                },
                store);
        }

        [HttpPatch("{storeId:guid}")]
        public async Task<ActionResult<StoreDto>> Update(
            Guid sellerId,
            Guid storeId,
            [FromBody] UpdateStoreRequest request,
            CancellationToken cancellationToken)
        {
            var store = await _storeService.UpdateAsync(
                sellerId,
                storeId,
                request,
                cancellationToken);

            if (store is null)
                return NotFound();

            return Ok(store);
        }
    }
}
