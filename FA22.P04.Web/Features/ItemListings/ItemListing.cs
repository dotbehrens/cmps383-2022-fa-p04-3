﻿using FA22.P04.Web.Features.Items;
using FA22.P04.Web.Features.Listings;

namespace FA22.P04.Web.Features.ItemListings;

public class ItemListing
{
    public int Id { get; set; }
    public int ListingId { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public virtual Listing Listing { get; set; }
}
