var App = angular.module("App", []);

App.controller("faqController", function ($scope, $http) {

    var url = '/api/FAQ/';
    $scope.sletteFeil = false;

    function sporsmalListe() {
        $http.get(url).success(function (alleSporsmal) {
            $scope.alleSporsmal = alleSporsmal;
            $scope.laster = false;
        }).error(function (data, status) {
            console.log(status + data);
        });
    };
    $scope.FAQ = true;
    $scope.Sporsmal = true;
    $scope.Liste = true;
    $scope.laster = true;
    sporsmalListe();

    $scope.visSporsmal = function () {
        $scope.email = "";
        $scope.beskrivelse = "";
        $scope.FAQ = false;
        $scope.Sporsmal = true;
        $scope.Liste = false;
    };
    $scope.visFAQ = function () {
        $scope.email = "";
        $scope.beskrivelse = "";
        $scope.FAQ = true;
        $scope.Sporsmal = false;
        $scope.Liste = false;
    };
    $scope.visListe = function () {
        $scope.email = "";
        $scope.beskrivelse = "";
        $scope.Liste = true;
        $scope.FAQ = false;
        $scope.Sporsmal = false;
    };

    $scope.setTempid = function (id) {
        $scope.tempid = id;
        console.log(id);
    };

    $scope.lagreSporsmal = function () {
        var Spørsmal = {
            email: $scope.email,
            beskrivelse: $scope.beskrivelse
        };
        
        $http.post(url, Spørsmal).
            success(function (data) {
                sporsmalListe();
                $scope.visFAQ;
                //vis knapper
            }).
        error(function (data, status) {
            console.log(status + data);
        });
    };
    

    $scope.sletteSporsmal = function (id) {
        $http.delete(url+id).
        success(function (data) {
            sporsmalListe();
            $tempId = 0;
        }).
        error(function (data, status) {
            console.log(status + data);
            $tempId = 0;
        });
    };
   



});