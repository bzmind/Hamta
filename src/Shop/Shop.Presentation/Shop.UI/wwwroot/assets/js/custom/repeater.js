$(document).ready(function ()
{
  $("[data-repeater-container]").repeater();
});

$.fn.repeater = function ()
{
  $(this).each(function ()
  {
    const $container = $(this);
    const $list = $container.find("[data-repeater-list]");
    const $creater = $container.find("[data-repeater-create]");
    const $cachedItem = $list.find("[data-repeater-item]").last();
    setupRemovers($container);

    $creater.on("click", function ()
    {
      const $item = $list.find("[data-repeater-item]").last();
      removeExtraCheckboxes($item);
      let clone;
      if ($item.length === 0)
        clone = $cachedItem.clone();
      else
      {
        clone = $item.clone();
        setupIndexes(clone);
      }
      clone.css("display", "none");
      clone.appendTo($list).slideDown(200, () =>
      {
        setupRemovers($container);
      });
    });
  });

  function setupRemovers(container)
  {
    const $removers = container.find("[data-repeater-remove]");

    $removers.each(function (index, remover)
    {
      $(remover).on("click", function ()
      {
        const $lastItemInList = $(this).closest("[data-repeater-item]");
        removeExtraCheckboxes($lastItemInList);
        $lastItemInList.slideUp(200, () =>
        {
          $lastItemInList.remove();
        });
      });
    });
  };

  function removeExtraCheckboxes($item)
  {
    const mainCheckboxes = $item.find("input[type='checkbox']");
    mainCheckboxes.each((index, checkbox) =>
    {
      $item.parents("[data-repeater-container]").find("input[type='hidden']")
        .each((innerIndex, hiddenCheckbox) =>
        {
          const checkboxNameAttr = checkbox.getAttribute("name");
          const hiddenCheckboxNameAttr = hiddenCheckbox.getAttribute("name");
          if (checkboxNameAttr === hiddenCheckboxNameAttr)
            hiddenCheckbox.remove();
        });
    });
  }

  function setupIndexes(clone)
  {
    // Will grab the number between brackets: [0] => 0
    const regex = /(?<=\[).+?(?=\])/g;

    clone.find("[name]").each(function (elementIndex, element)
    {
      const attr = element.getAttribute("name");
      const index = parseInt(attr.match(regex));
      const newAttr = attr.replace(regex, index + 1);
      element.setAttribute("name", newAttr);
    });

    clone.find("[data-valmsg-for]").each(function (elementIndex, element)
    {
      const attr = element.getAttribute("data-valmsg-for");
      const index = parseInt(attr.match(regex));
      const newAttr = attr.replace(regex, index + 1);
      element.setAttribute("data-valmsg-for", newAttr);
    });

    // Will grab the last number(s) in a string: customCheckBox4 => 4
    const grabLastNumberRegex = /(\d+)(?!.*\d)/;
    clone.find("[for]").each(function (forElementIndex, elementWithFor) {
      let forAttr = elementWithFor.getAttribute("for");

      clone.find("[id]").each(function (idElementIndex, elementWithId) {
        let idAttr = elementWithId.getAttribute("id");
        if (forAttr !== idAttr)
          return;

        const forNumber = forAttr.match(grabLastNumberRegex);
        if (forNumber == null) {
          elementWithFor.setAttribute("for", forAttr += 1);
        } else {
          const forIndex = parseInt(forNumber);
          const newForAttr = forAttr.replace(grabLastNumberRegex, forIndex + 1);
          elementWithFor.setAttribute("for", newForAttr);
        }

        const idNumber = idAttr.match(grabLastNumberRegex);
        if (idNumber == null) {
          elementWithId.setAttribute("id", idAttr += 1);
        } else {
          const idIndex = parseInt(idNumber);
          const newIdAttr = idAttr.replace(grabLastNumberRegex, idIndex + 1);
          elementWithId.setAttribute("id", newIdAttr);
        }
      });
    });
  }

  function resetIndexes(itemsList) {
    
  }
};