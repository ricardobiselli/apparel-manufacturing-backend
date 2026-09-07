using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApparelManufacturingApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[AllowAnonymous]

public class OrderController : ControllerBase
{

    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpGet("GetAll")]
    public async Task<ActionResult<ICollection<OrderDTO>>> GetOrders()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpPost("CreateOrder")]
    [Authorize(Roles = nameof(UserRole.Admin))]

    public async Task<ActionResult<AddOrderDTO>> AddOrder(AddOrderDTO addOrderDTO)
    {
        await _orderService.AddAsync(addOrderDTO);
        return Ok();

    }

    [HttpGet("Get-One/{id}")]
    public async Task<ActionResult<OrderDTO>> GetById([FromRoute] int id)
    {

        var order = await _orderService.GetByIdAsync(id);
        return Ok(order);
    }

    [HttpDelete("Delete/{id}")]
    [Authorize(Roles = nameof(UserRole.Admin))]

    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        await _orderService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("Update")]
    [Authorize(Roles = nameof(UserRole.Admin))]

    public async Task<ActionResult> Update(UpdateOrderDTO dto)
    {
        
        await _orderService.UpdateAsync(dto);

        return NoContent();
    }

}
