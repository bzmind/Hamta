$(document).ready(function ()
{
  setupColorsRadio();
  setupFilterCheckboxes();
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

function changeUrlParamValue(paramName, paramValue)
{
  const url = new URL(window.location.href);
  const searchParams = url.searchParams;

  searchParams.set(paramName, paramValue);
  url.search = searchParams.toString();

  const newUrl = url.toString();
  window.location.replace(newUrl);
}

function setOrRemoveParam(paramName, paramValue)
{
  const isChecked = paramValue;
  const url = new URL(window.location.href);
  const searchParams = url.searchParams;

  if (isChecked)
  {
    searchParams.set(paramName, "true");
    url.search = searchParams.toString();
    const newUrl = url.toString();
    window.location.replace(newUrl);
  }
  else
  {
    searchParams.delete(paramName);
    url.search = searchParams.toString();
    const newUrl = url.toString();
    window.location.replace(newUrl);
  }
}

function setupFilterCheckboxes()
{
  $(".widget-content input:checkbox").on("change", function ()
  {
    const value = $(this).val();
    const name = $(this).attr("name");
    setOrRemoveParam(name, value);
  });
}