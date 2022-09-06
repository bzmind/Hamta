async function setupQuillImageUploader(quill)
{
  // watch for images added:
  const imgs = Array.from(
    quill.container.querySelectorAll('img[src^="data:"]:not(.loading)')
  );
  for (const img of imgs)
  {
    img.classList.add("imgLoading");
    img.setAttribute("src", await uploadBase64Img(img.getAttribute("src")));
    img.classList.remove("imgLoading");
    $(img).show();
  }
}

// wait for upload
async function uploadBase64Img(base64Str)
{
  if (typeof base64Str !== "string" || base64Str.length < 100)
  {
    return base64Str;
  }
  const url = await base64ToUrl(base64Str);
  return url;
}

/**
* Convert a base64 string in a Blob according to the data and contentType.
* 
* @param base64Data {String} Pure base64 string without contentType
* @param contentType {String} the content type of the file i.e (image/jpeg - image/png - text/plain)
* @param sliceSize {Int} SliceSize to process the byteCharacters
* @see http://stackoverflow.com/questions/16245767/creating-a-blob-from-a-base64-string-in-javascript
* @return Blob
*/
function base64toBlob(base64Data, contentType, sliceSize)
{
  contentType = contentType || "";
  sliceSize = sliceSize || 512;

  const byteCharacters = atob(base64Data);
  const byteArrays = [];

  for (let offset = 0; offset < byteCharacters.length; offset += sliceSize)
  {
    const slice = byteCharacters.slice(offset, offset + sliceSize);

    const byteNumbers = new Array(slice.length);
    for (let i = 0; i < slice.length; i++)
    {
      byteNumbers[i] = slice.charCodeAt(i);
    }

    const byteArray = new Uint8Array(byteNumbers);

    byteArrays.push(byteArray);
  }

  const blob = new Blob(byteArrays, { type: contentType });
  return blob;
}

// this is my upload function. I'm converting the base64 to blob for more efficient file 
// upload and so it works with my existing file upload processing
// see here for more info on this approach https://ourcodeworld.com/articles/read/322/how-to-convert-a-base64-image-into-a-image-file-and-upload-it-with-an-asynchronous-form-using-jquery
function base64ToUrl(base64)
{
  return new Promise(resolve =>
  {
    // Split the base64 string in data and contentType
    var block = base64.split(";");
    // Get the content type of the image
    var contentType = block[0].split(":")[1];
    // get the real base64 content of the file
    var realData = block[1].split(",")[1];
    // Convert it to a blob to upload
    var blob = base64toBlob(realData, contentType);
    // create form data
    const formData = new FormData();
    // replace "file_upload" with whatever form field you expect the file to be uploaded to
    formData.append("Image", blob, "blob.jpeg");

    const antiForgeryToken = getAntiForgeryToken();
    if (antiForgeryToken == null)
      reject();
    formData.append("__RequestVerificationToken", antiForgeryToken);

    console.log(formData);
    // replace "/upload" with whatever the path is to your upload handler
    sendAjaxPost(`${$(location).attr("pathname")}/addReviewImage`, formData).then(result =>
    {
      checkResult(result);
      result = JSON.parse(result);
      resolve(result.Data);
    });
  });
}