//API--------------------------------

app.controller("bookinglistController", function ($scope, $http, $rootScope, $filter, DayData, TimeData, ReloadData, LoginData) {
    $rootScope.$on("loginEvent", function () {
        $scope.login = LoginData.get();

        if ($scope.login.status) {
            $scope.UserId = $scope.login.id;

            $scope.datein = moment();
        }
    });

    $scope.$watch('datein', function (newvalue, oldvalue) {
        $http({
            method: 'GET',
            url: 'http://localhost:52917/api/booking/month?id=' + $scope.UserId + '&month=' + newvalue.format("YYYY-MM")
        }).then(function successCallback(response) {
            $scope.bookings = response.data;

            console.log(response);
            //alert("yes");
        }, function errorCallback(response) {
            alert("Get Booking error");
        });
    });

    $scope.timein;

    $rootScope.$on("dayEvent", function () {
        $scope.datein = DayData.get();
    });

    $rootScope.$on("timeEvent", function () {
        $scope.timein = TimeData.get();
    });

    $rootScope.$on("reloadEvent", function () {
        $scope.reloadData = ReloadData.get();
        if ($scope.reloadData == true) {
            $http({
                method: 'GET',
                url: 'http://localhost:52917/api/booking/month?id=' + $scope.UserId + '&month=' + $scope.datein.format("YYYY-MM")
            }).then(function successCallback(response) {
                $scope.bookings = response.data;

                console.log(response);

                //alert("reload bookings");
            }, function errorCallback(response) {
                alert("reload error");
            });
        }

        $scope.reloadData = "off";
    });

    $scope.logout = function () {
        LoginData.set({ "id": "", "status": false });

        $("#calnderlogin").show();
        $("#calnderbody").hide();
    };

    //delete-----------------------------
    $scope.delete = function (id) {
        //Delete from dom

        var index = 0;

        for (var i = 0; i < $scope.bookings.length; i++) {
            if (id == $scope.bookings[i].id) {
                index = i;
                break;
            }
        }

        $scope.bookings.splice(index, 1)

        //Delete from DB
        $http({
            method: 'DELETE',
            url: 'http://localhost:52917/api/booking/' + id,
        }).then(function successCallback(response) {
            console.log("Delete");
            //alert("Delete");
        }, function errorCallback(response) {
            //alert("Delete error");
            console.log("Delete error");
        });
    };
});