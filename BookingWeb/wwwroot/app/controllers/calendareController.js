//Calendare-------------------------

app.controller("calendarbooking", function ($scope, DayData) {
    $scope.day = moment();

    $scope.date = "";

    $scope.$watch('date', function (newvalue, oldvalue) {
        DayData.set(newvalue);
    });
});