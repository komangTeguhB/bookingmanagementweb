var app = angular.module('staffSessionModule', ['apiModule', 'ui.bootstrap']);

app.controller('staffSessionController', function ($scope, $http, StaffSessionService, WebApi, $filter, $log) {
    $scope.accesstoken = sessionStorage.getItem('accessToken');

    $scope.authHeaders = {};
    if ($scope.accesstoken) {
        $scope.authHeaders.Authorization = 'Bearer ' + $scope.accesstoken;
    }

    $scope.sessionsData = null;
    $scope.filterList = null;
    $scope.filtered = {};
    $scope.consulTypes = {};
    $scope.staffs = {};
    $scope.timeTemp = new Date();
    $scope.dt = new Date();
    $scope.dateTemp = new Date();

    $scope.getConsultationTypes = function () {
        $http({
            method: 'GET',
            url: WebApi.url + 'api/ConsultationTypes/',
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.consulTypes = response.data;
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
        });
    }

    $scope.getStaffs = function () {
        $http({
            method: 'GET',
            url: WebApi.url + 'api/Staffs/',
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.staffs = response.data;
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
        });
    }

    $scope.ActiveValues = [
       { label: "Yes", value: 1 },
       { label: "No", value: 0 }
    ]

    $scope.init = function () {

        //configure datetime picker
        $scope.dateOptions = {
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 1
        };
       
        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.popup1 = {
            opened: false
        };
        //end configure

        //configure time picker
        $scope.hstep = 1;
        $scope.mstep = 15;
        $scope.options = {
            hstep: [1, 2, 3],
            mstep: [1, 5, 10, 15, 25, 30]
        };
        $scope.ismeridian = false;

        $scope.changed = function () {
            $log.log('Time changed to: ' + $scope.StaffSession.Time);
        };
        //end configure

   
        // Fetching records from the factory created at the bottom of the script file
        StaffSessionService.GetAllRecords($scope.authHeaders).then(function (d) {
            $scope.sessionsData = d.data; // Success
            $scope.filterList = $scope.sessionsData; // will be used for search or filtering
            $scope.pagination =
            {
                totalItems: $scope.sessionsData.length,
                currentPage: 1,
                numPerPage: 5
            };
            $scope.getConsultationTypes();
            $scope.getStaffs();
        }, function (d) {
            alert(d.data.Message); // Failed
        });
    }

    
    $scope.StaffSession = {
        Id: 0,
        Date: new Date(),
        Time: '',
        ConsultationType: '',
        Staff: '',
        IsActive: '',
    };


    // Reset StaffSession details
    $scope.clear = function () {
        $scope.StaffSession.Id = 0;
        $scope.StaffSession.Date = new Date();
        $scope.StaffSession.Time = '';
        $scope.StaffSession.ConsultationType = '';
        $scope.StaffSession.Staff = '';
        $scope.StaffSession.IsActive = '';
        $scope.timeTemp = new Date();
    }

    $scope.init();

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    //Add StaffSession Item
    $scope.save = function () {
        $scope.StaffSession.Time = $filter('date')($scope.timeTemp, "HH:mm:ss");
        if ($scope.StaffSession.Date !== "" && $scope.StaffSession.Time !== "" ) {
            $http({
                method: 'POST',
                url: WebApi.url + 'api/StaffSessions',
                data: $scope.StaffSession,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.sessionsData.push(response.data);
                $scope.clear();
                $scope.init();
                alert("Staff Session Added Successfully");
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

    // Edit StaffSession details
    $scope.edit = function (data) {
        $scope.StaffSession = { Id: data.Id, Time: data.Time, ConsultationType: data.ConsultationType, Staff: data.Staff, IsActive: data.IsActive };
        $scope.dateTemp = new Date(data.Date)
        //set timepicker based on record
        var str = $scope.StaffSession.Time;
        var res = str.split(":");
        $scope.timeTemp = new Date();
        $scope.timeTemp.setHours(res[0]);
        $scope.timeTemp.setMinutes(res[1]);
    }

    // Cancel StaffSession details
    $scope.cancel = function () {
        $scope.clear();
    }

    // Update StaffSession details
    $scope.update = function () {
        $scope.StaffSession.Time = $filter('date')($scope.timeTemp, "HH:mm:ss");
        if ($scope.StaffSession.Time !== "") {
            if ($scope.StaffSession.Date === undefined)
            {
                $scope.StaffSession.Date = $scope.dateTemp;
            }
            $http({
                method: 'PUT',
                url: WebApi.url + 'api/StaffSessions/' + $scope.StaffSession.Id,
                data: $scope.StaffSession,
                headers: $scope.authHeaders
            }).then(function successCallback(response) {
                $scope.sessionsData = response.data;
                $scope.clear();
                $scope.init();
                alert("Staff Session Updated Successfully");
            }, function errorCallback(response) {
                console.log(response);
                alert("Error : " + response.data.Message);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Delete StaffSession details
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            url: WebApi.url + 'api/StaffSessions/' + index.Id,
            headers: $scope.authHeaders
        }).then(function successCallback(response) {
            $scope.sessionsData.splice(index, 1);
            $scope.init();
            alert("Staff Session Deleted Successfully");
        }, function errorCallback(response) {
            alert("Error : " + response.data.Message);
        });
    };

    $scope.$watch('searchText', function (term) {
        var obj = term;
        $scope.filterList = $filter('filter')($scope.sessionsData, obj);
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

app.factory('StaffSessionService', function ($http, WebApi) {
    var fac = {};
    fac.GetAllRecords = function (authHeaders) {
        return $http.get(WebApi.url + 'api/StaffSessions',
          {
              headers: authHeaders
          });
    }
    return fac;
});