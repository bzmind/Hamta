$(document).ready(function ()
{
  setupColorsRadio();
  getCartSummary();
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
  sendAjaxGet(`/product/slug/ShowComments?productId=${productId}&pageId=${pageId}`).then((result) =>
  {
    checkResult(result);
    replaceElementWithAjaxResult(".comments-list", result);
  });
}

function getCartSummary()
{
  sendAjaxGet("/cart/cartSummary").then(result =>
  {
    if (result.items == null)
    {
      $(".cart-list ul").remove();
      return;
    }

    const cartItemsNumberElements = $(".bag-items-number");
    cartItemsNumberElements.html(result.itemsCount);

    const cartPrice = $(".cart-footer .total");
    cartPrice.html(result.price);

    if (result.items.length == 0)
      $(".cart-list ul").remove();
    else
    {
      result.items.map((item) =>
      {
        let numBlock = `
          <span class="minus" onclick="sendAjaxPostWithRouteDataAndReplaceElement
            (event, '/cart/decreaseitemcount?itemid=${item.id}', '')">
          </span>`;

        if (item.count == 1)
          numBlock = `
            <button class="d-flex fd-col p-0 ai-end jc-center h-32px w-30px c-danger bg-none"
                onclick="deleteItem('/cart/removeitem?itemid=${item.id}')">
              <i class="far fa-trash-alt fs-18px"></i>
            </button>`;

        $(".cart-items .cart-items-ul").append(`
          <li class="cart-item">
            <span class="d-flex align-items-center mb-2">
              <a href="/product/${item.productSlug}">
                <img src="https://localhost:7087/images/products/main/${item.productMainImage}" alt="${item.productName}">
              </a>
              <span>
                <a href="/product/${item.productSlug}">
                  <span class="title-item">${item.productName}</span>
                </a>
                <span class="color d-flex align-items-center">
                  رنگ:
                  <span class="hw-15px b-none" style="background-color: ${item.colorCode}"></span>
                </span>
              </span>
            </span>
            <span class="price">${splitNumber(item.eachItemDiscountedPrice * item.count)} تومان</span>
            <div class="item-quantity remove-item">
              <div class="num-block">
                <div class="num-in">
                  <span class="plus" onclick="sendAjaxPostWithRouteDataAndReplaceElement
                    (event, '/cart/increaseitemcount?inventoryid=${item.inventoryId}&itemid=${item.id}', '')">
                  </span>
                  <input type="text" value="${item.count}" readonly="">
                  ${numBlock}
                </div>
              </div>
            </div>
          </li>`);
      });
    }
  });
}

function splitNumber(value)
{
  return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}