var quills = [];
$(document).ready(function ()
{
  setupProductImagesEventListeners();
  getProductCategorySpecifications($(".category-radio:checked"));
  showSelectedCategoryBreadCrumb();
  setProductImagesFiles();
  setupQuillEditors();
  setupColorSearch();
  setupValidationErrors();
  setupPriceAndDiscountSummary();
});

function setupPriceAndDiscountSummary()
{
  if ($(`[data-discount]`).length === 0)
    return;

  $("input[data-money]").on("input", function ()
  {
    updatePriceSummary($(this));
  });
  $("input[data-discount]").on("input", function ()
  {
    updateDiscountSummary($(this));
  });
  updatePriceSummary($("input[data-money]"));
  updateDiscountSummary($("input[data-discount]"));
}

function updatePriceSummary(inputs)
{
  inputs.each((i, input) =>
  {
    input = $(input);
    const attrValue = input.attr("data-money");
    const realValue = parseInt(input.val().replace(/,/g, ""));
    const pairField = $(`[data-money-pair='${attrValue}']`);
    const discountInputValue = parseInt($(`[data-discount='${attrValue}']`).val());
    const discountAmountField = $(`[data-discount-amount='${attrValue}']`);
    const finalPriceField = $(`[data-final-price='${attrValue}']`);

    const discountAmount = realValue * discountInputValue / 100;
    const formattedDiscountAmount = numeral(discountAmount).format("0,0");
    const formattedFinalPrice = numeral(realValue - discountAmount).format("0,0");

    pairField.html(input.val());
    discountAmountField.html(formattedDiscountAmount);
    finalPriceField.html(formattedFinalPrice);
  });
}

function updateDiscountSummary(discountInputs)
{
  discountInputs.each((i, discountInput) =>
  {
    discountInput = $(discountInput);
    const attrValue = discountInput.attr("data-discount");
    const priceInput = $(`[data-money='${attrValue}']`);
    const priceValue = parseInt(priceInput.val().replace(/,/g, ""));
    const discountInputValue = parseInt(discountInput.val());
    const discountAmountField = $(`[data-discount-amount='${attrValue}']`);
    const finalPriceField = $(`[data-final-price='${attrValue}']`);

    const discountAmount = priceValue * discountInputValue / 100;
    const formattedDiscountAmount = numeral(discountAmount).format("0,0");
    const formattedFinalPrice = numeral(priceValue - discountAmount).format("0,0");

    discountAmountField.html(formattedDiscountAmount);
    finalPriceField.html(formattedFinalPrice);
  });
}

function setupValidationErrors()
{
  const spans = $("span[data-valmsg-for][data-val-for]");
  const observer = new MutationObserver(function (mutations)
  {
    mutations.forEach(function (mutation)
    {
      const thisSpan = $(mutation.target);
      const errorSource = $(`[data-val-id="${thisSpan.attr("data-val-for")}"]`);
      const className = thisSpan.prop(mutation.attributeName);
      if (strContains(className, "field-validation-error"))
        errorSource.css("border", "1px solid #fc3232");
      else
        errorSource.css("border", "");
    });
  });

  spans.each((i, span) =>
  {
    observer.observe(span,
      {
        attributes: true,
        attributeFilter: ["class"]
      });
  });
}

function setupQuillEditors()
{
  const editors = $(".quill-editor-container");
  const toolbars = $(".quill-editor-toolbar-container");
  for (let i = 0; i < editors.length; i++)
  {
    const quill = new Quill(editors[i], {
      modules: {
        syntax: true,
        toolbar: toolbars[i]
      },
      theme: "snow"
    });
    quills.push(quill);
  }
  if (editors.length > 0)
    initQuillEditorsContent();
  setupQuillImageUploaderEventListeners(quills);
  transferQuillsContentToTheirTextAreasOnTextChange();
}

function setupQuillImageUploaderEventListeners(quills)
{
  $("form button:submit").on("click", async function ()
  {
    $(this).parents("form").valid();
    const errorSpans = $(".field-validation-error");
    const errorsOtherThanQuillErrors = $.grep(errorSpans, (errorSpan, i) =>
    {
      errorSpan = $(errorSpan);
      const previousElements = errorSpan.prevAll(".quill-editor-container");
      if (previousElements.length > 0)
        return false;
      return true;
    });

    if (errorsOtherThanQuillErrors.length > 0)
      return;

    for (const quill of quills)
    {
      await setupQuillImageUploader(quill);
    }
    $(this).parents("form").submit();
  });
}

// To use this, your quills should have a data-quill-id attr, and their text areas should have
// a data-quill-for attr, which should have a matching value like data-quill-id="quillOne" and
// data-quill-for="quillOne"
function initQuillEditorsContent()
{
  const quills = $("[data-quill-id]");
  quills.each((index, quill) =>
  {
    quill = $(quill);
    const quillObject = Quill.find(quill[0]);
    const textArea = $(`[data-quill-for="${quill.attr("data-quill-id")}"]`);
    if (textArea == null)
      return;
    const quillContent = quillObject.clipboard.convert(textArea.val());
    quillObject.setContents(quillContent, "silent");
  });
}

function transferQuillsContentToTheirTextAreasOnTextChange()
{
  const quills = $("[data-quill-id]");
  quills.each((index, quill) =>
  {
    quill = $(quill);
    const quillObject = Quill.find(quill[0]);
    quillObject.on("text-change", function (delta, oldDelta, source)
    {
      const textArea = $(`[data-quill-for="${quill.attr("data-quill-id")}"]`);
      textArea.val(quillObject.container.firstChild.innerHTML);
      textArea.valid();
    });
  });
}

function getProductCategorySpecifications(currentSelectedRadioInput)
{
  if (currentSelectedRadioInput.length === 0 || $(".category-specifications").length === 0)
    return;
  const categoryId = currentSelectedRadioInput.val();
  sendAjaxGetWithRouteData(`${$(location).attr("pathname")}/showCategorySpecifications?categoryId=${categoryId}`)
    .then((result) =>
    {
      checkResult(result);
      $(".category-specification").remove();
      prependAjaxResultToElement(".category-specifications", result);
      resetIndexes($(".category-specifications"), "[data-repeater-item]");
      reinitializeScripts();
      setupProductImagesEventListeners();
    });
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

function setupColorSearch()
{
  $("#product-color-search").on("input", function ()
  {
    const searchText = $(this).val();
    const colors = $("#product-colors");
    const foundColors = $.grep(colors.find(".product-color"), (color, i) =>
    {
      if ($(color).text().toLowerCase().indexOf(searchText.toLowerCase()) !== -1)
        return true;
      return false;
    });
    colors.find(".product-color").hide(50);
    $(foundColors).show(50);
  });
}