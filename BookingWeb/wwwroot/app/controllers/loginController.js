//Login-----------------------------

app.controller("loginController", function ($scope, $http, LoginData) {
    $scope.userName = undefined;

    $scope.password = undefined;

    $scope.send = function () {
        if ($scope.userName !== undefined && $scope.password !== undefined) {
            var Person = {
                "Name": $scope.userName,
                "Password": $scope.password,
            }

            $http({
                method: 'POST',
                url: 'http://localhost:52917/api/Person',
                data: Person,
                dataType: 'json',
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function successCallback(response) {
                if (response.data.status === false) {
                    $scope.loginError = response.data.message;
                } else {
                    $scope.loginError = "";

                    $scope.logdin = response.data.status;
                    $scope.userId = response.data.id;

                    $("#calnderbody").show();
                    $("#calnderlogin").hide();

                    LoginData.set({ "id": response.data.id, "status": response.data.status });

                    $scope.userName = undefined;
                    $scope.password = undefined;
                }

                //alert("add");
            }, function errorCallback(response) {
                alert("Login error");
            });

            return "You send " + $scope.selectedItem;
        } else {
            if ($scope.userName == undefined) {
                $scope.userNameError = "Username can not be empty";
            }
            if ($scope.password == undefined) {
                $scope.passwordError = "Password can not be empty";
            }
            return "Please select an item";
        }
    };
});