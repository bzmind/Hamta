$(document).ready(function ()
{
  setupColorsRadio();
});

function setupColorsRadio()
{
  const animDuration = 100;

  showInventoriesWithSameColor($(".colors-radio:checked"));
  $(".colors-radio").on("change", function ()
  {
    showInventoriesWithSameColor($(this));
    showProductInfoWithSameColor($(this));
  });

  function showInventoriesWithSameColor(selectedColorRadio)
  {
    const colorId = selectedColorRadio.val();
    const inventoriesWithSameColor = $(`[data-inventory-color-id="${colorId}"]`);

    $(`[data-inventory-color-id]:visible`).fadeOut(animDuration, () =>
    {
      inventoriesWithSameColor.fadeIn(animDuration);
    });
  }

  function showProductInfoWithSameColor(selectedColorRadio)
  {
    const colorId = selectedColorRadio.val();
    const productInfoWithSameColor = $(`[data-product-color-id="${colorId}"]`).first();

    $(`[data-product-color-id]:visible`).fadeOut(animDuration, () =>
    {
      productInfoWithSameColor.fadeIn(animDuration);
    });
  }
}

function getProductComments(pageId)
{
  const productId = $("#productId").val();
  sendAjaxGetWithRouteData(`/product/slug/ShowComments?productId=${productId}&pageId=${pageId}`).then((result) =>
  {
    checkResult(result);
    replaceElementWithAjaxResult(".comments-list", result);
  });
}

function toggleUrlParam(paramName, paramValue)
{
  toggleParam(paramName, paramValue);
}

function toggleUrlParamByElement(paramName, elementWithParamValue)
{
  const paramValue = $(elementWithParamValue).val();
  if (deleteParamIfValueIsNull(paramName, paramValue))
    return;
  toggleParam(paramName, paramValue);
}

function deleteParamIfValueIsNull(paramName, paramValue)
{
  const url = new URL(window.location.href);
  const searchParams = url.searchParams;
  if (paramValue == null || paramValue == "" || paramValue == 0)
  {
    url.search = deleteSpecificParamFromUrl(searchParams, paramName, paramValue);
    const newUrl = url.toString();
    window.location.href = newUrl;
    return true;
  }
  return false;
}

function toggleParam(paramName, paramValue)
{
  const url = new URL(window.location.href);
  const searchParams = url.searchParams;

  const exists = searchParams.has(paramName) && searchParams.getAll(paramName).includes(paramValue);
  if (exists)
  {
    url.search = deleteSpecificParamFromUrl(searchParams, paramName, paramValue);
    const newUrl = url.toString();
    window.location.href = newUrl;
  }
  else
  {
    searchParams.set(paramName, paramValue);
    url.search = searchParams.toString();
    const newUrl = url.toString();
    window.location.href = newUrl;
  }
}

function addUrlParam(paramName, paramValue)
{
  const url = new URL(window.location.href);
  const searchParams = url.searchParams;

  const exists = searchParams.has(paramName) && searchParams.getAll(paramName).includes(paramValue);
  if (exists)
  {
    url.search = deleteSpecificParamFromUrl(searchParams, paramName, paramValue);
    const newUrl = url.toString();
    window.location.href = newUrl;
  }
  else
  {
    searchParams.append(paramName, paramValue);
    url.search = searchParams.toString();
    const newUrl = url.toString();
    window.location.href = newUrl;
  }
}

function setUrlParam(paramName, paramValue)
{
  setParam(paramName, paramValue);
}

function setUrlParamByElement(paramName, elementWithParamValue)
{
  const paramValue = $(elementWithParamValue).val();
  if (deleteParamIfValueIsNull(paramName, paramValue))
    return;
  setParam(paramName, paramValue);
}

function setUrlParamByMultipleElements(inputsContainerSelector)
{
  const url = new URL(window.location.href);
  const searchParams = url.searchParams;
  $(inputsContainerSelector).find("input").each((i, input) =>
  {
    input = $(input);
    let paramValue = input.val();
    if (input.attr("data-range") != null)
      paramValue = paramValue.replace(/,/g, "");
    const paramName = input.attr("name");
    searchParams.set(paramName, paramValue);
  });
  url.search = searchParams.toString();
  const newUrl = url.toString();
  window.location.href = newUrl;
}

function setParam(paramName, paramValue)
{
  const url = new URL(window.location.href);
  const searchParams = url.searchParams;
  searchParams.set(paramName, paramValue);

  url.search = searchParams.toString();
  const newUrl = url.toString();
  window.location.href = newUrl;
}

function deleteSpecificParamFromUrl(searchParams, parameterName, parameterValue)
{
  const exactParam = `${parameterName}=${encodeURIComponent(parameterValue).replace(/%20/g, "+")}`;
  return searchParams.toString().replace(exactParam, "").replace("&&", "&").replace(/&$/, "");
}