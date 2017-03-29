
$(document).ready(function () {
    // Show rates based on radio button selection
    // If asset is for sale or for rent 
    function chooseAssetOption() {
        $("#IsForRent").hide();
        $("input[name=assetCreateOptions]")
        .change(function () {
            if (this.value === "IsForSale") {
                $("#IsForRent").css("display", "none");
                $("#IsForSale").show();
            }
            if (this.value === "IsForRent") {
                $("#IsForSale").css("display", "none");
                $("#IsForRent").show();
            }
        });
    }
    chooseAssetOption();
    
    function addAccessoryItem() {
        var maxFields = 10; // Max number of accessories that can be added
        var count = 1;
        $("#add-accessory-item").click(function (e) {
            e.preventDefault();
            if (count < maxFields) {
                count++;
                var item = $(".accessory-item:first").clone().appendTo("#accessories-container");
                // Give an id to the accessory
                item.attr("id", "accessory-item" + count);
            }
        });
        // Give the first accessory an id
        $(".accessory-item:first").attr("id", "accessory-item1");
    }
    addAccessoryItem();

});

