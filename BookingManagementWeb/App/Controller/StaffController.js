// Defining angularjs module
var app = angular.module('staffModule', ['apiModule', 'ui.bootstrap']);

app.controller('staffController', function ($scope, $http, StaffService, WebApi, $filter) {
    $scope.accesstoken = sessionStorage.getItem('accessToken');

    $scope.authHeaders = {};
    if ($scope.accesstoken) {
        $scope.authHeaders.Authorization = 'Bearer ' + $scope.accesstoken;
    }

    $scope.staffsData = null;
    $scope.filterList = null;
    $scope.filtered = {};

    $scope.init = function () {
        // Fetching records from the factory created at the bottom of the script file
        StaffService.GetAllRecords($scope.authHeaders).then(function (d) {
            $scope.staffsData = d.data; // Success
            $scope.filterList = $scope.staffsData; // will be used for search or filtering
            $scope.pagination =
            {
                totalItems: $scope.staffsData.length,
                currentPage: 1,
                numPerPage: 5
            };
        }, function (d) {
            alert(d.data.Message); // Failed
        });
    }

    $scope.ActiveValues = [
       { label: "Yes", value: 1 },
       { label: "No", value: 0 }
    ]

    $scope.Staff = {
        Id: 0,
        Name: '',
        Email: '',
        Phone: '',
        Address: '',
        Status: '',
    };


    // Reset staff details
    $scope.clear = function () {
        $scope.Staff.Id = 0;
        $scope.Staff.Name = '';
        $scope.Staff.Email = '';
        $scope.Staff.Phone = '';
        $scope.Staff.Address = '';
        $scope.Staff.Status = '';
    }

    $scope.init();

    //Add staff Item
    $scope.save = function () {
        if ($scope.Staff.Name !== "" &&
       $scope.Staff.Email !== "" && $scope.Staff.Address !== "" && $scope.Staff.Phone !== "") {
            $http({
                method: 'POST',
                url: WebApi.url + 'api/Staffs',
                data: $scope.Staff,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.staffsData.push(response.data);
                $scope.clear();
                $scope.init();
                alert("Staff Added Successfully");
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                alert("Error : " + response.data.Message);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Edit staff details
    $scope.edit = function (data) {
        $scope.Staff = { Id: data.Id, Name: data.Name, Email: data.Email, Address: data.Address, Phone: data.Phone, Status: data.Status };
    }

    // Cancel staff details
    $scope.cancel = function () {
        $scope.clear();
    }

    // Update staff details
    $scope.update = function () {
        if ($scope.Staff.Name !== "" &&
       $scope.Staff.Email !== "" && $scope.Staff.Address !== "" && $scope.Staff.Phone !== "") {
            $http({
                method: 'PUT',
                url: WebApi.url + 'api/Staffs/' + $scope.Staff.Id,
                data: $scope.Staff,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                $scope.staffsData = response.data;
                $scope.clear();
                $scope.init();
                alert("Staff Updated Successfully");
            }, function errorCallback(response) {
                alert("Error : " + response.data.Message);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Delete staff details
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            url: WebApi.url + 'api/Staffs/' + index.Id,
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.staffsData.splice(index, 1);
            $scope.init();
            alert("Staff Deleted Successfully");
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
        });
    };

    $scope.$watch('searchText', function (term) {
        var obj = term;
        $scope.filterList = $filter('filter')($scope.staffsData, obj);
        $scope.currentPage = 1;
    });

});

//used for filtering or search
app.filter('start', function () {
    return function (input, start) {
        if (!input || !input.length) { return; }
        start = +start;
        return input.slice(start);
    };
});

app.factory('StaffService', function ($http, WebApi) {
    var fac = {};
    fac.GetAllRecords = function (authHeaders) {
        return $http.get(WebApi.url + 'api/Staffs',
          {
              headers: authHeaders
          });
    }
    return fac;
});