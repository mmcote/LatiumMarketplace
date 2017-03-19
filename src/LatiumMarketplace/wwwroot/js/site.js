// Write your Javascript code.


$(document).ready(function () {
    // Show rates based on radio button selection
    // If asset is for sale or for rent
    function chooseAssetOption () {
    $("input[name=assetCreateOptions]").click(function () {
        var inputValue = $(this).attr("value");
        var targetElement = $("#" + inputValue);
        $(".asset-rates").not(targetElement).hide();
        $(targetElement).show();
    });
    }
    chooseAssetOption();
});