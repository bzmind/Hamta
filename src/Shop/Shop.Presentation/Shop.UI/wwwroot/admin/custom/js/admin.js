var quills = [];
$(document).ready(function ()
{
  setupEventListeners();
  getProductCategorySpecifications($(".category-radio:checked"));
  showSelectedCategoryBreadCrumb();
  setProductImagesFiles();
  setupQuillEditors();
  setupColorSearch();
  setupValidationErrors();
});

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
    fillQuillEditorsContent();
  setupQuillImageUploaderEventListeners(quills);
}

function setupQuillImageUploaderEventListeners(quills)
{
  $("form button:submit").on("click", async function ()
  {
    for (const quill of quills)
    {
      await setupQuillImageUploader(quill);
    }
    $(this).parents("form").submit();
  });
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

$("#productForm").submit(function ()
{
  const productIntroductionQuill = Quill.find($("#productIntroductionQuill")[0]);
  $("#productIntroduction")[0].value = productIntroductionQuill.container.firstChild.innerHTML;

  const productReviewQuill = Quill.find($("#productReviewQuill")[0]);
  $("#productReview")[0].value = productReviewQuill.container.firstChild.innerHTML;
});

function getProductCategorySpecifications(currentSelectedRadioInput)
{
  if (currentSelectedRadioInput.length === 0 || $(".input-validation-error").length > 0
    || $(".category-specifications").length === 0)
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
      setupEventListeners();
    });
}

function setupEventListeners()
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
    const imageElement = $(`img[data-img-preview=${guid}]`);

    if (file)
    {
      imageElement.attr("src", URL.createObjectURL(file));
      imageElement.show();
      URL.revokeObjectURL(file);
    }
  });

  setupGalleryImagesEventListeners();
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