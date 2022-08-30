var quill;
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
  for (let i = 0; i < editors.length; i++)
  {
    quill = new Quill(editors[i], {
      modules: {
        syntax: true,
        toolbar: toolbars[i]
      },
      theme: "snow"
    });
    setupQuillImageUploader(quill);
  }

  if (editors.length > 0)
    fillQuillEditorsContent();
});

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