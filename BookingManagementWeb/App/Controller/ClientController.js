// Defining angularjs module
var app = angular.module('clientModule', ['apiModule', 'ui.bootstrap']);

// Defining angularjs Controller and injecting ClientService
app.controller('clientController', function ($scope, $http, ClientService, WebApi, $filter) {
    $scope.accesstoken = sessionStorage.getItem('accessToken');

    $scope.authHeaders = {};
    if ($scope.accesstoken) {
        $scope.authHeaders.Authorization = 'Bearer ' + $scope.accesstoken;
    }

    $scope.clientsData = null;
    $scope.filterList = null;
    $scope.filtered = {};

    $scope.init = function () {
        // Fetching records from the factory created at the bottom of the script file
        ClientService.GetAllRecords($scope.authHeaders).then(function (d) {
            $scope.clientsData = d.data; // Success
            $scope.filterList = $scope.clientsData; // will be used for search or filtering
            $scope.pagination =
            {
                totalItems: $scope.clientsData.length,
                currentPage: 1,
                numPerPage: 5
            };
        }, function (d) {
            alert(d.data.Message); // Failed
        });
    }

    $scope.ActiveValues = [
       { label: "Yes", value: 1 },
       { label: "No",  value: 0 }
    ]

    $scope.Client = {
        Id: 0,
        Name: '',
        Email: '',
        Phone: '',
        Address: '',
        Status: '',
    };


    // Reset client details
    $scope.clear = function () {
        $scope.Client.Id = 0;
        $scope.Client.Name = '';
        $scope.Client.Email = '';
        $scope.Client.Phone = '';
        $scope.Client.Address = '';
        $scope.Client.Status = '';
    }

    $scope.init();

    //Add client Item
    $scope.save = function () {
        if ($scope.Client.Name !== "" &&
       $scope.Client.Email !== "" && $scope.Client.Address !== "" && $scope.Client.Phone !== "") {
            $http({
                method: 'POST',
                url: WebApi.url + 'api/Clients',
                data: $scope.Client,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.clientsData.push(response.data);
                $scope.clear();
                $scope.init();
                alert("Client Added Successfully");
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

    // Edit client details
    $scope.edit = function (data) {
        $scope.Client = { Id: data.Id, Name: data.Name, Email: data.Email, Address: data.Address, Phone: data.Phone, Status: data.Status };
    }

    // Cancel client details
    $scope.cancel = function () {
        $scope.clear();
    }

    // Update client details
    $scope.update = function () {
        if ($scope.Client.Name !== "" &&
       $scope.Client.Email !== "" && $scope.Client.Address !== "" && $scope.Client.Phone !== "") {
            $http({
                method: 'PUT',
                url: WebApi.url + 'api/Clients/' + $scope.Client.Id,
                data: $scope.Client,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                $scope.clientsData = response.data;
                $scope.clear();
                $scope.init();
                alert("Client Updated Successfully");
            }, function errorCallback(response) {
                alert("Error : " + response.data.Message);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Delete client details
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            url: WebApi.url + 'api/Clients/' + index.Id,
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.clientsData.splice(index, 1);
            $scope.init();
            alert("Client Deleted Successfully");
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
        });
    };
    
    $scope.$watch('searchText', function (term) {
        var obj = term;
        $scope.filterList = $filter('filter')($scope.clientsData, obj);
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

app.factory('ClientService', function ($http, WebApi) {
    var fac = {};
    fac.GetAllRecords = function (authHeaders) {
        return $http.get(WebApi.url + 'api/Clients',
          {
              headers: authHeaders
          });
    }
    return fac;
});