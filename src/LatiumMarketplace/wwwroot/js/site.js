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

    // myURL was set in View
    function getSubCategoryAjax(categoryId) {
        $.ajax({
            url: myURL,
            type: 'GET',
            contentType: 'application/json; charset=utf8',
            data: { CategoryId : categoryId },
            success: function (result) {
                var subCat = "";
                
                for (var i = 0; i < result.length; ++i) {
                    //if (result[i] && result[i].categoryName) {
                        //var subCatId, subCatName;
                        //subCatName = result[i].categoryName;
                        //alert(result[i].categoryName);
                    //}
                    try {
                        var subCatId, subCatName;
                        subCatId = result[i].categoryId;
                        subCatName = result[i].categoryName;
                        //alert(subCatId + " " + subCatName);
                        subCat += '<option value="' + subCatId + '">' + subCatName + '</option>';
                    } catch (err) {
                        console.log(err);
                    }
                }
                
                
               //(subCat).appendTo("div#subCategoryContainer");
               $("div#subCategoryContainer select").html(subCat);

                console.log("Success");
                console.log(result);
                console.log("Finished");
            },
            error: function (result) {
                alert("Something went wrong");
            }
        });
        console.log("Test Done")
    }

    function getSubCategory() {
        $("#AssetCategories").change(function () {
            $("select#AssetCategories option:selected").each(function () {
                var currentCat = $(this).val();
                if (currentCat != '') {
                    getSubCategoryAjax(currentCat);
                }
            });
        });
    }
    getSubCategory();


});

