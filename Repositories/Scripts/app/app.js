(function () {
    var app = angular
        .module("myApp", [])
        .controller("homeController", function ($scope, $http) {
            $scope.showPic = false;
            $scope.popupTitle = "Message";
            $scope.popupContent = "";
            $scope.myUrl = "https://localhost:44305";
            $scope.itemsCollection = [];
            var resultItems = [];

            $scope.ButtonClick = function () {
                if ($scope.searchWord != null) {
                    $http({
                        method: "POST",
                        url: $scope.myUrl + "/home/Search",
                        dataType: 'json',
                        data: { searchWord: $scope.searchWord },
                        headers: { "Content-Type": "application/json" }
                    })
                        .then(function (response) {
                            if (response.data !== "EmptySearching" && response.status === 200) {
                                $scope.popupTitle = "Message";
                                //if ruturn "Empty" means not found or sent null for serch
                                resultItems = response.data.Items;
                                if (resultItems.length > 0) {
                                    $scope.showPic = true;
                                    $scope.itemsCollection = resultItems;
                                }
                                else {
                                    $scope.showPic = false;
                                    $scope.popupContent = "Please try to search something else!";
                                    $('#exampleModalCenter').modal('show');
                                }
                            }
                            else {
                                $scope.popupTitle = "Error Server";
                                $scope.popupContent = "Something went wrong.. Please try again!";
                                $('#exampleModalCenter').modal('show');
                            }
                        });
                }
                else {
                    $scope.popupTitle = "Empty searching";
                    $scope.popupContent = "Please search something word!";
                    $('#exampleModalCenter').modal('show');
                }
            }
            $scope.addItemToCart = function (item) {
                $scope.itemCart = item;
                if ($scope.itemCart !== 0) {
                    $http({
                        method: "POST",
                        url: $scope.myUrl + "/home/SaveItems",
                        dataType: 'json',
                        data: {
                            itemId: item.Id,
                            itemName: item.Name,
                            itemOwnerId: item.Owner.Id,
                            itemOwnerAvatar: item.Owner.Avatar_url,
                        },
                        headers: { "Content-Type": "application/json" }
                    })
                        .then(function (response) {
                            if (response.data == "new" || response.data == "old") {
                                $scope.popupTitle = "Saving Data";
                                $scope.popupContent = "succesfully adding to " + response.data + " list session!";
                            }
                            else {
                                if (response.data == "itemExist") {
                                    $scope.popupTitle = "Item Exist";
                                    $scope.popupContent = "The item you trying to add already exist. Please try to add another one";
                                }
                                else {
                                    $scope.popupTitle = "Error";
                                    $scope.popupContent = "Error: " + response.data;
                                }
                            }
                            $('#exampleModalCenter').modal('show');
                        });
                }
            };
        })
        .directive("myDirective", function ($compile) {
            return {
                template: "<p>{{popupContent}}</p>",

                link: function (scope, element, attrs) {

                }

            }
        })
        .directive("myDirective2", function ($compile) {
            return {
                template:
                    `<h5 class="modal-title">{{popupTitle}}</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>`,

                link: function (scope, element, attrs) {

                }

            }
        });
})();