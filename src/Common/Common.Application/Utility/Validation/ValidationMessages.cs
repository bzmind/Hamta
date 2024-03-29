﻿namespace Common.Application.Utility.Validation;

public static class ValidationMessages
{
    public const string Required = "وارد کردن این فیلد اجباری است";
    public const string NotFound = "اطلاعات درخواستی یافت نشد";
    public const string MaxCharactersLength = "تعداد کاراکتر های وارد شده بیشتر از حد مجاز است";
    public const string MinCharactersLength = "تعداد کاراکتر های وارد شده کمتر از حد مجاز است";

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
    public const string CategoryRequired = "لطفا دسته‌بندی را وارد کنید";
    public const string CategorySpecificationRequired = "لطفا مشخصات دسته‌بندی را وارد کنید";
    public const string AvatarRequired = "لطفا عکس آواتار را وارد کنید";
    public const string TitleRequired = "لطفا عنوان را وارد کنید";
    public const string IntroductionRequired = "لطفا متن معرفی را وارد کنید";
    public const string ReviewRequired = "لطفا متن بررسی تخصصی را وارد کنید";
    public const string DescriptionRequired = "لطفا توضیحات را وارد کنید";
    public const string ScoreRequired = "لطفا امتیاز را وارد کنید";
    public const string SlugRequired = "لطفا اسلاگ را وارد کنید";
    public const string ColorNameRequired = "لطفا نام رنگ را وارد کنید";
    public const string ColorCodeRequired = "لطفا کد رنگ را وارد کنید";
    public const string CommentRecommendationRequired = "لطفا پیشنهاد خود را وارد کنید";
    public const string CommentStatusRequired = "لطفا وضعیت نظر را وارد کنید";
    public const string OrderStatusRequired = "لطفا وضعیت سفارش را وارد کنید";
    public const string QuestionStatusRequired = "لطفا وضعیت سوال را وارد کنید";
    public const string QuantityRequired = "لطفا تعداد را وارد کنید";
    public const string PriceRequired = "لطفا قیمت را وارد کنید";
    public const string DiscountPercentageRequired = "لطفا درصد تخفیف را وارد کنید";
    public const string InvalidDiscountPercentageRange = "درصد تخفیف باید بین 0 تا 100 باشد";
    public const string ProductNameRequired = "لطفا نام محصول را وارد کنید";
    public const string ProductEnglishNameRequired = "لطفا نام انگلیسی محصول را وارد کنید";
    public const string PermissionsRequired = "لطفا مجوز ها را وارد کنید";
    public const string NationalCodeRequired = "لطفا کدملی را وارد کنید";
    public const string ShopNameRequired = "لطفا نام فروشگاه را وارد کنید";
    public const string LinkRequired = "لطفا لینک را وارد کنید";
    public const string BannerImageRequired = "لطفا عکس بنر را وارد کنید";
    public const string SliderImageRequired = "لطفا عکس اسلایدر را وارد کنید";

    public const string ChooseProduct = "لطفا محصول را انتخاب کنید";
    public const string ChooseProductMainImage = "لطفا عکس اصلی محصول را انتخاب کنید";
    public const string ChooseProductGalleryImage = "لطفا عکس های گالری محصول را انتخاب کنید";
    public const string ChooseUser = "لطفا کاربر را انتخاب کنید";
    public const string ChooseUserAddress = "لطفا آیدی آدرس کاربر را وارد کنید";
    public const string ChooseComment = "لطفا نظر را انتخاب کنید";
    public const string ChooseCategory = "لطفا دسته‌بندی را انتخاب کنید";
    public const string ChooseCategorySpecification = "لطفا مشخصات دسته‌بندی را انتخاب کنید";
    public const string ChooseColor = "لطفا رنگ را انتخاب کنید";
    public const string ChooseQuestion = "لطفا سوال را انتخاب کنید";
    public const string ChooseSeller = "لطفا فروشنده را انتخاب کنید";
    public const string ChooseSellerInventory = "لطفا انبار را انتخاب کنید";
    public const string ChooseOrder = "لطفا سفارش را انتخاب کنید";
    public const string ChooseShipping = "لطفا روش ارسال را انتخاب کنید";
    public const string ChooseStatusRequired = "لطفا وضعیت را انتخاب کنید";
    public const string ChooseRole = "لطفا نقش را انتخاب کنید";
    public const string ChooseAvatar = "لطفا آواتار را انتخاب کنید";
    public const string ChooseGender = "لطفا جنسیت را انتخاب کنید";
    public const string ChooseBanner = "لطفا بنر را انتخاب کنید";
    public const string ChooseBannerPosition = "لطفا موقعیت بنر را انتخاب کنید";
    public const string ChooseSlider = "لطفا اسلایدر را انتخاب کنید";

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
    public const string InvalidPrice = "قیمت نامعتبر است";
    public const string InvalidGender = "جنسیت نامعتبر است";
    public const string InvalidBannerPosition = "موقعیت بنر نامعتبر است";
    public const string InvalidBannerImage = "عکس بنر نامعتبر است";
    public const string InvalidAvatar = "آواتار نامعتبر است";
    public const string InvalidImage = "عکس نامعتبر است";
    public const string InvalidTitle = "عنوان نامعتبر است";
    public const string InvalidDescription = "توضیحات نامعتبر است";
    public const string InvalidIntroduction = "متن معرفی نامعتبر است";
    public const string InvalidReview = "متن بررسی تخصصی نامعتبر است";
    public const string InvalidColorName = "نام رنگ نامعتبر است";
    public const string InvalidColorCode = "کد رنگ نامعتبر است";
    public const string InvalidCommentRecommendation = "پیشنهاد نامعتبر است";
    public const string InvalidList = "لیست نامعتبر است";
    public const string InvalidCommentStatus = "وضعیت نظر نامعتبر است";
    public const string InvalidOrderStatus = "وضعیت سفارش نامعتبر است";
    public const string InvalidQuestionStatus = "وضعیت سوال نامعتبر است";
    public const string InvalidPermissions = "مجوز ها نامعتبر هستند";
    public const string InvalidNationalCode = "کدملی نامعتبر است";
    public const string InvalidShopName = "نام فروشگاه نامعتبر است";
    public const string InvalidStatus = "وضعیت نامعتبر است";
    public const string InvalidLink = "لینک نامعتبر است";
    public const string InvalidSliderImage = "عکس اسلایدر نامعتبر است";

    public const string EmailRegex = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";
    public const string IranPhoneRegex = @"^(?:0)?(9\d{9})$";
    public const string EmailOrPhoneRegex = EmailRegex + "|" + IranPhoneRegex;
    public const string OnlyNumberRegex = @"^\d+$";

    /// <summary>
    /// <example>Example: نام} را وارد کنید}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    public static string FieldRequired(string fieldName) => $"لطفا {fieldName} را وارد کنید";

    /// <summary>
    /// <example>Example: نام} نامعتبر است}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    public static string FieldInvalid(string fieldName) => $"{fieldName} نامعتبر است";

    /// <summary>
    /// <example>Example: امتیاز} نمی‌تواند بیشتر از {3} باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="maxLength"></param>
    public static string FieldMaxAmount(string fieldName, int maxLength)
        => $"{fieldName} نمی‌تواند بیشتر از {maxLength} باشد";

    /// <summary>
    /// <example>Example: امتیاز} نمی‌تواند کمتر از {3} باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="minLength"></param>
    public static string FieldMinAmount(string fieldName, int minLength)
        => $"{fieldName} نمی‌تواند کمتر از {minLength} باشد";

    /// <summary>
    /// <example>Example: نام} باید {3} کاراکتر باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="staticLength"></param>
    public static string FieldCharactersStaticLength(string fieldName, int staticLength)
        => $"{fieldName} باید {staticLength} کاراکتر باشد";

    /// <summary>
    /// <example>Example: نام} نمی‌تواند بیشتر از {3} کاراکتر باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="maxLength"></param>
    public static string FieldCharactersMaxLength(string fieldName, int maxLength)
        => $"{fieldName} نمی‌تواند بیشتر از {maxLength} کاراکتر باشد";

    /// <summary>
    /// <example>Example: نام} نمی‌تواند کمتر از {3} کاراکتر باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="minLength"></param>
    public static string FieldCharactersMinLength(string fieldName, int minLength)
        => $"{fieldName} نمی‌تواند کمتر از {minLength} کاراکتر باشد";

    /// <summary>
    /// <example>Example: شماره تلفن} باید {11} رقم باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="staticLength"></param>
    public static string FieldDigitsStaticNumber(string fieldName, int staticLength)
        => $"{fieldName} باید {staticLength} رقم باشد";

    /// <summary>
    /// <example>Example: شماره تلفن} نمی‌تواند بیشتر از {12} رقم باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="maxLength"></param>
    public static string FieldDigitsMaxNumber(string fieldName, int maxLength)
        => $"{fieldName} نمی‌تواند بیشتر از {maxLength} رقم باشد";

    /// <summary>
    /// <example>Example: شماره تلفن} نمی‌تواند کمتر از {12} رقم باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="minLength"></param>
    public static string FieldDigitsMinNumber(string fieldName, int minLength)
        => $"{fieldName} نمی‌تواند کمتر از {minLength} رقم باشد";

    /// <summary>
    /// <example>Example: محصولات} نمی‌تواند بیشتر از {50} عدد باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="maxLength"></param>
    public static string FieldQuantityMaxNumber(string fieldName, int maxLength)
        => $"{fieldName} نمی‌تواند بیشتر از {maxLength} عدد باشد";

    /// <summary>
    /// <example>Example: محصولات} نمی‌تواند کمتر از {0} عدد باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="minLength"></param>
    public static string FieldQuantityMinNumber(string fieldName, int minLength)
        => $"{fieldName} نمی‌تواند کمتر از {minLength} عدد باشد";
    
    /// <summary>
    /// <example>Example: تخفیف} نمی‌تواند بیشتر از {100} درصد باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="maxPercentage"></param>
    public static string FieldPercentageMaximum(string fieldName, int maxPercentage)
        => $"{fieldName} نمی‌تواند بیشتر از {maxPercentage} درصد باشد";
    
    /// <summary>
    /// <example>Example: تخفیف} نمی‌تواند کمتر از {0} درصد باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="maxPercentage"></param>
    public static string FieldPercentageMinimum(string fieldName, int maxPercentage)
        => $"{fieldName} نمی‌تواند کمتر از {maxPercentage} درصد باشد";

    /// <summary>
    /// <example>Example: قیمت} باید کمتر از {100000} تومان باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="maxAmount"></param>
    public static string TomanMaxAmount(string fieldName, int maxAmount)
        => $"{fieldName} نمی‌تواند بیشتر از {maxAmount} تومان باشد";

    /// <summary>
    /// <example>Example: قیمت} باید بیشتر از {0} تومان باشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="minAmount"></param>
    public static string TomanMinAmount(string fieldName, int minAmount)
        => $"{fieldName} نمی‌تواند کمتر از {minAmount} تومان باشد";

    /// <summary>
    /// <example>Example: سفارش} یافت نشد}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    public static string FieldNotFound(string fieldName)
        => $"{fieldName} یافت نشد";

    /// <summary>
    /// <example>Example: نام} تکراری است}</example>
    /// </summary>
    /// <param name="fieldName"></param>
    public static string FieldDuplicate(string fieldName)
        => $"{fieldName} تکراری است";

    /// <summary>
    /// <example>Example امکان حذف این {سفارش} وجود ندارد</example>
    /// </summary>
    /// <param name="fieldName"></param>
    public static string FieldCantBeRemoved(string fieldName)
        => $"امکان حذف این {fieldName} وجود ندارد";
}