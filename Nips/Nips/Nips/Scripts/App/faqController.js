var App = angular.module("App", []);

App.controller("faqController", function ($scope, $http) {

    var url = '/api/Oss/';
    $scope.sletteFeil = false;

    function sporsmalListe() {
        $http.get(url).success(function (alleSporsmal) {
            $scope.alleSporsmal = alleSporsmal;
            $scope.laster = false;
            console.log("Fungerte det?");
        }).error(function (data, status) {
            console.log(status + data);
        });
    };
    $scope.visFAQ = true;
    $scope.visKnapp = true;
    $scope.laster = true;
    sporsmalListe();

    $scope.visRegSporsmal = function () {
        $scope.email = "";
        $scope.beskrivelse = "";

        $scope.setPristine();
          $scope.regKnapp = false;
          $scope.visSkjema = true;
          $scope.visKunder = false;
          $scope.sendKnapp = true;
          $scope.oppdatering = false;
        //vis knapper
    }

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

        $scope.sletteSporsmal = function (id) {
            $http.delete(url + id).
            success(function (data) {
                sporsmalListe();

            }).
            error(function (data, status) {
                console.log(status + data);
            });
        };

    }

});