namespace Common.Application.Validation;

public static class ValidationMessages
{
    public const string Required = "وارد کردن این فیلد اجباری است";
    public const string NotFound = "اطلاعات درخواستی یافت نشد";
    public const string MaxLength = "تعداد کاراکتر های وارد شده بیشتر از حد مجاز است";
    public const string MinLength = "تعداد کاراکتر های وارد شده کمتر از حد مجاز است";
    public const string InvalidPhoneNumber = "شماره تلفن وارد شده نامعتبر است";
    public const string InvalidPrice = "مبلغ وارد شده نامعتبر است";

    /// <summary>
    /// <example>Example: وارد کردن {نام} الزامی است</example>
    /// </summary>
    /// <param name="field"></param>
    public static string FieldRequired(string field) => $"وارد کردن {field} الزامی است";

    /// <summary>
    /// <example>Example: نام} وارد شده نامعتبر است}</example>
    /// </summary>
    /// <param name="field"></param>
    public static string FieldInvalid(string field) => $"{field} وارد شده نامعتبر است";


    /// <summary>
    /// <example>Example: نام} باید بیشتر یا مساوی {3} باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxLength"></param>
    public static string FieldGreaterThanOrEqualTo(string field, int maxLength)
        => $"{field} باید بیشتر یا مساوی {maxLength} باشد";

    /// <summary>
    /// <example>Example: نام} باید کمتر یا مساوی {3} باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxLength"></param>
    public static string FieldLessThanOrEqualTo(string field, int maxLength)
        => $"{field} باید کمتر یا مساوی {maxLength} باشد";

    /// <summary>
    /// <example>Example: نام} باید کمتر از {3} باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxLength"></param>
    public static string FieldMaxLength(string field, int maxLength)
        => $"{field} باید کمتر از {maxLength} باشد";

    /// <summary>
    /// <example>Example: نام} باید بیشتر از {3} باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minLength"></param>
    public static string FieldMinLength(string field, int minLength)
        => $"{field} باید بیشتر از {minLength} باشد";

    /// <summary>
    /// <example>Example: نام} باید {3} کاراکتر باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="staticLength"></param>
    public static string FieldCharactersStaticLength(string field, int staticLength)
        => $"{field} باید {staticLength} کاراکتر باشد";

    /// <summary>
    /// <example>Example: نام} باید کمتر از {3} کاراکتر باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxLength"></param>
    public static string FieldCharactersMaxLength(string field, int maxLength)
        => $"{field} باید کمتر از {maxLength} کاراکتر باشد";

    /// <summary>
    /// <example>Example: نام} باید بیشتر از {3} کاراکتر باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minLength"></param>
    public static string FieldCharactersMinLength(string field, int minLength)
        => $"{field} باید بیشتر از {minLength} کاراکتر باشد";

    /// <summary>
    /// <example>Example: شماره تلفن} باید {11} رقم باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="staticLength"></param>
    public static string FieldDigitsStaticNumber(string field, int staticLength)
        => $"{field} باید {staticLength} رقم باشد";

    /// <summary>
    /// <example>Example: شماره تلفن} باید کمتر از {12} رقم باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxLength"></param>
    public static string FieldDigitsMaxNumber(string field, int maxLength)
        => $"{field} باید کمتر از {maxLength} رقم باشد";

    /// <summary>
    /// <example>Example: شماره تلفن} باید بیشتر از {12} رقم باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minLength"></param>
    public static string FieldDigitsMinNumber(string field, int minLength)
        => $"{field} باید بیشتر از {minLength} رقم باشد";

    /// <summary>
    /// <example>Example: تعداد {محصولات} باید کمتر از {0} عدد باشد</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxLength"></param>
    public static string FieldQuantityMaxNumber(string field, int maxLength)
        => $"{field} باید کمتر از {maxLength} عدد باشد";

    /// <summary>
    /// <example>Example: تعداد {محصولات} باید بیشتر از {0} عدد باشد</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minLength"></param>
    public static string FieldQuantityMinNumber(string field, int minLength)
        => $"{field} باید بیشتر از {minLength} عدد باشد";

    /// <summary>
    /// <example>Example: تخفیف} باید کمتر از {100} درصد باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxPercentage"></param>
    public static string DiscountMaxPercentage(string field, int maxPercentage)
        => $"{field} باید کمتر از {maxPercentage} درصد باشد";

    /// <summary>
    /// <example>Example: تخفیف} باید بیشتر از {0} درصد باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minPercentage"></param>
    public static string DiscountMinPercentage(string field, int minPercentage)
        => $"{field} باید بیشتر از {minPercentage} درصد باشد";

    /// <summary>
    /// <example>Example: قیمت} باید کمتر از {100000} تومان باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxAmount"></param>
    public static string PriceMaxAmount(string field, int maxAmount)
        => $"{field} باید کمتر از {maxAmount.ToString("C0")} درصد باشد";

    /// <summary>
    /// <example>Example: قیمت} باید بیشتر از {0} تومان باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minAmount"></param>
    public static string PriceMinAmount(string field, int minAmount)
        => $"{field} باید بیشتر از {minAmount.ToString("C0")} درصد باشد";

    /// <summary>
    /// <example>Example: سفارش} یافت نشد}</example>
    /// </summary>
    /// <param name="field"></param>
    public static string FieldNotFound(string field)
        => $"{field} یافت نشد";
}