using System;
using System.Text.Json.Serialization;

namespace TooGoodToGoWatcherCore.DataContracts
{
    public partial class LoginResponse
    {
        [JsonPropertyNameAttribute("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyNameAttribute("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyNameAttribute("startup_data")]
        public StartupData StartupData { get; set; }
    }

    public partial class StartupData
    {
        [JsonPropertyNameAttribute("user")]
        public User User { get; set; }

        [JsonPropertyNameAttribute("app_settings")]
        public AppSettings AppSettings { get; set; }

        [JsonPropertyNameAttribute("user_settings")]
        public UserSettings UserSettings { get; set; }

        [JsonPropertyNameAttribute("orders")]
        public Orders Orders { get; set; }

        [JsonPropertyNameAttribute("vouchers")]
        public Vouchers Vouchers { get; set; }
    }

    public partial class AppSettings
    {
        [JsonPropertyNameAttribute("on_app_open_message")]
        public string OnAppOpenMessage { get; set; }

        [JsonPropertyNameAttribute("open_message_type")]
        public string OpenMessageType { get; set; }

        [JsonPropertyNameAttribute("open_message_url")]
        public Uri OpenMessageUrl { get; set; }

        [JsonPropertyNameAttribute("countries")]
        public Country[] Countries { get; set; }

        //[JsonPropertyNameAttribute("purchase_rating_start")]
        [JsonIgnore]
        public DateTimeOffset PurchaseRatingStart { get; set; }

        //[JsonPropertyNameAttribute("purchase_rating_end")]
        [JsonIgnore]
        public DateTimeOffset PurchaseRatingEnd { get; set; }

        //[JsonPropertyNameAttribute("purchase_rating_delay")]
        [JsonIgnore]
        public long PurchaseRatingDelay { get; set; }
    }

    public partial class Country
    {
        [JsonPropertyNameAttribute("country_iso_code")]
        public string CountryIsoCode { get; set; }

        [JsonPropertyNameAttribute("terms_url")]
        public Uri TermsUrl { get; set; }

        [JsonPropertyNameAttribute("privacy_url")]
        public Uri PrivacyUrl { get; set; }
    }

    public partial class Orders
    {
        [JsonPropertyNameAttribute("current_time")]
        public DateTimeOffset CurrentTime { get; set; }

        [JsonPropertyNameAttribute("has_more")]
        public bool HasMore { get; set; }

        [JsonPropertyNameAttribute("orders")]
        public object[] OrdersOrders { get; set; }
    }

    public partial class User
    {
        [JsonPropertyNameAttribute("user_id")]
        public long UserId { get; set; }

        [JsonPropertyNameAttribute("name")]
        public string Name { get; set; }

        [JsonPropertyNameAttribute("country_id")]
        public string CountryId { get; set; }

        [JsonPropertyNameAttribute("email")]
        public string Email { get; set; }

        [JsonPropertyNameAttribute("phone_country_code")]
        public string PhoneCountryCode { get; set; }

        [JsonPropertyNameAttribute("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyNameAttribute("role")]
        public string Role { get; set; }

        [JsonPropertyNameAttribute("is_partner")]
        public bool IsPartner { get; set; }

        [JsonPropertyNameAttribute("newsletter_opt_in")]
        public bool NewsletterOptIn { get; set; }

        [JsonPropertyNameAttribute("push_notifications_opt_in")]
        public bool PushNotificationsOptIn { get; set; }
    }

    public partial class UserSettings
    {
        [JsonPropertyNameAttribute("country_iso_code")]
        public string CountryIsoCode { get; set; }

        [JsonPropertyNameAttribute("phone_country_code_suggestion")]
        public long PhoneCountryCodeSuggestion { get; set; }

        [JsonPropertyNameAttribute("is_user_email_verified")]
        public bool IsUserEmailVerified { get; set; }

        [JsonPropertyNameAttribute("terms_url")]
        public Uri TermsUrl { get; set; }

        [JsonPropertyNameAttribute("privacy_url")]
        public Uri PrivacyUrl { get; set; }

        [JsonPropertyNameAttribute("contact_form_url")]
        public Uri ContactFormUrl { get; set; }

        [JsonPropertyNameAttribute("blog_url")]
        public Uri BlogUrl { get; set; }

        [JsonPropertyNameAttribute("careers_url")]
        public Uri CareersUrl { get; set; }

        [JsonPropertyNameAttribute("education_url")]
        public Uri EducationUrl { get; set; }

        [JsonPropertyNameAttribute("instagram_url")]
        public Uri InstagramUrl { get; set; }

        [JsonPropertyNameAttribute("store_signup_url")]
        public Uri StoreSignupUrl { get; set; }

        [JsonPropertyNameAttribute("store_contact_url")]
        public Uri StoreContactUrl { get; set; }

        [JsonPropertyNameAttribute("bound_sw")]
        public Bound BoundSw { get; set; }

        [JsonPropertyNameAttribute("bound_ne")]
        public Bound BoundNe { get; set; }

        [JsonPropertyNameAttribute("meals_saved")]
        public MealsSaved MealsSaved { get; set; }

        [JsonPropertyNameAttribute("has_any_vouchers")]
        public bool HasAnyVouchers { get; set; }

        [JsonPropertyNameAttribute("can_show_best_before_explainer")]
        public bool CanShowBestBeforeExplainer { get; set; }
    }

    public partial class Bound
    {
        [JsonPropertyNameAttribute("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyNameAttribute("latitude")]
        public double Latitude { get; set; }
    }

    public partial class MealsSaved
    {
        [JsonPropertyNameAttribute("country_iso_code")]
        public string CountryIsoCode { get; set; }

        [JsonPropertyNameAttribute("share_url")]
        public Uri ShareUrl { get; set; }

        [JsonPropertyNameAttribute("image_url")]
        public Uri ImageUrl { get; set; }

        [JsonPropertyNameAttribute("meals_saved_last_month")]
        public long MealsSavedLastMonth { get; set; }

        [JsonPropertyNameAttribute("month")]
        public long Month { get; set; }

        [JsonPropertyNameAttribute("year")]
        public long Year { get; set; }
    }

    public partial class Vouchers
    {
        [JsonPropertyNameAttribute("vouchers")]
        public object[] VouchersVouchers { get; set; }
    }
}
