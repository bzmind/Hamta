$(document).ready(function ()
{
  $("[data-repeater-container]").repeater();
});

$.fn.repeater = function ()
{
  $(this).each(function ()
  {
    const container = $(this);
    const list = container.find("[data-repeater-list]");
    const creator = container.find("[data-repeater-create]");
    const lastItem = list.find("[data-repeater-item]").last();
    const lastItemClone = lastItem.clone();
    removeExtraCheckboxes(lastItem);
    setupRemovers(container);
    removeDefaultItem(list);

    creator.on("click", function ()
    {
      let item = list.find("[data-repeater-item]").last();
      removeExtraCheckboxes(item);
      if (item.length === 0)
        item = lastItemClone;
      else
        item = item.clone();

      appendItemToList(item, list, container);
    });
  });
};

function appendItemToList(item, list, container)
{
  item.find("[type='text']").val("");
  item.removeAttr("style");
  item.find("[data-item-id]").remove();
  item.css("display", "none");
  item.appendTo(list).slideDown(200, () =>
  {
    setupRemovers(container);
    resetIndexes(list, "[data-repeater-item]");
    reinitializeScripts();
  });
}

function setupRemovers(container)
{
  const removers = container.find("[data-repeater-remove]");
  removers.each(function (index, remover)
  {
    $(remover).on("click", function ()
    {
      const item = $(this).closest("[data-repeater-item]");
      const itemsList = item.parents("[data-repeater-list]");
      removeExtraCheckboxes(item);
      item.slideUp(200, () =>
      {
        item.remove();
        resetIndexes(itemsList, "[data-repeater-item]");
      });
    });
  });
};

function removeExtraCheckboxes(item)
{
  const mainCheckboxes = item.find("input[type='checkbox']");
  mainCheckboxes.each((index, mainCheckbox) =>
  {
    item.parents("[data-repeater-container]").find("input[type='hidden']").each((innerIndex, hiddenCheckbox) =>
    {
      const checkboxNameAttr = mainCheckbox.getAttribute("name");
      const hiddenCheckboxNameAttr = hiddenCheckbox.getAttribute("name");
      if (checkboxNameAttr === hiddenCheckboxNameAttr)
        hiddenCheckbox.remove();
    });
  });
}

function removeDefaultItem(itemsList)
{
  const items = itemsList.find("[data-repeater-item]");
  if (items.length !== 1)
    return;
  const firstItem = items.first();
  const inputsInFirstItem = firstItem.find("input[type=text], textarea");
  let hasInputWithValue = false;
  inputsInFirstItem.each((i, input) =>
  {
    input = $(input);
    if (input.val() != "")
      hasInputWithValue = true;
  });
  if (hasInputWithValue)
    return;
  firstItem.remove();
}

function resetIndexes(itemsList, itemSelector)
{
  const items = itemsList.find(itemSelector);
  for (let i = 0; i < items.length; i++)
  {
    const item = $(items[i]);
    // Will grab the number between brackets: [0] => 0
    const regex = /(?<=\[).+?(?=\])/g;
    item.find("[name]").each(function (elementIndex, element)
    {
      const attr = element.getAttribute("name");
      const newAttr = attr.replace(regex, i);
      element.setAttribute("name", newAttr);
    });

    item.find("[data-valmsg-for]").each(function (elementIndex, element)
    {
      const attr = element.getAttribute("data-valmsg-for");
      const newAttr = attr.replace(regex, i);
      element.setAttribute("data-valmsg-for", newAttr);
    });

    // Will grab the last number(s) in a string: customCheckBox4 => 4
    const grabLastNumberRegex = /(\d+)(?!.*\d)/;
    item.find("[for]").each(function (forElementIndex, elementWithFor)
    {
      const forAttr = elementWithFor.getAttribute("for");

      item.find("[id]").each(function (idElementIndex, elementWithId)
      {
        const idAttr = elementWithId.getAttribute("id");
        if (forAttr !== idAttr)
          return;

        const forNumber = forAttr.match(grabLastNumberRegex);
        if (forNumber == null)
        {
          elementWithFor.setAttribute("for", i);
        } else
        {
          const newForAttr = forAttr.replace(grabLastNumberRegex, i);
          elementWithFor.setAttribute("for", newForAttr);
        }

        const idNumber = idAttr.match(grabLastNumberRegex);
        if (idNumber == null)
        {
          elementWithId.setAttribute("id", i);
        } else
        {
          const newIdAttr = idAttr.replace(grabLastNumberRegex, i);
          elementWithId.setAttribute("id", newIdAttr);
        }
      });
    });
  }
}