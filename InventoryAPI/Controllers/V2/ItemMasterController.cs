using Microsoft.AspNetCore.Mvc;
using Business.ViewModel;
using Business.Contract;
using Asp.Versioning;

namespace InventoryAPI.Controllers.V2;

[ApiController]
[Route("api/v{v:apiVersion}/[Controller]")]
[ApiVersion("2.0")]
public class ItemMasterController : ControllerBase
{
    I_ItemMasterManager _itemMasterManager;
    public ItemMasterController(I_ItemMasterManager itemMasterManager)
    {
        _itemMasterManager = itemMasterManager;
    }

    [HttpPost("GetItemMaster")]
    public async Task<ActionResult> GetItemMaster([FromBody] INVM_ItemMasterViewModel request)
    {

        var result = await _itemMasterManager.GetItemMasterAsync(request.Id);
        throw new Exception("Test exception handling middleware");
        return Ok(result);
    }

}