function setupImageCroppers()
{
  const images = $("img[data-crop]");
  images.each((i, image) =>
  {
    createCropper(image);
    $(image).on("load", function ()
    {
      $(image).parent().css("border", "0");
      createCropper(image);
    });
  });
}

function createCropper(image)
{
  if ($(image).hasAttr("data-crop") && image.cropper != null)
    image.cropper.destroy();

  const cropper = new Cropper(image, {
    minContainerWidth: 491,
    minContainerHeight: 245.5,
    aspectRatio: 2 / 1,
    autoCropArea: 1,
    zoomable: false
  });
}

function setupCroppersForUpload()
{
  const defaultType = "image/jpeg";
  $("form button:submit").on("click", function (e)
  {
    e.preventDefault();
    const cropImages = $("img[data-crop]");
    cropImages.each((i, image) =>
    {
      const cropAttrValue = $(image).attr("data-crop");
      image.cropper.crop();
      image.cropper.getCroppedCanvas().toBlob((blob) =>
      {
        const metadata = {
          type: blob.type || defaultType,
          size: blob.size
        };
        const fileName = `banner.${blob.type.replace("image/", "") || "jpg"}`;
        const file = new File([blob], fileName, metadata);
        const input = $(`input[data-img-preview="${cropAttrValue}"]`);

        const dataTransfer = new DataTransfer();
        dataTransfer.items.add(file);
        input[0].files = dataTransfer.files;

        $(image).parents("form").submit();
      });
    });
  });
}