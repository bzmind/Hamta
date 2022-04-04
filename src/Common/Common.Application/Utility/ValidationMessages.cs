namespace Common.Application.Utility;

public static class ValidationMessages
{
    public const string Required = "وارد کردن این فیلد اجباری است";
    public const string NotFound = "اطلاعات درخواستی یافت نشد";
    public const string MaxLength = "تعداد کاراکتر های وارد شده بیشتر از حد مجاز است";
    public const string MinLength = "تعداد کاراکتر های وارد شده کمتر از حد مجاز است";
    public const string InvalidPhoneNumber = "شماره تلفن وارد شده نامعتبر است";
    public const string InvalidPrice = "مبلغ وارد شده نامعتبر است";

    public static string FieldRequired(string field) => $"وارد کردن {field} اجباری است";
    public static string FieldInvalid(string field) => $"{field} وارد شده نا معتبر است";
    public static string FieldMaxLength(string field, int maxLength) => $"{field} باید کمتر از {maxLength} کاراکتر باشد";
    public static string FieldMinLength(string field, int minLength) => $"{field} باید بیشتر از {minLength} کاراکتر باشد";
}