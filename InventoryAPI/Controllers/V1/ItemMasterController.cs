using Microsoft.AspNetCore.Mvc;
using Business.ViewModel;
using Business.Contract;
using Asp.Versioning;

namespace InventoryAPI.Controllers.V1;

// [Route("api/[controller]/[action]")] method level routing
// [Route("api/[controller]")] // controller level routing

//auto-validates the request body against the model's data annotations before executing the action method.
// If the model is invalid, it automatically returns a 400 Bad Request response with validation errors.
// If we comment it, we have to manually check ModelState.IsValid in each action method.
[ApiController]
[Route("api/v{v:apiVersion}/[Controller]")]
[ApiVersion("1.0")]
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
        //throw new Exception("Test exception handling middleware");
        return Ok(result);
        // return Ok(Task.FromResult(new INVM_ItemMasterViewModel
        // {
        //     Id = 1,
        //     CreatedDate = DateTime.Now,
        //     ModifiedDate = DateTime.Now,
        //     CreatedBy = "Admin",
        //     ModifiedBy = "Admin",
        //     Active = true,
        //     ItemCode = "ITM001",
        //     ItemName = "Sample Item",
        //     DrugFormId = 2,
        //     Strength = "500mg",
        //     PackSize = 30
        // })); 
    }

}