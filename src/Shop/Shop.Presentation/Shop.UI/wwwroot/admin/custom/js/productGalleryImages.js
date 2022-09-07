function setProductImagesFiles()
{
  if ($(".galleryImagesList").length > 0)
    turnImagesIntoInputFiles($(".galleryImagesList"), ".galleryImageItem img", ".galleryImagesContainer input[multiple]");

  if ($(".mainImageContainer").length > 0)
    turnImagesIntoInputFiles($(".mainImageContainer"), ".mainImage img", ".mainImageContainer input");
}

function resetGalleryImagesIndexes(itemsList, itemSelector)
{
  resetIndexes(itemsList, itemSelector);
  for (let i = 0; i < itemsList.children(itemSelector).length; i++)
  {
    const guid = getGuid();
    const productImage = $(itemsList.children(itemSelector)[i]);
    productImage.find("[data-img-preview]").each((index, element) =>
    {
      element = $(element);
      element.attr("data-img-preview", guid);
    });
    productImage.find("span").each((index, element) =>
    {
      element = $(element);
      element.html(i + 1);
      element.show();
    });
  }
}

function deleteGalleryImage(e)
{
  $(e.target).parents(".galleryImageItem").remove();
  const itemsList = $(".galleryImagesList");
  resetGalleryImagesIndexes(itemsList, ".galleryImageItem");
  turnImagesIntoInputFiles(itemsList, ".galleryImageItem img", ".galleryImagesContainer input[multiple]");
}

function setupProductImagesEventListeners()
{
  $(".category-radio").unbind("change");
  $(".category-radio").change(function ()
  {
    showSelectedCategoryBreadCrumb();
    getProductCategorySpecifications($(this));
  });

  $("input[data-img-preview]:not([multiple])").unbind("change");
  $("input[data-img-preview]:not([multiple])").change(function ()
  {
    const guid = $(this).attr("data-img-preview");
    const file = $(this).prop("files")[0];
    const imageElement = $(`img[data-img-preview="${guid}"]`);

    if (file)
    {
      imageElement.attr("src", URL.createObjectURL(file));
      imageElement.show();
      URL.revokeObjectURL(file);
    }
  });

  if ($("table.productGalleryImagesTable")[0])
  {
    const productGalleryImagesTable = $("table.productGalleryImagesTable tbody");
    Sortable.create(productGalleryImagesTable[0], {
      animation: 150,
      direction: "vertical",
      handle: "tr",
      onEnd: function (e)
      {
        resetGalleryImagesIndexes(productGalleryImagesTable, ".product-gallery-image");
      }
    });
  }

  if ($(".galleryImagesList")[0])
  {
    const productGalleryImages = $(".galleryImagesList");
    Sortable.create(productGalleryImages[0], {
      animation: 150,
      onEnd: function (e)
      {
        resetGalleryImagesIndexes(productGalleryImages, ".galleryImageItem");
        turnImagesIntoInputFiles(productGalleryImages, ".galleryImageItem img",
          ".galleryImagesContainer input[multiple]");
      }
    });
  }

  $(".imgOptions input[data-img-preview]:not([multiple])").change(function ()
  {
    const itemsList = $(`.galleryImagesList[data-img-preview-list]`);
    resetGalleryImagesIndexes(itemsList, ".galleryImageItem");
    turnImagesIntoInputFiles(itemsList, ".galleryImageItem img", ".galleryImagesContainer input[multiple]");
  });

  setupReplaceAllGalleryImagesButton();
  setupAddToGalleryImagesButton();
}

function setupReplaceAllGalleryImagesButton()
{
  $(".galleryImagesContainer input[data-img-preview][multiple][data-replace]").unbind("change");
  $(".galleryImagesContainer input[data-img-preview][multiple][data-replace]").change(function ()
  {
    const guid = $(this).attr("data-img-preview");
    const itemsList = $(`[data-img-preview-list="${guid}"]`);
    const item = itemsList.find("[data-img-preview-item]:first-child").clone();

    const files = $(this).prop("files");
    if (files)
    {
      itemsList.html("");
      for (let i = 0; i < files.length; i++)
      {
        const itemClone = item.clone().show();
        const img = itemClone.find(`img`);
        img.attr("src", URL.createObjectURL(files[i]));
        img.attr("data-img-name", files[i].name);
        img.show();
        URL.revokeObjectURL(files[i]);
        itemClone.appendTo(itemsList);
      }
      setupProductImagesEventListeners();
      resetGalleryImagesIndexes(itemsList, `[data-img-preview-item]`);
    }
  });
}

function setupAddToGalleryImagesButton()
{
  $(".galleryImagesContainer input[data-img-preview][multiple][data-add]").unbind("change");
  $(".galleryImagesContainer input[data-img-preview][multiple][data-add]").change(function ()
  {
    const guid = $(this).attr("data-img-preview");
    const itemsList = $(`[data-img-preview-list="${guid}"]`);
    const item = itemsList.find("[data-img-preview-item]:first-child").clone();
    itemsList.find(`input:hidden`).remove();

    const files = $(this).prop("files");
    if (files)
    {
      itemsList.find(".galleryImageItem:hidden").remove();
      for (let i = 0; i < files.length; i++)
      {
        const itemClone = item.clone().show();
        const img = itemClone.find(`img`);
        img.attr("src", URL.createObjectURL(files[i]));
        img.attr("data-img-name", files[i].name);
        img.show();
        URL.revokeObjectURL(files[i]);
        itemClone.appendTo(itemsList);
      }
      setupProductImagesEventListeners();
      resetGalleryImagesIndexes(itemsList, `[data-img-preview-item]`);
      turnImagesIntoInputFiles(itemsList, ".galleryImageItem img", ".galleryImagesContainer input[multiple]");
    }
  });
}