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
  console.log(categories);
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

$.fn.attrContains = function (attribute, string)
{
  return $(this).attr(attribute).indexOf(string) > -1;
};

$.fn.isVisible = function ()
{
  return $(this).css("display") !== "none";
};

$.fn.isHidden = function ()
{
  return $(this).css("display") === "none";
};