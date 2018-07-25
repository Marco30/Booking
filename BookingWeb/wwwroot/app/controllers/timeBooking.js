//BookigTime-------------------------------
app.controller("timeBooking", function ($scope, $http, $rootScope, TimeData, DayData, ReloadData, LoginData) {
    //test---------------
    $scope.timeError = undefined;
    $scope.dateError = undefined;
    $scope.timeList = ["08.00 - 11.00", "11.00 - 14.00", "14.00 - 16.00"];
    $scope.selectedItem = undefined;

    $scope.getSelectedText = function () {
        if ($scope.selectedItem !== undefined) {
            $scope.timeError = undefined;
            $scope.selected = $scope.selectedItem;
        } else {
        }
    };

    $scope.date = undefined;

    $rootScope.$on("dayEvent", function () {
        var t = DayData.get();
        $scope.date = t.format("YYYY-MM-DD");
        $scope.dateError = undefined;
    });

    $rootScope.$on("loginEvent", function () {
        $scope.login = LoginData.get();

        if ($scope.login.status) {
            $scope.UserId = $scope.login.id;
        }
    });

    $scope.send = function () {
        if ($scope.selectedItem !== undefined && $scope.date !== undefined && $scope.selectedItem !== "") {
            var booking = {
                "Date": $scope.date,
                "Time": $scope.selectedItem,
                "PersonId": $scope.UserId
            }

            $http({
                method: 'POST',
                url: 'http://localhost:52917/api/booking',
                data: booking,
                dataType: 'json',
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(function successCallback(response) {
                if (response.data.status === false) {
                    $scope.bookingError = response.data.message;
                } else {
                    $scope.bookingError = "";

                    $scope.reloadBookingList = response.data.status;
                }

                //alert("add");
            }, function errorCallback(response) {
                alert("add error");
            });
        } else {
            if ($scope.selectedItem == undefined || $scope.selectedItem == "") {
                $scope.timeError = "choose a time";
            }
            if ($scope.date == undefined) {
                $scope.dateError = "choose a day";
            }
        }
    };

    $scope.$watch('reloadBookingList', function (newvalue, oldvalue) {
        ReloadData.set(newvalue);
        $scope.reloadBookingList = "Off";
    });

    $scope.$watch('selectedItem', function (newvalue, oldvalue) {
        TimeData.set(newvalue);
    });

    $scope.timein;
});