using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TooGoodToGoWatcherCore
{
    public partial class FavoriteResponse
    {
        [JsonPropertyNameAttribute("items")]
        public List<ItemElement> Items { get; set; }
    }

    public partial class ItemElement
    {
        [JsonPropertyNameAttribute("item")]
        public ItemItem Item { get; set; }

        [JsonPropertyNameAttribute("store")]
        public Store Store { get; set; }

        [JsonPropertyNameAttribute("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyNameAttribute("pickup_interval")] // nullable
        public PickupInterval? PickupInterval { get; set; }

        [JsonPropertyNameAttribute("pickup_location")]
        public Location PickupLocation { get; set; }

        [JsonPropertyNameAttribute("purchase_end")]//NullValueHandling = NullValueHandling.Ignore
        public DateTimeOffset? PurchaseEnd { get; set; }

        [JsonPropertyNameAttribute("items_available")]
        public long ItemsAvailable { get; set; }

        [JsonPropertyNameAttribute("sold_out_at")]//NullValueHandling = NullValueHandling.Ignore
        public DateTimeOffset? SoldOutAt { get; set; }

        [JsonPropertyNameAttribute("distance")]
        public double Distance { get; set; }

        [JsonPropertyNameAttribute("favorite")]
        public bool Favorite { get; set; }

        [JsonPropertyNameAttribute("in_sales_window")]
        public bool InSalesWindow { get; set; }

        [JsonPropertyNameAttribute("new_item")]
        public bool NewItem { get; set; }
    }

    public partial class ItemItem
    {
        [JsonPropertyNameAttribute("item_id")]
        public long ItemId { get; set; }

        [JsonPropertyNameAttribute("price")]
        public Price Price { get; set; }

        [JsonPropertyNameAttribute("sales_taxes")]
        public object[] SalesTaxes { get; set; }

        [JsonPropertyNameAttribute("tax_amount")]
        public Price TaxAmount { get; set; }

        [JsonPropertyNameAttribute("price_excluding_taxes")]
        public Price PriceExcludingTaxes { get; set; }

        [JsonPropertyNameAttribute("price_including_taxes")]
        public Price PriceIncludingTaxes { get; set; }

        [JsonPropertyNameAttribute("value_excluding_taxes")]
        public Price ValueExcludingTaxes { get; set; }

        [JsonPropertyNameAttribute("value_including_taxes")]
        public Price ValueIncludingTaxes { get; set; }

        [JsonPropertyNameAttribute("taxation_policy")]
        public string TaxationPolicy { get; set; }

        [JsonPropertyNameAttribute("show_sales_taxes")]
        public bool ShowSalesTaxes { get; set; }

        [JsonPropertyNameAttribute("cover_picture")]
        public Picture CoverPicture { get; set; }

        [JsonPropertyNameAttribute("logo_picture")]
        public Picture LogoPicture { get; set; }

        [JsonPropertyNameAttribute("name")]
        public string Name { get; set; }

        [JsonPropertyNameAttribute("description")]
        public string Description { get; set; }

        [JsonPropertyNameAttribute("can_user_supply_packaging")]
        public bool CanUserSupplyPackaging { get; set; }

        [JsonPropertyNameAttribute("packaging_option")]
        public string PackagingOption { get; set; }

        [JsonPropertyNameAttribute("collection_info")]
        public string CollectionInfo { get; set; }

        [JsonPropertyNameAttribute("diet_categories")]
        public string[] DietCategories { get; set; }

        [JsonPropertyNameAttribute("item_category")]
        public string ItemCategory { get; set; }

        [JsonPropertyNameAttribute("badges")]
        public Badge[] Badges { get; set; }

        [JsonPropertyNameAttribute("positive_rating_reasons")]
        public string[] PositiveRatingReasons { get; set; }

        [JsonPropertyNameAttribute("average_overall_rating")] // NullValueHandling = NullValueHandling.Ignore
        public AverageOverallRating AverageOverallRating { get; set; }

        [JsonPropertyNameAttribute("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonPropertyNameAttribute("buffet")]
        public bool Buffet { get; set; }

        [JsonPropertyNameAttribute("food_handling_instructions")] // NullValueHandling = NullValueHandling.Ignore
        public string FoodHandlingInstructions { get; set; }
    }

    public partial class AverageOverallRating
    {
        [JsonPropertyNameAttribute("average_overall_rating")]
        public double AverageOverallRatingAverageOverallRating { get; set; }

        [JsonPropertyNameAttribute("rating_count")]
        public long RatingCount { get; set; }

        [JsonPropertyNameAttribute("month_count")]
        public long MonthCount { get; set; }
    }

    public partial class Badge
    {
        [JsonPropertyNameAttribute("badge_type")]
        public string BadgeType { get; set; }

        [JsonPropertyNameAttribute("rating_group")]
        public string RatingGroup { get; set; }

        [JsonPropertyNameAttribute("percentage")]
        public long Percentage { get; set; }

        [JsonPropertyNameAttribute("user_count")]
        public long UserCount { get; set; }

        [JsonPropertyNameAttribute("month_count")]
        public long MonthCount { get; set; }
    }

    public partial class Picture
    {
        [JsonPropertyNameAttribute("picture_id")]
        public long PictureId { get; set; }

        [JsonPropertyNameAttribute("current_url")]
        public Uri CurrentUrl { get; set; }

        [JsonPropertyNameAttribute("is_automatically_created")]
        public bool IsAutomaticallyCreated { get; set; }
    }

    public partial class Price
    {
        [JsonPropertyNameAttribute("code")]
        public string Code { get; set; }

        [JsonPropertyNameAttribute("minor_units")]
        public long MinorUnits { get; set; }

        [JsonPropertyNameAttribute("decimals")]
        public long Decimals { get; set; }
    }

    public partial class PickupInterval
    {
        [JsonPropertyNameAttribute("start")]
        public DateTimeOffset Start { get; set; }

        [JsonPropertyNameAttribute("end")]
        public DateTimeOffset End { get; set; }
    }

    public partial class Location
    {
        [JsonPropertyNameAttribute("address")]
        public Address Address { get; set; }

        [JsonPropertyNameAttribute("location")]
        public LocationClass LocationLocation { get; set; }
    }

    public partial class Address
    {
        [JsonPropertyNameAttribute("country")]
        public Country Country { get; set; }

        [JsonPropertyNameAttribute("address_line")]
        public string AddressLine { get; set; }

        [JsonPropertyNameAttribute("city")]
        public string City { get; set; }

        [JsonPropertyNameAttribute("postal_code")]
        public string PostalCode { get; set; }
    }

    public partial class Country
    {
        [JsonPropertyNameAttribute("iso_code")]
        public string IsoCode { get; set; }

        [JsonPropertyNameAttribute("name")]
        public string Name { get; set; }
    }

    public partial class LocationClass
    {
        [JsonPropertyNameAttribute("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyNameAttribute("latitude")]
        public double Latitude { get; set; }
    }

    public partial class Store
    {
        [JsonPropertyNameAttribute("store_id")]
        public string StoreId { get; set; }

        [JsonPropertyNameAttribute("store_name")]
        public string StoreName { get; set; }

        [JsonPropertyNameAttribute("branch")]
        public string Branch { get; set; }

        [JsonPropertyNameAttribute("description")]
        public string Description { get; set; }

        [JsonPropertyNameAttribute("website")]
        public string Website { get; set; }

        [JsonPropertyNameAttribute("store_location")]
        public Location StoreLocation { get; set; }

        [JsonPropertyNameAttribute("logo_picture")]
        public Picture LogoPicture { get; set; }

        [JsonPropertyNameAttribute("store_time_zone")]
        public string StoreTimeZone { get; set; }

        [JsonPropertyNameAttribute("hidden")]
        public bool Hidden { get; set; }

        [JsonPropertyNameAttribute("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonPropertyNameAttribute("we_care")]
        public bool WeCare { get; set; }

        [JsonPropertyNameAttribute("distance")]
        public double Distance { get; set; }

        [JsonPropertyNameAttribute("cover_picture")]
        public Picture CoverPicture { get; set; }
    }

/*
    public enum BadgeType { OVERALL_RATING_TRUST_SCORE, SERVICE_RATING_SCORE };

    public enum RatingGroup { LIKED, LOVED };

    public enum ItemCategory { BAKED_GOODS, GROCERIES, OTHER };

    public enum PackagingOption { BAG_ALLOWED, MUST_BRING_BAG };

    public enum PositiveRatingReason {
        POSITIVE_FEEDBACK_GREAT_QUANTITY,
        POSITIVE_FEEDBACK_FRIENDLY_STAFF,
        POSITIVE_FEEDBACK_GREAT_VALUE,
        POSITIVE_FEEDBACK_QUICK_COLLECTION,
        POSITIVE_FEEDBACK_DELICIOUS_FOOD,
        POSITIVE_FEEDBACK_GREAT_VARIETY 
    }

    public enum Code { EUR };

    public enum TaxationPolicy { PRICE_INCLUDES_TAXES };

    public enum IsoCode { NL };

    public enum Name { NETHERLANDS };

    public enum StoreTimeZone { EUROPE_AMSTERDAM };
    */
}
