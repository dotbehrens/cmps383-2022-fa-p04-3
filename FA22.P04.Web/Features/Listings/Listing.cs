﻿using FA22.P04.Web.Features.ItemListings;
using FA22.P04.Web.Features.Users;
namespace FA22.P04.Web.Features.Listings;

public class Listing
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public DateTimeOffset StartUtc { get; set; }

    public DateTimeOffset EndUtc { get; set; }

    public virtual ICollection<ItemListing> ItemsForSale { get; set; } = new List<ItemListing>();
    //Owner: User
    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
    public User Owner { get; set; }
}