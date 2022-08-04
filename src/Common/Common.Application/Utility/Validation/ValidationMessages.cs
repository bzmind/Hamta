namespace Common.Application.Utility.Validation;

public static class ValidationMessages
{
    public const string Required = "وارد کردن این فیلد اجباری است";
    public const string NotFound = "اطلاعات درخواستی یافت نشد";
    public const string MaxCharactersLength = "تعداد کاراکتر های وارد شده بیشتر از حد مجاز است";
    public const string MinCharactersLength = "تعداد کاراکتر های وارد شده کمتر از حد مجاز است";

    public const string IdRequired = "لطفا آیدی را وارد کنید";
    public const string NameRequired = "لطفا نام را وارد کنید";
    public const string FamilyNameRequired = "لطفا نام خانوادگی را وارد کنید";
    public const string FullNameRequired = "لطفا نام و نام خانوادگی را وارد کنید";
    public const string PhoneNumberRequired = "لطفا شماره موبایل را وارد کنید";
    public const string EmailRequired = "لطفا ایمیل را وارد کنید";
    public const string NewPasswordRequired = "لطفا رمز عبور جدید را وارد کنید";
    public const string CurrentPasswordRequired = "لطفا رمز عبور فعلی را وارد کنید";
    public const string PasswordRequired = "لطفا رمز عبور را وارد کنید";
    public const string ConfirmPasswordRequired = "لطفا تکرار رمز عبور را وارد کنید";
    public const string ConfirmNewPasswordRequired = "لطفا تکرار رمز عبور جدید را وارد کنید";
    public const string EmailOrPhoneRequired = "لطفا شماره موبایل یا ایمیل را وارد کنید";
    public const string GenderRequired = "لطفا جنسیت را انتخاب کنید";
    public const string AvatarRequired = "لطفا عکس آواتار را وارد کنید";
    public const string TitleRequired = "لطفا عنوان را وارد کنید";
    public const string DescriptionRequired = "لطفا توضیحات را وارد کنید";
    public const string SlugRequired = "لطفا اسلاگ را وارد کنید";
    public const string ColorNameRequired = "لطفا نام رنگ را وارد کنید";
    public const string ColorCodeRequired = "لطفا کد رنگ را وارد کنید";
    public const string CommentRecommendationRequired = "لطفا پیشنهاد خود را وارد کنید";
    public const string CommentStatusRequired = "لطفا وضعیت نظر را وارد کنید";
    public const string OrderStatusRequired = "لطفا وضعیت سفارش را وارد کنید";
    public const string QuestionStatusRequired = "لطفا وضعیت سوال را وارد کنید";
    public const string QuantityRequired = "لطفا تعداد را وارد کنید";
    public const string PriceRequired = "لطفا قیمت را وارد کنید";
    public const string ProductNameRequired = "لطفا نام محصول را وارد کنید";
    public const string ProductEnglishNameRequired = "لطفا نام انگلیسی محصول را وارد کنید";
    public const string PermissionsRequired = "لطفا مجوز ها را وارد کنید";
    public const string NationalCodeRequired = "لطفا کدملی را وارد کنید";
    public const string ShopNameRequired = "لطفا نام فروشگاه را وارد کنید";
    public const string StatusRequired = "لطفا وضعیت را وارد کنید";

    public const string InvalidName = "نام نامعتبر است";
    public const string InvalidFamilyName = "نام خانوادگی نامعتبر است";
    public const string InvalidFullName = "نام و نام خانوادگی نامعتبر است";
    public const string InvalidEmailOrPhone = "شماره موبایل یا ایمیل نامعتبر است";
    public const string InvalidEmail = "ایمیل نامعتبر است";
    public const string InvalidPhoneNumber = "شماره موبایل نامعتبر است";
    public const string InvalidNewPassword = "رمز عبور جدید نامعتبر است";
    public const string InvalidCurrentPassword = "رمز عبور فعلی نامعتبر است";
    public const string InvalidPassword = "رمز عبور نامعتبر است";
    public const string InvalidConfirmPassword = "رمز های عبور با هم یکسان نیستند";
    public const string InvalidConfirmNewPassword = "رمز های عبور جدید با هم یکسان نیستند";
    public const string InvalidPrice = "مبلغ نامعتبر است";
    public const string InvalidGender = "جنسیت نامعتبر است";
    public const string InvalidAvatar = "آواتار نامعتبر است";
    public const string InvalidImage = "عکس نامعتبر است";
    public const string InvalidTitle = "عنوان نامعتبر است";
    public const string InvalidDescription = "توضیحات نامعتبر است";
    public const string InvalidColorName = "نام رنگ نامعتبر است";
    public const string InvalidColorCode = "کد رنگ نامعتبر است";
    public const string InvalidCommentRecommendation = "پیشنهاد نامعتبر است";
    public const string InvalidList = "لیست نامعتبر است";
    public const string InvalidCommentStatus = "وضعیت نظر نامعتبر است";
    public const string InvalidOrderStatus = "وضعیت سفارش نامعتبر است";
    public const string InvalidQuestionStatus = "وضعیت سوال نامعتبر است";
    public const string InvalidPermissions = "مجوز ها نامعتبر هستند";
    public const string InvalidNationalCode = "کدملی نامعتبر است";
    public const string ShopNameNationalCode = "نام فروشگاه نامعتبر است";
    public const string StatusNationalCode = "وضعیت نامعتبر است";

    public const string EmailRegex = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";
    public const string IranPhoneRegex = @"^(?:0)?(9\d{9})$";
    public const string EmailOrPhoneRegex = EmailRegex + "|" + IranPhoneRegex;
    public const string OnlyNumberRegex = @"^\d+$";

    /// <summary>
    /// <example>Example: نام} را وارد کنید}</example>
    /// </summary>
    /// <param name="field"></param>
    public static string FieldRequired(string field) => $"{field} را وارد کنید";

    /// <summary>
    /// <example>Example: نام} نامعتبر است}</example>
    /// </summary>
    /// <param name="field"></param>
    public static string FieldInvalid(string field) => $"{field} نامعتبر است";


    /// <summary>
    /// <example>Example: امتیاز} باید بیشتر یا مساوی {3} باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxLength"></param>
    public static string FieldGreaterThanOrEqualTo(string field, int maxLength)
        => $"{field} باید بیشتر یا مساوی {maxLength} باشد";

    /// <summary>
    /// <example>Example: امتیاز} باید کمتر یا مساوی {3} باشد}</example>
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
    /// <example>Example: محصولات} باید کمتر از {0} عدد باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxLength"></param>
    public static string FieldQuantityMaxNumber(string field, int maxLength)
        => $"{field} باید کمتر از {maxLength} عدد باشد";

    /// <summary>
    /// <example>Example: محصولات} باید بیشتر از {0} عدد باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minLength"></param>
    public static string FieldQuantityMinNumber(string field, int minLength)
        => $"{field} باید بیشتر از {minLength} عدد باشد";
    
    /// <summary>
    /// <example>Example: تخفیف} حداکثر می‌تواند {100} درصد باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxPercentage"></param>
    public static string FieldPercentageMaximum(string field, int maxPercentage)
        => $"{field} حداکثر می‌تواند {maxPercentage} درصد باشد";

    /// <summary>
    /// <example>Example: تخفیف} باید کمتر از {100} درصد باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxPercentage"></param>
    public static string FieldPercentageLessThan(string field, int maxPercentage)
        => $"{field} باید کمتر از {maxPercentage} درصد باشد";

    /// <summary>
    /// <example>Example: تخفیف} باید بیشتر یا مساوی {0} درصد باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minPercentage"></param>
    public static string FieldPercentageGreaterThanOrEqualTo(string field, int minPercentage)
        => $"{field} باید بیشتر از {minPercentage} درصد باشد";

    /// <summary>
    /// <example>Example: تخفیف} باید بیشتر از {0} درصد باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minPercentage"></param>
    public static string FieldPercentageGreaterThan(string field, int minPercentage)
        => $"{field} باید بیشتر از {minPercentage} درصد باشد";

    /// <summary>
    /// <example>Example: قیمت} باید کمتر از {100000} تومان باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="maxAmount"></param>
    public static string TomanMaxAmount(string field, int maxAmount)
        => $"{field} باید کمتر از {maxAmount:C0} درصد باشد";

    /// <summary>
    /// <example>Example: قیمت} باید بیشتر از {0} تومان باشد}</example>
    /// </summary>
    /// <param name="field"></param>
    /// <param name="minAmount"></param>
    public static string TomanMinAmount(string field, int minAmount)
        => $"{field} باید بیشتر از {minAmount:C0} درصد باشد";

    /// <summary>
    /// <example>Example: سفارش} یافت نشد}</example>
    /// </summary>
    /// <param name="field"></param>
    public static string FieldNotFound(string field)
        => $"{field} یافت نشد";

    /// <summary>
    /// <example>Example: نام} تکراری است}</example>
    /// </summary>
    /// <param name="field"></param>
    public static string FieldDuplicate(string field)
        => $"{field} تکراری است";

    /// <summary>
    /// <example>Example امکان حذف این {سفارش} وجود ندارد</example>
    /// </summary>
    /// <param name="field"></param>
    public static string FieldCantBeRemoved(string field)
        => $"امکان حذف این {field} وجود ندارد";
}