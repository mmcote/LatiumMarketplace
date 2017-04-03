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

    function postMainCategory(categoryID) {

        $.ajax({
            url: '/GetSubCategories/AssetsAPIController',
            type: "POST",
            contentType: "application/json",
            data: { CategoryId: categoryID },
            success: function (data) {
                alert("HI");
                console.log("Success");

                console.log("Finished");
            },
            error: function (error) {
                var x = error; //break here for debugging.
            }
        });
        console.log("Test Done")
    }

    function getSubCategory() {
        $("#AssetCategories").change(function () {
            $("select#AssetCategories option:selected").each(function () {
                var currentCat = $(this).val();
                if (currentCat != '') {
                    alert("Before postMain");
                    postMainCategory(currentCat);
                }
            });
        });
    }
    getSubCategory();


});

