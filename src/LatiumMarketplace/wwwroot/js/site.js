// Write your Javascript code.


// Show rates based on radio button selection
// If asset is for sale or for rent
$(document).ready(function () {
    $("input[name=assetCreateOptions]").click(function () {
        var inputValue = $(this).attr("value");
        var targetElement = $("#" + inputValue);
        $(".asset-rates").not(targetElement).hide();
        $(targetElement).show();
    });
});