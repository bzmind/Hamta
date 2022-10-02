jQuery.validator.addMethod("enumNotNullOrZero", function (value, element, params)
{
  if (value === "none")
    return false;
  return true;
});

jQuery.validator.addMethod("listNotEmpty", function (value, element, params)
{
  if ($(element).is("input[type='file']"))
  {
    if ($(element).get(0).files.length > 0)
      return true;
    return false;
  }
  // TODO: This 👇 might be nonsense that I wrote just to fill here...
  if (value.length > 0)
    return true;
  return false;
});

jQuery.validator.addMethod("listMinLength", function (value, element, params)
{
  element = $(element);
  const otherElementsInList = $(`[data-listId="${element.attr("data-listId")}"]`);
  if (otherElementsInList.length < parseInt(element.attr("data-listMinLength")))
  {
    return false;
  } else
  {
    return true;
  }
});

jQuery.validator.addMethod("listMaxLength", function (value, element, params)
{
  element = $(element);
  const otherElementsInList = $(`[data-listId="${element.attr("data-listId")}"]`);
  if (otherElementsInList.length > parseInt(element.attr("data-listMaxLength")))
  {
    return false;
  } else
  {
    return true;
  }
});

jQuery.validator.addMethod("listMembersNotEmpty", function (value, element, params)
{
  if (isEmptyOrSpaces(value))
    return false;
  return true;
});

jQuery.validator.addMethod("listMembersCharactersMinLength", function (value, element, params)
{
  const minLength = $(element).attr("data-listMembersCharactersMinLength");

  if (isEmptyOrSpaces(value))
    return false;

  if (value.length < minLength)
    return false;

  return true;
});

jQuery.validator.addMethod("listMembersCharactersMaxLength", function (value, element, params)
{
  const maxLength = $(element).attr("data-listMembersCharactersMaxLength");

  if (isEmptyOrSpaces(value))
    return false;

  if (value.length > maxLength)
    return false;

  return true;
});

jQuery.validator.addMethod("imageFile", function (value, element, params)
{
  if ($(element).prop("files").length === 0)
    return true;

  if ($(element).is("[multiple]"))
    value = $.map($(element).prop("files"), function (val) { return val.name; });

  return validateImageExtension(value);
});

function validateImageExtension(imageNames)
{
  let imageNamesArray = imageNames;
  if (!Array.isArray(imageNames))
  {
    imageNamesArray = [];
    imageNamesArray.push(imageNames);
  }

  for (let i = 0; i < imageNamesArray.length; i++)
  {
    const fileType = getExtension(imageNamesArray[i].toLowerCase());
    if (fileType !== "png" && fileType !== "jpg" && fileType !== "jpeg" &&
      fileType !== "bmp" && fileType !== "svg" && fileType !== "gif" &&
      fileType !== "tiff" && fileType !== "webp" && fileType !== "ico" && fileType !== "pjpeg")
      return false;
  }
  return true;
}

function getExtension(name)
{
  const basename = name.split(/[\\/]/).pop();
  const pos = basename.lastIndexOf(".");

  if (basename === "" || pos < 1)
    return "";

  return basename.slice(pos + 1);
}

function isEmptyOrSpaces(str)
{
  return str === null || str.match(/^ *$/) !== null;
}

jQuery.validator.addMethod("requiredif",
  function (value, element, parameters)
  {
    const guid = element.getAttribute("data-otherPropertyGuid");
    const targetId = parameters.otherProperty;
    const targetValue = parameters.otherPropertyDesiredValue;
    const otherPropertyValue = (targetValue == null || targetValue == undefined ? "" : targetValue).toString();
    const otherPropertyElement = $(`#${targetId}${guid}`);

    if (!value.trim() && otherPropertyElement.val() == otherPropertyValue)
    {
      const isValid = $.validator.methods.required.call(this, value, element, parameters);
      return isValid;
    }

    return true;
  }
);

jQuery.validator.unobtrusive.adapters.addBool("enumNotNullOrZero");
jQuery.validator.unobtrusive.adapters.addBool("listNotEmpty");
jQuery.validator.unobtrusive.adapters.addBool("listMinLength");
jQuery.validator.unobtrusive.adapters.addBool("listMaxLength");
jQuery.validator.unobtrusive.adapters.addBool("listMembersNotEmpty");
jQuery.validator.unobtrusive.adapters.addBool("listMembersCharactersMinLength");
jQuery.validator.unobtrusive.adapters.addBool("listMembersCharactersMaxLength");
jQuery.validator.unobtrusive.adapters.addBool("imageFile");
jQuery.validator.unobtrusive.adapters.add("requiredif", ["otherProperty", "otherPropertyDesiredValue"],
  function (options)
  {
    options.rules["requiredif"] = options.params;
    options.messages["requiredif"] = options.message;
  });