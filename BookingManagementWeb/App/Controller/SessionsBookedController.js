// Defining angularjs module
var app = angular.module('sessionsBookedModule', ['apiModule', 'ui.bootstrap']);

app.controller('sessionsBookedController', function ($scope, $http, SBService, WebApi, $filter) {
    $scope.accesstoken = sessionStorage.getItem('accessToken');

    $scope.authHeaders = {};
    if ($scope.accesstoken) {
        $scope.authHeaders.Authorization = 'Bearer ' + $scope.accesstoken;
    }

    $scope.sbookedData = null;
    $scope.filterList = null;
    $scope.filtered = {};
    $scope.clients = {};
    $scope.staffSessions = {};
    $scope.allStaffSessions= {};
    $scope.staffSessionsTemp = {};
    $scope.sbTemp = {};

    $scope.getClients = function (cb) {
        $http({
            method: 'GET',
            url: WebApi.url + 'api/Clients/',
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.clients = response.data;
            return cb(response);
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
            return cb(null, response.data.Message);
        });
    }

    $scope.getStaffSessions = function (cb) {
        $http({
            method: 'GET',
            url: WebApi.url + 'api/GetStaffSessionAvailable/',
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.staffSessions = response.data;
            if ($scope.staffSessionsTemp.length > 0) {
                $scope.staffSessions.push($scope.staffSessionsTemp[0]);
            }
            return cb(response);
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
            return cb(null, response.data.Message);
        });
    }

    $scope.getAllStaffSessions = function (cb) {
        $http({
            method: 'GET',
            url: WebApi.url + 'api/StaffSessions/',
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.allStaffSessions = response.data;
            return cb(response);
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
            return cb(null, response.data.Message);
        });
    }

    $scope.init = function () {
        $scope.staffSessions = {};
        $scope.allStaffSessions = {};
        $scope.staffSessionsTemp = {};
        // Fetching records from the factory created at the bottom of the script file
        SBService.GetAllRecords($scope.authHeaders).then(function (d) {
            $scope.sbookedData = d.data; // Success
            $scope.filterList = $scope.sbookedData; // will be used for search or filtering
            $scope.pagination =
            {
                totalItems: $scope.sbookedData.length,
                currentPage: 1,
                numPerPage: 5
            };
            $scope.getClients(function success() {
                $scope.getStaffSessions(function success() {
                    $scope.getAllStaffSessions(function success() {
                        
                    });
                });
            });
        }, function (d) {
            alert(d.data.Message); // Failed
        });
    }

    $scope.ActiveValues = [
       { label: "Yes", value: 1 },
       { label: "No", value: 0 }
    ]

    $scope.SBooked = {
        Id: 0,
        Client: '',
        StaffSession: '',
        IsActive: ''
    };


    // Reset SBooked details
    $scope.clear = function () {
        $scope.SBooked.Id = 0;
        $scope.SBooked.Client = '';
        $scope.SBooked.StaffSession = '';
        $scope.SBooked.IsActive = '';
    }

    $scope.init();

    //Add SBooked Item
    $scope.save = function () {
        if ($scope.SBooked.Client !== "" && $scope.SBooked.StaffSession !== "") {
            $http({
                method: 'POST',
                url: WebApi.url + 'api/SessionsBooked',
                data: $scope.SBooked,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.sbookedData.push(response.data);
                $scope.updateStaffSession($scope.staffSessionsTemp[0].Id, 0, function success() {
                    $scope.clear();
                    $scope.init();
                    alert("Session Booked Successfully");
                });
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

    // Edit SBooked details
    $scope.edit = function (data) {
        $scope.SBooked = { Id: data.Id, Client: data.Client, StaffSession: data.StaffSession, IsActive: data.IsActive };
        $scope.sbTemp = angular.copy($scope.SBooked);
        //current staff session is deactivated, should add it manually to the staffSessions for select option box
        $scope.staffSessionsTemp = $filter('filter')($scope.allStaffSessions, function (vc) { return vc.Id == $scope.SBooked.StaffSession });
        $scope.getStaffSessions();
    }

    // Cancel SBooked details
    $scope.cancel = function () {
        $scope.clear();
    }

    $scope.updateStaffSession = function (staffSession, isActive, cb) {
        $scope.staffSessionsTemp = $filter('filter')($scope.allStaffSessions, function (vc) { return vc.Id == staffSession });
        $scope.staffSessionsTemp[0].IsActive = isActive;
                    $http({
                        method: 'PUT',
                        url: WebApi.url + 'api/StaffSessions/' + $scope.staffSessionsTemp[0].Id,
                        data: $scope.staffSessionsTemp[0],
                        headers: $scope.authHeaders,
                    }).then(function successCallback(response) {
                        $scope.clear();
                        $scope.init();
                        return cb(response);
                    }, function errorCallback(response) {
                        return cb(response.data.Message);
                    });
    }

    // Update SBooked details
    $scope.update = function () {
        if ($scope.SBooked.Client !== "" && $scope.SBooked.StaffSession !== "") {
            $http({
                method: 'PUT',
                url: WebApi.url + 'api/SessionsBooked/' + $scope.SBooked.Id,
                data: $scope.SBooked,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                //check previous staff session, if it is updated, then previous one should be activated and the new one should be deactivated
                if ($scope.sbTemp.StaffSession != $scope.SBooked.StaffSession)
                {
                    $scope.updateStaffSession(parseInt($scope.SBooked.StaffSession), 0,
                        function success(result) {
                            $scope.updateStaffSession($scope.sbTemp.StaffSession, 1, function success(result) {
                                $scope.sbookedData = response.data;
                                $scope.clear();
                                $scope.init();
                                alert("Session Booked Successfully");
                            });
                        });
                } else if ($scope.SBooked.IsActive == 0)
                {
                    $scope.updateStaffSession(parseInt($scope.SBooked.StaffSession), 1,
                    function success(result) {
                                $scope.sbookedData = response.data;
                                $scope.clear();
                                $scope.init();
                                alert("Session Booked Successfully");
                    });
                }
                else if ($scope.SBooked.IsActive == 1) {
                    $scope.updateStaffSession(parseInt($scope.SBooked.StaffSession), 0,
                    function success(result) {
                        $scope.sbookedData = response.data;
                        $scope.clear();
                        $scope.init();
                        alert("Session Booked Successfully");
                    });
                }
                else {
                    $scope.sbookedData = response.data;
                    $scope.clear();
                    $scope.init();
                    alert("Session Booked Successfully");
                }
            }, function errorCallback(response) {
                alert("Error : " + response.data.Message);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Delete SBooked details
    $scope.delete = function (index) {
       $http({
            method: 'DELETE',
            url: WebApi.url + 'api/SessionsBooked/' + index.Id,
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.updateStaffSession(index.StaffSession, 1, function success() {
                $scope.sbookedData.splice(index, 1);
                $scope.init();
                alert("Session Booked has been deleted Successfully");
            });
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
        });
    };

    //this staff session data will be deactivated when this staff session is selected for appointment
    $scope.saveSSValue = function (ssValue) { 
        $scope.staffSessionsTemp = $filter('filter')($scope.staffSessions, function (vc) { return vc.Id == ssValue });
    };

    $scope.$watch('searchText', function (term) {
        var obj = term;
        $scope.filterList = $filter('filter')($scope.sbookedData, obj);
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

app.factory('SBService', function ($http, WebApi) {
    var fac = {};
    fac.GetAllRecords = function (authHeaders) {
        return $http.get(WebApi.url + 'api/SessionsBooked',
          {
              headers: authHeaders
          });
    }
    return fac;
});