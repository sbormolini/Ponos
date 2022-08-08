using Microsoft.AspNetCore.Mvc;
using Bos.Todo.Api.Models;

namespace Bos.Todo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    //private readonly ILogger _logger;
    private readonly ICosmosDbService _cosmosDbService;

    public ItemController(ICosmosDbService cosmosDbService) => _cosmosDbService = cosmosDbService;


    [HttpGet]
    public async Task<ActionResult<IList<Item>>> GetAll()
    {
        try
        {
            var items = await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
            if (items is null)
                return NotFound("No results found");

            return Ok(items);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.Message);
            return StatusCode(500, $"Some error occured while retrieving data: {ex}");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Item>> GetById([FromRoute] Guid id)
    {
        try
        {
            var platform = await _cosmosDbService.GetItemAsync(id.ToString());
            if (platform is null)
                return NotFound("No results found");

            return Ok(platform);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.Message);
            return StatusCode(500, $"Some error occured while retrieving data: {ex}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Item item)
    {
        try
        {
            await _cosmosDbService.AddItemAsync(item);
            return Ok();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.Message);
            return StatusCode(500, $"Some error occured while retrieving data: {ex}");
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] Item updatedItem)
    {
        try
        {
            await _cosmosDbService.UpdateItemAsync(id.ToString(), updatedItem);
            return Ok();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.Message);
            return StatusCode(500, $"Some error occured while retrieving data: {ex}");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _cosmosDbService.DeleteItemAsync(id.ToString());
            return Ok();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.Message);
            return StatusCode(500, $"Some error occured while retrieving data: {ex}");
        }
    }
}