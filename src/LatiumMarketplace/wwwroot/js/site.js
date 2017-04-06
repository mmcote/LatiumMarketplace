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

        // Show option to add accessories based on button selection. If yes show add button.
        $("#asset-accessories-content-container").hide();
        $("input[name=addAccessoriesOptions]")
        .change(function () {
            if (this.value === "Yes") {
                $("#asset-accessories-content-container").show();

                var maxFields = 10; // Max number of accessories that can be added
                var count = 1;

                var $wrapper = $("#asset-accessories-container");

                $wrapper.on("click", ".btn-add", function (e) {

                    e.preventDefault();

                    if (count < maxFields) {
                        count++;

                        var $content = $("#asset-accessories-content-container");
                        var $currentItem = $(this).parents(".accessory-item:first");
                        var $newItem = $currentItem.clone().appendTo($content);

                        // Clear input fields
                        $newItem.find("input").val("");

                        $content.find(".accessory-item:not(:last) .glyphicon")
                                .removeClass("glyphicon-plus")
                                .addClass("glyphicon-minus");

                        $content.find(".accessory-item:not(:last) .btn-add")
                            .removeClass("btn-add").addClass("btn-remove")
                            .removeClass("btn-success").addClass("btn-danger");

                        //Give an id to the accessory
                        $content.find(".accessory-item:last")
                            .attr("id", "accessory-item" + count);

                    }

                }).on("click", ".btn-remove", function (e) {
                    count--;
                    $(this).parents(".accessory-item:first").remove();

                    e.preventDefault();
                    return false;
                });























                /*
                var tempItem = $(".accessory-item:first").clone();

                var maxFields = 10; // Max number of accessories that can be added
                var count = 1;
                // Give the first accessory an id
                $(".accessory-item:first").attr("id", "accessory-item1");


                $(".accessory-item:last .btn-add").on("click", function (e) {
                    e.preventDefault();
                    if (count < maxFields) {
                        count++;
                        var item = tempItem.clone().insertAfter(".accessory-item:last");
                        // Give an id to the accessory
                        item.attr("id", "accessory-item" + count).addClass("remove-item");

                        
                        $("#asset-accessories-content-container")
                            .find(".accessory-item:not(:last) .btn")
                            .removeClass("btn-success btn-add")
                            .addClass("btn-danger btn-remove");

                        $("#asset-accessories-content-container")
                            .find(".accessory-item:not(:last) .glyphicon")
                            .removeClass("glyphicon-plus")
                            .addClass("glyphicon-minus");
                           
                    }
                });
                */
    
                /*
                $("#add-accessory-item-btn").click(function (e) {
                    e.preventDefault();
                    if (count < maxFields) {
                        count++;
                        var item = tempItem.clone().appendTo("#asset-accessories-content-container");
                        // Give an id to the accessory
                        item.attr("id", "accessory-item" + count).addClass("remove-item");
                    }
                });
                */




            }
            if (this.value === "No") {
                $("#asset-accessories-content-container").hide();
            }
        });
        
    }
    addAccessoryItem();

    /* Helper function to get the number of subcategories. This function sets async to false*/
    function getNumSubCategoryAjax(categoryId) {
        // myURL was set in View
        var numSubCategories;
        $.ajax({
            url: myURL,
            type: 'GET',
            contentType: 'application/json; charset=utf8',
            data: { CategoryId: categoryId },
            success: function (result) {
                if (result.length > 0) {
                    numSubCategories = result.length;
                }
                // Debug messages
                console.log("Success");
                console.log(result);
                console.log("Finished");

            },
            async: false,
            error: function (result) {
                console.log("Something went wrong");
            }
        });

        return numSubCategories;
    }

    /* Helper function to get subcategories of a selected category from DB */
    function getSubCategoryAjax(categoryId) {
        // myURL was set in View
        $.ajax({
            url: myURL,
            type: 'GET',
            contentType: 'application/json; charset=utf8',
            data: { CategoryId: categoryId },
            success: function (result) {
                var subCat = '<option value>--- Select a sub-category ---</option>'; // Holds all the subcategories option menues

                if (result.length > 0) {
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
                }

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
                var myResult = getNumSubCategoryAjax(currentCat);
                if (currentCat != '' && myResult != '') {
                    $("div#subCategoryContainer").show();
                    getSubCategoryAjax(currentCat);
                }
                if (myResult == undefined || myResult == '') {
                    $("div#subCategoryContainer").hide();
                    $("div#subCategoryContainer option").remove();
                }
            });
        });
    }
    getSubCategory();

});

