$(document).ready(function () {
    /* Show rates based on radio button selection. If asset is for sale or for rent */
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

    /* Add accessory item to fom submission */
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

    /* Helper function to get subcategories of a selected category from DB */
    function getSubCategoryAjax(categoryId) {
        // myURL was set in View
        $.ajax({
            url: myURL,
            type: 'GET',
            contentType: 'application/json; charset=utf8',
            data: { CategoryId : categoryId },
            success: function (result) {
                var subCat = '<option value>--- Select a sub-category ---</option>'; // Holds all the subcategories option menues
                
                for (var i = 0; i < result.length; ++i) {
                    try {
                        var subCatId, subCatName;
                        subCatId = result[i].categoryId;
                        subCatName = result[i].categoryName;
                        subCat += '<option value="' + subCatId + '">' + subCatName + '</option>';
                    } catch (err) {
                        console.log(err);
                    }
                }
                $("div#subCategoryContainer select").html(subCat);
                
                // Debug messages
                console.log("Success");
                console.log(result);
                console.log("Finished");
            },
            error: function (result) {
                console.log("Something went wrong");
            }
        });
    }

    /* Get subcategories and display them in form */
    function getSubCategory() {
        $("div#subCategoryContainer").hide();

        $("#AssetCategories").change(function () {
            $("select#AssetCategories option:selected").each(function () {
                var currentCat = $(this).val();
                if (currentCat != '') {
                    $("div#subCategoryContainer").show();
                    getSubCategoryAjax(currentCat);
                }
                else {
                    $("div#subCategoryContainer").hide();
                }
            });
        });
    }
    getSubCategory();


});

