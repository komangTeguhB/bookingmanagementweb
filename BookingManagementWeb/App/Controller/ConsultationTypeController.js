var app = angular.module('consultationTypeModule', ['apiModule', 'ui.bootstrap']);

app.controller('consultationTypeController', function ($scope, $http, ConsultationTypeService, WebApi, $filter) {
    $scope.accesstoken = sessionStorage.getItem('accessToken');

    $scope.authHeaders = {};
    if ($scope.accesstoken) {
        $scope.authHeaders.Authorization = 'Bearer ' + $scope.accesstoken;
    }

    $scope.typesData = null;
    $scope.filterList = null;
    $scope.filtered = {};

    $scope.init = function () {
        // Fetching records from the factory created at the bottom of the script file
        ConsultationTypeService.GetAllRecords($scope.authHeaders).then(function (d) {
            $scope.typesData = d.data; // Success
            $scope.filterList = $scope.typesData; // will be used for search or filtering
            $scope.pagination =
            {
                totalItems: $scope.typesData.length,
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

    $scope.CTypes = {
        Id: 0,
        TypeName: '',
        Duration: ''
    };


    // Reset CTypes details
    $scope.clear = function () {
        $scope.CTypes.Id = 0;
        $scope.CTypes.TypeName = '';
        $scope.CTypes.Duration = '';
    }

    $scope.init();

    //Add CTypes Item
    $scope.save = function () {
        if ($scope.CTypes.TypeName !== "" && $scope.CTypes.Duration !== "") {
            $http({
                method: 'POST',
                url: WebApi.url + 'api/ConsultationTypes',
                data: $scope.CTypes,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.typesData.push(response.data);
                $scope.clear();
                $scope.init();
                alert("Consultation Type Added Successfully");
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

    // Edit CTypes details
    $scope.edit = function (data) {
        $scope.CTypes = { Id: data.Id, TypeName: data.TypeName, Duration: data.Duration};
    }

    // Cancel CTypes details
    $scope.cancel = function () {
        $scope.clear();
    }

    // Update CTypes details
    $scope.update = function () {
        if ($scope.CTypes.TypeName !== "" && $scope.CTypes.Duration !== "") {
            $http({
                method: 'PUT',
                url: WebApi.url + 'api/ConsultationTypes/' + $scope.CTypes.Id,
                data: $scope.CTypes,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                $scope.typesData = response.data;
                $scope.clear();
                $scope.init();
                alert("Consultation Type  Updated Successfully");
            }, function errorCallback(response) {
                console.log(response);
                alert("Error : " + response.data.Message);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Delete CTypes details
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            url: WebApi.url + 'api/ConsultationTypes/' + index.Id,
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.typesData.splice(index, 1);
            $scope.init();
            alert("Consultation Type Deleted Successfully");
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
        });
    };

    $scope.$watch('searchText', function (term) {
        var obj = term;
        $scope.filterList = $filter('filter')($scope.typesData, obj);
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

app.factory('ConsultationTypeService', function ($http, WebApi) {
    var fac = {};
    fac.GetAllRecords = function (authHeaders) {
        return $http.get(WebApi.url + 'api/ConsultationTypes',
          {
              headers: authHeaders
          });
    }
    return fac;
});