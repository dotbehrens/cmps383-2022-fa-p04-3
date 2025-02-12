﻿using FA22.P04.Web.Data;
using FA22.P04.Web.Features.Items;
using FA22.P04.Web.Features.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA22.P04.Web.Controllers;

[Route("api/items")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly DbSet<Item> items;
    private readonly DataContext dataContext;
    private readonly DbSet<Product> products;

    public ItemsController(DataContext dataContext)
    {
        this.dataContext = dataContext;
        items = dataContext.Set<Item>();
        products = dataContext.Set<Product>();
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<ItemDto> GetItemById(int id)
    {
        var result = GetItemDtos(items.Where(x => x.Id == id)).FirstOrDefault();
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<ItemDto> CreateItem(ItemDto dto)
    {
        if (IsInvalid(dto))
        {
            return BadRequest();
        }

        var product = products.FirstOrDefault(x => x.Id == dto.ProductId);
        if (product == null)
        {
            return BadRequest();
        }

        var item = new Item
        {
            Condition = dto.Condition,
            ProductId = dto.ProductId.Value,
        };
        items.Add(item);

        dataContext.SaveChanges();

        dto.Id = item.Id;
        dto.ProductName = product.Name;

        return CreatedAtAction(nameof(GetItemById), new { id = dto.Id }, dto);
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult<ItemDto> UpdateItem(int id, ItemDto dto)
    {
        if (IsInvalid(dto))
        {
            return BadRequest();
        }

        var product = products.FirstOrDefault(x => x.Id == dto.ProductId);
        if (product == null)
        {
            return BadRequest();
        }

        var item = items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        item.Condition = dto.Condition;
        item.ProductId = dto.ProductId.Value;

        dataContext.SaveChanges();

        dto.Id = item.Id;
        dto.ProductName = product.Name;

        return Ok(dto);
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeleteItem(int id)
    {
        var item = items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        items.Remove(item);

        dataContext.SaveChanges();

        return Ok();
    }

    private static bool IsInvalid(ItemDto dto)
    {
        return string.IsNullOrWhiteSpace(dto.Condition) || dto.ProductId == null;
    }

    public static IQueryable<ItemDto> GetItemDtos(IQueryable<Item> items)
    {
        return items
            .Select(x => new ItemDto
            {
                Id = x.Id,
                Condition = x.Condition,
                ProductId = x.ProductId,
                ProductName = x.Product.Name
            });
    }
}
