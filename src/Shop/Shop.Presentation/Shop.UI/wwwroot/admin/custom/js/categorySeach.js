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
  if (selectedCategoryRow.length === 0) {
    const categoryBreadCrumb = $(".category-breadcrumb");
    categoryBreadCrumb.html("");
    categoryBreadCrumb.append($(`<li class="breadcrumb-item">دسته‌بندی انتخاب نشده است</li>`));
    return;
  }
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