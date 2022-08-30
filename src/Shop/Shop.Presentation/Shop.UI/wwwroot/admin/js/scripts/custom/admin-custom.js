$(document).ready(function ()
{
  if ($(".category-radio:checked").length > 0)
  {
    if ($(".input-validation-error").length > 0)
      return;
    getProductCategorySpecifications($(".category-radio:checked"));
  }

  showSelectedCategoryBreadCrumb();
  setProductImagesFiles();

  const editors = $(".quill-editor-container");
  const toolbars = $(".quill-editor-toolbar-container");
  var quill;
  for (let i = 0; i < editors.length; i++)
  {
    quill = new Quill(editors[i], {
      modules: {
        syntax: true,
        toolbar: toolbars[i]
      },
      theme: "snow"
    });
  }

  if (editors.length > 0)
    fillQuillEditorsContent();
});

function setProductImagesFiles()
{
  if ($(".galleryImagesList").length > 0)
    turnImagesIntoInputFiles($(".galleryImagesList"), ".galleryImageItem img",
      ".galleryImagesContainer input[multiple]");
  if ($(".mainImageContainer").length > 0)
    turnImagesIntoInputFiles($(".mainImageContainer"), ".mainImage img",
      ".mainImageContainer input");
}

function fillQuillEditorsContent()
{
  const productIntroduction = $("#productIntroduction");
  const productIntroductionQuill = Quill.find($("#productIntroductionQuill")[0]);
  if (productIntroduction.length > 0 && productIntroductionQuill != null)
  {
    const quillContent = productIntroductionQuill.clipboard.convert(productIntroduction.val());
    productIntroductionQuill.setContents(quillContent, "silent");
  }

  const productReview = $("#productReview");
  const productReviewQuill = Quill.find($("#productReviewQuill")[0]);
  if (productReview.length > 0 && productReviewQuill != null)
  {
    const quillContent = productReviewQuill.clipboard.convert(productReview.val());
    productReviewQuill.setContents(quillContent, "silent");
  }
}

function getGuid()
{
  return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
    (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
  );
}

$.fn.attrContains = function (attrName, string) { return $(this).attr(attrName).indexOf(string) > -1; };

$.fn.isVisible = function () { return $(this).css("display") !== "none"; };

$.fn.isHidden = function () { return $(this).css("display") === "none"; };

$("#productForm").submit(function ()
{
  const productIntroductionQuill = Quill.find($("#productIntroductionQuill")[0]);
  $("#productIntroduction")[0].value = productIntroductionQuill.container.firstChild.innerHTML;

  const productReviewQuill = Quill.find($("#productReviewQuill")[0]);
  $("#productReview")[0].value = productReviewQuill.container.firstChild.innerHTML;
});

function setupRangeSlider(minRange, maxRange, currentMin, currentMax)
{
  var sliderWithInput = document.getElementById("slider-with-input");
  noUiSlider.create(sliderWithInput, {
    start: [currentMin, currentMax],
    direction: "rtl",
    connect: true,
    range: {
      'min': minRange,
      'max': maxRange
    }
  });

  const maxInput = document.getElementById("slider-input-max");
  const minInput = document.getElementById("slider-input-min");
  const maxInputValue = document.getElementById("slider-input-max-value");
  const minInputValue = document.getElementById("slider-input-min-value");

  sliderWithInput.noUiSlider.on("update", function (values, handle)
  {
    const value = values[handle];
    if (handle)
    {
      maxInput.value = numeral(value).format("0,0");
      maxInput.setAttribute("value", value);
      maxInputValue.setAttribute("value", parseInt(value));
    } else
    {
      minInput.value = numeral(value).format("0,0");
      minInput.setAttribute("value", value);
      minInputValue.setAttribute("value", parseInt(value));
    }
  });

  minInput.addEventListener("input", function ()
  {
    const value = parseInt(numeral(this.value).value());
    sliderWithInput.noUiSlider.set([value, null]);
  });

  maxInput.addEventListener("input", function ()
  {
    const value = parseInt(numeral(this.value).value());
    sliderWithInput.noUiSlider.set([null, value]);
  });

  minInput.addEventListener("input", function ()
  {
    this.value = numeral(this.value).format("0,0");
  });

  maxInput.addEventListener("input", function ()
  {
    this.value = numeral(this.value).format("0,0");
  });
}

function showCategory(category)
{
  if (hasSubCategory(category))
  {
    toggleParentCategoryIconAndClass(category);
    const subCategories = getSubCategories(category);
    subCategories.toggle();
  }
}

function hideSubCategories(parentCategory)
{
  if (parentCategory.isHidden() || !hasSubCategory(parentCategory) || !hasVisibleSubCategories(parentCategory))
    return;

  toggleParentCategoryIconAndClass(parentCategory);

  const subCategories = getSubCategories(parentCategory);
  subCategories.each((index, subCategory) =>
  {
    subCategory = $(subCategory);
    if (subCategory.isHidden())
      return;
    if (hasSubCategory(subCategory))
      hideSubCategories(subCategory);
    subCategory.toggle();
  });
}

function toggleParentCategoryIconAndClass(category)
{
  category.toggleClass("open");
  category.find("i.bx").toggleClass("bx bx-caret-up bx bx-caret-down");
}

function getSubCategories(parentCategory)
{
  return parentCategory.nextAll(`[data-parentCategoryId='${parentCategory.attr("data-categoryId")}']`);
}

$("[data-hasSubCategory='true']").click(function ()
{
  if ($(this).attr("class").indexOf("open") <= -1)
  {
    showCategory($(this));
  } else
  {
    hideSubCategories($(this));
  }
});

$("#category-search").on("input", function ()
{
  const searchText = $(this).val();
  const categoryTable = $(".category-table");
  clearPreviousSearchedCategories(categoryTable);

  const visibleParentCategories = categoryTable.find("tr.parent:visible");
  const categories = categoryTable.find(`td:contains(${searchText})`).find(".category-title");

  if ($.trim(searchText).length === 0 || categories.length === 0)
  {
    visibleParentCategories.each((i, parentCategory) =>
    {
      hideSubCategories($(parentCategory));
    });
    return;
  }
  categories.attr("data-searched", "true");
  categories.each((index, category) =>
  {
    category = $(category);
    const categoryNearestParent = category.parents("tr");
    toggleMajorCategoryParents(categoryNearestParent);
  });
});

function clearPreviousSearchedCategories(categoryTable)
{
  const previousSearches = categoryTable.find("[data-searched]");
  previousSearches.each((i, el) =>
  {
    $(el).removeAttr("data-searched");
  });

  const visibleParentCategories = categoryTable.find("tr.parent:visible");
  visibleParentCategories.each((i, parentCategory) =>
  {
    hideSubCategories($(parentCategory));
  });
}

function toggleMajorCategoryParents(category)
{
  if (category.attrContains("class", "parent"))
    toggleParentCategoryIconAndClass(category);

  if (hasSubCategory(category) && hasVisibleSubCategories(category) && !category.attrContains("class", "open"))
    toggleParentCategoryIconAndClass(category);

  if (category.isVisible())
    return;

  category.toggle();

  const parent = getCategoryParent(category);
  if (parent.length > 0)
    toggleMajorCategoryParents(parent);
}

function hasVisibleSubCategories(parentCategory)
{
  if (!hasSubCategory(parentCategory))
    return false;
  return getSubCategories(parentCategory).filter(":visible").length > 0;
}

function getCategoryParent(category)
{
  return category.prevAll(`[data-categoryId="${category.attr("data-parentCategoryId")}"]`);
}

function hasSubCategory(category)
{
  return category.attr("[data-hasSubCategory='true']") == null;
}

function showSelectedCategoryBreadCrumb()
{
  const selectedCategoryRow = $(".category-radio:checked").parents().closest("tr");
  if (selectedCategoryRow.length === 0)
    return;
  const majorParents = getCategoryMajorParents(selectedCategoryRow).reverse();
  majorParents.push(selectedCategoryRow);
  const categoryBreadCrumb = $(".category-breadcrumb");
  categoryBreadCrumb.html("");

  majorParents.forEach(majorParent =>
  {
    categoryBreadCrumb.append($(`<li class="breadcrumb-item"><span>${majorParent.text()}</span></li>`));
  });
}

function getCategoryMajorParents(category, previousMajorParentsArray = [])
{
  const previousRows = category.prevAll("tr");
  previousRows.each((i, row) =>
  {
    row = $(row);
    if (row.attr("data-categoryId") === category.attr("data-parentCategoryId"))
    {
      previousMajorParentsArray.push(row);
      getCategoryMajorParents(row, previousMajorParentsArray);
    }
  });
  return previousMajorParentsArray;
}

function getProductCategorySpecifications(currentSelectedRadioInput)
{
  const categoryId = currentSelectedRadioInput.val();
  sendAjaxGetWithRouteData(`${$(location).attr("pathname")}/showCategorySpecifications?categoryId=${categoryId}`)
    .then((result) =>
    {
      checkResult(result);
      $(".category-specification").remove();
      prependAjaxResultToElement(".category-specifications", result);
      resetIndexes($(".category-specifications"), "[data-repeater-item]");
      reinitializeScripts();
    });
  showSelectedCategoryBreadCrumb();
}

function setupEventListeners()
{
  $(".category-radio").unbind("change");
  $(".category-radio").change(function ()
  {
    getProductCategorySpecifications($(this));
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

  $("input[data-img-preview]:not([multiple])").unbind("change");
  $("input[data-img-preview]:not([multiple])").change(function ()
  {
    const guid = $(this).attr("data-img-preview");
    const file = $(this).prop("files")[0];
    const imageElement = $(`img[data-img-preview=${guid}]`);

    if (file)
    {
      imageElement.attr("src", URL.createObjectURL(file));
      imageElement.show();
      URL.revokeObjectURL(file);
    }
  });

  $(".imgOptions input[data-img-preview]:not([multiple])").change(function ()
  {
    const itemsList = $(`.galleryImagesList[data-img-preview-list]`);
    resetGalleryImagesIndexes(itemsList, ".galleryImageItem");
    turnImagesIntoInputFiles(itemsList, ".galleryImageItem img", ".galleryImagesContainer input[multiple]");
  });

  $(".galleryImagesContainer input[data-img-preview][multiple][data-replace]").unbind("change");
  $(".galleryImagesContainer input[data-img-preview][multiple][data-replace]").change(function ()
  {
    const guid = $(this).attr("data-img-preview");
    const itemsList = $(`[data-img-preview-list="${guid}"]`);
    const item = itemsList.find("[data-img-preview-item]:first-child").clone();
    item.find("input:hidden").remove();

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
      resetGalleryImagesIndexes(itemsList, `[data-img-preview-item]`);
    }
  });

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
      resetGalleryImagesIndexes(itemsList, `[data-img-preview-item]`);
      turnImagesIntoInputFiles(itemsList, ".galleryImageItem img", ".galleryImagesContainer input[multiple]");
    }
  });
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

function turnImagesIntoInputFiles(imagesContainer, imageSelector, inputToAddFilesTo)
{
  let currentPromise = Promise.resolve([]);

  imagesContainer.find(imageSelector).each((i, img) =>
  {
    img = $(img);
    const imageName = img.attr("data-img-name");
    const blobUrl = img.attr("src");
    if (blobUrl === "")
      return;

    currentPromise = currentPromise.then(allFiles =>
    {
      return createFile(blobUrl, imageName).then(file =>
      {
        allFiles.push(file);
        return allFiles;
      });
    });
  });

  currentPromise.then(allFiles =>
  {
    const fileList = new DataTransfer();
    for (let i = 0; i < allFiles.length; i++)
    {
      fileList.items.add(allFiles[i]);
    }
    $(inputToAddFilesTo)[0].files = fileList.files;
  });
}

async function createFile(blobUrl, fileName)
{
  const defaultType = "image/jpeg";

  const options = {
    mode: "cors",
    cache: "no-store"
  };

  return await fetch(blobUrl, options).then(async res =>
  {
    const data = await res.blob();
    const metadata = {
      type: data.type || defaultType,
      size: data.size
    };
    const file = new File([data], fileName, metadata);
    return file;
  });
}

function deleteGalleryImage(e)
{
  $(e.target).parents(".galleryImageItem").remove();
  const itemsList = $(".galleryImagesList");
  resetGalleryImagesIndexes(itemsList, ".galleryImageItem");
  turnImagesIntoInputFiles(itemsList, ".galleryImageItem img", ".galleryImagesContainer input[multiple]");
}