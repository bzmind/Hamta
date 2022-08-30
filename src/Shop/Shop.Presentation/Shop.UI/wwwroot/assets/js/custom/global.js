$(document).ready(function ()
{
  if ($(".pace"))
    $(".pace").remove();
  checkForAlertCookies();
  $("form").each(function ()
  {
    if ($(this).data("validator"))
      $(this).data("validator").settings.ignore = ".quill-editor-container *";
  });
  reinitializeScripts();
});

function setupPasswordInputsVisibilityToggle()
{
  const eyeIcons = $(".fa-eye-slash");
  eyeIcons.each((index, input) =>
  {
    $(input).on("click", (e) =>
    {
      const icon = e.target;
      $(icon).toggleClass("fa-eye fa-eye-slash");

      const input = e.target.nextElementSibling;
      if (input.type === "password")
      {
        input.type = "text";
      } else
      {
        input.type = "password";
      }
    });
  });
}

function checkForAlertCookies()
{
  var cookie = getCookie("alert");
  if (cookie)
  {
    document.querySelector("#alerts").style.display = "block";
    cookie = JSON.parse(cookie);
    Alert(cookie);
    deleteCookie("alert");
  }
}

function getCookie(cookieName)
{
  const name = cookieName + "=";
  const decodedCookie = decodeURIComponent(document.cookie);
  const ca = decodedCookie.split(";");
  for (let i = 0; i < ca.length; i++)
  {
    let c = ca[i];
    while (c.charAt(0) == " ")
    {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0)
    {
      return decodeURIComponent(c.substring(name.length, c.length));
    }
  }
  return "";
}

function deleteCookie(cookieName)
{
  document.cookie = `${cookieName}=;expires=Thu, 01 Jan 1970;path=/`;
}

function Alert(alert)
{
  setAlertMessage(alert.MetaData.Message, alert.IsSuccessful);
  removeAlert(10000);
}

function setAlertMessage(message, isSuccessful)
{
  let alerts = document.querySelector("#alerts");
  if (alerts == null)
    $('<div id="alerts"></div>').appendTo("body");
  alerts = document.querySelector("#alerts");

  const ul = document.createElement("ul");
  if (isSuccessful === true)
    ul.setAttribute("class", "alert alert-primary");
  else
    ul.setAttribute("class", "alert alert-danger");

  const lastChar = message.charAt(message.length - 1);
  if (lastChar !== ".")
    message = message + ".";

  ul.textContent = message;
  alerts.appendChild(ul);
}

function removeAlert(delay)
{
  setTimeout(() =>
  {
    const alerts = document.querySelector("#alerts");
    const alertsUl = document.querySelector("#alerts ul:last-child");
    const allAlertUls = document.querySelectorAll("#alerts ul");
    alertsUl.remove();
    if (allAlertUls.length <= 1)
      alerts.style.display = "none";
  }, delay);
}

function submitFormWithAjaxAndReplaceElement(e, elementSelector)
{
  e.preventDefault();
  const form = $(e.target);
  const url = form.attr("action");
  const formData = new FormData(e.target);

  if (form.valid() === false)
    return false;

  sendAjaxPost(url, formData).then(function (result)
  {
    checkResult(result);
    replaceElementWithAjaxResult(elementSelector, result);
    reinitializeScripts();
  });

  return false;
}

function sendAjaxPost(url, data)
{
  return new Promise(function (resolve)
  {
    $.ajax({
      url: url,
      type: "POST",
      data: data,
      enctype: "multipart/form-data",
      processData: false,
      contentType: false,
      beforeSend: function ()
      {
        $(".loading").show();
      },
      complete: function ()
      {
        $(".loading").hide();
      },
      success: function (result)
      {
        resolve(result);
      }
    }).always(() =>
    {
      checkForAlertCookies();
    });
  });
}

function checkResult(result)
{
  checkForAlertCookies();
  result = JSON.parse(result);
  if (result.status != undefined && result.status !== 200)
    return;
  if (result.IsRedirection === true)
    window.location.replace(result.RedirectPath);
}

function reinitializeScripts() {
  reinitializeJqueryUnobtrusive();
  reinitializeElementsScripts();
  reinitializeSelect2();
}

function reinitializeJqueryUnobtrusive()
{
  $("form").removeData("validator").removeData("unobtrusiveValidation");
  $.validator.unobtrusive.parse(document);
}

function reinitializeElementsScripts()
{
  setupPasswordInputsVisibilityToggle();
  triggerSelect2ValidationOnChange();
  triggerFileInputsValidationOnChange();
  avatarInputChangeListener();
  setupEventListeners();
}

function triggerSelect2ValidationOnChange()
{
  $(".select2").on("change", function ()
  {
    $(this).valid();
  });
}

function triggerFileInputsValidationOnChange()
{
  $("input[type='file']").change(function ()
  {
    $(this).valid();
  });
}

function reinitializeSelect2()
{
  try
  {
    const select2 = $(".select2");
    select2.select2();
    select2.on("change", function (e)
    {
      $(this).parent(".form-element-row").find(".input-validation-error")
        .removeClass("input-validation-error");
      $(this).parent(".form-element-row").next(".field-validation-error")
        .removeClass("field-validation-error").html("");
    });
  } catch (e)
  {
    return;
  }
}

function avatarInputChangeListener()
{
  $(".avatar-file-label input").change(function ()
  {
    $(".avatar-file-label").css("border-color", "#5a8dee");
  });
}

function openModal(url, modalTitle, modalSize)
{
  const modalName = "default-modal";
  $(`#${modalName} .modal-body`).html("");

  $.ajax({
    url: url,
    type: "GET",
    beforeSend: function ()
    {
      $(".loading").show();
    },
    complete: function ()
    {
      $(".loading").hide();
    }
  }).always(function (result)
  {
    result = JSON.parse(result);
    if (result.Data)
    {
      $(`#${modalName} .modal-body`).html(result.Data);
      $(`#${modalName} .modal-title`).html(modalTitle);

      $(`#${modalName}`).modal({
        backdrop: "dark",
        keyboard: true
      }, "show");

      $(`#${modalName} .modal-dialog`).removeClass("modal-lg modal-xl modal-sm modal-full");
      $(`#${modalName} .modal-dialog`).addClass(modalSize);

      reinitializeScripts();
    }
  });
}

function deleteItem(urlWithRouteData)
{
  SweetAlert.fire({
    title: "آیا از حذف اطمینان دارید؟",
    icon: "warning",
    confirmButtonText: "بله مطمئنم",
    cancelButtonText: "خیر",
    showCancelButton: true
  }).then((result) =>
  {
    if (result.isConfirmed)
      sendAjaxPostWithRouteData(urlWithRouteData);
  });
}

function sendAjaxPostWithRouteData(urlWithRouteData)
{
  const antiForgeryToken = $('#antiForgeryToken input[name="__RequestVerificationToken"]').val();
  if (antiForgeryToken == null)
    return;
  const data = new FormData();
  data.append("__RequestVerificationToken", antiForgeryToken);
  sendAjaxPost(urlWithRouteData, data).then((result) =>
  {
    checkResult(result);
  });
}

function sendAjaxGetWithRouteData(urlWithRouteData)
{
  return new Promise(function (resolve)
  {
    $.ajax({
      url: urlWithRouteData,
      type: "GET",
      success: function (result)
      {
        resolve(result);
      }
    }).always(() =>
    {
      checkForAlertCookies();
    });
  });
}

function replaceElementWithAjaxResult(elementSelector, result)
{
  result = JSON.parse(result);
  const replaceElement = $(elementSelector);
  if (result.IsHtml !== true || replaceElement == null)
    return;
  replaceElement.html(result.Data);
}

function prependAjaxResultToElement(elementSelector, result)
{
  result = JSON.parse(result);
  const replaceElement = $(elementSelector);
  if (result.IsHtml !== true || replaceElement == null)
    return;
  replaceElement.prepend(result.Data);
}