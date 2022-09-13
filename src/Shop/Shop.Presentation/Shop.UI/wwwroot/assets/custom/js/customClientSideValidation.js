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
  if (value.length > 0)
    return true;
  return false;
});

jQuery.validator.addMethod("listMinLength", function (value, element, params)
{
  // TODO: Check the list's length
});

jQuery.validator.addMethod("listMaxLength", function (value, element, params)
{
  if ($(element).is("input[type='file']"))
  {
    const maxLength = parseInt($(element).attr("data-listMaxLength"));
    if ($(element).get(0).files.length > maxLength)
      return false;
    return true;
  }
  // TODO: Check if this attribute is on a div element for example and then, figure out how many
  // inputs exist in that div, so it's not more than the maxlength, something like that I guess...
  return false;
});

jQuery.validator.addMethod("listMembersCharactersMinLength", function (value, element, params)
{
  // TODO: Check to see if the string type members of a list are in specified length
});

jQuery.validator.addMethod("listMembersCharactersMaxLength", function (value, element, params)
{
  // TODO: Check to see if the string type members of a list are in specified length
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

function getExtension(path)
{
  const basename = path.split(/[\\/]/).pop();
  const pos = basename.lastIndexOf(".");

  if (basename === "" || pos < 1)
    return "";

  return basename.slice(pos + 1);
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
jQuery.validator.unobtrusive.adapters.addBool("listMembersCharactersMinLength");
jQuery.validator.unobtrusive.adapters.addBool("listMembersCharactersMaxLength");
jQuery.validator.unobtrusive.adapters.addBool("imageFile");
jQuery.validator.unobtrusive.adapters.add("requiredif", ["otherProperty", "otherPropertyDesiredValue"],
  function (options)
  {
    options.rules["requiredif"] = options.params;
    options.messages["requiredif"] = options.message;
  });