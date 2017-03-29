// Write your Javascript code.
//function updateMessageNotificationCount(URLstring, notificationUserName) {
//    console.log(URLstring);
//    console.log(notificationUserName);
//    $.ajax({
//        url: URLstring,
//        type: "POST",
//        contentType: "application/json",
//        data: JSON.stringify(notificationUserName),
//        success: function (messageCount) {
//            messageNotificationCount = messageCount;
//            if (messageNotificationCount != 0) {
//                $('#messageNotificationCount').html(messageNotificationCount);
//            }
//        },
//        error: function (error) {
//            console.log(error)
//            var x = error; //break here for debugging.
//        }
//    });
//}

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