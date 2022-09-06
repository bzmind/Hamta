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
  const minInputValue = document.getElementById("slider-input-min-value");
  const maxInputValue = document.getElementById("slider-input-max-value");

  sliderWithInput.noUiSlider.on("update", function (values, handle)
  {
    const value = values[handle];
    if (handle) {

      const formattedValue = numeral(parseInt(value)).format("0,0");
      $(maxInput).val(formattedValue);
      $(maxInputValue).val(parseInt(value));
    }
    else
    {
      const formattedValue = numeral(parseInt(value)).format("0,0");
      $(minInput).val(formattedValue);
      $(minInputValue).val(parseInt(value));
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